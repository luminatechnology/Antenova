using AntenovaCustomizations.DAC;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntenovaCustomizations.Graph
{
    public class ENGineeringMaint : PXGraph<ENGineeringMaint>
    {

        public ENGineeringMaint()
        {
            actionsFolder.MenuAutoOpen = true;
            actionsFolder.AddMenuAction(changeToNew);
            actionsFolder.AddMenuAction(changeToAwaiting);
            actionsFolder.AddMenuAction(changeToProcess);
            actionsFolder.AddMenuAction(changeToOnHold);
            actionsFolder.AddMenuAction(changeToClosed);
            actionsFolder.AddMenuAction(changeToCompletion);
        }

        #region ENUM
        public enum ENGStatus : int
        {
            New = 1,
            Process = 2,
            Awaiting = 3,
            Hold = 4,
            Closed = 5,
            Completion = 6,
            Recycled = 7
        }
        #endregion

        #region View / Setup

        [PXHidden]
        public PXSetup<ENGSetup> setup;

        [PXViewName("Engineering")]
        public SelectFrom<ENGineering>.View Document;

        [PXViewName("Line")]
        public SelectFrom<ENGLine>
               .Where<ENGLine.engrNbr.IsEqual<ENGineering.engrNbr.FromCurrent>>.View Line;

        [PXViewName("CurrentLine")]
        public SelectFrom<ENGLine>
               .Where<ENGLine.engrNbr.IsEqual<ENGineering.engrNbr.FromCurrent>>.View CurrentLine;
        [PXViewName("RevenueLine")]
        public SelectFrom<ENGRevenueLine>
               .Where<ENGRevenueLine.engrNbr.IsEqual<ENGineering.engrNbr.FromCurrent>>.View RevenueLine;

        #endregion

        #region Action
        public PXSave<ENGineering> Save;
        public PXCancel<ENGineering> Cancel;
        public PXInsert<ENGineering> Insert;
        public PXCopyPasteAction<ENGineering> CopyPaste;
        public PXDelete<ENGineering> Delete;
        public PXFirst<ENGineering> First;
        public PXPrevious<ENGineering> Previous;
        public PXNext<ENGineering> Next;
        public PXLast<ENGineering> Last;

        public PXAction<ENGineering> actionsFolder;
        public PXAction<ENGineering> changeToNew;
        public PXAction<ENGineering> changeToProcess;
        public PXAction<ENGineering> changeToAwaiting;
        public PXAction<ENGineering> changeToOnHold;
        public PXAction<ENGineering> changeToClosed;
        public PXAction<ENGineering> changeToCompletion;

        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "Actions", MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable ActionsFolder(PXAdapter adapter)
        {
            return adapter.Get();
        }

        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "Change To New", MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable ChangeToNew(PXAdapter adapter)
        {
            ChangeStatus(ENGStatus.New);
            return adapter.Get();
        }

        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "Change To Process", MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable ChangeToProcess(PXAdapter adapter)
        {
            ChangeStatus(ENGStatus.Process);
            return adapter.Get();
        }

        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "Change To Awaiting", MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable ChangeToAwaiting(PXAdapter adapter)
        {
            ChangeStatus(ENGStatus.Awaiting);
            return adapter.Get();
        }

        [PXButton(CommitChanges = true, SpecialType = PXSpecialButtonType.Save)]
        [PXUIField(DisplayName = "Change To On Hold", MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable ChangeToOnHold(PXAdapter adapter)
        {
            ChangeStatus(ENGStatus.Hold);
            return adapter.Get();
        }

        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "Change To Closed", MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable ChangeToClosed(PXAdapter adapter)
        {
            ChangeStatus(ENGStatus.Closed);
            return adapter.Get();
        }

        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "Change To Completion", MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable ChangeToCompletion(PXAdapter adapter)
        {
            ChangeStatus(ENGStatus.Completion);
            return adapter.Get();
        }

        #endregion

        #region Override DAC
        /// <summary> engref </summary>
        [PXDefault]
        [PXMergeAttributes(Method = MergeMethod.Append)]
        public void _(Events.CacheAttached<ENGineering.engref> e) { }

        /// <summary> opprid </summary>
        [PXSelector(typeof(SearchFor<CROpportunity.opportunityID>),
                    typeof(CROpportunity.opportunityID),
                    typeof(CROpportunity.classID),
                    typeof(CROpportunity.status),
                    typeof(CROpportunity.subject),
                    typeof(CROpportunity.locationID))]
        [PXMergeAttributes(Method = MergeMethod.Append)]
        public void _(Events.CacheAttached<ENGineering.opprid> e) { }

        // <summary> oppBAccountID </summary>
        [PXDefault]
        [PXMergeAttributes(Method = MergeMethod.Append)]
        public void _(Events.CacheAttached<ENGineering.oppBAccountID> e) { }

        #endregion

        #region Events

        #region Row Persisting

        /// <summary> ENGineering RowPersisting </summary>
        public void _(Events.RowPersisting<ENGineering> e)
            => ValidField(e);

        /// <summary> ENGLine RowPersisting </summary>
        public void _(Events.RowPersisting<ENGLine> e)
            => ValidField(e);

        #endregion

        #region Row Selected

        /// <summary> RowSelected Engineering  </summary>
        public void _(Events.RowSelected<ENGineering> e)
        {
            var _prjType = SelectFrom<ENGProjectType>.View.Select(this).RowCast<ENGProjectType>();
            if (e.Row != null)
            {
                PXStringListAttribute.SetList<ENGineering.prjtype>(
                    e.Cache,
                    e.Row,
                    _prjType.Select(x => x.Prjtype).ToArray(),
                    _prjType.Select(x => x.Description).ToArray());
            }
        }

        #endregion

        #region FieldUpdated

        /// <summary> FieldUpdated ENGineering.opprid </summary>
        public void _(Events.FieldUpdated<ENGineering.opprid> e)
        {
            var _graphOpportunity = PXGraph.CreateInstance<OpportunityMaint>();
            var row = e.Row as ENGineering;
            if (e.NewValue != null)
            {
                // Auto get Engineering Field Data
                CROpportunity _oppor = SelectFrom<CROpportunity>
                                    .Where<CROpportunity.opportunityID.IsEqual<P.AsString>>
                                    .View.ReadOnly.SelectSingleBound(this, new object[] { row }, e.NewValue);
                row.OppBAccountID = _oppor.BAccountID;
                row.EndCust = _oppor.GetExtension<CROpportunityExt>().UsrEndCust;
                row.SalesPerson = _oppor.GetExtension<CROpportunityExt>().UsrSalesPerson;
                row.SalesRegion = _oppor.GetExtension<CROpportunityExt>().UsrSalesRegion;

                // Auto Get Revenule Line Data
                if (this.RevenueLine.Select().Count == 0)
                {
                    var _oppProduct = SelectFrom<CROpportunityProducts>
                                      .InnerJoin<CROpportunity>.On<CROpportunityProducts.quoteID.IsEqual<CROpportunity.defQuoteID>>
                                      .Where<CROpportunity.opportunityID.IsEqual<P.AsString>>
                                      .View.Select(this, row.Opprid).RowCast<CROpportunityProducts>();
                    foreach (var _prod in _oppProduct)
                    {
                        var _data = this.RevenueLine.Insert((ENGRevenueLine)this.RevenueLine.Cache.CreateInstance());
                        _data.InventoryID = _prod.InventoryID;
                        _data.Descr = _prod.Descr;
                        _data.Quantity = _prod.Quantity;
                        _data.Uom = _prod.UOM;
                        _data.UnitPrice = _prod.UnitPrice;
                        _data.ExtPrice = _prod.ExtPrice;
                    }
                }
            }
        }

        /// <summary> FieldUpdated ENGineering.engref </summary>
        public void _(Events.FieldUpdated<ENGineering.engref> e)
            => (e.Row as ENGineering).Engref = e.NewValue.ToString().ToUpper();

        /// <summary> FieldUpdated ENGRevenueLine.inventoryID </summary>
        public void _(Events.FieldUpdated<ENGRevenueLine.inventoryID> e)
        {
            var row = e.Row as ENGRevenueLine;
            if (e.NewValue != null)
            {
                InventoryItem _item = SelectFrom<InventoryItem>
                            .Where<InventoryItem.inventoryID.IsEqual<P.AsInt>>
                            .View.ReadOnly.SelectSingleBound(this, new object[] { row }, e.NewValue);
                row.Uom = _item.BaseUnit;
                row.Descr = _item.Descr;
            }
        }

        #endregion

        #region Field Selecting

        /// <summary> FieldSelecting ENGLine engrAgeDays </summary>
        public void _(Events.FieldSelecting<ENGLine.engrAgeDays> e)
        {
            var row = e.Row as ENGLine;
            if (e.Row != null && row.ActComplete.HasValue && row.ProcessDate.HasValue)
                row.EngrAgeDays = (int)(row.ActComplete.Value.Date - row.ProcessDate.Value.Date).TotalDays;

        }

        #endregion

        #endregion

        #region Function

        /// <summary> Valid ENGineering Row </summary>
        public void ValidField(Events.RowPersisting<ENGineering> e)
        {
            var row = e.Row as ENGineering;

            #region Valid Opprid
            bool IsRqeuired = SelectFrom<ENGProjectType>
                                 .Where<ENGProjectType.prjtype.IsEqual<P.AsString>>
                                 .View.ReadOnly.SelectSingleBound(this, new object[] { row }, row.Prjtype)
                                 .RowCast<ENGProjectType>().FirstOrDefault()?.LinkOppr ?? false;
            if (IsRqeuired && string.IsNullOrEmpty(row.Opprid))
                e.Cache.RaiseExceptionHandling<ENGineering.opprid>(e.Row, row.Opprid,
                new PXSetPropertyException<ENGineering.opprid>("Opportunity Nbr can not be empty"));
            #endregion

            #region Valid Repeat
            if (string.IsNullOrEmpty(row.Repeat))
                e.Cache.RaiseExceptionHandling<ENGineering.repeat>(e.Row, row.Repeat,
                    new PXSetPropertyException<ENGineering.repeat>("Engineer Repeat can not be empty"));
            #endregion

            #region Valid Assign Engineer
            if (row.Engineer == null)
                e.Cache.RaiseExceptionHandling<ENGineering.engineer>(e.Row, row.Engineer,
                    new PXSetPropertyException<ENGineering.engineer>("Assign Engineer can not be empty"));

            #endregion

            #region Valid EngRef
            if (string.IsNullOrEmpty(row.Engref))
                e.Cache.RaiseExceptionHandling<ENGineering.engref>(e.Row, row.Engref,
                    new PXSetPropertyException<ENGineering.engref>("Engineering Ref can not be empty"));
            #endregion

            #region Valid [EngrRef] + [Engineer Repeat]
            var isExixts = new PXGraph().Select<ENGineering>()
                                        .Where(x => x.Engref == row.Engref &&
                                                    x.Repeat == row.Repeat &&
                                                    x.EngrNbr != row.EngrNbr).Count() > 0;
            if (isExixts && !string.IsNullOrEmpty(row.Engref))
                e.Cache.RaiseExceptionHandling<ENGineering.engref>(e.Row, row.Engref,
                  new PXSetPropertyException<ENGineering.engref>("[EngrRef] + [Engineer Repeat] is not allowed duplicated"));
            #endregion
        }

        /// <summary> Valid ENGLine Row </summary>
        public void ValidField(Events.RowPersisting<ENGLine> e)
        {
            var row = e.Row as ENGLine;

            #region Valid Gerber Nbr
            if (row.IsGerber.Value && string.IsNullOrEmpty(row.GerberNbr))
                e.Cache.RaiseExceptionHandling<ENGLine.gerberNbr>(e.Row, row.GerberNbr,
                    new PXSetPropertyException<ENGLine.gerberNbr>("Gerber Review Number can not be empty"));
            #endregion

            #region Valid Awaiting
            if (!string.IsNullOrEmpty(row.AwaitReason))
            {
                if (!row.AwaitdateFrom.HasValue)
                    e.Cache.RaiseExceptionHandling<ENGLine.awaitdateFrom>(e.Row, row.AwaitdateFrom,
                        new PXSetPropertyException<ENGLine.awaitdateFrom>("Awaite Date From can not be empty"));
                if (!row.AwaitdateTo.HasValue)
                    e.Cache.RaiseExceptionHandling<ENGLine.awaitdateTo>(e.Row, row.AwaitdateTo,
                       new PXSetPropertyException<ENGLine.awaitdateTo>("Awaite Date To can not be empty"));

            }
            else if (row.AwaitdateTo.HasValue || row.AwaitdateFrom.HasValue)
            {
                if (string.IsNullOrEmpty(row.AwaitReason))
                    e.Cache.RaiseExceptionHandling<ENGLine.awaitReason>(e.Row, row.AwaitReason,
                       new PXSetPropertyException<ENGLine.awaitReason>("Awaite Reason can not be empty"));
                if (!row.AwaitdateFrom.HasValue)
                    e.Cache.RaiseExceptionHandling<ENGLine.awaitdateFrom>(e.Row, row.AwaitdateFrom,
                        new PXSetPropertyException<ENGLine.awaitdateFrom>("Awaite Date From can not be empty"));
                if (!row.AwaitdateTo.HasValue)
                    e.Cache.RaiseExceptionHandling<ENGLine.awaitdateTo>(e.Row, row.AwaitdateTo,
                       new PXSetPropertyException<ENGLine.awaitdateTo>("Awaite Date To can not be empty"));
            }
            if (row.AwaitdateFrom.HasValue && row.AwaitdateTo.HasValue && row.AwaitdateTo.Value < row.AwaitdateFrom)
                e.Cache.RaiseExceptionHandling<ENGLine.awaitdateTo>(e.Row, row.AwaitdateTo,
                      new PXSetPropertyException<ENGLine.awaitdateTo>("Awaite Date To must Greater then Await From Date"));
            #endregion

            #region Valid OnHold

            if (row.OnholdDate.HasValue || !string.IsNullOrEmpty(row.OnholdReason))
            {
                if (string.IsNullOrEmpty(row.OnholdReason))
                    e.Cache.RaiseExceptionHandling<ENGLine.onholdReason>(e.Row, row.OnholdReason,
                        new PXSetPropertyException<ENGLine.onholdReason>("On Hold Reason can not be empty"));
                if (!row.OnholdDate.HasValue)
                    e.Cache.RaiseExceptionHandling<ENGLine.onholdDate>(e.Row, row.OnholdDate,
                   new PXSetPropertyException<ENGLine.onholdDate>("On Hold Date can not be empty"));
            }

            #endregion

            #region Valid Close
            if (row.CloseDate.HasValue || !string.IsNullOrEmpty(row.CloseReason))
            {
                if (string.IsNullOrEmpty(row.CloseReason))
                    e.Cache.RaiseExceptionHandling<ENGLine.closeReason>(e.Row, row.CloseReason,
                        new PXSetPropertyException<ENGLine.closeReason>("Close Reason can not be empty"));
                if (!row.CloseDate.HasValue)
                    e.Cache.RaiseExceptionHandling<ENGLine.closeDate>(e.Row, row.CloseDate,
                   new PXSetPropertyException<ENGLine.closeDate>("Close Date can not be empty"));
            }
            #endregion

        }

        /// <summary> Change Status </summary>
        public void ChangeStatus(ENGStatus _status)
        {
            var IsValid = true;
            var doc = this.Document.Cache.Current as ENGineering;
            var line = this.Line.Cache.Current as ENGLine ?? this.Line.Cache.CreateInstance() as ENGLine;

            if (doc == null)
                return;

            switch (_status)
            {
                case ENGStatus.Process:
                    if (!line.ActStart.HasValue)
                        IsValid = this.Line.Cache.RaiseExceptionHandling<ENGLine.actStart>(line, line.ActStart,
                                    new PXSetPropertyException<ENGLine.actStart>("Actual Start Date can not be empty"));
                    if (!line.EstComplete.HasValue)
                        IsValid = this.Line.Cache.RaiseExceptionHandling<ENGLine.estComplete>(line, line.EstComplete,
                            new PXSetPropertyException<ENGLine.estComplete>("Est. Complete Date can not be empty"));
                    break;
                case ENGStatus.Awaiting:
                    if (!line.EstStar.HasValue)
                        IsValid = this.Line.Cache.RaiseExceptionHandling<ENGLine.estStar>(line, line.EstStar,
                            new PXSetPropertyException<ENGLine.estStar>("Est. Start Date can not be empty"));
                    if (!line.EstComplete.HasValue)
                        IsValid = this.Line.Cache.RaiseExceptionHandling<ENGLine.estComplete>(line, line.EstComplete,
                            new PXSetPropertyException<ENGLine.estComplete>("Est. Complete Date can not be empty"));
                    if (!line.AwaitdateFrom.HasValue)
                        IsValid = this.Line.Cache.RaiseExceptionHandling<ENGLine.awaitdateFrom>(line, line.AwaitdateFrom,
                            new PXSetPropertyException<ENGLine.awaitdateFrom>("Awaiting From Date can not be empty"));
                    if (!line.AwaitdateTo.HasValue)
                        IsValid = this.Line.Cache.RaiseExceptionHandling<ENGLine.awaitdateTo>(line, line.AwaitdateTo,
                            new PXSetPropertyException<ENGLine.awaitdateTo>("Awaiting To Date can not be empty"));
                    if (string.IsNullOrEmpty(line.AwaitReason))
                        IsValid = this.Line.Cache.RaiseExceptionHandling<ENGLine.awaitReason>(line, line.AwaitReason,
                            new PXSetPropertyException<ENGLine.awaitReason>("Awaiting Reason can not be empty"));
                    break;
                case ENGStatus.Hold:
                    if (!line.OnholdDate.HasValue)
                        IsValid = this.Line.Cache.RaiseExceptionHandling<ENGLine.onholdDate>(line, line.OnholdDate,
                            new PXSetPropertyException<ENGLine.onholdDate>("On Hold Date can not be empty"));
                    if (string.IsNullOrEmpty(line.OnholdReason))
                        IsValid = this.Line.Cache.RaiseExceptionHandling<ENGLine.onholdReason>(line, line.OnholdReason,
                           new PXSetPropertyException<ENGLine.onholdReason>("On Hold Reason can not be emtpy"));
                    break;
                case ENGStatus.Closed:
                    if (!line.CloseDate.HasValue)
                        IsValid = this.Line.Cache.RaiseExceptionHandling<ENGLine.closeDate>(line, line.CloseDate,
                          new PXSetPropertyException<ENGLine.closeDate>("Close Date can not be empty"));
                    if (String.IsNullOrEmpty(line.CloseReason))
                        IsValid = this.Line.Cache.RaiseExceptionHandling<ENGLine.closeReason>(line, line.CloseReason,
                          new PXSetPropertyException<ENGLine.closeReason>("Close Reason can not be empty"));
                    break;
                case ENGStatus.Completion:
                    if (!line.ActComplete.HasValue)
                        IsValid = this.Line.Cache.RaiseExceptionHandling<ENGLine.actComplete>(line, line.ActComplete,
                         new PXSetPropertyException<ENGLine.actComplete>("Actual Complete Date can not be empty"));
                    if (string.IsNullOrEmpty(line.CompleteSummary))
                        IsValid = this.Line.Cache.RaiseExceptionHandling<ENGLine.completeSummary>(line, line.CompleteSummary,
                         new PXSetPropertyException<ENGLine.completeSummary>("Complete summary can not be empty"));
                    break;
            }
            if (IsValid)
            {
                doc.Status = ((int)_status).ToString();
                if (_status == ENGStatus.Process && !line.ProcessDate.HasValue)
                    line.ProcessDate = DateTime.Now;
            }
            this.Document.Cache.Update(doc);
            this.Line.Cache.Update(line);
            this.Save.Press();
        }

        #endregion

    }
}
