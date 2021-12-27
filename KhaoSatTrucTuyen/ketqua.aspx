<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ketqua.aspx.cs" Inherits="KhaoSatTrucTuyen.ketqua" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table {
          border-collapse: collapse;
        }
        th, td{
            padding: 3px 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table border="1">
            <thead>
                <tr>
                    <th>Tên tài khoản</th> 
                    <% foreach (System.Data.DataRow dataRowCauHoi in Cauhoi.Rows) { %>
                          <th><% Response.Write(dataRowCauHoi["noidung_cauhoi"].ToString()); %></th>
                    <% } %>
                </tr>
            </thead>
             <tbody>
                 <% foreach (System.Data.DataRow dataRowUser in UserSurvey.Rows) { %>
                       <tr>
                           <td><% Response.Write(dataRowUser["ten_taikhoan"].ToString()); %></td>
                       </tr>
                 <% } %>
             </tbody>
        </table>
    </div>
</asp:Content>
