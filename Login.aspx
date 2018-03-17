<%@ Page Title="Log In" Language="C#" MasterPageFile="~/MasterPages/DefaultPageLayout.Master"
    AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DreamItAliveWebsite.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="padding-top: 0px;">
    </div>
    <table cellpadding="0" cellspacing="0" style="background-image:url(/images/account/login.jpg);background-repeat:no-repeat;text-align:center;margin-left:auto;margin-right:auto;">
        <tr>
            <td style="vertical-align: top;text-align:center;">
                <%--<ajaxToolkit:RoundedCornersExtender ID="rce" runat="server" TargetControlID="pnlLogin"
                    Radius="10" Corners="All" />--%>
                <asp:Panel runat="server" ID="pnlLogin" Width="900px">
                    <div style="padding: 15px;">
                        <table cellpadding="0" cellspacing="0" style="margin-left: auto; margin-right: auto;
                            padding: 0px 25px 10px 25px;">
                            <tr runat="server" id="trHeader">
                                <td style="padding-bottom: 15px; font-size: 15px; font-weight: bold; ">
                                    <div class="DIAGrayFilledHeader">
                                        <asp:Label runat="server" ID="lblHeaderMsg" Text="Please login or sign up for an account to continue." />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 15px; padding-bottom: 15px;text-align:center;margin-left:auto;margin-right:auto;">
                                    <table cellpadding="0" cellspacing="0" style="text-align:center;margin-left:auto;margin-right:auto;">
                                        <tr>
                                            <td>
                                                <dia:Login runat="server" ID="diaLogin" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </td>
            <%--<td style="padding-left: 50px; vertical-align: top;">
                <table cellpadding="0" cellspacing="0" class="DIATableDefault" style="">
                    <tr>
                        <td style="padding: 0px 0 16px 0;">
                            <img src="/images/homepage/fb-title.png" alt="Connect with us on Facebook" />
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 0px; float: left; text-align: left;">
                            <div class="fb-like-box" data-href="http://www.facebook.com/dreamitalive" data-width="415"
                                data-show-faces="true" data-border-color="#efefef" data-stream="true" data-header="false">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>--%>
        </tr>
    </table>
    <div style="padding-bottom:50px;"></div>
    <%--
    <h2>
        Log In
    </h2>
    <p>
    </p>
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false" >
        <LayoutTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
                 ValidationGroup="LoginUserValidationGroup"/>
            <div class="accountInfo">
                <fieldset class="login">
                    <legend>Account Information</legend>
                    <p>
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                        <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                        <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                             CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:CheckBox ID="RememberMe" runat="server"/>
                        <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
                    </p>
                </fieldset>
                <p class="submitButton">
                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup"/>
                </p>
            </div>
        </LayoutTemplate>
    </asp:Login>
    --%>
</asp:Content>
