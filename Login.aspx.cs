using System;
using System.Web;
using DIABusinessObjects;
using DreamItAliveWebsite.WebUtilities;

namespace DreamItAliveWebsite.Account
{
    public partial class Login : DreamItAliveWebPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //hlnkRegister.Text = "Register";
            //hlnkRegister.NavigateUrl = WebPageUtils.GeneratePageUrl(Resources.PathResources.PageRegisterMember, "?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]), true);
            trHeader.Visible = Request.QueryString["ReturnUrl"] != null;
            diaLogin.ShouldRedirectUponLogin = true;

            if (!string.IsNullOrEmpty(Request.QueryString[MembershipUtils.EMAIL_QUERYSTRING_KEY]))
            {
                trHeader.Visible = true;
                lblHeaderMsg.Text = "Thank you for verifying your email. Please login below to continue";
            }
            else if (!string.IsNullOrEmpty(Request.QueryString[MembershipUtils.JOIN_GROUP]))
            {
                trHeader.Visible = true;
                lblHeaderMsg.Text = "In order to submit your Dreamboard to a group, you need to log in or create an account if you don't have one.";
            }

            //((MasterPages.PageWithRightRail)Page.Master).CacheExpirationTime = 60 * 60;
            ((DreamItAliveWebsite.MasterPages.DefaultPageLayout)Page.Master).FacebookLikePopupPosition = DreamItAliveWebsite.MasterPages.MainHeaderAndFooter.FacebookLikePopupPositionEnum.None;
            ((DreamItAliveWebsite.MasterPages.DefaultPageLayout)Page.Master).ShowExitPopup = false;

        }
    }
}
