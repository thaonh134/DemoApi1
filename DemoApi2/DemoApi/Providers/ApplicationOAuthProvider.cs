using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using DemoApi.Models;
using DemoApi.Services;
using DemoApi.Database.IdentityContext;
using Castle.Windsor;
using Castle.MicroKernel.Lifestyle;
using DemoApi.Services.Services.Interface;
using DemoApi.Models.Account;
using DemoApi.Common.Exceptions;

namespace DemoApi.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private IWindsorContainer _windsorContainer;
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId, IWindsorContainer windsorContainer)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
            _windsorContainer = windsorContainer;
        }
        public override Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            return base.ValidateTokenRequest(context);
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (_windsorContainer.BeginScope())
            {
                IUserService userService = _windsorContainer.Resolve<IUserService>();

                LoginModel loginModel = new LoginModel()
                {
                    ////Rem this code because change request to use client send Otp
                    //Otp = context.OwinContext.Get<string>("Otp"),
                    TypeDevice = context.OwinContext.Get<string>("TypeDevice"),
                    Password = context.Password,
                    UserName = context.UserName,
                    AuthAuthenticationType = context.Options.AuthenticationType
                };

                try
                {
                    LoginResultModel result = await userService.Login(loginModel);
                    context.Validated(result.IdendityLoginResult.Identity);
                    AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(result.User.UserName);
                    context.Request.Context.Authentication.SignIn(properties, result.IdendityLoginResult.OAuthIdentity, result.IdendityLoginResult.CookiesIdentity);
                }
                catch (BaseApiException vovEx)
                {
                    context.SetError(vovEx.ErrorCode, vovEx.ErrorMessage);
                }
            }


            //var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            //ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            //if (user == null)
            //{
            //    context.SetError("invalid_grant", "The user name or password is incorrect.");
            //    return;
            //}

            //ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
            //   OAuthDefaults.AuthenticationType);
            //ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
            //    CookieAuthenticationDefaults.AuthenticationType);

            //AuthenticationProperties properties = CreateProperties(user.UserName);
            //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //identity.AddClaim(new Claim(ClaimTypes.PrimarySid, user.Id));

            //AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            //context.Validated(ticket);
            //context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}