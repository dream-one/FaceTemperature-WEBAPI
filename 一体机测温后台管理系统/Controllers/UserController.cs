using Face.BLL;
using FaceServer.Filters;
using FaceServer.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Entity.Core;
using System.Web.Http;
using System.Web;

namespace FaceServer.Controllers {
    public class UserController : ApiController {
        /// <summary>
        /// 登录action
        /// </summary>
        /// <param name="loginViewModel">登录参数模型</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Login(Models.LoginViewModel loginViewModel) {
            if (ModelState.IsValid) {
                try {
                    var result = UserManager.Login(loginViewModel.LoginName, loginViewModel.LoginPwd);
                    if (result) {
                        var token = JwtTools.Encode(new Dictionary<string, string>() {
                        {"name",loginViewModel.LoginName }
                    });
                        return this.SendData(token);
                    }
                    else {
                        return this.ErrorData("账号密码错误");
                    }
                }
                catch (Exception ex) {

                    throw new Exception(ex.Message);
                }
            }
            else {
                return this.ErrorData("发生异常");
            }
        }

        /// <summary>
        /// 获取用户登录信息比如所属的学校、区、 角色等
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [MyAuth]
        public IHttpActionResult GetUserInfo() {
            if (ModelState.IsValid) {
                string token = HttpContext.Current.Request.Headers["token"];
                var result = JwtTools.DEcode(token);
                try {
                    var user = UserManager.GetUserInfo(result["name"]);
                    var organizationName = UserManager.GetOrganizationNameAsync(user);
                    var respon = new { user, organizationName };
                    return this.SendData(respon);
                }
                catch {
                    return this.ErrorData("用户名错误");
                }
            }
            else {
                return this.ErrorData("权限错误");
            }
        }

        /// <summary>
        /// 获取左侧菜单  根据账号等级
        /// </summary>
        /// <param name="Level"></param>
        /// <returns></returns>
        [HttpGet]
        [MyAuth]
        public IHttpActionResult GetMenu(int Level) {
            if (ModelState.IsValid) {
                try {
                    switch (Level) {
                        case 1:
                            object[] children = new object[] { new { name = "在职人员管理", id = "teacherMan" }, new { name = "学生管理", id = "studentMan" }, new { name = "班/年级管理", id = "classGradeMan" } ,new { name="考勤管理",id="attendances"} };
                            object[] data = new object[] { new { name = "首页", id = "home", icon = "el-icon-s-home" },
                                new { name = "设备管理", id = "equiment", icon = "el-icon-s-platform"},
                                new { name = "进出记录", id = "notify", icon = "el-icon-s-data" },
                                new{name = "校园模块", id = "3", icon = "el-icon-user", children }
                            };
                            return this.SendData(data);
                        case 2:
                            object[] data2 = new object[] { new { name = "首页", id = "home", icon = "el-icon-s-home" }, new { name = "人脸识别数据汇总", id = "all-notify", icon = "el-icon-s-data" } };
                            return this.SendData(data2);
                        case 3:
                            object[] data3 = new object[] { new { name = "首页", id = "home", icon = "el-icon-s-home" }, new { name = "人脸识别数据汇总", id = "all-notify", icon = "el-icon-s-data" } };
                            return this.SendData(data3);
                    }
                    return this.ErrorData("未找到用户");
                }
                catch {
                    return this.ErrorData("用户名错误");
                }
            }
            else {
                return this.ErrorData("权限错误");
            }
        }


    }
}
