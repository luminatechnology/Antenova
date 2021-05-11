using PX.Data;
using PX.Data.BQL.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntenovaCustomizations.DAC;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.AR;
using AntenovaCustomizations.Graph;
using System.Collections;
using static PX.Objects.CR.OpportunityMaint;
using PX.Objects.SO;

namespace PX.Objects.CR
{
    public class OpportunityMaint_Etension : PXGraphExtension<OpportunityMaint>
    {
        public PXSetup<ENGSetup> setup;

        public SelectFrom<ENGineering>
                .Where<ENGineering.opprid.IsEqual<CROpportunity.opportunityID.FromCurrent>>.View ENGList;

        public SelectFrom<ENGLine>
               .InnerJoin<ENGineering>.On<ENGLine.engrRef.IsEqual<ENGineering.engrRef>>
               .Where<ENGineering.opprid.IsEqual<CROpportunity.opportunityID.FromCurrent>>.View ENGListLine;

        #region Override Base Method

        public delegate void DoCreateSalesOrderDelegate(CreateSalesOrderFilter param);
        /// <summary> Create Sales Order From Opportunity 帶入opportunityID into SOLine </summary>
        [PXOverride]
        public virtual void DoCreateSalesOrder(CreateSalesOrderFilter param, DoCreateSalesOrderDelegate baseMethod)
        {
            try
            {
                baseMethod.Invoke(param);
            }
            catch (PXRedirectRequiredException ex)
            {
                var opportunityID = Base.Opportunity.Current.OpportunityID;
                var soGraph = ex.Graph;
                var soLine = soGraph.Caches[typeof(SOLine)].Cached.RowCast<SOLine>();
                foreach (var item in soLine)
                    item.GetExtension<SOLineExt>().UsrOpportunityID = opportunityID;
                throw new PXRedirectRequiredException(soGraph, "");
            }
        }

        #endregion

        #region Override Base DataView

        public virtual IEnumerable salesOrders()
        {
            var result = SelectFrom<SOOrder>
                .InnerJoin<CRRelation>.On<SOOrder.noteID.IsEqual<CRRelation.refNoteID>>
                .LeftJoin<SOLine>.On<SOOrder.orderNbr.IsEqual<SOLine.orderNbr>
                    .And<SOOrder.orderType.IsEqual<SOLine.orderType>>>
                .Where<CRRelation.targetNoteID.IsEqual<CROpportunity.noteID.FromCurrent>>
                .View.Select(Base);
            return result;
        }

        #endregion

        #region Override DAC

        [PXDefault]
        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXUIField(DisplayName = "Engineering Ref", Required = true)]
        [AutoNumber(typeof(ENGSetup.eNGSequenceID), typeof(AccessInfo.businessDate))]
        [PXMergeAttributes(Method = MergeMethod.Replace)]
        public void _(Events.CacheAttached<ENGineering.engrRef> e) { }
        #endregion

        #region Action
        public PXAction<ENGineering> viewENGDoc;

        [PXButton]
        [PXUIField(Visible = false)]
        public virtual IEnumerable ViewENGDoc(PXAdapter adapter)
        {
            var row = this.ENGList.Current;
            var graph = PXGraph.CreateInstance<ENGineeringMaint>();
            if (row != null)
            {
                graph.Document.Current = SelectFrom<ENGineering>
                                         .Where<ENGineering.engrRef.IsEqual<P.AsString>>
                                         .View.Select(Base, row.EngrRef);
                PXRedirectHelper.TryRedirect(graph, PXRedirectHelper.WindowMode.NewWindow);
            }
            return adapter.Get();
        }

        #endregion

        #region Events

