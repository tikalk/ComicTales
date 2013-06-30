using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR.Hubs;

namespace ComicTales.SignalR
{
    //public class HubAuthorizeAttribute : Attribute, IAuthorizeHubConnection, IAuthorizeHubMethodInvocation
    //{
    //    public virtual bool AuthorizeHubConnection(HubDescriptor hubDescriptor, Microsoft.AspNet.SignalR.IRequest request)
    //    {
    //        IAuthorizationProvider authorizationProvider = DependencyResolver.Current.GetService<IAuthorizationProvider>();

    //        return authorizationProvider.IsAuthorizedController(hubDescriptor.Name);
    //    }

    //    public virtual bool AuthorizeHubMethodInvocation(IHubIncomingInvokerContext hubIncomingInvokerContext)
    //    {
    //        IAuthorizationProvider authorizationProvider = DependencyResolver.Current.GetService<IAuthorizationProvider>();

    //        return authorizationProvider.IsAuthorizedAction(hubIncomingInvokerContext.MethodDescriptor.Hub.Name, hubIncomingInvokerContext.MethodDescriptor.Name);
    //    }
    //}
}