<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FacebookLogin.ascx.cs" Inherits="DreamItAliveWebsite.UserControls.FacebookLogin" %>

<asp:ImageButton ID="imgBtnFacebookConnect" runat="server" Text="Facebook Login" onclick="imgBtnFacebookConnect_Click" />   

<%--
<p>
    <fb:login-button></fb:login-button>
</p>
<div id="fb-root">
</div>
<script src="http://connect.facebook.net/en_US/all.js"></script>
<script>
    FB.init({ appId: '<%: Facebook.FacebookApplication.Current.AppId %>', status: true, cookie: true, xfbml: true });
    FB.Event.subscribe('auth.sessionChange', function (response) {
        if (response.session) {
            // A user has logged in, and a new cookie has been saved
            window.location.reload();
        } else {
            // The user has logged out, and the cookie has been cleared
        }
    });
</script>                
--%>
