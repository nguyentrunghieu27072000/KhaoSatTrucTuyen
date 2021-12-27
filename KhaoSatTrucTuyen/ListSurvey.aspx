<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ListSurvey.aspx.cs" Inherits="KhaoSatTrucTuyen.ListSurvey" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container{
            display:flex;
            justify-content: center;
            justify-items: center;
        }
        .send-open-modal{
            position: absolute;
            cursor: pointer;
            padding: 5px 7px;
            border-radius: 3px;
            background-color: #0984e3;
            border: none;
            color: #fff;
            font-weight: bold;
            font-size: 16px;
            right: 2%;
            top: 25%;
        }
        .item-user{
            display: flex;
            justify-content:space-between;
            margin-top: 7px;
        }
        .send{
            cursor: pointer;
            padding: 5px 7px;
            border-radius: 3px;
            background-color: #e7f3ff;
            border: none;
            color: #1877f2;
        }
        .sent{
            background-color:#e4e6eb;
            color:#bcc0c4;
            padding: 5px 7px;
            border-radius: 3px;
            border: none;
            cursor:no-drop;
        }
        #not-save{
            display: block;
            margin: 20px 0;
            padding: 5px 7px;
            border-radius: 3px;
            background-color: #e4e6eb;
            border: none;
            color: #bcc0c4;
            font-weight: bold;
            font-size: 16px;
            float: right;
            cursor:no-drop;
        }
        #khongcodulieu{
            text-align: center;
            background: #a29bfe;
            padding: 15px 5px;
            font-weight: bold;
            text-transform: uppercase;
            color: #fff;
        }
    </style>
    <link href="assets/css/survey.css" rel="stylesheet" />
    <script src="assets/js/survey.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form method="post" action="SurveyForm.aspx">
        <div class="container">
            <div id="list-survey">
                <h2>Danh sách mẫu khảo sát</h2>
                <%= list_servey %>
            </div>
            <div id="survey-active">
                <% if(ma_khaosat_active != ""){ %>
                <div class="nav">
                    <nav class="ag-nav_box">
                        <ul class="ag-nav_list">
                            <li class="ag-nav_item">
                                <a href="#" class="js-ag-nav_link js-ag-active">Câu hỏi</a>
                            </li>
                            <li class="ag-nav_item">
                                <a href="ketqua.aspx?id=<%=ma_khaosat_active %>" class="js-ag-nav_link js-ag-active">Câu Trả lời</a>
                            </li>
                            <li class="ag-nav_item">
                                <a target="_blank" class="js-ag-nav_link" href="ExportExcel.aspx?id=<%=ma_khaosat_active %>">Excel</a>
                            </li>
                        </ul>
                    </nav>
                    <button type="button" class="send-open-modal" id="openmodale">👋 Gửi</button>
                </div>
                <div class="title-survey">
                    <input id="title_survey" name="title_survey" type="text" class="input-survey" value="<%= title_servey %>" autocomplete="off">
                </div>
                <div class="question-survey">
                    <input id="sl_question" name="sl_question" type="hidden" class="sl_question" value="<%= count_question %>" />
                    <%int cauhoi = 1; %>
                    <% foreach (System.Data.DataRow daRow in ds_cauhoi.Rows){ %>
                       <div class="item-question" data-id="<% Response.Write(cauhoi); %>">
                            <div class="format-question">
                                <div class="left-question">
                                    <input name="title_question_<% Response.Write(cauhoi); %>" type="text" class="title-question" value="<% Response.Write(daRow["noidung_cauhoi"]); %>" placeholder="Câu hỏi" autocomplete="off">
                                </div>
                                <div class="right-question">
                                    <select name="Kind_of_question_<% Response.Write(cauhoi); %>" class="Kind-of-question">
                                        <% foreach(System.Data.DataRow drCauhoi in list_loaicauhoi.Rows){ %>
                                            <% if (Equals(drCauhoi["ma_loaicauhoi"],daRow["ma_loaicauhoi"])){ %>
                                                <option value='<% Response.Write(drCauhoi["ma_loaicauhoi"]); %>' selected><% Response.Write(drCauhoi["ten_loaicauhoi"]); %></option>
                                            <%continue; }%> 
                                            <option value='<% Response.Write(drCauhoi["ma_loaicauhoi"]); %>'><% Response.Write(drCauhoi["ten_loaicauhoi"]); %></option>
                                        <%}%>
                                    </select>
                                </div>
                            </div>
                            <div class="answer">
                                    <%int dapan = 0; %>
                                    <% foreach(System.Data.DataRow drdapan in ds_dapan.Rows){ %>
                                        <% if(Equals(drdapan["ma_cauhoi"],daRow["ma_cauhoi"])){ %>
                                            <% switch(daRow["ma_loaicauhoi"]){ %>
                                                <% case "doan": %>
                                                    <input disabled name="title-answer" type="text" class="title-answer" placeholder="Văn bản trả lời dài" autocomplete="off">
                                                <% break; %>
                                                <% case "tracnhiem": %>
                                                    <div class="item-radio-answer item-answer">
                                                        <input type="radio" disabled>
                                                        <input name="<% Response.Write(daRow["ma_loaicauhoi"] + "_" + cauhoi + "_" + ((int)dapan + 1)); %>" value="<% Response.Write(drdapan["tieude_dapan"]); %>" type="text" class="title-answer" placeholder="Tùy chọn" autocomplete="off">
                                                        <i class="fas fa-times fa-xs"></i>
                                                    </div>
                                                <% break; %>
                                                <% case "hopkiem": %>
                                                    <div class="item-checkbox-answer item-answer">
                                                        <input type="checkbox" disabled>
                                                        <input name="<% Response.Write(daRow["ma_loaicauhoi"] + "_" + cauhoi + "_" + ((int)dapan + 1)); %>" value="<% Response.Write(drdapan["tieude_dapan"]); %>" type="text" class="title-answer" placeholder="Tùy chọn" autocomplete="off">
                                                        <i class="fas fa-times fa-xs"></i>
                                                    </div>
                                                <% break; %>
                                                <% case "menuthaxuong": %>
                                                    <div class="item-select-answer item-answer">
                                                        <i class="far fa-caret-square-down"></i>
                                                        <input name="<% Response.Write(daRow["ma_loaicauhoi"] + "_" + cauhoi + "_" + ((int)dapan + 1)); %>" value="<% Response.Write(drdapan["tieude_dapan"]); %>" type="text" class="title-answer" placeholder="Tùy chọn" autocomplete="off">
                                                        <i class="fas fa-times fa-xs"></i>  
                                                    </div>
                                                <% break; %>
                                            <%} %>
                                            <%dapan++; %>
                                        <%} %>
                                    <%} %>
                                    <%if(!Equals(daRow["ma_loaicauhoi"],"doan")){ %>
                                        <div class="add-item-answer">
                                            <i class="fas fa-plus fa-xs"></i>
                                            <span>Thêm tùy chọn</span>
                                        </div>
                                    <%} %>
                                    <input type="hidden" class="sl_answer" name="sl_answer<% Response.Write(cauhoi); %>" value="<% Response.Write(dapan);%>" />
                                </div>
                            <div class="action-question">
                                <i class="fas fa-trash-alt"></i>
                            </div>
                        </div>
                    <%cauhoi++; %>
                    <%} %>
                </div>
                <div id="add-question">
                    <i class="fas fa-plus-circle fa-lg"></i>
                    <%if(Equals(check_send,0)){ %>
                        <button type="submit" id="save-survey" name="save-survey" value="<% Response.Write(ma_khaosat_active); %>">Lưu</button>
                    <%} %>
                    <%else{ %>
                        <button type="button" id="not-save" title="Không thể cập nhật mẫu khi đã gửi">Lưu</button>
                    <%} %>
                </div>
                <%} %>
                <%else{ %>
                    <div id="khongcodulieu">
                        <span>Chưa chọn mẫu khảo sát</span>
                    </div>
                <%} %>
            </div>
        </div>
    </form>
    <form action="ListSurvey.aspx" runat="server">
        <!-- Modal -->
            <div class="modale" aria-hidden="true">
              <div class="modal-dialog">
                <div class="modal-header">
                  <h2>Gửi biểu mẫu</h2>
                  <a href="#" class="btn-close closemodale" aria-hidden="true">&times;</a>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </div>
                <div class="modal-body">
                    <div class="modal-send">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <asp:ListView ID="Lv_User" runat="server">
                                    <ItemTemplate> 
                                        <div class="item-user">
                                            <span style="font-size: 20px;"><%# Eval("ten_taikhoan")%></span>  
                                            <div>
                                                <span runat="server" Visible='<%# Eval("ma_phieukhaosat").ToString() == "" %>'>
                                                    <asp:Button CommandArgument='<%# Eval("id_taikhoan") %>' ID="send" Text="Gửi" CssClass="send" OnClick="send_click" runat="server"/>
                                                </span>
                                                <span runat="server" Visible='<%# Eval("ma_phieukhaosat").ToString() != "" %>'>
                                                    <button class="sent">Đã gửi</button>
                                                </span>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <img src="assets/images/loadding.gif" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>
                <div class="modal-footer">
                    <span style="color:red">* Chú ý: Sau khi gửi biểu mẫu bạn sẽ không thể sửa lại mẫu</span>
                </div>
              </div>
            </div>
        <!-- /Modal -->
    </form>
</asp:Content>