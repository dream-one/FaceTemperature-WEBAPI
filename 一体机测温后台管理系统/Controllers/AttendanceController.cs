using Face.BLL;
using FaceServer.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FaceServer.Controllers {
    public class AttendanceController : ApiController {
        [HttpGet]
        public async System.Threading.Tasks.Task<IHttpActionResult> SetTimeAsync(string inTime, string outTime, int SchoolID) {
            try {
                await AttendanceManager.SetTimeAsync(inTime, outTime, SchoolID);
                return this.SendData("设置成功");
            }
            catch (Exception ex) {
                return this.ErrorData("错误" + ex.Message);
                throw;
            }
        }
    }
}
