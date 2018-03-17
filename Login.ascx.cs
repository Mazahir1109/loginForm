using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using DIABusinessObjects;
using DIABusinessObjects.ControllerObjects;
using DIABusinessObjects.InfoObjects;
using DIABusinessObjects.SmartEnums;
using DIABusinessObjects.Utilities;
using DIALogging;
using DIAMail;
using DreamItAliveWebsite.WebUtilities;

namespace DreamItAliveWebsite.UserControls
{
    public partial class Login : System.Web.UI.UserControl
    {
        public event EventHandler LoggedIn;

        public bool ShouldRedirectUponLogin
        {
            get
            {
                if (ViewState["ShouldRedirectUponLogin"] != null)
                {
                    return (bool)ViewState["ShouldRedirectUponLogin"];
                }
                return true;
            }
            set { ViewState["ShouldRedirectUponLogin"] = value; }
        }

        public string UserName
        {
            get
            {
                if (ViewState["UserName"] != null)
                {
                    return (string)ViewState["UserName"];
                }
                return null;
            }
            set { ViewState["UserName"] = value; }
        }

        public string DestinationPageUrl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //hlnkMemberCommunicationSettingsMgmt.NavigateUrl =
            //    WebPageUtils.GeneratePageUrl(
            //        Resources.PathResources.PageMemberCommunicationFrequencyPreferencesMgmt, "?ReturnUrl=" + HttpUtility.UrlEncode(WebPageUtils.GenerateMemberDreamboardPage(userName)), false);

            //already logged in so just redirect
            if (MembershipUtils.IsUserLoggedIn && Request.QueryString["ReturnUrl"] != null)
            {
                Response.Redirect(Request.QueryString["ReturnUrl"]);
            }

            string queryString = null;
            if (Request.QueryString["ReturnUrl"] != null)
            {
                string returnUrl = HttpUtility.HtmlDecode(Request.QueryString["ReturnUrl"].ToLower());
                returnUrl += string.Format("&{0}=1", MembershipUtils.NEW_MEMBER);
                queryString = "?ReturnUrl=" + HttpUtility.UrlEncode(returnUrl);
            }
            anchorJoinCommunity.HRef = WebPageUtils.GeneratePageUrl(Resources.PathResources.PageRegisterMember, queryString, true);
            //                if (!string.IsNullOrEmpty(Request.QueryString["AR"])) //allow registration
            //                {
            //                }
            //                else
            //                {
            //                    hlnkRegister.Text = "Request an Invite";
            //                    hlnkRegister.NavigateUrl = WebPageUtils.GeneratePageUrl(
            //                        Resources.PathResources.PageWaitListSqueezePage,
            //                        queryString, true);
            //                }
            LinkButton lnkBtnForgotPasswordTemp = (LinkButton)login.FindControl("lnkBtnForgotPassword");
            lnkBtnForgotPasswordTemp.PostBackUrl =
                WebPageUtils.GeneratePageUrl(Resources.PathResources.PagePasswordRecovery, null, false);

            //                HyperLink hlnkJoinTemp = (HyperLink) login.FindControl("hlnkJoin");
            //                hlnkJoinTemp.NavigateUrl = WebPageUtils.GeneratePageUrl(Resources.PathResources.PageRegisterMember, null,
            //                                                                        true);
            
            if(!string.IsNullOrEmpty(DestinationPageUrl))
            {
                login.DestinationPageUrl = DestinationPageUrl;
            }
            else
            {
                login.DestinationPageUrl = WebPageUtils.GeneratePageUrl(Resources.PathResources.PageMemberProfileDreamFeed, "?RRFilter=disabled", false);
                //login.DestinationPageUrl = WebPageUtils.GenerateMemberDreamboardPage(CurrentUserName);
            }


