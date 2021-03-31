using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Objects.SO
{
    public class SOPackageDetailExt : PXCacheExtension<SOPackageDetail>
    {
        [PXDBString(30, IsUnicode = true)]
        [PXUIField(DisplayName = "Carton Nbr")]
        public virtual string CustomRefNbr1 { get; set; }
        public abstract class customRefNbr1 : BqlString.Field<customRefNbr1> { }

        [PXDBQuantity]
        [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Qty", Enabled = true)]
        public virtual decimal? Qty { get; set; }
        public abstract class qty : BqlDecimal.Field<qty> { }

        [PXDBString(15, IsUnicode = true)]
        [PXDefault]
        [PXSelector(typeof(Search2<CSBox.boxID, LeftJoin<CarrierPackage, On<CSBox.boxID, Equal<CarrierPackage.boxID>, And<Current<SOShipment.shipVia>, IsNotNull>>>, Where<Current<SOShipment.shipVia>, IsNull, Or<Where<CarrierPackage.carrierID, Equal<Current<SOShipment.shipVia>>, And<Current<SOShipment.shipVia>, IsNotNull>>>>>))]
        [PXUIField(DisplayName = "Box ID")]
        public virtual string BoxID { get; set; }

        [PXDBDecimal]
        [PXUIField(DisplayName = "Gross Weight")]
        public virtual decimal? UsrGrossWeight { get; set; }
        public abstract class usrGrossWeight : BqlDecimal.Field<usrGrossWeight> { }

        [PXDBString(50)]
        [PXUIField(DisplayName = "Country")]
        [PXStringList()]
        public virtual string UsrCountry { get; set; }
        public abstract class usrCountry : BqlString.Field<usrCountry> { }

        [PXDBString(50)]
        [PXUIField(DisplayName = "Date Code")]
        [PXStringList(MultiSelect = true)]
        //[PXSelector(typeof(Search<SOShipLineSplit.lotSerialNbr,
        //                   Where<SOShipLineSplit.shipmentNbr, Equal<Optional<SOPackageDetail.shipmentNbr>>,
        //                     And<SOShipLineSplit.lineNbr, Equal<Optional<SOPackageDetailExt.usrShipmentSplitLineNbr>>>>>),
        //            typeof(SOShipLineSplit.inventoryID),
        //            typeof(SOShipLineSplit.lotSerialNbr),
        //            typeof(SOShipLineSplit.qty),
        //            typeof(SOShipLineSplit.expireDate), ValidateValue = false)]
        public virtual string UsrDateCode { get; set; }
        public abstract class usrDateCode : BqlString.Field<usrDateCode> { }

        [PXDBInt]
        [PXUIField(DisplayName = "Shipment Split Line Nbr")]
        [PXSelector(typeof(Search<SOShipLine.lineNbr,
                           Where<SOShipLine.shipmentNbr, Equal<Current<SOPackageDetail.shipmentNbr>>>>),
                    typeof(SOShipLine.origOrderType),
                    typeof(SOShipLine.origOrderNbr),
                    typeof(SOShipLine.inventoryID),
                    typeof(SOShipLine.shippedQty),
                    typeof(SOShipLine.packedQty),
                    typeof(SOShipLine.uOM))]
        public virtual int? UsrShipmentSplitLineNbr { get; set; }
        public abstract class usrShipmentSplitLineNbr : BqlInt.Field<usrShipmentSplitLineNbr> { }

        //[PXStringList(new string[] { "A","B"},new string[] { "AA","BB"}, MultiSelect = true)]
        //[PXUIField(DisplayName = "GG123")]
        //public virtual  string UsrMyCode { get; set; }
        //public abstract class usrMyCode : BqlString.Field<usrMyCode> { }
    }
}
