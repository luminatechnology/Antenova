﻿using AntenovaCustomizations;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CR.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntenovaCustomizations.DAC;
using AntenovaCustomizations.Library;
using PX.Common;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR.MassProcess;
using PX.Objects.EP;
using PX.TM;

namespace PX.Objects.CR
{
    public class LeadMaint_Extension : PXGraphExtension<LeadWorkflow, LeadMaint>
    {
        /// <summary> RowSelected CRLead </summary>
        public void _(Events.RowSelected<CRLead> e, PXRowSelected baseMethod)
        {
            baseMethod?.Invoke(e.Cache,e.Args);
            var wgID = (e.Row as CRLead).WorkgroupID;
            var role = new PublicFunc().CheckAcessRoleByWP(PXAccess.GetUserID(), wgID);
            if (!role && wgID.HasValue)
                throw new PXException("You don't have right to read this data.");
        }

        #region Override DAC
        [PXDBInt]
        [PXUIField(DisplayName = "Workgroup")]
        [PXSelector(typeof(SelectFrom<EPCompanyTree>
           .InnerJoin<vSALESPERSONREGIONMAPPING>.On<EPCompanyTree.workGroupID.IsEqual<vSALESPERSONREGIONMAPPING.workGroupID>
               .And<vSALESPERSONREGIONMAPPING.userid.IsEqual<AccessInfo.userID.FromCurrent>>>
           .SearchFor<EPCompanyTree.workGroupID>),
           SubstituteKey = typeof(EPCompanyTree.description))]
        [PXMassUpdatableField]
        [PXMassMergableField]
        [PXMergeAttributes(Method = MergeMethod.Replace)]
        public void _(Events.CacheAttached<CRLead.workgroupID> e) { } 
        #endregion

        public void _(Events.RowPersisting<CRLead> e, PXRowPersisting baseMethod)
        {
            baseMethod?.Invoke(e.Cache, e.Args);
            var row = e.Row as CRLead;
            if (row.Resolution == "OT" && string.IsNullOrEmpty(row.GetExtension<CRLeadExt>().UsrReasonNote))
                throw new PXException("Reason Note can not be empty");
        }

        public void _(Events.FieldDefaulting<CRLead.workgroupID> e, PXFieldDefaulting baseMethod)
        {
           baseMethod?.Invoke(e.Cache,e.Args);
           e.NewValue = SelectFrom<EPCompanyTreeMember>
               .Where<EPCompanyTreeMember.userID.IsEqual<AccessInfo.userID.FromCurrent>>
               .View.Select(Base).RowCast<EPCompanyTreeMember>().FirstOrDefault()?.WorkGroupID;
        }

        public void _(Events.FieldUpdated<CRLeadExt.usrsalesPerson> e)
        {
            var row = e.Row as CRLead;
            if (e.NewValue == null)
                return;
            var record = PXSelectorAttribute.Select<CRLeadExt.usrsalesPerson>(e.Cache, row) as vSALESPERSONREGIONMAPPING;
            e.Cache.SetValueExt<CRLead.workgroupID>(row, record.WorkGroupID ?? null);
        }
    }
}
