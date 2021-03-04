using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using System.Collections;
using System.Collections.Generic;

namespace PX.Objects.SO
{
    public class SOShipmentEntry_Extension : PXGraphExtension<SOShipmentEntry>
    {

        public override void Initialize()
        {
            base.Initialize();
            Base.report.AddMenuAction(COCReport);
        }

        #region Action

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

        #region Event Handlers
        protected virtual void _(PXCache sender, PXRowSelectedEventArgs e)
        {
            SOShipment row = (SOShipment)e.Row;
            if (row == null) return;

            sender.AllowUpdate = true;
            PXUIFieldAttribute.SetEnabled<SOShipmentExt.usrCarrierPluginID>(sender, null, true);
            PXUIFieldAttribute.SetEnabled<SOShipmentExt.usrWaybill>(sender, null, true);
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
        #endregion
    }
}