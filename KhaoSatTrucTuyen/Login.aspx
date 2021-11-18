<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="KhaoSatTrucTuyen.Dangnhap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng nhập hệ thống khảo sát trực tuyến</title>
    <link href="assets/css/DangNhap.css" rel="stylesheet" />
</head>
<body>
     <div class="container">
		<div class="screen">
			<div class="screen__content">
				<form class="login" method="post" runat="server">
					<div class="login__field">
						<i class="login__icon fas fa-user"></i>
						<asp:TextBox runat="server" ID="username" CssClass="login__input" placeholder="Username"></asp:TextBox>
					</div>
					<div class="login__field">
						<i class="login__icon fas fa-lock"></i>
						<asp:TextBox runat="server" ID="password" CssClass="login__input" TextMode="Password" placeholder="Password"></asp:TextBox>
					</div>
					<asp:label id="errorLogin" runat="server"/>
					<asp:Button ID="login" Text="Log In Now" CssClass="button login__submit" runat="server" OnClick="btnLogin_Click"/>
				</form>
				<div class="social-login">
					<h3>log in via</h3>
				</div>
			</div>
			<div class="screen__background">
				<span class="screen__background__shape screen__background__shape4"></span>
				<span class="screen__background__shape screen__background__shape3"></span>		
				<span class="screen__background__shape screen__background__shape2"></span>
				<span class="screen__background__shape screen__background__shape1"></span>
			</div>		
		</div>
	</div>
</body>
</html>
