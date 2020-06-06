using Face.DAL;
using Face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.BLL {
    public class TeacherManager {
        public static async Task AddTeacherAsync(int Id, string Name, string PhoneNum, int SchoolID, string DeviceSerial, string image) {
            using (TeacherService teacher = new TeacherService()) {
                await teacher.CreatAsync(new Models.Teacher() { Id = Id, Name = Name, PhoneNum = PhoneNum, SchoolId = SchoolID, DeviceSerial = DeviceSerial, Image = image }, true);
            }
        }

        public static async Task EditTeacherAsync(int Id, string Name, string PhoneNum, int SchoolID, string DeviceSerial, string image) {
            using (TeacherService teacher = new TeacherService()) {
                await teacher.Edit(new Models.Teacher() { Id = Id, Name = Name, SchoolId = SchoolID, PhoneNum = PhoneNum, DeviceSerial = DeviceSerial, Image = image }, true);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static string DeleteTeacher(int Id) {
            try {
                using (TeacherService teacherService = new TeacherService()) {
                    Teacher teacher = new Teacher();
                    teacher.Id = Id;
                    teacher.IsDelete = true;
                    teacherService.Delete(teacher);
                    return "删除成功";
                }
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        public static List<Teacher> QueryTeacher(int SchoolID, int PageSize, int PageIndex, string Name = null) {
            using (TeacherService teacherService = new TeacherService()) {
                List<Teacher> rst;
                if (Name != null) {
                    rst = teacherService.GetAll(i => i.IsDelete == false && i.SchoolId == SchoolID && i.Name == Name, true, PageSize, PageIndex).ToList();
                }
                else {
                    rst = teacherService.GetAll(i => i.IsDelete == false && i.SchoolId == SchoolID, true, PageSize, PageIndex).ToList();
                }
                return rst;

            }


        }
        public static int QueryTeacherCount(int SchoolID) {
            using (TeacherService teacherService = new TeacherService()) {
                return teacherService.GetAll(i => i.IsDelete == false && i.SchoolId == SchoolID).Count();
            }
        }

        /// <summary>
        /// 获取最大的一个老师的ID
        /// </summary>
        /// <returns></returns>
        public static int GetTeacherIdMax() {
            using (TeacherService teacher = new TeacherService()) {
                var reslut = teacher.GetAll();
                if (reslut.Count() == 0) {
                    return 0;
                }
                int num = reslut.Select(m => m.Id).Max();
                return num;

            }
        }

    }
}
