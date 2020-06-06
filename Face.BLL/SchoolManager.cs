using Face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.BLL {
    public class SchoolManager {
        /// <summary>
        /// 根据学校名获取学校对象
        /// </summary>
        /// <param name="schoolName"></param>
        /// <returns></returns>
        public static DTO.SchoolDTO QuerySchool(int SchoolID) {
            using (DAL.SchoolService schoolService = new DAL.SchoolService()) {
                 return  schoolService.GetAll(i => i.Id == SchoolID).Select(m => new DTO.SchoolDTO() {
                    Name = m.Name,
                    Id = m.Id
                }).FirstOrDefault();

            }
        }
        public static DTO.SchoolDTO QuerySchool(string SchollName) {
            using (DAL.SchoolService schoolService = new DAL.SchoolService()) {
                return schoolService.GetAll(i => i.Name == SchollName).Select(m => new DTO.SchoolDTO() {
                    Name = m.Name,
                    Id = m.Id
                }).FirstOrDefault();

            }
        }
    }
}
