using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Security;
using DIABusinessObjects.Utilities;
using DIABusinessObjects.ControllerObjects;
using DIABusinessObjects.InfoObjects;
using DIABusinessObjects.SmartEnums;
using DIALogging;
using Facebook;

namespace DreamItAliveWebsite.UserControls
{
    public partial class FacebookLogin : System.Web.UI.UserControl
    {
        public enum ButtonTypeEnum { SignUp, SignUpLong, Login}
        public string AuthenticationUrl
        {
            get
            {
                Session["FBState"] = new Random().Next().ToString();
                return
                    string.Format("https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}&state={3}",
                        Facebook.FacebookApplication.Current.AppId, HttpUtility.UrlEncode(RedirectUrl), Scope, Session["FBState"]);
            }
        }

        public string RedirectUrl
        {
            get
            {
                if(ViewState["RedirectUrl"] != null)
                {
                    return (string) ViewState["RedirectUrl"];
                }
                String referrerUserName = Request.QueryString["r"];

                string urlPrefix = WebPageUtils.GetCurrentUrlPrefix(HttpContext.Current);
                string queryString = !String.IsNullOrWhiteSpace(referrerUserName) ? "?r=" + referrerUserName : string.Empty;
                string url = string.Format("{0}FBRegCallback.aspx{1}", urlPrefix, queryString);

                return url;
            }
            set { ViewState["RedirectUrl"] = value; }
        }

        public string Scope
        {
            get
            {
                return "email"; //"email,user_birthday";                
            }
        }

        public ButtonTypeEnum ButtonType { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
//            var authorizer = new Facebook.Web.FacebookWebAuthorizer();
//
//            if (authorizer.FacebookWebRequest.IsAuthorized())
//            {
//                long userId = authorizer.FacebookWebRequest.Session.UserId;
//
//                MemberInfo memberInfo;
//                if (!MemberController.GetMemberByFBUserId(userId, out memberInfo))
//                {
//                    DIAErrorLogger.LogError("MemberController.GetMemberByFBUserId failed for userId:" + userId, this.GetType().FullName + ":" + Request.Url.PathAndQuery + ":" + Request.Url.PathAndQuery, Membership.GetUser() == null ? null : (Guid?)Membership.GetUser().ProviderUserKey, DIAErrorLogger.ErrorLevel.Severe);
//                    WebPageUtils.RedirectToErrorPage(Response, Request);
//                }
//
//                if (memberInfo == null)
//                {
//                    var fb = new Facebook.Web.FacebookWebClient();
//                    var result = (IDictionary<string, object>)fb.Get("4");
                    //                    memberInfo = MemberInfo.MemberInfoFactory(Guid.NewGuid(), (string)result["first_name"], (string)result["last_name"], GenderSmartEnum.NotProvided, )
                    //                    if(!MemberController.InsertMemberMaster(memberInfo, null))
                    //                    {
                    //                        
                    //                    }
//                }
                // check if return url is local.
                //Response.Redirect(HttpUtility.UrlDecode("https://graph.facebook.com/oauth/authorize?client_id=275619482476868&redirect_uri=http://www.dreamitalive.com/oauth_redirect"));
//                Response.Redirect(HttpUtility.UrlDecode(Request.QueryString["returnUrl"] ?? "/"));
//            }


            imgBtnFacebookConnect.ImageUrl = WebPageUtils.GetAbsolutePath(Request.Url.AbsoluteUri,
                                         "images/SocialMedia/FBConnectButton-sign-in.jpg");

            if (ButtonType == ButtonTypeEnum.SignUp)
            {
                imgBtnFacebookConnect.ImageUrl = WebPageUtils.GetAbsolutePath(Request.Url.AbsoluteUri,
                                             "images/SocialMedia/FBConnectButton.jpg");
            }
            else if(ButtonType == ButtonTypeEnum.SignUpLong)
            {
                imgBtnFacebookConnect.ImageUrl = WebPageUtils.GetAbsolutePath(Request.Url.AbsoluteUri,
                                             "images/SocialMedia/facebook-sign-up-longer.jpg");
            }
        }

        protected void imgBtnFacebookConnect_Click(object sender, EventArgs e)
        {
            Response.Redirect(AuthenticationUrl);
        }
    }
}