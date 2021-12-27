<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="KhaoSatTrucTuyen.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="center">
            <asp:Label ID="user" runat="server" Text=""></asp:Label>
        </div>
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                 <asp:Button runat="server" ID="btnChart" OnClick="btnChart_Click" Text="Biểu đồ tròn" />
                 <asp:Chart ID="chart1" runat="server" Height="444px" Width="580px">
                    <Series>
                        <asp:Series Name="ChartBDT" ChartType="Pie"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
    </form>
</asp:Content>
