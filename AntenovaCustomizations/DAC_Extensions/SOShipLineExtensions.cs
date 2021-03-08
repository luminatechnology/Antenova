using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Objects.SO
{
    public class SOShipLineExt : PXCacheExtension<SOShipLine>
    {
        [PXDBDecimal]
        [PXDefault(0)]
        [PXUIField(DisplayName = "Packing Qty")]
        public virtual decimal? UsrPackingQty { get; set; }
        public abstract class usrPackingQty : BqlDecimal.Field<usrPackingQty> { }

        [PXDecimal]
        [PXUIField(DisplayName = "Remaining Qty")]
        public virtual decimal? RemainingQty { get; set; }

        [PXString]
        [PXUIField(DisplayName = "Qty Per Carton")]
        public virtual string QtyPerCarton { get; set; }
    }
}
