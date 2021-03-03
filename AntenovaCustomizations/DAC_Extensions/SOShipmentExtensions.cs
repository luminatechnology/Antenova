using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;

namespace PX.Objects.SO
{
    public class SOShipmentExt : PXCacheExtension<PX.Objects.SO.SOShipment>
    {
        #region UsrCarrierPluginID
        [PXDBString(15, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Carrier", Enabled = false)]
        [PXSelector(typeof(Search<CarrierPlugin.carrierPluginID>),
                    typeof(CarrierPlugin.carrierPluginID),
                    typeof(CarrierPlugin.pluginTypeName),
                    typeof(CarrierPlugin.description))]
        public virtual string UsrCarrierPluginID { get; set; }
        public abstract class usrCarrierPluginID : PX.Data.BQL.BqlString.Field<usrCarrierPluginID> { }
        #endregion

        #region UsrWaybill
        [PXDBString(20, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Waybill", Enabled = false)]
        public virtual string UsrWaybill { get; set; }
        public abstract class usrWaybill : PX.Data.BQL.BqlString.Field<usrWaybill> { }
        #endregion

        #region UsrNote
        [PXString(InputMask = "", IsUnicode = true)]
        [PXUIField(IsReadOnly = true)]
        public virtual string UsrNote { get; set; }
        public abstract class usrNote : PX.Data.BQL.BqlString.Field<usrNote> { }
        #endregion
    }
}