<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="SurveyForm.aspx.cs" Inherits="KhaoSatTrucTuyen.Bieumau" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container {
            width: 50%;
            height: 100%;
            margin: 0 auto;
            background-color: #fff;
        }
    </style>
    <link href="assets/css/survey.css" rel="stylesheet" />
    <script src="assets/js/survey.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form method="post" runat="server">
        <div class="container">
            <div class="nav">
                <nav class="ag-nav_box">
                    <ul class="ag-nav_list">
                        <li class="ag-nav_item">
                            <a href="#js-ag-primary-block" class="js-ag-nav_link js-ag-active">Câu hỏi</a>
                        </li>
                        <li class="ag-nav_item">
                            <a href="#" class="js-ag-nav_link" runat="server">
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Always">
                                     <ContentTemplate>
                                         <label id="sl_khaosat" runat="server"></label>
                                     </ContentTemplate>
                                </asp:UpdatePanel>
                            </a>
                        </li>
                    </ul>
                </nav>
            </div>
            <div class="title-survey">
                <input id="title_survey" name="title_survey" type="text" class="input-survey" value="Mẫu không có tiêu đề" autocomplete="off">
            </div>
            <div class="question-survey">
                <input id="sl_question" name="sl_question" type="hidden" class="sl_question" value="1" />
                <div class="item-question" data-id="1">
                    <div class="format-question">
                        <div class="left-question">
                            <input name="title_question_1" type="text" class="title-question" placeholder="Câu hỏi" autocomplete="off">
                        </div>
                        <div class="right-question">
                            <select id="Kind_of_question_1" name="Kind_of_question_1" class="Kind-of-question">
                                <%= list_loaicauhoi %>
                            </select>
                        </div>
                    </div>
                    <div class="answer">
                        <input type="hidden" class="sl_answer" name="sl_answer1" value="1" />
                        <input disabled name="title-answer" type="text" class="title-answer" placeholder="Văn bản trả lời dài" autocomplete="off">
                    </div>
                    <div class="action-question">
                        <i class="fas fa-trash-alt"></i>
                    </div>
                </div>
            </div>
            <div id="add-question">
                <i class="fas fa-plus-circle"></i>
                <button type="submit" id="save-survey" name="save-survey" value="save-survey">Lưu</button>
            </div>
        </div>
    </form>
</asp:Content>
