using AntenovaCustomizations;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Objects.CR.Standalone
{
    public class CRLeadStandaloneExt : PXCacheExtension<PX.Objects.CR.Standalone.CRLead>
    {
        [PXDBString(20, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "End Customer")]
        public virtual string UsrEndCust { get; set; }
        public abstract class usrEndCust : PX.Data.BQL.BqlString.Field<usrEndCust> { }

        [PXDBString(200, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Reason Note")]
        public virtual string UsrReasonNote { get; set; }
        public abstract class usrReasonNote : PX.Data.BQL.BqlString.Field<usrReasonNote> { }
    }
}
