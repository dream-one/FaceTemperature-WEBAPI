using FaceServer.Filters;
using FaceServer.Models;
using FaceServer.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web.Http;
using Face.BLL;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Diagnostics.Eventing.Reader;

namespace FaceServer.Controllers {

    /// <summary>
    /// 人员管理
    /// </summary>
    public class StudentController : ApiController {

        /// <summary>
        /// 获取学生对象
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        [HttpGet]
        [MyAuth]
        public IHttpActionResult GetStudentArr(string PageSize, string PageIndex, string Name = null) {

            //通过token获取用户名
            string token = HttpContext.Current.Request.Headers["token"];
            var userInfo = JwtTools.DEcode(token);
            //调用usermanager.getuserinfo 获取用户信息
            var user = UserManager.GetUserInfo(userInfo["name"]);
            var result = StudentManager.QueryCount(user.organizationID, Convert.ToInt32(PageSize), Convert.ToInt32(PageIndex), Name);
            return this.SendData(result);

        }

        /// <summary>
        /// 批量删除学生信息
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        [HttpPost]
        [MyAuth]
        public IHttpActionResult BatchDelete(List<Face.Models.Student> students) {
            if (ModelState.IsValid) {

                try {
                    foreach (var item in students) {//遍历学生列表
                        int count = 0;
                        foreach (var socket in WebSocketController.dic_Sockets.Values) {//遍历正在连接的设备
                            count++;
                            if (item.DeviceSerial == socket.deviceSerial) {
                                var msgId = Guid.NewGuid().ToString();
                                var obj = new { url = "deleteFace", msgId, faceID = item.Id };
                                socket.socket.Send(JsonConvert.SerializeObject(obj));
                                JObject jObject = null;
                                for (int i = 0; i < 7; i++) {//循环去内存中查找有没有回应
                                    Thread.Sleep(200);
                                    jObject = WebApiApplication.GetCache(msgId) as JObject;
                                    if (jObject != null) {
                                        if (jObject["errCode"].ToString() == "0") {
                                            StudentManager.DeleteStudent(item.Id);
                                            break;
                                        }
                                        else {
                                            return this.ErrorData(jObject["errMsg"].ToString());
                                        }
                                    }
                                    else {
                                        continue;
                                    }
                                }
                                if (jObject != null) {
                                    break;
                                }
                            }
                            else if (WebSocketController.dic_Sockets.Values.Count == count) {
                                //最后一次还没找到
                                return this.ErrorData("查看设备是否连接");
                            }
                        }
                    }
                    return this.SendData(true);
                }
                catch (Exception ex) {

                    return this.ErrorData(ex.Message);
                }
            }
            else {
                return this.ErrorData("请求错误");
            }
        }

        //[HttpGet]
        //[MyAuth]
        //public IHttpActionResult DeleteStudent(int Id) {
        //    if (ModelState.IsValid) {
        //        var result = StudentManager.DeleteStudent(Id);
        //        return this.SendData(result);
        //    }
        //    else {
        //        return this.ErrorData("请求错误");
        //    }
        //}


    }
}
