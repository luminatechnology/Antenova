using AntenovaCustomizations;
using AntenovaCustomizations.Descriptor;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Objects.CR
{
    public class CROpportunityExt : PXCacheExtension<CROpportunity>
    {
        #region UsrEndCust
        [PXDBString(20, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "End Customer")]
        [PXSelector(typeof(CRMendcust.custId),
          typeof(CRMendcust.name),
          DescriptionField = typeof(CRMendcust.name))]
        public virtual string UsrEndCust { get; set; }
        public abstract class usrendCust : PX.Data.BQL.BqlString.Field<usrendCust> { }
        #endregion

        #region UsrSalesPerson
        [PXDBInt]
        [PXUIField(DisplayName = "Sales Person")]
        [PXSelector(typeof(Search<PX.Objects.AR.SalesPerson.salesPersonID>),
                    typeof(PX.Objects.AR.SalesPerson.salesPersonCD),
                    typeof(PX.Objects.AR.SalesPerson.isActive),
                    typeof(PX.Objects.AR.SalesPerson.commnPct),
                    SubstituteKey = typeof(PX.Objects.AR.SalesPerson.salesPersonCD))]
        public virtual int? UsrSalesPerson { get; set; }
        public abstract class usrSalesPerson : PX.Data.BQL.BqlInt.Field<usrSalesPerson> { }
        #endregion

        #region UsrSalesRegion
        [GetDropDownAttribute("REGION")]
        [PXDBString(10, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Sales Region")]
        public virtual string UsrSalesRegion { get; set; }
        public abstract class usrsalesRegion : PX.Data.BQL.BqlString.Field<usrsalesRegion> { }
        #endregion

        #region UsrSource
        [PXDBString(10, InputMask = "")]
        [PXUIField(DisplayName = "Source")]
        [PXSelector(typeof(CRMSource.sourceID),
            typeof(CRMSource.descrption))]
        public virtual string UsrSource { get; set; }
        public abstract class usrSource : PX.Data.BQL.BqlString.Field<usrSource> { }
        #endregion
    }
}
