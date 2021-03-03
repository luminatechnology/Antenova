using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;

namespace PX.Objects.SO
{
    public class SOShipmentEntry_Extension : PXGraphExtension<SOShipmentEntry>
    {
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
        #endregion
    }
}