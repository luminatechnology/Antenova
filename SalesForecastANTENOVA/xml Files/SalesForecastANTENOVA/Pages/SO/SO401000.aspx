<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="SO401000.aspx.cs" Inherits="Page_SO401000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="SalesForecastANTENOVA.SalesFcstInquiryEntry"
        PrimaryView="SOFcstInq">
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
	<px:PXGrid runat="server" SyncPosition="True" SkinID="Inquire" Width="100%" ID="grid" AllowAutoHide="false" DataSourceID="ds" AllowPaging="True" AdjustPageSize="Auto">
		<AutoSize Enabled="True" Container="Window" ></AutoSize>
		<Mode AllowAddNew="True" AllowUpdate="False" ></Mode>
		<Levels>
			<px:PXGridLevel DataMember="SOFcstInq">
				<Columns>
					<px:PXGridColumn DataField="Numbering" Width="70" ></px:PXGridColumn>
					<px:PXGridColumn DataField="LineNbr" Width="70" ></px:PXGridColumn>
					<px:PXGridColumn DataField="Status" Width="70" ></px:PXGridColumn>
					<px:PXGridColumn DataField="OrderNbr" Width="140" ></px:PXGridColumn>
					<px:PXGridColumn DataField="OrderDate" Width="90" ></px:PXGridColumn>
					<px:PXGridColumn DataField="CustomerOrderNbr" Width="180" ></px:PXGridColumn>
					<px:PXGridColumn DataField="CustomerID" Width="140" ></px:PXGridColumn>
					<px:PXGridColumn DataField="CustomerID_BAccountR_acctName" Width="220" ></px:PXGridColumn>
					<px:PXGridColumn DataField="RequestDate" Width="90" ></px:PXGridColumn>
					<px:PXGridColumn DataField="ShipDate" Width="90" ></px:PXGridColumn>
					<px:PXGridColumn DataField="InventoryID" Width="70" ></px:PXGridColumn>
					<px:PXGridColumn DataField="InventoryID_description" Width="280" ></px:PXGridColumn>
					<px:PXGridColumn DataField="OrderQty" Width="100" ></px:PXGridColumn>
					<px:PXGridColumn DataField="OpenQty" Width="100" ></px:PXGridColumn>
					<px:PXGridColumn DataField="FinPeriodID" Width="72" ></px:PXGridColumn>
					<px:PXGridColumn DataField="Qty" Width="100" ></px:PXGridColumn>
					<px:PXGridColumn DataField="UnitPrice" Width="100" ></px:PXGridColumn>
					<px:PXGridColumn DataField="Amount" Width="100" ></px:PXGridColumn>
					<px:PXGridColumn DataField="CountryID" Width="70" ></px:PXGridColumn>
					<px:PXGridColumn DataField="SalesPersonID" Width="140" ></px:PXGridColumn>
					<px:PXGridColumn DataField="EndCustomer" Width="180" ></px:PXGridColumn>
					<px:PXGridColumn DataField="SalesRegion" Width="180" ></px:PXGridColumn>
					<px:PXGridColumn Label="Product Category" DataField="ItemClassID_description" Width="280" ></px:PXGridColumn>
					<px:PXGridColumn DataField="TermsID" Width="120" ></px:PXGridColumn>
					<px:PXGridColumn DataField="PercentSplit" Width="100" ></px:PXGridColumn>
					<px:PXGridColumn Type="CheckBox" DataField="IsTotal" Width="60" ></px:PXGridColumn>
					<px:PXGridColumn Type="CheckBox" DataField="IsSplit" Width="60" ></px:PXGridColumn></Columns></px:PXGridLevel></Levels></px:PXGrid></asp:Content>