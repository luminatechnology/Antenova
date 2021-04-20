﻿using PX.Data;
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

namespace PX.Objects.CR
{
    public class OpportunityMaint_Etension : PXGraphExtension<OpportunityMaint>
    {
        public PXSetup<ENGSetup> setup;

        public SelectFrom<ENGineering>
                .Where<ENGineering.opprid.IsEqual<CROpportunity.opportunityID.FromCurrent>>.View ENGList;

        #region Override DAC

        [PXDefault]
        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXUIField(DisplayName = "Engineering Nbr", Required = true)]
        [AutoNumber(typeof(ENGSetup.eNGSequenceID), typeof(AccessInfo.businessDate))]
        [PXMergeAttributes(Method = MergeMethod.Replace)]
        public void _(Events.CacheAttached<ENGineering.engrNbr> e) { }
        #endregion

        #region Action
        public PXAction<ENGineering> viewENGDoc;

        [PXButton]
        [PXUIField(Visible = false)]
        public virtual IEnumerable ViewENGDoc(PXAdapter adapter)
        {
            var row = this.ENGList.Current;
            var graph = PXGraph.CreateInstance<ENGineeringMaint>();
            graph.Document.Current = SelectFrom<ENGineering>
                                     .Where<ENGineering.engrNbr.IsEqual<P.AsString>>
                                     .View.Select(Base, row.EngrNbr);
            PXRedirectHelper.TryRedirect(graph, PXRedirectHelper.WindowMode.NewWindow);
            return adapter.Get();
        }

        #endregion

        public void _(Events.RowSelected<CROpportunity> e, PXRowSelected baseHandler)
        {
            baseHandler?.Invoke(e.Cache, e.Args);
            var a = e.Row.GetExtension<CROpportunityExt>();
            var b = SelectFrom<CROpportunity>.Where<CROpportunity.opportunityID.IsEqual<CROpportunity.opportunityID.FromCurrent>>.View.Select(Base);
            var c = b.RowCast<CROpportunity>().FirstOrDefault().GetExtension<CROpportunityExt>();
        }

        public void _(Events.RowPersisting<ENGineering> e)
        {
            var row = (ENGineering)e.Row;
            var _oppID = Base.Opportunity.Current.OpportunityID;
            if (row.Opprid.ToLower().Contains("new"))
                e.Row.Opprid = _oppID;
        }

        public void _(Events.RowPersisted<ENGineering> e)
        {
            var row = e.Row as ENGineering;
            var _RevenueData = new PXGraph().Select<ENGRevenueLine>().Where(x => x.EngrNbr == row.EngrNbr);
            if (_RevenueData.Count() == 0)
            {
                var _graph = PXGraph.CreateInstance<ENGineeringMaint>();
                var _oppProduct = Base.Products.Select().RowCast<CROpportunityProducts>();
                foreach (var _prod in _oppProduct)
                {
                    var _data = _graph.RevenueLine.Insert(_graph.RevenueLine.Cache.CreateInstance() as ENGRevenueLine);
                    _data.EngrNbr = row.EngrNbr;
                    _data.InventoryID = _prod.InventoryID;
                    _data.Descr = _prod.Descr;
                    _data.Quantity = _prod.Quantity;
                    _data.Uom = _prod.UOM;
                    _data.UnitPrice = _prod.UnitPrice;
                    _data.ExtPrice = _prod.ExtPrice;
                }
                _graph.Actions.PressSave();
            }

        }

        /// <summary> Set EngrNbr Disabled </summary>
        public void _(Events.FieldSelecting<ENGineering.engrNbr> e)
        {
            PXUIFieldAttribute.SetEnabled<ENGineering.engrNbr>(e.Cache, null, false);
        }

        /// <summary> Initial Project Type DDL </summary>
        public void _(Events.FieldSelecting<ENGineering.prjtype> e)
        {
            var _prjType = SelectFrom<ENGProjectType>.View.Select(Base).RowCast<ENGProjectType>();
            if (e.Row != null)
            {
                PXStringListAttribute.SetList<ENGineering.prjtype>(
                    e.Cache,
                    e.Row,
                    _prjType.Select(x => x.Prjtype).ToArray(),
                    _prjType.Select(x => x.Prjtype).ToArray());
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

        public void _(Events.FieldDefaulting<ENGineering.salesPerson> e)
            => e.NewValue = (Base.Opportunity.Cache.GetValueExt<CROpportunityExt.usrSalesPerson>(Base.Opportunity.Current) as PXStringState).Value;

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
    }
}
