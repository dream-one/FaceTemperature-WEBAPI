using Face.DAL;
using Face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.BLL {
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserManager {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="userid">userid</param>
        /// <returns></returns>
        public static bool Login(string name, string pwd) {
            using (var userSvc = new UserService()) {
                var user = userSvc.GetAll(m => m.Name == name && m.PassWord == pwd).FirstOrDefault();
                if (user == null) {
                    return false;
                }
                else {
                    return true;
                }
            }
        }

        /// <summary>
        /// 获取在账号信息
        /// </summary>
        /// <param name="Name">账号名</param>
        /// <returns>账号对象</returns>
        public static User GetUserInfo(string Name) {
            using (var userSvc = new UserService()) {
                var user = userSvc.GetAll(m => m.Name == Name).FirstOrDefault();
                if (user == null) {
                    throw new Exception("未查找到此用户");
                }
                else {

                    return user;
                }
            }
        }

        public static string GetOrganizationNameAsync(User user) {
            string organizationName = "";
            switch (user.Level) {
                case 1:
                    var school = SchoolManager.QuerySchool(user.organizationID);
                    organizationName = school.Name;
                    break;
                case 2:
                    var district = DistrictManager.QueryDistrict(user.organizationID);
                    organizationName = district.Name;
                    break;
                case 3:
                    var city = CityManager.QueryCity(user.organizationID);
                    organizationName = city.Name;
                    break;
            }
            return organizationName;
        }


    }
}
