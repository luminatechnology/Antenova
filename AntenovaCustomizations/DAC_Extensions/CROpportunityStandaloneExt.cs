using AntenovaCustomizations.Descriptor;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntenovaCustomizations.DAC_Extensions
{
    public class CROpportunityStandaloneExt : PXCacheExtension<PX.Objects.CR.Standalone.CROpportunity>
    {
        [PXDBString(20, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "End Customer")]
        public virtual string UsrEndCust { get; set; }
        public abstract class usrendCust : PX.Data.BQL.BqlString.Field<usrendCust> { }

        [PXDBInt]
        [PXUIField(DisplayName = "Sales Person")]
        public virtual int? UsrSalesPerson { get; set; }
        public abstract class usrSalesPerson : PX.Data.BQL.BqlInt.Field<usrSalesPerson> { }

        [PXDBString(10, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Sales Region")]
        public virtual string UsrSalesRegion { get; set; }
        public abstract class usrsalesRegion : PX.Data.BQL.BqlString.Field<usrsalesRegion> { }
    }
}
