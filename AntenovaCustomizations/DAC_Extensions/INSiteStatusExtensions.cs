using System;
using PX.Data;
using static PX.Objects.SO.SOOrderEntry_Extension;

namespace PX.Objects.IN
{
    public class INSiteStatusExt : PXCacheExtension<PX.Objects.IN.INSiteStatus>
    {
        #region UsrQtyCreditHold
        [PXQuantity]
        [PXUIField(DisplayName = "Qty Credit Hold", Enabled = false)]
        public virtual decimal? UsrQtyCreditHold 
        {
            get
            {
                return CalcQtyCreditHold(Base.InventoryID, Base.SiteID);
            }
            set { } 
        }
        public abstract class usrQtyCreditHold : PX.Data.BQL.BqlDecimal.Field<usrQtyCreditHold> { }
        #endregion
    }
}
