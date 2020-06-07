using FaceServer.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Routing;

namespace FaceServer {
    public class WebApiApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            WebSocketController.Connect();//开启websocket连接
            GlobalConfiguration.Configure(WebApiConfig.Register);
            log4net.Config.XmlConfigurator.Configure();
        }

        public static object GetCache(string cacheKey) {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            return cache[cacheKey];

        }
        public static void SetCache(string cacheKey, object obj) {
            if (obj == null) return;
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            cache.Insert(cacheKey, obj);
        }

        /// <summary>  
        /// 设置数据缓存  
        /// </summary>  
        public static void SetCache(string cacheKey, object objObject, int timeout = 30) {
            try {
                if (objObject == null) return;
                var objCache = HttpRuntime.Cache;
                //相对过期  
                //objCache.Insert(cacheKey, objObject, null, DateTime.MaxValue, timeout, CacheItemPriority.NotRemovable, null);  
                //绝对过期时间  
                objCache.Insert(cacheKey, objObject, null, DateTime.Now.AddSeconds(timeout), TimeSpan.Zero, CacheItemPriority.High, null);
            }
            catch (Exception) {
                //throw;  
            }
        }
    }
}
