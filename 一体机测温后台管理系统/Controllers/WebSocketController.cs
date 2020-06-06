using Face.BLL;
using FaceServer.Filters;
using FaceServer.Models;
using FaceServer.Tools;
using Fleck;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace FaceServer.Controllers {
    public class WebSocketController : ApiController {
        //客户端url以及其对应的Socket对象字典
        public static IDictionary<string, WebSocketViewModel> dic_Sockets;


        [HttpGet]
        public static void Connect() {
            dic_Sockets = new Dictionary<string, WebSocketViewModel>();
            WebSocketServer server = new WebSocketServer("ws://0.0.0.0:9527");//监听所有的的地址
            WebSocketViewModel socketViewModel = new WebSocketViewModel();
            //出错后进行重启
            server.RestartAfterListenError = true;
            server.Start(socket => {
                socket.OnOpen = () =>   //连接建立事件
                {
                    //获取客户端网页的url
                    string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                    socketViewModel.socket = socket;
                    socketViewModel.IP = clientUrl;
                    dic_Sockets.Add(clientUrl, socketViewModel);
                    LogTool.Write("|服务器:和客户端网页:" + clientUrl + " 建立WebSock连接！" + "当前连接数量：" + dic_Sockets.Count);

                };
                socket.OnClose = () =>  //连接关闭事件
                {
                    string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                    //如果存在这个客户端,那么对这个socket进行移除
                    if (dic_Sockets.ContainsKey(clientUrl)) {
                        //注:Fleck中有释放
                        //关闭对象连接 
                        //if (dic_Sockets[clientUrl] != null)
                        //{
                        //dic_Sockets[clientUrl].Close();
                        //}
                        dic_Sockets.Remove(clientUrl);
                    }
                    LogTool.Write("|服务器:和客户端网页:" + clientUrl + " 断开WebSock连接！");
                };
                socket.OnMessage = message =>  //接受客户端网页消息事件
                {
                    string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                    JObject ResponObj = JsonConvert.DeserializeObject<JObject>(message);
                    if (ResponObj != null) {
                        if (ResponObj["msgId"] != null) {//把设备发送的数据缓存起来，时长30秒
                            WebApiApplication.SetCache(ResponObj["msgId"].ToString(), ResponObj, 30);
                        }
                        if (ResponObj["url"] != null) {
                            if (ResponObj["url"].ToString() == "login") {
                                socketViewModel.deviceSerial = ResponObj["deviceSerial"].ToString();
                                var obj = new { msgId = ResponObj["msgId"].ToString(), errCode = 0, errMsg = "success" };
                                socket.Send(JsonConvert.SerializeObject(obj));
                            }
                        }
                    }
                    LogTool.WriteData("|服务器:【收到】来客户端:" + clientUrl + "的信息：\n" + message);
                };
            });
        }

        public IHttpActionResult GetEqDetail() {
            ArrayList arrayList = new ArrayList();
            foreach (var item in dic_Sockets.Values) {
                string msgId = Guid.NewGuid().ToString();//定义一个标识，标识这个请求，设备回应的数据应该带上
                var obj = new { msgId, url = "queryDeviceDetail" };
                item.socket.Send(JsonConvert.SerializeObject(obj));
                JObject jObject = null;
                for (int i = 0; i < 7; i++) {//循环去内存中查找有没有回应
                    Thread.Sleep(200);
                    jObject = WebApiApplication.GetCache(msgId) as JObject;
                    if (jObject != null) {
                        if (jObject["errCode"].ToString() == "0") {
                            arrayList.Add(jObject["productModel"].ToString());
                        }
                        else {
                            return this.ErrorData(jObject["errMsg"].ToString());
                        }

                    }
                    else {
                        continue;
                    }

                }
            }
            return this.SendData(arrayList);
        }

        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [MyAuth]
        [HttpPost]
        public async Task<IHttpActionResult> AddStudentAsync(Student student) {
            try {
                foreach (var item in dic_Sockets.Values) {
                    if (item.deviceSerial == student.EquipmentNum) {
                        //获取最大的学生ID，然后加一，就当作faceID了，再将这个faceID传入前端设备
                        int ID = StudentManager.GetStudentIdMax();
                        string faceID = (ID + 1).ToString();
                        string msgId = Guid.NewGuid().ToString();//定义一个标识，标识这个请求，设备回应的数据应该带上
                        var obj = new { msgId, faceID, faceName = student.Name, student.imageContent, url = "addFace" };
                        await item.socket.Send(JsonConvert.SerializeObject(obj));
                        JObject jObject = null;
                        for (int i = 0; i < 7; i++) {//循环去内存中查找有没有回应
                            Thread.Sleep(200);
                            jObject = WebApiApplication.GetCache(msgId) as JObject;
                            if (jObject != null) {
                                if (jObject["errCode"].ToString() == "0") {
                                    await StudentManager.CreatStudentAsync(Convert.ToInt32(faceID), student.Name, student.classValue, student.gradeValue, item.deviceSerial, student.StudentID,student.Caller,student.PhoneNum.ToString(), "data:image/jpeg;base64,"+student.imageContent);
                                    return this.SendData(jObject);
                                }
                                else {
                                    return this.ErrorData(jObject["errMsg"].ToString());
                                }
                            }
                            else {
                                continue;
                            }

                        }
                        return this.ErrorData("设备没有回应");
                    }
                }
                return this.ErrorData("没有找到此设备，请查看设备序列号是否正确");
            }
            catch (Exception ex) {

                return this.ErrorData(ex.Message);
            }
        }


        /// <summary>
        /// 添加老师
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [MyAuth]
        [HttpPost]
        public async Task<IHttpActionResult> AddTeacherAsync(TeacherVM teacher) {
            try {
                foreach (var item in dic_Sockets.Values) {
                    if (item.deviceSerial == teacher.EquipmentNum) {
                        //获取最大的学生ID，然后加一，就当作faceID了，再将这个faceID传入前端设备
                        int ID = TeacherManager.GetTeacherIdMax();
                        string faceID = (ID + 1).ToString();
                        string msgId = Guid.NewGuid().ToString();//定义一个标识，标识这个请求，设备回应的数据应该带上
                        var obj = new { msgId, faceID, faceName = teacher.Name, teacher.imageContent, url = "addFace" };
                        await item.socket.Send(JsonConvert.SerializeObject(obj));
                        JObject jObject = null;
                        for (int i = 0; i < 5; i++) {
                            Thread.Sleep(200);
                            jObject = WebApiApplication.GetCache(msgId) as JObject;
                            if (jObject != null) {
                                if (jObject["errCode"].ToString() == "0") {
                                    await TeacherManager.AddTeacherAsync(Convert.ToInt32(faceID), teacher.Name, teacher.PhoneNum, teacher.SchoolID, teacher.EquipmentNum, "data:image/jpeg;base64,"+teacher.imageContent);
                                    return this.SendData(jObject);
                                }
                                else {
                                    return this.ErrorData(jObject["errMsg"].ToString());
                                }
                            }
                            else {
                                continue;
                            }
                        }
                        return this.ErrorData("设备没有回应");
                    }
                }
                return this.ErrorData("没有找到此设备，请查看设备序列号是否正确");
            }
            catch (Exception ex) {
                LogTool.WriteError(ex.Message);
                return this.ErrorData(ex.Message);
            }

        }

        [HttpGet]
        public void DelteFace(int faceID, string action) {
            if (ModelState.IsValid) {
                string msgId = Guid.NewGuid().ToString();
                if (action == "student") {
                    var obj = new { url = "deleteFace", msgId, faceID, };

                }
            }
        }
    }
}

