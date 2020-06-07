using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaceServer.App_Start {
    public class Log4netRegister {
        public static void Register() {
            var path = HttpContext.Current.Server.MapPath("~/Log4net/Log4net.config");
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(path));
        }
    }
}