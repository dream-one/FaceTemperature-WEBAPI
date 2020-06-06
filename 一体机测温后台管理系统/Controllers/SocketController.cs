
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using FaceServer.Models;
using FaceServer.Filters;
using FaceServer.Tools;
using Newtonsoft.Json;
using Face.Models.Migrations;
using FaceServer.SocketHelp;
using System.Web.DynamicData;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Face.BLL;

namespace FaceServer.Controllers {
    public class SocketController : ApiController {


        private SocketHelper.TcpServer server;
        //[HttpPost]
        //[MyAuth]
        //public void AddStudentAsync(Student person) {
        [HttpPost]
        public async Task<IHttpActionResult> AddStudentAsync(Student student) {
            /*
             请求 WebAPI的时候，设置有超时时间，比如说3秒超时。（如果3秒之内你不给我数据，我就认为没有数据返回）。
在Socket这一端呢，我向Socket发送WebAPI的请求数据。我给WebAPI的请求数据加一个编号，我当然不知道Socket啥时候给我返回数据，所以开了一个异步的线程，只要Socket有返回数据。我就把返回数据存到缓存里（以请求编号为键，缓存数据，这样就可以找到是哪一次请求的数据了，缓存默认30秒过期）。
然后WebAPI请求就在主等待返回数据，肯定不能无限期地等待下去，这里有个请求循环，每个循环 之间延迟100毫秒,最多5次循环。在每次循环中，WebAPI请求都是在缓存中找数据，根据请求编号找对应的返回数据，如果找到请求返回的数据，就直接返回。没找到就循环继续找，直到5次循环结束，还没有找到就认为找不到这条数据。
             */
            try {
                server = WebApiApplication.GetCache("SocketServer") as SocketHelper.TcpServer;
                if (server != null) {
                    //获取最大的学生ID，然后加一，就当作faceID了，再将这个faceID传入前端设备
                    int ID = StudentManager.GetStudentIdMax();
                    string faceID = (ID + 1).ToString();
                    string StrGuid = Guid.NewGuid().ToString();//定义一个标识，标识这个请求，设备回应的数据应该带上
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("faceID", faceID);
                    dictionary.Add("faceName", student.Name);
                    dictionary.Add("Action", "addFace");
                    dictionary.Add("imageContent", student.imageContent);
                    dictionary.Add("StrGuid", StrGuid);
                    server.SendToClient(student.EquipmentNum, JsonConvert.SerializeObject(dictionary));//发送数据到客户端，第一个参数是指定设备的序列号，第二个参数是对象
                    JObject jObject = null;
                    for (int i = 0; i < 5; i++) {
                        Thread.Sleep(200);
                        jObject = WebApiApplication.GetCache(StrGuid) as JObject;
                        if (jObject != null) {
                            await StudentManager.CreatStudentAsync(Convert.ToInt32(faceID), student.Name, student.classValue, student.gradeValue, student.StudentID);
                            return this.SendData(jObject);
                        }
                    }
                    return this.ErrorData("没等到设备回应,查看设备是否已连接到服务器");
                }
                else {
                    return this.ErrorData("请检查设备是否连接到服务器");
                }
            }
            catch (Exception ex) {
                LogTool.WriteError(ex.Message);
                return this.ErrorData(ex.Message);
            }

        }


        [HttpPost]
        public async Task<IHttpActionResult> AddTeacherAsync(TeacherVM teacher) {
            try {
                server = WebApiApplication.GetCache("SocketServer") as SocketHelper.TcpServer;
                if (server != null) {
                    //获取最大的学生ID，然后加一，就当作faceID了，再将这个faceID传入前端设备
                    int ID = TeacherManager.GetTeacherIdMax();
                    string faceID = (ID + 1).ToString();
                    string StrGuid = Guid.NewGuid().ToString();//定义一个标识，标识这个请求，设备回应的数据应该带上
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("faceID", faceID);
                    dictionary.Add("faceName", teacher.Name);
                    dictionary.Add("Action", "addFace");
                    dictionary.Add("imageContent", teacher.imageContent);
                    dictionary.Add("StrGuid", StrGuid);
                    server.SendToClient(teacher.EquipmentNum, JsonConvert.SerializeObject(dictionary));//发送数据到客户端，第一个参数是指定设备的序列号，第二个参数是对象
                    JObject jObject = null;
                    for (int i = 0; i < 5; i++) {
                        Thread.Sleep(200);
                        jObject = WebApiApplication.GetCache(StrGuid) as JObject;
                        if (jObject != null) {
                            await TeacherManager.AddTeacherAsync(Convert.ToInt32(faceID), teacher.Name, teacher.PhoneNum, teacher.SchoolID);
                            return this.SendData(jObject);
                        }
                    }
                    return this.ErrorData("没等到设备回应,查看设备是否已连接到服务器");
                }
                else {
                    return this.ErrorData("请检查设备是否连接到服务器");
                }
            }
            catch (Exception ex) {
                LogTool.WriteError(ex.Message);
                return this.ErrorData(ex.Message);
            }

        }


    }
}
