using Face.BLL;
using FaceServer.Filters;
using FaceServer.Models;
using FaceServer.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FaceServer.Controllers {
    public class notifyUrlController : ApiController {

        /// <summary>
        /// 向数据库添加通行记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/notify")]
        public async Task<object> notifyAsync(EnterAndLeaveViewModel model) {
            //通过查询设备ID获取到学校ID，再将学校ID赋值给进出记录ID
            try {
                #region 前置设置
                string inouttype = "";
                switch (model.authType) {
                    case "ICCard":
                        model.authType = "IC卡";
                        break;
                    case "QRCode":
                        model.authType = "二维码";
                        break;
                    case "FaceReco":
                        model.authType = "人脸识别";
                        break;
                }
                switch (model.inOutType) {
                    case 0:
                        inouttype = "进";
                        break;
                    case 1:
                        inouttype = "出";
                        break;
                }
                string State = "正常";


                #endregion
                int SchoolID = EqManager.GetSchoolIdByEqNum(model.deviceSerial);
                var result = AttendanceManager.GetTime(SchoolID);
                if (result == null) {
                    State = "未设置考勤范围";
                }
                else {
                    if (model.inOutType == 0) {
                        DateTime jinTime = DateTime.Parse(model.time);
                        if (jinTime.Hour >= result.InTime.Hour) {
                            if (jinTime.Minute > 0) {
                                //迟到,增加一个考勤字段，在这里将这个考勤字段设置为迟到
                                State = "迟到";
                            }
                        }
                    }

                    if (model.inOutType == 1) {
                        DateTime jinTime = DateTime.Parse(model.time);
                        if (jinTime.Hour <= result.OutTime.Hour) {
                            if (jinTime.Minute < 60) {
                                //早退
                                State = "早退";
                            }
                        }
                    }
                }

                await NotifyManager.AddNotify(SchoolID, model.faceID, model.deviceSerial, model.faceName, model.authType, inouttype, model.time, State, model.deviceID, model.snapshotUrl, "data:image/jpeg;base64," + model.snapshotContent, model.temperature);

                var rst = new { errCode = 0, errMsg = "success" };
                return rst;
            }
            catch (Exception ex) {

                var err = new { errCode = 1, errMsg = ex.Message };
                return err;
            }
        }

        /// <summary>
        /// 通过姓名查询进出记录表
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        [HttpGet]
        [MyAuth]
        [Route("api/GetNotifyByName")]
        public IHttpActionResult SearchNotifyByName(string Name = null, string StartTime = null, string EndTime = null) {

            string token = HttpContext.Current.Request.Headers["token"];
            var userInfo = JwtTools.DEcode(token);
            //调用usermanager.getuserinfo 获取用户信息
            var user = UserManager.GetUserInfo(userInfo["name"]);
            var result = NotifyManager.GetNotifyByName(user.organizationID, Name, StartTime, EndTime);
            return this.SendData(result);
        }


        /// <summary>
        /// 分页获取进出记录
        /// </summary>
        /// <param name="PageSize">一页的数量</param>
        /// <param name="PageIndex">页数,第一页是0</param>
        /// <returns></returns>
        [MyAuth]
        [HttpGet]
        [Route("api/GetNotify")]
        public IHttpActionResult GetNotify(string PageSize, string PageIndex) {
            if (ModelState.IsValid) {
                //通过token获取用户名
                string token = HttpContext.Current.Request.Headers["token"];
                var userInfo = JwtTools.DEcode(token);
                //调用usermanager.getuserinfo 获取用户信息
                var user = UserManager.GetUserInfo(userInfo["name"]);

                var count = NotifyManager.GetNotifyCount();
                var result = NotifyManager.GetNotify(Convert.ToInt32(PageSize), Convert.ToInt32(PageIndex), user.organizationID);

                var data = new { result, count };
                return this.SendData(data);
            }
            else {
                return this.ErrorData("发生异常，请重新登录");
            }
        }

        [MyAuth]
        [HttpGet]
        [Route("api/GetNotify")]
        public IHttpActionResult GetNotify(string PageSize, string PageIndex, int SchoolID) {
            if (ModelState.IsValid) {
                var count = NotifyManager.GetNotifyCount();
                var result = NotifyManager.GetNotify(Convert.ToInt32(PageSize), Convert.ToInt32(PageIndex), SchoolID);

                var data = new { result, count };
                return this.SendData(data);
            }
            else {
                return this.ErrorData("发生异常，请重新登录");
            }
        }

        [MyAuth]
        [HttpGet]
        [Route("api/GetNotifyBySchoolName")]
        public IHttpActionResult GetNotifyBySchoolName(string SchoolName, string PageIndex = null, string PageSize = null) {

            try {
                if (PageSize == null) PageSize = "50";
                var result = NotifyManager.GetNotifyBySchoolName(SchoolName, PageIndex, PageSize);
                return this.SendData(result);
            }
            catch (Exception ex) {

                return this.ErrorData(ex.Message);
            }
        }

    }


}

