<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="DoSurvey.aspx.cs" Inherits="KhaoSatTrucTuyen.DoSurvey" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container{
            display: flex;
            justify-content: center;
            justify-items: center;
        }
        input{
            outline: none;
        }
        #list-survey-join{
            width: 400px;
            border-radius: 7px;
            background: #fff;
            height: 100%;
        }
        #fill-survey{
            width:calc( 100% - 600px);
            background: #fff;
            margin-left: 50px;
            height: 100%;
            border-top: 8px solid rgb(103, 58, 183); 
            border-radius: 8px ;
        }
        #list-survey-join h2{
            padding: 10px 10px;
            text-align: center;
            border-bottom: 1px solid #000;
            color: #000;
        }
        .item-survey{
            display: flex;
            justify-content: space-between;
            justify-items: center;
            padding: 7px 10px;
            border-bottom: 1px solid #ddd
        }
        .item-survey:hover{
            background: #a29bfe;
            cursor: pointer;
        }
        .item-survey a{
            width:100%;
            display: block;
            text-decoration: none;
            color: #000;
        }
        .title-survey { 
            padding: 20px 20px ;
            border-bottom: 1px solid #ddd;
        }
        .title-survey span{
            display: block;
            font-size: 30px;
            height: 50px;
        }


        .content-answer {
            background-size: 0 2px, 100% 1px;
            border: none;
            width: 100%;
            background-image: linear-gradient(#707cd2, #707cd2), linear-gradient(rgba(120, 130, 140, .13), rgba(120, 130, 140, .13));
            background-color: rgba(0, 0, 0, 0);
            background-position: center bottom, center calc(99%);
            background-repeat: no-repeat;
            height: 30px;
        }
        .question-box ,.action{
            padding: 20px 20px;
        }
        .item-question .question-name{
            margin-bottom: 20px;
            font-size: 18px;
        }
        .content-answer:focus {
            color: #707cd2;
            background-size: 100% 2px, 100% 1px;
            transition-duration: .3s;
        }
        .boundary{
            background-color:#ede7f6;
            height: 7px;
            margin: 20px 0;
        }
        .radio-answer{
            background-size: 0 2px, 100% 1px;
            border: none;
            width: 100%;
            background-image: linear-gradient(#707cd2, #707cd2), linear-gradient(rgba(120, 130, 140, .13), rgba(120, 130, 140, .13));
            background-color: rgba(0, 0, 0, 0);
            background-position: center bottom, center calc(99%);
            background-repeat: no-repeat;
            height: 30px;
        }
        .item-anwer{
            display: flex;
            align-items: center;
            padding: 7px 0;
        }
        .item-anwer input{
            height: 20px;
            width: 20px;
        }
        .item-anwer label{
            display: inline-block;
            margin-left: 0.75em;
        }
        .select-answer{
            height: 35px;
            width: 30%;
            padding: 5px;
            border-radius: 3px;
        }
        .select-answer:focus-visible{
            outline:none;
        }
        #send{
            display: block;
            margin: 20px 0;
            padding: 5px 7px;
            border-radius: 3px;
            background-color: #0984e3;
            border: none;
            color: #fff;
            font-weight: bold;
            font-size: 16px;
            float: right;
        }
        .active-survey{
            background: #a29bfe;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form method="post">
        <div class="container">
        <div id="list-survey-join">
            <h2>Danh sách biểu mẫu tham gia</h2>
            <% foreach (System.Data.DataRow dr in dt_list_phieu.Rows){ %>
                <% string atv = ""; %>
                <% if (Equals(dr["ma_phieukhaosat"], ma_phieu_khaosat)) { atv = "active-survey"; } %>
                <div class='item-survey <%= atv %>'>
                    <a href='DoSurvey.aspx?id=<%= dr["ma_phieukhaosat"] %>'><%= dr["ten_khaosat"] %></a>
                    <% if (Equals(dr["trangthai"], 1)){ %>
                    <span class="status"><i class="fas fa-check"></i></span>
                    <%} %>
                </div>
            <%} %>
        </div>
        <div id="fill-survey">
            <% if(ma_phieu_khaosat != ""){ %>
                <div class="title-survey">
                    <span><%= title_survey %></span>
                </div>
                <div class="question-box">
                    <%int cauhoi = 1; %>
                    <% foreach(System.Data.DataRow dr_cauhoi in ds_cauhoi.Rows){ %>
                        <div class="item-question">
                            <div class="question-name"><%= dr_cauhoi["noidung_cauhoi"] %></div>
                            <div class="answer-box">
                                <% if (Equals(dr_cauhoi["ma_loaicauhoi"], "menuthaxuong")){ %><select name="<%= dr_cauhoi["ma_cauhoi"]%>" class="select-answer"><option value="">Chọn</option> <%}  %>
                                <%int dapan = 0; %>
                                <% foreach(System.Data.DataRow drdapan in ds_dapan.Rows){ %>
                                    <% if(Equals(drdapan["ma_cauhoi"],dr_cauhoi["ma_cauhoi"])){ %>
                                        <% string mach = drdapan["ma_cauhoi"].ToString(); %>
                                        <% string mada = drdapan["ma_dapan"].ToString(); %>
                                        <% switch(dr_cauhoi["ma_loaicauhoi"]){ %>
                                            <% case "doan": %>
                                                <div>
                                                    <input name="<%= mada %>" type="text" value="<% if (chitiet_conlai.Count > 0) { Response.Write(chitiet_conlai[mach]); } %>" class="content-answer" placeholder="Câu trả lời của bạn" autocomplete="off">
                                                    <input type="hidden" name="<%= mach %>" value="<%= mada %>" />
                                                </div>
                                            <% break; %>
                                            <% case "tracnhiem": %>
                                                <% string checkRadio = ""; %>
                                                <% if (chitiet_conlai.Count > 0 && Equals(mada,chitiet_conlai[mach])){ checkRadio = "checked"; } %>
                                                <div class="item-anwer">
                                                    <input id="<%= cauhoi+"_"+dapan %>" name="<%= mach %>" type="radio" class="radio-answer" value="<%= mada%>" <%= checkRadio %>>
                                                    <label for="<%= cauhoi+"_"+dapan %>"><%= drdapan["tieude_dapan"]%></label>
                                                </div>
                                            <% break; %>
                                            <% case "hopkiem": %>
                                                <% string check = ""; %>
                                                <% if (termsList.Count > 0 && ((IList)termsList).Contains(mada)){ check = "checked"; } %>
                                                <div class="item-anwer">
                                                    <input id="<%= cauhoi+"_"+dapan %>" name="<%= mach%>" type="checkbox" class="checkbox-answer" value="<%= mada %>" <%= check %> />
                                                    <label for="<%= cauhoi+"_"+dapan %>"><%= drdapan["tieude_dapan"] %></label>
                                                </div>
                                            <% break; %>
                                            <% case "menuthaxuong": %>
                                                <% string selected = ""; %>
                                                <% if (chitiet_conlai.Count > 0 && Equals(mada,chitiet_conlai[mach])){ selected = "selected"; } %>
                                                <option <%=selected %> value="<%= mada %>"><%= drdapan["tieude_dapan"]%></option>
                                            <% break; %>
                                        <%} %>
                                        <%dapan++; %>
                                    <%} %>
                                <%} %>
                                <% if (Equals(dr_cauhoi["ma_loaicauhoi"], "menuthaxuong")){ %></select> <%}  %>
                            </div>
                            <p class="boundary"></p>
                        </div>  
                    <%cauhoi++; %>
                    <%} %>
                </div>
                <div class="action">
                    <% if(Equals(status_survey,"0")){ %>
                    <button id="send" name="send" type="submit" value="send-survey-test">Gửi</button>
                    <%} %>
                </div>
            <%} %>
            <%else{ %>
                <div id="khongcodulieu">
                    <span>Chưa chọn phiếu khảo sát</span>
                </div>
            <%} %>
        </div>
    </div>
    </form>
</asp:Content>
