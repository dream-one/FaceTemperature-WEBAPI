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
    public class EquipmentController : ApiController {
        /*
         * 1.这是添加设备需要调用的接口
         * 2.首先根据输入的IP地址发起请求
         * 3.如果数据正确就添加
         *
         */
        [MyAuth]
        [HttpPost]
        public async System.Threading.Tasks.Task<IHttpActionResult> AddEqAsync(EqVM model) {
            if (ModelState.IsValid) {
                try {
            //        string Url = @"http://" + model.EquipmentIP + ":8080/queryDeviceDetail";
           //         var result = HTTP.Http.Get(Url, "application / json");
                    //反序列化
             //       JObject ResponObj = JsonConvert.DeserializeObject<JObject>(result);
               //     if (ResponObj["errCode"].ToString() == "0") {
                 //       string deviceSerial = ResponObj["deviceSerial"].ToString();
                //        if (deviceSerial == model.EquipmentNum) {
                            //通过token获取用户名
                            string token = HttpContext.Current.Request.Headers["token"];
                            var userInfo = JwtTools.DEcode(token);
                            //调用usermanager.getuserinfo 获取用户信息
                            var user = UserManager.GetUserInfo(userInfo["name"]);
                            //查询此用户的学校ID，将其赋值给设备表的外键 ：SchoolId
                            await EqManager.AddEq( model.EquipmentNum, model.Local, user.organizationID);
                            return this.SendData("成功");
                        }
                        
                  
                
                catch (Exception ex) {
                    return this.ErrorData(ex.Message);
                }
            }
            else {
                return this.ErrorData("发生异常，请重新登录");
            }
        }

        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <returns></returns>
        [MyAuth]
        [HttpGet]
        public IHttpActionResult QueryEq() {
            if (ModelState.IsValid) {
                //通过token获取用户名
                string token = HttpContext.Current.Request.Headers["token"];
                var userInfo = JwtTools.DEcode(token);
                //调用usermanager.getuserinfo 获取用户信息
                var user = UserManager.GetUserInfo(userInfo["name"]);
                var result = EqManager.GetEq(user.organizationID);
                return this.SendData(result);
            }
            else {
                return this.ErrorData("发生错误");
            }
        }

        [HttpGet]
        [MyAuth]
        public IHttpActionResult DelEq(int Id, int SchoolId) {
            if (ModelState.IsValid) {
                try {
                    EqManager.DelEq(Id, SchoolId);
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
    }
}