            Button loginBtn = login.FindControl(("LoginButton")) as Button;
            if (loginBtn != null)
            {
                Page.Form.DefaultButton = loginBtn.UniqueID;
            }
            facebookLogin.ButtonType = FacebookLogin.ButtonTypeEnum.Login;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[MembershipUtils.EMAIL_QUERYSTRING_KEY]))
            {
                string redirectUrl;
                GetRedirectUrlUponLoggingIn(UserName, true, false, out redirectUrl);
                Response.Redirect(redirectUrl);
                //mpeConfirmEmail.Show();
            }
        }

        protected void login_LoggedIn(object sender, EventArgs e)
        {
            //MoxieManager code
            //Session["isLoggedIn"] = true;
            ///* Azure */
            //string basePath = string.Format("{0}.blob.core.windows.net.", ConfigUtils.GetCloudStorageAccountName());
            //string baseStr = string.Format("azure.containers.{0}", basePath);
            //Session[baseStr + "container"] = ConfigUtils.GetCloudStorageUserImagesContainerName() + "/moxie";
            //Session[baseStr + "account"] = ConfigUtils.GetCloudStorageAccountName();
            //Session[baseStr + "sharedkey"] = ConfigUtils.GetCloudStorageAccountKey();

            ///* Setup rootpath */
            ////Session["moxiemanager.filesystem.rootpath"] = "azure://myazureclient";            
            //Session["moxiemanager.filesystem.rootpath"] = string.Format("azure://{0}", basePath);            

            if (ShouldRedirectUponLogin)
            {
                string redirectUrl;
                if (!GetRedirectUrlUponLoggingIn(UserName, false, false, out redirectUrl))
                {
                    WebPageUtils.RedirectToErrorPage(Response, Request);
                }
                Response.Redirect(redirectUrl);
            }
            else if (Request.QueryString["ReturnUrl"] != null && HttpUtility.UrlDecode(Request.QueryString["ReturnUrl"]) != null)
            {
                Response.Redirect(Request.QueryString["ReturnUrl"]);
            }

            if (LoggedIn != null)
            {
                LoggedIn(sender, e);
            }
        }

        public static bool GetRedirectUrlUponLoggingIn(string userName, bool isBrandNewMember, bool forceUpdateCommunicationPreferences, out string redirectUrl)
        {
            redirectUrl = null;
            try
            {

                MembershipUser user = Membership.GetUser(userName);
                if (user == null)
                {
                    DIAErrorLogger.LogError(
                        "Membership.GetUser failed for userName: " + userName,
                        HttpContext.Current.Request.Url.PathAndQuery,
                        Membership.GetUser() != null ? (Guid)Membership.GetUser().ProviderUserKey : (Guid?)null,
                        DIAErrorLogger.ErrorLevel.Severe);
                    return false;
                }
                Guid userId = (Guid)user.ProviderUserKey;
                MembershipUtils.CurrentUserId = userId;

                string decodedUrl;
                if (HttpContext.Current.Request.QueryString["ReturnUrl"] != null && (decodedUrl = HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["ReturnUrl"])) != null)
                {
                    redirectUrl = decodedUrl;
                    return true;
                    //HttpContext.Current.Response.Redirect(decodedUrl);
                }

                if (Roles.IsUserInRole(userName, RolesSmartEnum.PhotoContributor.Description))
                {
                    redirectUrl = WebPageUtils.GeneratePageUrl(Resources.PathResources.PagePhotoContributorsDashboard, null, false);
                    return true;
                    //HttpContext.Current.Response.Redirect(WebPageUtils.GeneratePageUrl(Resources.PathResources.PagePhotoContributorsDashboard, null, false));
                }
                else if (Roles.IsUserInRole(userName, RolesSmartEnum.MemberAccountDeactivated.Description))
                {
                    redirectUrl = WebPageUtils.GeneratePageUrl(Resources.PathResources.PageMemberAccountDeactivated, null, false);
                    return true;
                    //HttpContext.Current.Response.Redirect(WebPageUtils.GeneratePageUrl(Resources.PathResources.PageMemberAccountDeactivated, null, false));
                }
                else if (Roles.IsUserInRole(userName, RolesSmartEnum.Member.Description))
                {
                    redirectUrl = WebPageUtils.GenerateMemberDreamboardPage(userName);
                    redirectUrl += "/lgn";

                    List<MemberDreamInfo> memberDreams;
                    if (!MemberDreamController.GetMemberDreamsByUserId(userId, false, out memberDreams))
                    {
                        DIAErrorLogger.LogError(
                            "MemberDreamController.GetMemberDreamsByUserId failed for userId: " + userId,
                            HttpContext.Current.Request.Url.PathAndQuery,
                            Membership.GetUser() != null ? (Guid)Membership.GetUser().ProviderUserKey : (Guid?)null,
                            DIAErrorLogger.ErrorLevel.Severe);
                        return false;
                    }
                    string querystring = string.Empty;
                    if (memberDreams == null || memberDreams.Count < 1)
                    {
                        querystring = "?FD=1";
                    }

                    if(isBrandNewMember)
                    {
                        redirectUrl = WebPageUtils.GenerateCreateNewDreamBuilderPage(userName, WebPageUtils.WizardStepEnum.Four, WebPageUtils.CreateNewDreamBuilderSubStepEnum.None, querystring);
                    }
                    else //if (!isBrandNewMember)
                    {
                        if (memberDreams != null && memberDreams.Count > 0)
                        {
                            //redirectUrl = WebPageUtils.GeneratePageUrl(Resources.PathResources.PageMemberProfileDreamFeed, "?RRFilter=disabled", false);
                            //make sure the dreamboard images are current
                            //AutoDreamboardInfo.CreateDreamboardImages(userId, userName, true, HttpContext.Current, DreamboardCategoryTypeSmartEnum.LifeAspirations);
                            //MemberProfileUtils.ReCreateAutoAndCollageImagesOfDreamboard(userName, userId, false);
                            DreamboardImageController.AddMessageToDreamboardImagesToProcessQueue(userName, userId);
                        }

                        if (forceUpdateCommunicationPreferences)
                        {
                            redirectUrl = WebPageUtils.GeneratePageUrl(Resources.PathResources.PageMemberCommunicationFrequencyPreferencesMgmt, "?ReturnUrl=" + HttpUtility.UrlEncode(redirectUrl), false);
                            return true;
                            //HttpContext.Current.Response.Redirect(WebPageUtils.GeneratePageUrl(Resources.PathResources.PageMemberCommunicationFrequencyPreferencesMgmt, "?ReturnUrl=" + HttpUtility.UrlEncode(redirectUrl), false));
                        }

                        //insert communication preferences if they don't exist
                        //string ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                        MemberProfileUtils.InsertDefaultCommunicationPreferencesIfNeeded(userId, null, EmailVerificationStatusSmartEnum.Unverified);

                        if (MembershipUtils.ShouldPrompt(userId, PromptUserPageTypeSmartEnum.ProfileInfoBuilder, true, true))
                        {
                            redirectUrl = WebPageUtils.GenerateMemberProfileInfoBuilderPage(WebPageUtils.WizardStepEnum.One);
                            return true;
                            //HttpContext.Current.Response.Redirect(WebPageUtils.GenerateMemberProfileInfoBuilderPage(WebPageUtils.WizardStepEnum.One));
                        }
                    }

                    //HttpContext.Current.Response.Redirect(redirectUrl, false);
                }
                return true;
            }
            catch (Exception e)
            {
                DIAErrorLogger.LogError(e, "Login::RedirectUserUponLoggingIn failed for userName:" + userName, DIAErrorLogger.ErrorLevel.Severe);
                return false;
            }
        }


        //        protected void login_Authenticate(object sender, AuthenticateEventArgs e)
        //        {
        //            
        //        }
        protected void btnRP_Click(object sender, EventArgs e)
        {
            if (ConfigUtils.IsInUnitTestMode())
            {
                TextBox UserNameTemp = (TextBox)login.FindControl("UserName");

                if (UserNameTemp != null && !string.IsNullOrEmpty(UserNameTemp.Text))
                {
                    MembershipUser user = Membership.GetUser(UserNameTemp.Text.Trim());
                    if (user != null)
                    {
                        string newPassword = user.ResetPassword();
                        lblRP.Text = newPassword;
                    }
                }
            }

        }




        protected void btnOk_Click(object sender, EventArgs e)
        {
            string redirectUrl;
            GetRedirectUrlUponLoggingIn(UserName, false, false, out redirectUrl);
            Response.Redirect(redirectUrl);
        }

        protected void login_Authenticate(object sender, AuthenticateEventArgs e)
        {
            e.Authenticated = false;
            if (login.UserName.Contains("@")) //Email Login
            {
                string userName = Membership.GetUserNameByEmail(login.UserName);
                if (userName != null)
                {
                    if (Membership.ValidateUser(userName, login.Password))
                    {
                        login.UserName = userName;
                        UserName = userName;
                        e.Authenticated = true;
                    }
                }
            }
            else  //Standard Username & Password Login
            {
                if (Membership.ValidateUser(login.UserName, login.Password))
                {
                    UserName = login.UserName;
                    e.Authenticated = true;
                }
            }

        }
    }
}