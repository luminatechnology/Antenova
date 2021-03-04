using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;

namespace PX.Objects.SO
{
    public class SOOrderEntry_Extension : PXGraphExtension<PX.Objects.SO.SOOrderEntry>
    {
        #region Static Method
        /// <summary>
        /// Specification(Shipment Related Customization).
        /// Get total SO line open quantity and only consider SO order as credit hold.
        /// </summary>
        /// <param name="inventoryID"></param>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public static decimal? CalcQtyCreditHold(int? inventoryID, int? siteID)
        {
            var soLine = SelectFrom<SOLine>.InnerJoin<SOOrder>.On<SOOrder.orderType.IsEqual<SOLine.orderType>
                                                                  .And<SOOrder.orderNbr.IsEqual<SOLine.orderNbr>>>
                                           .Where<SOOrder.creditHold.IsEqual<True>
                                                  .And<SOLine.inventoryID.IsEqual<@P.AsInt>>
                                                       .And<SOLine.siteID.IsEqual<@P.AsInt>>>
                                           .AggregateTo<Sum<SOLine.openQty>>.View.Select(new PXGraph(), inventoryID, siteID);

            return soLine?.TopFirst?.OpenQty;
        }
        #endregion
    }
}
