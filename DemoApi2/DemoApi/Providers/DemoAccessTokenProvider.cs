using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApi.Providers
{

    public class DemoAccessTokenProvider : AuthenticationTokenProvider
    {
        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
            var expired = context.Ticket.Properties.ExpiresUtc < DateTime.UtcNow;
            if (expired)
            {
                //If current token is expired, set a custom response header
                context.Response.Headers.Add("X-AccessTokenExpired", new string[] { "1" });
            }

            base.Receive(context);
        }

        public override void Create(AuthenticationTokenCreateContext context)
        {
            base.Create(context);
        }
    }
}