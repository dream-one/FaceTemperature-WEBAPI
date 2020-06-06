using Face.DAL;
using Face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.BLL {
    public class ClassManager {
        /// <summary>
        /// 创建班级
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="GradeName"></param>
        /// <returns></returns>
        public static async Task CreatClassAsync(string ClassName, string GradeName, int SchoolID) {
            try {
                using (ClassService graSvc = new ClassService()) {
                    //根据年级名获取年级对象
                    var Grade = GradeManager.QueryGrad(GradeName);
                    if (Grade == null) {
                        throw new Exception("没有这个年级");
                    }
                    if (ClassService.IsCunZai(ClassName, Grade.Id, SchoolID)) {
                        throw new Exception("已经存在相同名称的班级");
                    }
                    else {
                        await graSvc.CreatAsync(new Class() { Name = ClassName, GradeID = Grade.Id, SchoolID = SchoolID }, true);
                    }
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public static List<Class> GetClasses(string GradeName, int SchoolID) {

            //根据年级名获取年级对象
            var Grade = GradeManager.QueryGrad(GradeName);
            if (Grade == null) {
                throw new Exception("没有这个年级");
            }
            using (DAL.ClassService graSvc = new DAL.ClassService()) {
                return graSvc.GetAll(m => m.GradeID == Grade.Id && m.SchoolID == SchoolID && m.IsDelete == false).ToList();
            }


        }

        /// <summary>
        /// 通过班级名获取班级
        /// </summary>
        /// <param name="ClasseName"></param>
        /// <returns></returns>
        public static List<DTO.ClassDTO> QueryClass(string ClasseName) {
            using (DAL.ClassService classlService = new DAL.ClassService()) {
                return classlService.GetAll(i => i.Name == ClasseName).Select(m => new DTO.ClassDTO() {
                    Name = m.Name,
                    Id = m.Id,
                    GradeID = m.GradeID
                }).ToList();
            }
        }


        /// <summary>
        /// 查询班级信息，一般是初始化页面时调用
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <returns></returns>
        public static List<DTO.ClassDTO> QueryClassList(int SchoolID) {
            using (DAL.ClassService classlService = new DAL.ClassService()) {
                var result = classlService.GetAll(i => i.SchoolID == SchoolID).Select(m => new DTO.ClassDTO() {
                    Name = m.Name,
                    Id = m.Id,
                    GradeID = m.GradeID
                }).ToList();
                foreach (var item in result) {
                    var re = GradeManager.QueryGrad(item.GradeID);
                    item.GradeName = re.Name;
                    item.StudentCount = StudentManager.QueryStudentCount(item.Id);

                }
                return result;
            }
        }
    }
}
