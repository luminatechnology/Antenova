using System;
using System.Collections;
using System.Collections.Generic;
using PX.Data;
using PX.Objects.AR;

namespace PX.Objects.SO
{
    public class SOInvoiceEntry_Extension : PXGraphExtension<SOInvoiceEntry>
    {
        public const string ShipmentNoticeRpt = "LM643001";

        public override void Initialize()
        {
            base.Initialize();

            Base.report.AddMenuAction(printShipNotice);
            Base.report.AddMenuAction(InvoiceTaiwanReport);
            Base.report.AddMenuAction(InvoiceUKReport);
            Base.report.AddMenuAction(InvoiceSignatureReport);
        }

        #region Report Actions

        #region Invoice_Taiwan
        public PXAction<SOShipment> InvoiceTaiwanReport;
        [PXButton]
        [PXUIField(DisplayName = "Print Taiwan Invoice Report", Enabled = true, MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable invoiceTaiwanReport(PXAdapter adapter)
        {
            var _reportID = "so643001";
            if (Base.Document.Current != null)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters["DocType"] = Base.Document.Current.DocType;
                parameters["RefNbr"] = Base.Document.Current.RefNbr;
                throw new PXReportRequiredException(parameters, _reportID, string.Format("Report {0}", _reportID));
            }
            return adapter.Get();
        }
        #endregion

        #region Invoice_Taiwan
        public PXAction<SOShipment> InvoiceUKReport;
        [PXButton]
        [PXUIField(DisplayName = "Print UK Invoice Report", Enabled = true, MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable invoiceUKReport(PXAdapter adapter)
        {
            var _reportID = "so643002";
            if (Base.Document.Current != null)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters["DocType"] = Base.Document.Current.DocType;
                parameters["RefNbr"] = Base.Document.Current.RefNbr;
                throw new PXReportRequiredException(parameters, _reportID, string.Format("Report {0}", _reportID));
            }
            return adapter.Get();
        }
        #endregion

        #region Invoice_Taiwan
        public PXAction<SOShipment> InvoiceSignatureReport;
        [PXButton]
        [PXUIField(DisplayName = "Print Signature Invoice Report", Enabled = true, MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable invoiceSignatureReport(PXAdapter adapter)
        {
            var _reportID = "so643003";
            if (Base.Document.Current != null)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters["DocType"] = Base.Document.Current.DocType;
                parameters["RefNbr"] = Base.Document.Current.RefNbr;
                throw new PXReportRequiredException(parameters, _reportID, string.Format("Report {0}", _reportID));
            }
            return adapter.Get();
        }
        #endregion

        public PXAction<ARInvoice> printShipNotice;
        [PXButton]
        [PXUIField(DisplayName = "Print Shipment Notice", Enabled = true, MapEnableRights = PXCacheRights.Select)]
        protected virtual IEnumerable PrintShipNotice(PXAdapter adapter)
        {

            if (Base.Document.Current != null)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();

                parameters[nameof(ARInvoice.DocType)] = Base.Document.Current.DocType;
                parameters[nameof(ARInvoice.RefNbr)]  = Base.Document.Current.RefNbr;

                throw new PXReportRequiredException(parameters, ShipmentNoticeRpt);
            }
            return adapter.Get();
        }

        #endregion
    }
}