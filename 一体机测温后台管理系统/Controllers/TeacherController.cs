using FaceServer.Filters;
using Newtonsoft.Json;
using System;
using System.Web.Http;
using Face.BLL;
using System.Web;
using FaceServer.Tools;
using Newtonsoft.Json.Linq;
using System.Threading;
using FaceServer.Models;

namespace FaceServer.Controllers {

    public class TeacherController : ApiController {


        /// <summary>
        /// 删除 可数组或单个
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [MyAuth]
        public IHttpActionResult BatchDelete(Face.Models.Teacher[] teachers) {
            if (ModelState.IsValid) {
                try {
                    foreach (var item in teachers) {//遍历学生列表
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
                                            TeacherManager.DeleteTeacher(item.Id);
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

        [HttpGet]
        [MyAuth]
        public IHttpActionResult GetTeacherArr(string PageSize, string PageIndex, string Name = null) {
            try {
                //通过token获取用户名
                string token = HttpContext.Current.Request.Headers["token"];
                var userInfo = JwtTools.DEcode(token);
                //调用usermanager.getuserinfo 获取用户信息
                var user = UserManager.GetUserInfo(userInfo["name"]);
                var result = TeacherManager.QueryTeacher(user.organizationID, Convert.ToInt32(PageSize), Convert.ToInt32(PageIndex), Name);
                int count = TeacherManager.QueryTeacherCount(user.organizationID);
                return this.SendData(new { result, count });
            }
            catch (Exception ex) {

                return this.ErrorData(ex.Message);
            }

        }


        [HttpPost]
        [MyAuth]
        public async System.Threading.Tasks.Task<IHttpActionResult> EditTeacherAsync(TeacherVM teacher) {
            try {
                foreach (var socket in WebSocketController.dic_Sockets.Values) {//遍历正在连接的设备
                    if (teacher.EquipmentNum == socket.deviceSerial) {
                        var msgId = Guid.NewGuid().ToString();
                        var obj = new { url = "addFace", msgId, faceID = teacher.Id, imageContent = teacher.imageContent, overwrite = true };
                        await socket.socket.Send(JsonConvert.SerializeObject(obj));
                        JObject jObject = null;
                        for (int i = 0; i < 7; i++) {//循环去内存中查找有没有回应
                            Thread.Sleep(200);
                            jObject = WebApiApplication.GetCache(msgId) as JObject;
                            if (jObject != null) {
                                if (jObject["errCode"].ToString() == "0") {
                                    await TeacherManager.EditTeacherAsync(teacher.Id, teacher.Name, teacher.PhoneNum, teacher.SchoolID, teacher.EquipmentNum, "data: image / jpeg; base64,"+teacher.imageContent);
                                    return this.SendData(jObject);
                                }
                                else {
                                    return this.ErrorData(jObject["errMsg"].ToString());
                                }
                            }
                        }
                        return this.ErrorData("没有等到设备响应");
                    }
                }
                    return this.ErrorData("没找到设备，查看设备相关设置");
            }
            catch (Exception ex) {

                return this.ErrorData("错误" + ex.Message);
            }
        }
    }

}
