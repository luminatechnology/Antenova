<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="SO800000.aspx.cs" Inherits="Page_SO800000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="SalesForecastANTENOVA.SalesForecastEntry"
        PrimaryView="SalesForcast">
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
	<px:PXGrid PreservePageIndex="True" FastFilterFields="SalesPersonID,CustomerID,CountryID,SalesRegion,EndCustomer,InventoryID,ItemClassID,DesignSalesID" PageIndex="100" PageSize="100" AdjustPageSize="None" AllowPaging="True" AllowSearch="True" SyncPosition="True" ID="grid" runat="server" DataSourceID="ds" Width="100%" SkinID="Primary" AllowAutoHide="false">
		<Levels>
			<px:PXGridLevel DataMember="SalesForcast">
			    <Columns>
				<px:PXGridColumn DataField="SalesPersonID" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn CommitChanges="True" DataField="CustomerID" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="CustomerID_Customer_acctName" Width="220" ></px:PXGridColumn>
				<px:PXGridColumn DataField="CountryID" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SalesRegion" Width="180" ></px:PXGridColumn>
				<px:PXGridColumn DataField="EndCustomer" Width="180" ></px:PXGridColumn>
				<px:PXGridColumn DataField="DesignSalesID" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="OpportunityID" Width="120" />
				<px:PXGridColumn Type="CheckBox" DataField="IsTotal" Width="60" ></px:PXGridColumn>
				<px:PXGridColumn Type="CheckBox" DataField="IsSplit" Width="60" ></px:PXGridColumn>
				<px:PXGridColumn DataField="PercentSplit" Width="100" ></px:PXGridColumn>
				<px:PXGridColumn CommitChanges="True" DataField="InventoryID" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="InventoryID_description" Width="280" ></px:PXGridColumn>
				<px:PXGridColumn DataField="ItemClassID" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="ItemClassID_description" Width="280" ></px:PXGridColumn>
				<px:PXGridColumn DataField="FinPeriodID" Width="72" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Date" Width="90" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Qty" Width="100" ></px:PXGridColumn>
				<px:PXGridColumn DataField="UnitPrice" Width="100" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Amount" Width="100" ></px:PXGridColumn></Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True"></AutoSize>
		<ActionBar PagerVisible="Bottom" >
                   <PagerSettings Mode="NumericCompact" ></PagerSettings>
		</ActionBar>
		<Mode InitNewRow="True" AllowUpload="True" ></Mode></px:PXGrid>
</asp:Content>