﻿using FaceServer.Tools;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FaceServer.App_Start {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute {
        //重写基类的异常处理方法
        private ILog log = LogManager.GetLogger("BookingWebApi");

        public override void OnException(HttpActionExecutedContext actionExecutedContext) {
            //1.异常日志记录（正式项目里面一般是用log4net记录异常日志）
            log.Error(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "——" +
                actionExecutedContext.Exception.GetType().ToString() + "：" + actionExecutedContext.Exception.Message + "——堆栈信息：" +
                actionExecutedContext.Exception.StackTrace);

            //2.返回调用方具体的异常信息
            if (actionExecutedContext.Exception is NotImplementedException) {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
            else if (actionExecutedContext.Exception is TimeoutException) {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.RequestTimeout);
            }
            //.....这里可以根据项目需要返回到客户端特定的状态码。如果找不到相应的异常，统一返回服务端错误500
            else {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            base.OnException(actionExecutedContext);
        }
    }
}