<%@ Page Language="C#" AutoEventWireup="true" CodeFile="runs.aspx.cs" Inherits="runs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Spire Analytics | Runs</title>
    <link rel="stylesheet" href="styles.css">
    <style type="text/css">
        #form1 {
            width: 1200px;
        }

        body {
            margin-left: 10.5em;
        }
    </style>
</head>
<body>
    <h1>Viewing all runs</h1>
    <br />
    <h2><a href="default.html">Home</a></h2>
    <br />

    <form id="form1" runat="server">
        <asp:GridView ID="GoldGridView" runat="server" AutoGenerateColumns="False" DataSourceID="GoldDB2" Height="118px" Width="1766px" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Gold" HeaderText="Gold" SortExpression="Gold">
                    <ControlStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="Relics" HeaderText="Relics" SortExpression="Relics">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="Score" HeaderText="Score" SortExpression="Score">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="FloorReached" HeaderText="FloorReached" SortExpression="FloorReached">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="AscensionLevel" HeaderText="Ascension" SortExpression="AscensionLevel">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>
        <asp:SqlDataSource ID="GoldDB2" runat="server" ConnectionString="<%$ ConnectionStrings:GoldConnectionString2 %>" ProviderName="<%$ ConnectionStrings:GoldConnectionString2.ProviderName %>" SelectCommand="SELECT [Gold], [Relics], [Score], [FloorReached], [AscensionLevel] FROM [tblRuns] ORDER BY [Gold] DESC"></asp:SqlDataSource>
    </form>

</body>
</html>
