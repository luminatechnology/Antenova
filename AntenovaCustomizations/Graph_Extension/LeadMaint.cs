using AntenovaCustomizations;
using PX.Data;
using PX.Objects.CR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Objects.CR
{
    public class LeadMaint_Extension : PXGraphExtension<LeadMaint>
    {
        //[PXDBString(10,BqlField = typeof(Contact.source))]
        //[PXUIField(DisplayName = "Source")]
        //[PXSelector(typeof(CRMSource.sourceID),
        //            typeof(CRMSource.descrption))]
        //[PXMergeAttributes(Method = MergeMethod.Replace)]
        //public void _(Events.CacheAttached<CRLead.source> e) { }

        public void _(Events.RowPersisting<CRLead> e, PXRowPersisting baseMethod)
        {
            baseMethod?.Invoke(e.Cache, e.Args);
            var row = e.Row as CRLead;
            if (row.Resolution == "OT" && string.IsNullOrEmpty(row.GetExtension<CRLeadExt>().UsrReasonNote))
                throw new PXException("Reason Note can not be empty");
        }
    }
}
