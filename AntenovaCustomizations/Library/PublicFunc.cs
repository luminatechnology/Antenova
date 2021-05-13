using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntenovaCustomizations.DAC;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.EP;
using PX.TM;

namespace AntenovaCustomizations.Library
{
    public class PublicFunc
    {
        /// <summary> Work Group CRM Name Constant </summary>
        public class WorkGroupCrm : PX.Data.BQL.BqlString.Constant<WorkGroupCrm>
        {
            public WorkGroupCrm() : base("(CRM)") { }
        }

        public class CRMCompanyTree : EPCompanyTree
        {

        }

        /// <summary> Get WorkGroup and Employee Info By Sales Person </summary>
        public virtual PXResultset<vSALESPERSONREGIONMAPPING> GetWGInfoByEmployeeFromSP(int _salesPersonID)
        {
            return SelectFrom<vSALESPERSONREGIONMAPPING>
                   .Where<vSALESPERSONREGIONMAPPING.salespersonID.IsEqual<P.AsInt>>.View.Select(new PXGraph(), _salesPersonID);
        }

        /// <summary> Check AccessRole </summary>
        public virtual bool CheckAcessRoleByWP(Guid? _userID ,int? _workgroup)
        {
            var gpRoles = SelectFrom<EPCompanyTreeMember>
                .Where<EPCompanyTreeMember.userID.IsEqual<P.AsGuid>>
                .View.Select(new PXGraph(), _userID).RowCast<EPCompanyTreeMember>()
                .Select(x => x.WorkGroupID).Distinct();
            return gpRoles.Contains(_workgroup);
        }
    }
}
