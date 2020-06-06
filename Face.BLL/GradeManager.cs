using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.BLL {
    public class GradeManager {

        /// <summary>
        /// 创建年级 
        /// </summary>
        /// <param name="GradeName">年级名</param>
        /// <param name="SchoolName">学校名</param>
        /// <returns></returns>
        public static async Task CreatGrade(string GradeName, int SchoolID) {
            //根据学校名获取学校对象
            var School =  SchoolManager.QuerySchool(SchoolID);
            if (School == null) {
                throw new Exception("没有这个学校");
            }
            using (DAL.GradeService graSvc = new DAL.GradeService()) {
                await graSvc.CreatAsync(new Models.Grade() { Name = GradeName }, true);
            }
        }
        /// <summary>
        /// 根据ID查询年级对象
        /// </summary>
        /// <param name="GradeName">年级ID</param>
        /// <returns></returns>
        public static DTO.GradDTO QueryGrad(int GradeID) {
            using (DAL.GradeService gradelService = new DAL.GradeService()) {
                return gradelService.GetAll(i => i.Id == GradeID).Select(m => new DTO.GradDTO() {
                    Name = m.Name,
                    Id = m.Id
                }).FirstOrDefault();

            }
        }

        /// <summary>
        /// 根据年纪名查询年纪对象
        /// </summary>
        /// <param name="GradeName"></param>
        /// <returns></returns>
        public static DTO.GradDTO QueryGrad(string GradeName) {
            using (DAL.GradeService gradelService = new DAL.GradeService()) {
                return gradelService.GetAll(i => i.Name == GradeName).Select(m => new DTO.GradDTO() {
                    Name = m.Name,
                    Id = m.Id
                }).FirstOrDefault();

            }
        }
    }
}
