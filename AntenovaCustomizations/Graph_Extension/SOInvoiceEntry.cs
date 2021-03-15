using System;
using System.Collections;
using System.Collections.Generic;
using PX.Common;
using PX.Api;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.TX;
using PX.Objects.CA;
using ItemLotSerial = PX.Objects.IN.Overrides.INDocumentRelease.ItemLotSerial;
using SiteLotSerial = PX.Objects.IN.Overrides.INDocumentRelease.SiteLotSerial;
using LocationStatus = PX.Objects.IN.Overrides.INDocumentRelease.LocationStatus;
using LotSerialStatus = PX.Objects.IN.Overrides.INDocumentRelease.LotSerialStatus;
using POLineType = PX.Objects.PO.POLineType;
using POReceipt = PX.Objects.PO.POReceipt;
using POReceiptType = PX.Objects.PO.POReceiptType;
using POReceiptLine = PX.Objects.PO.POReceiptLine;
using SiteStatus = PX.Objects.IN.Overrides.INDocumentRelease.SiteStatus;
using System.Linq;
using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.Repositories;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Objects.AR.MigrationMode;
using PX.Objects.Common.Bql;
using PX.Common.Collection;
using PX.Objects.GL.FinPeriods;
using PX.Objects.Extensions.PaymentTransaction;
using PX.Data.BQL.Fluent;
using PX.Data.BQL;
using PX.Objects;
using PX.Objects.SO;

namespace PX.Objects.SO
{
    public class SOInvoiceEntry_Extension : PXGraphExtension<SOInvoiceEntry>
    {
        public override void Initialize()
        {
            base.Initialize();
            Base.report.AddMenuAction(InvoiceTaiwanReport);
            Base.report.AddMenuAction(InvoiceUKReport);
            Base.report.AddMenuAction(InvoiceSignatureReport);
        }

        #region Event Handlers

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

        #endregion
    }
}