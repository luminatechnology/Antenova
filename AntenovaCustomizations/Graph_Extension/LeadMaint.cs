using AntenovaCustomizations;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CR.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Objects.CR
{
    public class LeadMaint_Extension : PXGraphExtension<LeadWorkflow, LeadMaint>
    {
        public void _(Events.RowPersisting<CRLead> e, PXRowPersisting baseMethod)
        {
            baseMethod?.Invoke(e.Cache, e.Args);
            var row = e.Row as CRLead;
            if (row.Resolution == "OT" && string.IsNullOrEmpty(row.GetExtension<CRLeadExt>().UsrReasonNote))
                throw new PXException("Reason Note can not be empty");
        }
    }
}
