using AntenovaCustomizations;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.CR.MassProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Objects.CR
{
    public class CRLeadExt : PXCacheExtension<PX.Objects.CR.CRLead>
    {
        #region UsrEndCust

        [PXDBString(20, IsUnicode = true, InputMask = "", BqlField = typeof(Standalone.CRLeadStandaloneExt.usrEndCust))]
        [PXUIField(DisplayName = "End Customer")]
        [PXSelector(typeof(CRMendcust.custId),
                    typeof(CRMendcust.name),
                    DescriptionField = typeof(CRMendcust.name))]
        public virtual string UsrEndCust { get; set; }
        public abstract class usrEndCust : PX.Data.BQL.BqlString.Field<usrEndCust> { }
        #endregion

        #region UsrReasonNote

        [PXDBString(200, BqlField = typeof(Standalone.CRLeadStandaloneExt.usrReasonNote))]
        [PXUIField(DisplayName = "Reason Note")]
        public virtual string UsrReasonNote { get; set; }
        public abstract class usrReasonNote : PX.Data.BQL.BqlString.Field<usrReasonNote> { } 

        #endregion

    }
}
