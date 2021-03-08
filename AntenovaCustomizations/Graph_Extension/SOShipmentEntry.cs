using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PX.Objects.SO
{
    public class SOShipmentEntry_Extension : PXGraphExtension<SOShipmentEntry>
    {
        public delegate void PersistDelegate();

        public override void Initialize()
        {
            base.Initialize();
            Base.report.AddMenuAction(COCReport);
            Base.report.AddMenuAction(PackingList);
            Base.report.AddMenuAction(GeneralOuterLabel);
            Base.report.AddMenuAction(HanaOuterLabel);
            Base.report.AddMenuAction(AngliaOuterLabel);
            Base.report.AddMenuAction(GlobalEMSOuterLabel);
            Base.report.AddMenuAction(USIOuterLabel);
            Base.report.AddMenuAction(SanminaOuterLabel);
            Base.report.AddMenuAction(StandardOuterLabel1);
            Base.report.AddMenuAction(StandardOuterLabel2);
            Base.report.AddMenuAction(SanminaInnerLabel);
        }

        [PXOverride]
        public void Persist(PersistDelegate baseMethod)
        {
            var _packages = Base.Packages.Select().RowCast<SOPackageDetailEx>();
            var _shipLines = Base.Transactions.Select().RowCast<SOShipLine>();
            var _grpPackages = _packages.GroupBy(x => x.GetExtension<SOPackageDetailExt>().UsrShipmentSplitLineNbr)
                                       .Select(x => new {splitLineNbr = x.Key, _packedQty = x.Sum(y => y.Qty)});
            foreach (var item in _grpPackages)
                _shipLines.Where(x => x.LineNbr == item.splitLineNbr).FirstOrDefault().PackedQty = item._packedQty;

            baseMethod();
        }

        #region Action

        #region COCReport
        public PXAction<SOShipment> COCReport;
        [PXButton]
        [PXUIField(DisplayName = "Print COC Report", Enabled = true, MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable cOCReport(PXAdapter adapter)
        {
            var _reportID = "lm601000";
            if (Base.Document.Current != null)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters["ShipmentNbr"] = Base.Document.Current.ShipmentNbr;
                parameters["ShipmentType"] = Base.Document.Current.ShipmentType;
                parameters["_CompanyID"] = PXAccess.GetBranchID().ToString();
                throw new PXReportRequiredException(parameters, _reportID, string.Format("Report {0}", _reportID));
            }
            return adapter.Get();
        }
        #endregion

        #region Packing List - LM642005
        public PXAction<SOShipment> PackingList;
        [PXButton]
        [PXUIField(DisplayName = "Print Packing List", Enabled = true, MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable packingList(PXAdapter adapter)
        {
            var _reportID = "LM642005";
            var parameters = new Dictionary<string, string>()
            {
                ["ShipmentNbr"] = (Base.Caches<SOShipment>().Current as SOShipment)?.ShipmentNbr
            };
            if (parameters["ShipmentNbr"] != null)
                throw new PXReportRequiredException(parameters, _reportID, string.Format("Report {0}", _reportID));
            return adapter.Get<SOShipment>().ToList();
        }
        #endregion

        #region General Outer Label - LM642010
        public PXAction<SOShipment> GeneralOuterLabel;
        [PXButton]
        [PXUIField(DisplayName = "Print General Outer Label", Enabled = true, MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable generalOuterLabel(PXAdapter adapter)
        {
            var _reportID = "LM642010";
            var parameters = new Dictionary<string, string>()
            {
                ["ShipmentNbr"] = (Base.Caches<SOShipment>().Current as SOShipment)?.ShipmentNbr
            };
            if (parameters["ShipmentNbr"] != null)
                throw new PXReportRequiredException(parameters, _reportID, string.Format("Report {0}", _reportID));
            return adapter.Get<SOShipment>().ToList();
        }
        #endregion
        
        #region Hana Outer Label - LM642011
        public PXAction<SOShipment> HanaOuterLabel;
        [PXButton]
        [PXUIField(DisplayName = "Print Hana Outer Label", Enabled = true, MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable hanaOuterLabel(PXAdapter adapter)
        {
            var _reportID = "LM642011";
            var parameters = new Dictionary<string, string>()
            {
                ["ShipmentNbr"] = (Base.Caches<SOShipment>().Current as SOShipment)?.ShipmentNbr
            };
            if (parameters["ShipmentNbr"] != null)
                throw new PXReportRequiredException(parameters, _reportID, string.Format("Report {0}", _reportID));
            return adapter.Get<SOShipment>().ToList();
        }
        #endregion

        #region Anglia Outer Label - LM642012
        public PXAction<SOShipment> AngliaOuterLabel;
        [PXButton]
        [PXUIField(DisplayName = "Print Anglia Outer Label", Enabled = true, MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable angliaOuterLabel(PXAdapter adapter)
        {
            var _reportID = "LM642012";
            var parameters = new Dictionary<string, string>()
            {
                ["ShipmentNbr"] = (Base.Caches<SOShipment>().Current as SOShipment)?.ShipmentNbr
            };
            if (parameters["ShipmentNbr"] != null)
                throw new PXReportRequiredException(parameters, _reportID, string.Format("Report {0}", _reportID));
            return adapter.Get<SOShipment>().ToList();
        }
        #endregion

        #region Global EMS Outer Label - LM642013
        public PXAction<SOShipment> GlobalEMSOuterLabel;
        [PXButton]
        [PXUIField(DisplayName = "Print Global EMS Outer Label", Enabled = true, MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable globalEMSOuterLabel(PXAdapter adapter)
        {
            var _reportID = "LM642013";
            var parameters = new Dictionary<string, string>()
            {
                ["ShipmentNbr"] = (Base.Caches<SOShipment>().Current as SOShipment)?.ShipmentNbr
            };
            if (parameters["ShipmentNbr"] != null)
                throw new PXReportRequiredException(parameters, _reportID, string.Format("Report {0}", _reportID));
            return adapter.Get<SOShipment>().ToList();
        }
        #endregion

        #region USI Outer Label - LM642014
        public PXAction<SOShipment> USIOuterLabel;
        [PXButton]
        [PXUIField(DisplayName = "Print USI Outer Label", Enabled = true, MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable uSIOuterLabel(PXAdapter adapter)
        {
            var _reportID = "LM642014";
            var parameters = new Dictionary<string, string>()
            {
                ["ShipmentNbr"] = (Base.Caches<SOShipment>().Current as SOShipment)?.ShipmentNbr
            };
            if (parameters["ShipmentNbr"] != null)
                throw new PXReportRequiredException(parameters, _reportID, string.Format("Report {0}", _reportID));
            return adapter.Get<SOShipment>().ToList();
        }
        #endregion

        #region Sanmina Outer Label - LM642015
        public PXAction<SOShipment> SanminaOuterLabel;
        [PXButton]
        [PXUIField(DisplayName = "Print Sanmina Outer Label", Enabled = true, MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable sanminaOuterLabel(PXAdapter adapter)
        {
            var _reportID = "LM642015";
            var parameters = new Dictionary<string, string>()
            {
                ["ShipmentNbr"] = (Base.Caches<SOShipment>().Current as SOShipment)?.ShipmentNbr
            };
            if (parameters["ShipmentNbr"] != null)
                throw new PXReportRequiredException(parameters, _reportID, string.Format("Report {0}", _reportID));
            return adapter.Get<SOShipment>().ToList();
        }
        #endregion

        #region Standard Outer Label 1 - LM642016
        public PXAction<SOShipment> StandardOuterLabel1;
        [PXButton]
        [PXUIField(DisplayName = "Print Standard Outer Label 1", Enabled = true, MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable standardOuterLabel1(PXAdapter adapter)
        {
            var _reportID = "LM642016";
            var parameters = new Dictionary<string, string>()
            {
                ["ShipmentNbr"] = (Base.Caches<SOShipment>().Current as SOShipment)?.ShipmentNbr
            };
            if (parameters["ShipmentNbr"] != null)
                throw new PXReportRequiredException(parameters, _reportID, string.Format("Report {0}", _reportID));
            return adapter.Get<SOShipment>().ToList();
        }
        #endregion

        #region Standard Outer Label 2 - LM642017
        public PXAction<SOShipment> StandardOuterLabel2;
        [PXButton]
        [PXUIField(DisplayName = "Print Standard Outer Label 2", Enabled = true, MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable standardOuterLabel2(PXAdapter adapter)
        {
            var _reportID = "LM642017";
            var parameters = new Dictionary<string, string>()
            {
                ["ShipmentNbr"] = (Base.Caches<SOShipment>().Current as SOShipment)?.ShipmentNbr
            };
            if (parameters["ShipmentNbr"] != null)
                throw new PXReportRequiredException(parameters, _reportID, string.Format("Report {0}", _reportID));
            return adapter.Get<SOShipment>().ToList();
        }
        #endregion

        #region Sanmina Inner Label - LM642020
        public PXAction<SOShipment> SanminaInnerLabel;
        [PXButton]
        [PXUIField(DisplayName = "Print Sanmina Inner Label", Enabled = true, MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable sanminaInnerLabel(PXAdapter adapter)
        {
            var _reportID = "LM642020";
            var parameters = new Dictionary<string, string>()
            {
                ["ShipmentNbr"] = (Base.Caches<SOShipment>().Current as SOShipment)?.ShipmentNbr
            };
            if (parameters["ShipmentNbr"] != null)
                throw new PXReportRequiredException(parameters, _reportID, string.Format("Report {0}", _reportID));
            return adapter.Get<SOShipment>().ToList();
        }
        #endregion

        #endregion

        #region Event Handlers
        protected virtual void _(Events.RowSelected<SOShipment> e, PXRowSelected baseHandler)
        {
            baseHandler?.Invoke(e.Cache, e.Args);

            SOShipment row = (SOShipment)e.Row;

            if (row == null) return;

            e.Cache.AllowUpdate = true;
            PXUIFieldAttribute.SetEnabled<SOShipmentExt.usrCarrierPluginID>(e.Cache, null, true);
            PXUIFieldAttribute.SetEnabled<SOShipmentExt.usrWaybill>(e.Cache, null, true);
        }

        protected void _(Events.FieldSelecting<SOShipmentExt.usrNote> e)
        {
            SOShipment row = e.Row as SOShipment;
            string str = string.Empty;
            if (row != null)
            {
                foreach (PXResult<Note> pxResult1 in SelectFrom<Note>.InnerJoin<SOOrder>.On<SOOrder.noteID.IsEqual<Note.noteID>>
                                                                         .InnerJoin<SOOrderShipment>.On<SOOrderShipment.orderType.IsEqual<SOOrder.orderType>
                                                                                                        .And<SOOrderShipment.orderNbr.IsEqual<SOOrder.orderNbr>>>
                                                                         .Where<SOOrderShipment.shipmentNbr.IsEqual<P.AsString>>.View.ReadOnly.Select((PXGraph)this.Base, (object)row.ShipmentNbr))
                {

                    Note note = (Note)pxResult1;
                    if(note.NoteText.Length > 0)
                        str += note.NoteText + "\n--------------------\n";
                }
            }
            e.ReturnValue = (object)str;
        }


        /// <summary>SOPackageDetailExt_usrShipmentSplitLineNbr Updated Event </summary>
        protected virtual void _(Events.FieldUpdated<SOPackageDetailExt.usrShipmentSplitLineNbr> e)
        {
            if (e.NewValue == null)
                return;
            var _shipLine = Base.Transactions.Select().RowCast<SOShipLine>().Where(x => x.LineNbr == (int?)e.NewValue).SingleOrDefault();
            e.Cache.SetValueExt<SOPackageDetail.qty>(e.Row, _shipLine.ShippedQty);
            e.Cache.SetValueExt<SOPackageDetail.inventoryID>(e.Row, _shipLine.InventoryID);
        }

        /// <summary>SOPackageDetailExt_qty Updated Event </summary>
        protected void _(Events.FieldUpdated<SOPackageDetail.qty> e)
        {
            var row = (SOPackageDetailEx)e.Row;
            var _splitLineNbr = e.Cache.GetExtension<SOPackageDetailExt>(e.Row).UsrShipmentSplitLineNbr;
            if ((decimal?)e.NewValue == 0)
            {
                e.Cache.SetValueExt<SOPackageDetail.weight>(e.Row, 0);
                return;
            }
            var _shipLine = Base.Transactions.Select().RowCast<SOShipLine>().Where(x => x.LineNbr == _splitLineNbr).SingleOrDefault();
            InventoryItem _stockItem = SelectFrom<InventoryItem>
                             .Where<InventoryItem.inventoryID.IsEqual<P.AsInt>>
                             .View.Select(Base, _shipLine.InventoryID);
            var _qtyCarton = e.Cache.GetValueExt(e.Row, PX.Objects.CS.Messages.Attribute + "QTYCARTON") as PXFieldState;
            e.Cache.SetValueExt<SOPackageDetail.weight>(e.Row, row.Qty * _stockItem.BaseItemWeight / 1000);
        }

        #endregion
    }
}