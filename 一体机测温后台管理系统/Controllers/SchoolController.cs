using Face.BLL;
using FaceServer.Filters;
using FaceServer.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FaceServer.Controllers {

    public class SchoolController : ApiController {

        /// <summary>
        /// 创建班级
        /// </summary>
        /// <param name="GradeName">年级名</param>
        /// <param name="ClassName">班级名</param>
        /// <returns></returns>
        [HttpGet]
        [MyAuth]
        [Route("api/CreatClass")]
        public async System.Threading.Tasks.Task<IHttpActionResult> CreatClassAsync(string GradeName, string ClassName) {
            /*
             * 首先字段修改：年级去掉学校ID字段。班级新增学校ID字段
             * 前端传入年级和班级的名字
             * 通过账号信息，获取所在的学校ID
             * 通过年级名字，查询年级ID
             * 创建班级对象，ClassName直接赋值，学校ID赋值，年级ID赋值，是否毕业:false
             * 成功则返回成功
             * */
            if (ModelState.IsValid) {
                try {
                    string token = HttpContext.Current.Request.Headers["token"];
                    var userInfo = JwtTools.DEcode(token);
                    var user = UserManager.GetUserInfo(userInfo["name"]);
                    await ClassManager.CreatClassAsync(ClassName, GradeName, user.organizationID);
                    return this.SendData(true);
                }
                catch (Exception ex) {

                    return this.ErrorData(ex.Message);
                }
            }
            else {
                return this.ErrorData("发生异常");
            }
        }


        /// <summary>
        /// 获取班级信息
        /// </summary>
        /// <returns>班级名，所属年级名，学校名？，学生人数</returns>
        [HttpGet]
        [MyAuth]
        [Route("api/QueryClass")]
        public IHttpActionResult QueryClass() {
            if (ModelState.IsValid) {
                string token = HttpContext.Current.Request.Headers["token"];
                var userInfo = JwtTools.DEcode(token);
                //调用usermanager.getuserinfo 获取用户信息
                var user = UserManager.GetUserInfo(userInfo["name"]);
                var result = ClassManager.QueryClassList(user.organizationID);

                return this.SendData(result);
            }
            else {
                return this.ErrorData("请重新登录");
            }

        }

        /// <summary>
        /// 通过年级名获取班级名
        /// </summary>
        /// <param name="GradeName"></param>
        /// <returns></returns>
        [HttpGet]
        [MyAuth]
        [Route("api/QueryClassByGradeName")]
        public IHttpActionResult GetClassName(string GradeName) {
            if (ModelState.IsValid) {
                try {
                    string token = HttpContext.Current.Request.Headers["token"];
                    var userInfo = JwtTools.DEcode(token);
                    //调用usermanager.getuserinfo 获取用户信息
                    var user = UserManager.GetUserInfo(userInfo["name"]);
                    var result = ClassManager.GetClasses(GradeName, user.organizationID);
                    return this.SendData(result);
                }
                catch (Exception ex) {

                    return this.ErrorData(ex.Message);
                }

            }
            else {
                return this.ErrorData("重新登录");
            }
        }
    }
}
