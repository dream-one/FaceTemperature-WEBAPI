using Face.BLL;
using FaceServer.Filters;
using FaceServer.Models;
using FaceServer.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FaceServer.Controllers {
    /// <summary>
    /// 处理区级以及市级的人脸识别记录
    /// </summary>
    public class OptionsController : ApiController {
        /// <summary>
        /// 通过等级及单位ID获取所有的学校
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [MyAuth]
        public IHttpActionResult GetSchool(int Level, int organizationID) {
            if (ModelState.IsValid) {
                if (Level == 2) {
                    var result = OptionsManager.GetSchoolsByD(organizationID);
                    return this.SendData(result);
                }
                else if (Level == 3) {
                    var res = OptionsManager.GetSchoolsByC(organizationID);
                    return this.SendData(res);
                }
                else {
                    return this.ErrorData("error");
                }
            }
            else {
                return this.ErrorData("请求错误");
            }
        }
    }
}
