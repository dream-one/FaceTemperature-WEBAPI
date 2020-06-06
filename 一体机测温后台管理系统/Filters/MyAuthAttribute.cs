using FaceServer.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FaceServer.Filters {
    public class MyAuthAttribute : Attribute, IAuthorizationFilter {
        public bool AllowMultiple => throw new NotImplementedException();

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation) {
            //添加了AllowAnonymous特性的action将跳过校验
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>(true).Count > 0) {
                return await continuation();
            }
            //获取request =>headers  =>token
            IEnumerable<string> headers;
            if (actionContext.Request.Headers.TryGetValues("token", out headers)) {
                //获取到了

                return await continuation();
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }
}