        /// <summary> RowInserting CROpportunity </summary>
        public void _(Events.RowInserting<CROpportunity> e, PXRowInserting baseMethod)
        {
            baseMethod?.Invoke(e.Cache,e.Args);
            var row = e.Row;
            if (row != null && row.LeadID.HasValue)
            {
                var leads = SelectFrom<CRLead>
                    .Where<CRLead.noteID.IsEqual<P.AsGuid>>
                    .View.Select(Base, row.LeadID).RowCast<CRLead>().FirstOrDefault() ;
                row.GetExtension<CROpportunityExt>().UsrEndCust =
                    string.IsNullOrEmpty(row.GetExtension<CROpportunityExt>().UsrEndCust)
                        ? leads.GetExtension<CRLeadExt>().UsrEndCust
                        : row.GetExtension<CROpportunityExt>().UsrEndCust;
                row.GetExtension<CROpportunityExt>().UsrSource =
                    string.IsNullOrEmpty(row.GetExtension<CROpportunityExt>().UsrSource)
                        ? leads.GetExtension<CRLeadExt>().UsrSource
                        : row.GetExtension<CROpportunityExt>().UsrSource;
            }
        }

        /// <summary> RowPersisting CROpportunity </summary>
        public void _(Events.RowPersisting<CSAnswers> e, PXRowPersisting baseMethod)
        {
            baseMethod?.Invoke(e.Cache, e.Args);
            var stageID = Base.Opportunity.Current.StageID;
            var status = Base.Opportunity.Current.Status;
            var row = e.Row as CSAnswers;
            if (stageID == "MP" && row.AttributeID == "FULMPDATE" && string.IsNullOrEmpty(row.Value))
            {
                Base.Answers.Cache.RaiseExceptionHandling<CSAnswers.value>(e.Row, row.Value,
                    new PXSetPropertyException<CSAnswers.value>("Full MP Date Can not be Empty"));
                throw new PXSetPropertyException<CSAnswers.value>("Full MP Date Can not be Empty");
            }
        }

        /// <summary> RowPersisting ENGineering </summary>
        public void _(Events.RowPersisting<ENGineering> e)
        {
            var row = (ENGineering)e.Row;
            row.OppBAccountID = Base.Opportunity.Current.BAccountID;
            var _oppID = Base.Opportunity.Current.OpportunityID;
            if ((row.Opprid ?? string.Empty).ToLower().Contains("new"))
                e.Row.Opprid = _oppID;
        }

        /// <summary> RowPersisted ENGineering </summary>
        public void _(Events.RowPersisted<ENGineering> e)
        {
            int count = 0;
            var row = e.Row as ENGineering;

            if (string.IsNullOrEmpty(row.Description) || string.IsNullOrEmpty(row.Prjtype) || string.IsNullOrEmpty(row.Priority) || string.IsNullOrEmpty(row.SalesRegion))
                return;

            var _RevenueData = new PXGraph().Select<ENGRevenueLine>().Where(x => x.EngrRef == row.EngrRef);
            if (_RevenueData.Count() == 0)
            {
                var _graph = PXGraph.CreateInstance<ENGineeringMaint>();
                var _oppProduct = Base.Products.Select().RowCast<CROpportunityProducts>();
                foreach (var _prod in _oppProduct)
                {
                    var _data = _graph.RevenueLine.Insert(_graph.RevenueLine.Cache.CreateInstance() as ENGRevenueLine);
                    _data.EngrRef = row.EngrRef;
                    _data.InventoryID = _prod.InventoryID;
                    _data.Descr = _prod.Descr;
                    _data.Quantity = _prod.Quantity;
                    _data.Uom = _prod.UOM;
                    _data.UnitPrice = _prod.UnitPrice;
                    _data.ExtPrice = _prod.ExtPrice;
                    _data.LineNbr = ++count;
                }
                _graph.Actions.PressSave();
                // update reveCntr
                PXUpdate<Set<ENGineering.reveCntr, Required<ENGineering.reveCntr>>,
                         ENGineering,
                         Where<ENGineering.engrRef, Equal<Required<ENGineering.engrRef>>
                         >>.Update(Base, count, row.EngrRef);
            }

        }

        /// <summary> Set engrRef Disabled </summary>
        public void _(Events.FieldSelecting<ENGineering.engrRef> e)
            => PXUIFieldAttribute.SetEnabled<ENGineering.engrRef>(e.Cache, null, false);

        /// <summary> Initial Project Type DDL </summary>
        public void _(Events.FieldSelecting<ENGineering.prjtype> e)
        {
            var prjType = SelectFrom<ENGProjectType>.View.Select(Base).RowCast<ENGProjectType>();
            if (e.Row != null)
            {
                PXStringListAttribute.SetList<ENGineering.prjtype>(
                    e.Cache,
                    null,
                    prjType.Select(x => x.Prjtype).ToArray(),
                    prjType.Select(x => x.Description).ToArray());
            }
        }

