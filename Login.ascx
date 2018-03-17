<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="DreamItAliveWebsite.UserControls.Login" %>
<%@ Register Src="~/UserControls/FacebookLogin.ascx" TagName="FacebookLogin" TagPrefix="dia" %>
<style type="text/css">
    td.LeftPadded{padding-left:100px;}
</style>
<table cellpadding="0" cellspacing="0" style="width: 100%;text-align:center;">
    <tr>
        <td>
            WELCOME BACK!
        </td>
    </tr>
    <tr>
        <td style="padding-top:0px;padding-bottom:8px;">
            <h1>Log In to DreamItAlive</h1>
        </td>
    </tr>
    <tr>
        <td style="padding-bottom: 8px;padding-top:8px;text-align:center;margin-left:auto;margin-right:auto;">
                        <dia:FacebookLogin runat="server" ID="facebookLogin" />
        </td>
    </tr>
    <tr>
        <td >
            <asp:UpdatePanel runat="server" ID="updatePnlConfirmEmail" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button runat="server" ID="btnHidden" CausesValidation="false" OnClientClick="return false;"
                        Style="display: none;" />
                    <ajaxToolkit:ModalPopupExtender ID="mpeConfirmEmail" runat="server" PopupControlID="pnlEmailVerified"
                        BackgroundCssClass="modalBackground" TargetControlID="btnHidden"
                         />
                    <asp:Panel ID="pnlEmailVerified" runat="server" BorderStyle="Solid" BorderColor="Black"
                        BorderWidth="2" HorizontalAlign="Left" Width='400' CssClass="modalPopup" Style="display: none;">
                        <div style="text-align: left; color: Black; font-size: 13px; padding: 20px">
                            <b>Thank you for verifying your email!</b>
                            <br />
                            <br />
                            If you'd like to update how often you receive emails from DreamItAlive, please
                            <asp:HyperLink runat="server" ID="hlnkMemberCommunicationSettingsMgmt" Text="click here" Style="text-decoration:underline;"
                                 />
                        </div>
                        <table cellpadding="0" cellspacing="0" style="margin-left: auto; margin-right: auto;">
                            <tr>
                                <td style="font-weight:bold;padding-bottom:6px;">
                                    Please also take a moment to Like us on Facebook!
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="fb-like-box" data-href="http://www.facebook.com/dreamitalive" data-width="350"
                                        data-show-faces="true" data-border-color="#efefef" data-stream="false" data-header="false">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 25px; padding-bottom: 25px;padding-left:110px;">
                                    <asp:Button ID="btnOk" runat="server" Text="Continue" CssClass="DIAButtonDefault" Width="125px" OnClick="btnOk_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

        </td>
    </tr>
    <%--<tr>
        <td style="border-top:1px solid #a1a1a1;">&nbsp;</td>
    </tr>--%>
    <tr style="">
        <td style="text-align:center;padding-top:6px;">
            <%--<div class="centeredTextBetweenLine">Or</div> &nbsp;--%>            
            <hr />
        </td>
    </tr>
    <tr style="">
        <td style="padding-bottom:18px;padding-top:18px;">
            <asp:Login ID="login" runat="server" BorderPadding="0" BorderStyle="none" OnLoggedIn="login_LoggedIn" OnAuthenticate="login_Authenticate"
                FailureAction="Refresh" FailureText="The User Name or Password you entered is not correct. Please reenter your User Name and Password or click 'Forgot your login info?' to verify your identity and regain access to your account."
                Width="100%" BorderWidth="0">
                <TextBoxStyle Font-Size="0.8em" />
                <LayoutTemplate>
                    <div class="container">
                        <div class="row">
                            <div class="col-md-6">
                             <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-envelope fa" aria-hidden="true"></i></span>
                                    <asp:TextBox ID="UserName" runat="server" class="form-control" ValidationGroup="loginHeader" CssClass="inputs" placeholder="Email or User Name" 
                                Font-Bold="true" style="" />
                                <span>
                                <asp:RequiredFieldValidator CssClass="validatorNotification" ID="UserNameRequired"
                                ForeColor="Red" runat="server" ControlToValidate="UserName" ErrorMessage="Email or User Name is required."
                                Display="Static" ValidationGroup="loginHeader" EnableViewState="false">*</asp:RequiredFieldValidator></span>
                                </div>
                            </div>
                        </div>
                            <div class="col-md-6">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-lock" aria-hidden="true"></i></span>
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password" class="form-control" ValidationGroup="loginHeader" 
                                CssClass="inputs" placeholder="Password (case-sensitive)" Font-Bold="true" style="background-color:white;"/>
                                <span> <asp:RequiredFieldValidator CssClass="validatorNotification" ID="PasswordRequired"
                                ForeColor="Red" Display="Static" runat="server" ControlToValidate="Password"
                                ErrorMessage="Password is required." ValidationGroup="loginHeader" EnableViewState="false">*</asp:RequiredFieldValidator>
                                </span>
                                </div>
                            </div>
                            </div>
                        </div>
                    </div>
                    <asp:Label ID="FailureText" runat="server" EnableViewState="False" ForeColor="red" />
                </LayoutTemplate>
            </asp:Login>
        </td>
    </tr>
    <tr>
        <td>
            <table cellpadding="0" cellspacing="0" style="font-size:15px;">
                <tr>
                    <td>
                        New to DreamItAlive? 
                    </td>
                    <td style="padding-left:6px;">
                        <a href="javascript:;" runat="server" id="anchorJoinCommunity">Sign up now!</a>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <% if (ConfigurationManager.AppSettings["IN_UNIT_TESTING"] == "TRUE")
       {%>
    <tr id="trRP" runat="server">
        <td>
            <br />
            <asp:Button runat="server" ID="btnRP" Text="Test" OnClick="btnRP_Click" CausesValidation="false" />
            <asp:Label runat="server" ID="lblRP" />
        </td>
    </tr>
    <%
       }%>
</table>
