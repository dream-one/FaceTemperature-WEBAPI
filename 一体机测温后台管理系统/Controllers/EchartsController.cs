using Face.BLL;
using FaceServer.Filters;
using FaceServer.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FaceServer.Controllers {
    public class EchartsController : ApiController {

        [MyAuth]
        [HttpGet]
        public IHttpActionResult GetNotify(int organizationID) {
            if (ModelState.IsValid) {
                try {
                    var result = NotifyManager.GetNotifyByTime(organizationID);
                    return this.SendData(result);
                }
                catch (Exception ex) {

                    return this.ErrorData(ex.Message);

                }
            }
            else {
                return this.ErrorData("请求错误");
            }
        }

        /// <summary>
        /// 获取考勤状态
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        [MyAuth]
        [HttpGet]
        public IHttpActionResult GetAttendanceState(int organizationID) {
            if (ModelState.IsValid) {
                try {
                    var result = NotifyManager.GetNotifyStateByTime(organizationID);
                    return this.SendData(result);
                }
                catch (Exception ex) {

                    return this.ErrorData(ex.Message);

                }
            }
            else {
                return this.ErrorData("请求错误");
            }
        }
    }
}