        /// <summary> Set opprid Enabled </summary>
        public void _(Events.FieldSelecting<ENGineering.opprid> e)
        {
            if (e.Row != null)
                PXUIFieldAttribute.SetEnabled<ENGineering.opprid>(e.Cache, null, false);
        }

        /// <summary> Set opprid value </summary>
        public void _(Events.FieldDefaulting<ENGineering.opprid> e)
        {
            var AutoOppID = SelectFrom<ENGProjectType>
                            .Where<ENGProjectType.prjtype.IsEqual<P.AsString>>
                            .View.Select(Base, (e.Row as ENGineering).Prjtype).RowCast<ENGProjectType>().FirstOrDefault()?.LinkOppr ?? false;
            if (AutoOppID)
                e.NewValue = (Base.Opportunity.Cache.Current as CROpportunity).OpportunityID;
        }

        /// <summary> EndCust Default value </summary>
        public void _(Events.FieldDefaulting<ENGineering.endCust> e)
            => e.NewValue = Base.Opportunity.Cache.GetValueExt<CROpportunityExt.usrendCust>(Base.Opportunity.Current);

        /// <summary> Events.FieldUpdated ENGineering.salesPerson </summary>
        public void _(Events.FieldDefaulting<ENGineering.salesPerson> e)
            => e.NewValue = (Base.Opportunity.Cache.GetValueExt<CROpportunityExt.usrSalesPerson>(Base.Opportunity.Current) as PXStringState).Value;

        /// <summary> Events.FieldUpdated ENGineering.salesRegion </summary>
        public void _(Events.FieldDefaulting<ENGineering.salesRegion> e)
            => e.NewValue = (Base.Opportunity.Cache.GetValueExt<CROpportunityExt.usrsalesRegion>(Base.Opportunity.Current) as PXStringState).Value;

        /// <summary> Events.FieldUpdated ENGineering.prjtype </summary>
        public void _(Events.FieldUpdated<ENGineering.prjtype> e)
        {
            var AutoOppID = SelectFrom<ENGProjectType>
                           .Where<ENGProjectType.prjtype.IsEqual<P.AsString>>
                           .View.Select(Base, e.NewValue).RowCast<ENGProjectType>().FirstOrDefault()?.LinkOppr ?? false;
            if (AutoOppID)
                e.Cache.SetValueExt<ENGineering.opprid>(e.Row, (Base.Opportunity.Cache.Current as CROpportunity).OpportunityID);
        }

        /// <summary> Events.FieldUpdated CROpportunityExt.usrSalesPerson </summary>
        public void _(Events.FieldUpdated<CROpportunityExt.usrSalesPerson> e)
        {
            if (e.NewValue != null)
            {
                var _salesPerson = new PXGraph().Select<SalesPerson>()
                                                .Where(x => x.SalesPersonID == (int)e.NewValue)
                                                .FirstOrDefault()
                                                ?.GetExtension<SalesPersonExt>()
                                                ?.UsrSalesTerritory;
                e.Cache.SetValueExt<CROpportunityExt.usrsalesRegion>(e.Row, _salesPerson);
            }
        }

        /// <summary> Events.FieldUpdated ENGineering.salesPerson </summary>
        public void _(Events.FieldUpdated<ENGineering.salesPerson> e)
        {
            if (e.NewValue != null)
            {
                var _salesPerson = new PXGraph().Select<SalesPerson>()
                                                .Where(x => x.SalesPersonID == (int)e.NewValue)
                                                .FirstOrDefault()
                                                ?.GetExtension<SalesPersonExt>()
                                                ?.UsrSalesTerritory;
                e.Cache.SetValueExt<ENGineering.salesRegion>(e.Row, _salesPerson);
            }
        }

        /// <summary> FieldUpdated ENGineering.engref </summary>
        public void _(Events.FieldUpdated<ENGineering.engNbr> e)
            => (e.Row as ENGineering).EngrRef = e.NewValue.ToString().ToUpper();

        #endregion
    }
}
