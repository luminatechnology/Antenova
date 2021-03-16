using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PX.Objects.SO
{
    public class SOShipmentEntry_Extension : PXGraphExtension<SOShipmentEntry>
    {
        #region Constant Class & String Variables
        public const string UPS = "UPS";
        public const string DHL = "DHL";
        public const string FDX = "FEDEX";
        public const string TNT = "TNT";

        private const string _QTYINBOX = "QTYINBOX";
        private const string _MADEIN = "MADEIN";

        public class QtyCartonAttr : PX.Data.BQL.BqlString.Constant<QtyCartonAttr>
        {
            public QtyCartonAttr() : base("QTYCARTON") { }
        }

        #endregion

        protected bool _IsAutoPacking = false;

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
            Base.report.AddMenuAction(SanminaInnerLabel);
            Base.report.AddMenuAction(StandardOuterLabel1);
            Base.report.AddMenuAction(StandardOuterLabel2);
            Base.report.AddMenuAction(BoschLabel);
        }

        /// <summary> Override Persist Event </summary>
        [PXOverride]
        public void Persist(PersistDelegate baseMethod)
        {
            var needUpdatePackedQty = Base.Packages.Cache.Dirty.RowCast<SOPackageDetailEx>().Count() > 0;
            if (needUpdatePackedQty)
            {
                // Except Delete row
                IEnumerable<SOPackageDetailEx> _packages = Base.Packages.Cache.Cached.RowCast<SOPackageDetailEx>();
                _packages = _packages.Except((IEnumerable<SOPackageDetailEx>)Base.Packages.Cache.Deleted);
                var _shipLines = Base.Transactions.Cache.Cached.RowCast<SOShipLine>();
                _shipLines = _shipLines.Except((IEnumerable<SOShipLine>)Base.Transactions.Cache.Deleted);
                // Recalculate PackedQty
                foreach (var item in _shipLines)
                {
                    item.PackedQty = _packages.Where(x => x.GetExtension<SOPackageDetailExt>().UsrShipmentSplitLineNbr == item.LineNbr).Sum(x => x.Qty);
                    Base.Caches[typeof(SOShipLine)].Update(item);
                }
            }

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

        #region Standard Outer Label 2 - LM642018
        public PXAction<SOShipment> BoschLabel;
        [PXButton]
        [PXUIField(DisplayName = "Print Bosch Label", Enabled = true, MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable boschLabel(PXAdapter adapter)
        {
            var _reportID = "LM642018";
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

        ///// <summary> Auto Packaging Button Click Event </summary>
        public PXAction<SOShipment> autoPackaging;
        [PXButton]
        [PXUIField(DisplayName = "Auto Packaging", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        public virtual IEnumerable AutoPackaging(PXAdapter adapter)
        {
            int parseResult = 0;
            var _maxCartno = GetMaxCartonNbr();

            SOShipLine _line = (SOShipLine)Base.Transactions.Cache.Current;
            var _QtyPerCarton = int.TryParse(_line.GetExtension<SOShipLineExt>().QtyPerCarton, out parseResult) ? parseResult : 0;
            // valid QtyPerCarton
            if (_QtyPerCarton == 0)
                throw new PXException("Qty per carton Can not be null or 0");

            var NuberOfPackages = Math.Floor((_line.GetExtension<SOShipLineExt>().RemainingQty.Value / _QtyPerCarton));


            PXLongOperation.StartOperation(Base, () =>
            {
                var _stockItemInfo = GetStockInfo(_line.InventoryID.Value, _QTYINBOX);
                var _itemCountry = GetStockInfo(_line.InventoryID.Value, _MADEIN);
                var _shipLineSplit = new PXGraph().Select<SOShipLineSplit>().Where(x => x.ShipmentNbr == _line.ShipmentNbr && x.LineNbr == _line.LineNbr);

                // Setting Box Weight
                var boxsInfo = GetBoxsInfo(_line.InventoryID);
                var _boxWeight = new PXGraph().Select<CSBox>().Where(x => x.BoxID == (string.IsNullOrEmpty(boxsInfo.stockItemBox) ? boxsInfo.sBoxID : boxsInfo.stockItemBox))?.FirstOrDefault()?.BoxWeight ?? 0;
                for (int i = 0; i < NuberOfPackages; i++)
                {
                    this._IsAutoPacking = true;
                    SOPackageDetailEx _package = (SOPackageDetailEx)Base.Packages.Cache.CreateInstance();
                    _package.ShipmentNbr = _line.ShipmentNbr;
                    _package.CustomRefNbr1 = (++_maxCartno).ToString().PadLeft(3, '0');
                    _package.Weight = _QtyPerCarton * _stockItemInfo.GetItem<InventoryItem>()?.BaseItemWeight / decimal.Parse(_stockItemInfo.GetItem<CSAnswers>()?.Value ?? "1");
                    _package.GetExtension<SOPackageDetailExt>().UsrGrossWeight = _package.Weight + _boxWeight;
                    _package.Qty = _QtyPerCarton;
                    _package.GetExtension<SOPackageDetailExt>().UsrShipmentSplitLineNbr = _line.LineNbr;
                    //_package.GetExtension<SOPackageDetailExt>().UsrCountry = _itemCountry.GetItem<CSAnswers>()?.Value;
                    Base.Packages.Insert(_package);
                }
                Base.Save.Press();
            });
            return adapter.Get();
        }

        /// <summary> Maunal Packaging Button Click Event </summary>
        public PXAction<SOShipment> manualPackaging;
        [PXButton]
        [PXUIField(DisplayName = "Maunal Packaging", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        public virtual IEnumerable ManualPackaging(PXAdapter adapter)
        {
            var _shipLines = Base.Transactions.Cache.Cached.RowCast<SOShipLine>();
            if (_shipLines.Where(x => x.GetExtension<SOShipLineExt>().RemainingQty < x.GetExtension<SOShipLineExt>().UsrPackingQty).Count() > 0)
                throw new PXException("Packing Qty cannot exceed Remaining Qty");
            PXLongOperation.StartOperation(Base, () =>
            {
                var _maxCartonNbr = GetMaxCartonNbr() + 1;
                foreach (var _line in _shipLines.Where(x => x.GetExtension<SOShipLineExt>().UsrPackingQty > 0))
                {
                    this._IsAutoPacking = true;

                    var _stockItemInfo = GetStockInfo(_line.InventoryID.Value, _QTYINBOX);
                    var _itemCountry = GetStockInfo(_line.InventoryID.Value, _MADEIN);
                    var _shipLineSplit = new PXGraph().Select<SOShipLineSplit>().Where(x => x.ShipmentNbr == _line.ShipmentNbr && x.LineNbr == _line.LineNbr);

                    //Setting Box Weight
                    // Setting Box Weight
                    var boxsInfo = GetBoxsInfo(_line.InventoryID);
                    var _boxWeight = new PXGraph().Select<CSBox>().Where(x => x.BoxID == (string.IsNullOrEmpty(boxsInfo.stockItemBox) ? boxsInfo.sBoxID : boxsInfo.stockItemBox))?.FirstOrDefault()?.BoxWeight ?? 0;

                    var _packingQty = _line.GetExtension<SOShipLineExt>().UsrPackingQty;
                    SOPackageDetailEx _package = (SOPackageDetailEx)Base.Packages.Cache.CreateInstance();
                    _package.ShipmentNbr = _line.ShipmentNbr;
                    _package.CustomRefNbr1 = _maxCartonNbr.ToString().PadLeft(3, '0');
                    _package.Weight = _packingQty * _stockItemInfo.GetItem<InventoryItem>()?.BaseItemWeight / decimal.Parse(_stockItemInfo.GetItem<CSAnswers>()?.Value ?? "1");
                    _package.GetExtension<SOPackageDetailExt>().UsrGrossWeight = _package.Weight + _boxWeight;
                    _package.Qty = _packingQty;
                    _package.GetExtension<SOPackageDetailExt>().UsrShipmentSplitLineNbr = _line.LineNbr;
                    //_package.GetExtension<SOPackageDetailExt>().UsrCountry = _itemCountry.GetItem<CSAnswers>()?.Value;
                    Base.Packages.Insert(_package);
                }
                Base.Save.Press();
            });
            return adapter.Get();
        }


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
                    if (note.NoteText.Length > 0)
                        str += note.NoteText + "\n--------------------\n";
                }
            }
            e.ReturnValue = (object)str;
        }

        /// <summary> Get Country DDL </summary>
        protected void _(Events.FieldSelecting<SOPackageDetailExt.usrCountry> e)
        {
            if (e.Row != null)
            {
                var ddl = new PXGraph().Select<CSAttributeDetail>()
                                                .Where(x => x.AttributeID == "MADEIN");
                PXStringListAttribute.SetList<SOPackageDetailExt.usrCountry>(
                    e.Cache,
                    e.Row,
                    ddl.Select(x => x.ValueID).ToArray(),
                    ddl.Select(x => x.Description).ToArray());
            }
        }

        /// <summary> SOPackageDetailExt_usrShipmentSplitLineNbr Updated Event </summary>
        protected virtual void _(Events.FieldUpdated<SOPackageDetailExt.usrShipmentSplitLineNbr> e)
        {
            if (e.NewValue == null)
                return;
            var _shipLine = Base.Transactions.Cache.Cached.RowCast<SOShipLine>().Where(x => x.LineNbr == (int?)e.NewValue).SingleOrDefault();
            var _stockItemInfo = GetStockInfo(_shipLine.InventoryID.Value, _MADEIN);
            var _shipLineSplit = new PXGraph().Select<SOShipLineSplit>().Where(x => x.ShipmentNbr == _shipLine.ShipmentNbr && x.LineNbr == _shipLine.LineNbr);
            var boxsInfo = GetBoxsInfo(_shipLine.InventoryID);
            e.Cache.SetValueExt<SOPackageDetailEx.boxID>(e.Row, string.IsNullOrEmpty(boxsInfo.stockItemBox) ? boxsInfo.sBoxID : boxsInfo.stockItemBox);
            e.Cache.SetValueExt<SOPackageDetail.inventoryID>(e.Row, _shipLine.InventoryID);
            e.Cache.SetValueExt<SOPackageDetailExt.usrCountry>(e.Row, _stockItemInfo.GetItem<CSAnswers>()?.Value);
            if (!this._IsAutoPacking)
                e.Cache.SetValueExt<SOPackageDetail.qty>(e.Row, _shipLine.ShippedQty);
            // if ShipLineSplit count == 1 then set value
            if (_shipLineSplit.Count() == 1)
                e.Cache.SetValueExt<SOPackageDetailExt.usrDateCode>(e.Row, _shipLineSplit.FirstOrDefault()?.LotSerialNbr);
        }

        /// <summary> SOPackageDetailEx_qty Updated Event </summary>
        protected void _(Events.FieldUpdated<SOPackageDetail.qty> e)
        {
            var _splitLineNbr = e.Cache.GetExtension<SOPackageDetailExt>(e.Row).UsrShipmentSplitLineNbr;
            if (e.NewValue == null || _splitLineNbr == null)
                return;

            if ((decimal?)e.NewValue == 0)
            {
                e.Cache.SetValueExt<SOPackageDetail.weight>(e.Row, 0);
                return;
            }

            var row = (SOPackageDetailEx)e.Row;

            var _shipLine = Base.Transactions.Cache.Cached.RowCast<SOShipLine>().Where(x => x.LineNbr == _splitLineNbr).SingleOrDefault();
            PXResult<InventoryItem, CSAnswers> _sotockItemData = GetStockInfo(_shipLine.InventoryID.Value, _QTYINBOX);
            decimal _qtyInBox = 1;
            try
            {
                _qtyInBox = decimal.Parse(_sotockItemData.GetItem<CSAnswers>()?.Value);
            }
            catch (Exception)
            {
                _qtyInBox = 1;
            }

            e.Cache.SetValueExt<SOPackageDetail.weight>(e.Row, row.Qty * _sotockItemData.GetItem<InventoryItem>().BaseItemWeight / _qtyInBox);
        }

        /// <summary> SOPackageDetailEx_weight Updated Event</summary>
        protected void _(Events.FieldUpdated<SOPackageDetail.weight> e)
        {
            if (e.NewValue != null)
            {
                var row = (SOPackageDetailEx)e.Row;
                var _boxWeight = new PXGraph().Select<CSBox>().Where(x => x.BoxID == row.BoxID)?.FirstOrDefault()?.BoxWeight ?? 0;
                e.Cache.SetValueExt<SOPackageDetailExt.usrGrossWeight>(e.Row, (decimal)e.NewValue + _boxWeight);
            }
        }

        /// <summary> Verify Carton Nbr Is Numerical </summary>
        protected void _(Events.FieldVerifying<SOPackageDetail.customRefNbr1> e)
        {
            int result = 0;
            if (!int.TryParse(e.NewValue.ToString(), out result))
                throw new PXSetPropertyException<SOPackageDetail.customRefNbr1>("Carton Nbr must be numerical.");
        }

        /// <summary> Verify Packing Qty </summary>
        protected void _(Events.FieldVerifying<SOShipLineExt.usrPackingQty> e)
        {
            if ((decimal?)e.NewValue < 0)
                throw new PXSetPropertyException("Packing Qty must br greater than 0");
            if ((decimal?)e.NewValue > ((SOShipLine)e.Row).GetExtension<SOShipLineExt>().RemainingQty)
                throw new PXSetPropertyException("Packing Qty cannot exceed Remaining Qty");
        }

        /// <summary> SOPackageDetailEx_BoxID Defaulting </summary>
        protected void _(Events.FieldDefaulting<SOPackageDetailEx.boxID> e)
        {
            var row = (SOPackageDetailEx)e.Row;
            var boxsInfo = GetBoxsInfo(row.InventoryID);
            e.NewValue = string.IsNullOrEmpty(boxsInfo.stockItemBox) ? boxsInfo.sBoxID : boxsInfo.stockItemBox;
        }

        #endregion

        #region Method

        /// <summary> Get Max Carton Nbr </summary>
        public int GetMaxCartonNbr()
        {
            int result;
            var _PackageDetail = Base.Caches<SOPackageDetailEx>().Cached.RowCast<SOPackageDetailEx>();
            try
            {
                return _PackageDetail.Any() ? _PackageDetail.Where(x => !string.IsNullOrEmpty(x.CustomRefNbr1) && int.TryParse(x.CustomRefNbr1, out result)).Max(x => int.Parse(x.CustomRefNbr1)) : 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary> Get StockItem Info </summary>
        public PXResult<InventoryItem, CSAnswers> GetStockInfo(int InventoryID, string AttributeID)
        {
            return (PXResult<InventoryItem, CSAnswers>)SelectFrom<InventoryItem>
                                                      .LeftJoin<CSAnswers>.On<InventoryItem.noteID.IsEqual<CSAnswers.refNoteID>
                                                           .And<CSAnswers.attributeID.IsEqual<P.AsString>>>
                                                      .Where<InventoryItem.inventoryID.IsEqual<P.AsInt>>
                                                      .View.Select(Base, AttributeID, InventoryID);
        }

        /// <summary> Get sBoxID and stockItemBox </summary>
        public (string sBoxID, string stockItemBox) GetBoxsInfo(int? InventoryID)
        {
            var _sboxID = new PXGraph().Select<CSBox>().FirstOrDefault()?.BoxID;
            var _stockItemBoxID = new PXGraph().Select<INItemBoxEx>().Where(x => x.InventoryID == InventoryID).FirstOrDefault()?.BoxID;
            return (_sboxID, _stockItemBoxID);
        }
        #endregion

        #region Static Method
        public static string GetShipWaybillURL(string carrier, string wayBill)
        {
            string url = null;

            carrier = string.IsNullOrEmpty(carrier) ? string.Empty : carrier.ToUpper();

            switch (carrier)
            {
                case UPS:
                    url = $"https://www.ups.com/track?loc=en_tw&tracknum={wayBill.Trim()}&requester=WT/trackdetails";
                    break;
                case DHL:
                    url = $"https://mydhl.express.dhl/tw/en/tracking.html#/results?id={wayBill.Trim()}";
                    break;
                case FDX:
                    url = $"https://www.fedex.com/fedextrack/?trknbr={wayBill.Trim()}";
                    break;
                case TNT:
                    url = $" https://www.tnt.com/express/en_gc/site/shipping-tools/track.html?searchType=con&cons={wayBill.Trim()}";
                    break;
            }

            return url;
        }
        #endregion
    }
}