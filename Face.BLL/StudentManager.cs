using Face.DAL;
using Face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Face.DTO;
using System.Collections;

namespace Face.BLL {
    public class StudentManager {

        /// <summary>
        /// 创建学生
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ClassName"></param>
        /// <param name="GradeName"></param>
        /// <param name="phoneNum"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static async Task CreatStudentAsync(int Id, string name, string ClassName, string GradeName, string DeviceSerial, string StudentID = null, string caller = null, string phoneNum = null, string image = null) {
            try {
                using (DAL.StudentService stuSvc = new DAL.StudentService()) {
                    var Grad = GradeManager.QueryGrad(GradeName);
                    var Classes = ClassManager.QueryClass(ClassName);
                    foreach (var item in Classes) {
                        if (item.GradeID == Grad.Id) {
                            await stuSvc.CreatAsync(new Student() { Id = Id, Name = name, PhoneNum = phoneNum, Image = image, ClassId = item.Id, ClassName = ClassName, GradeName = GradeName, StudentID = StudentID, DeviceSerial = DeviceSerial }, true);
                        }
                        else {
                            continue;
                        }

                    }

                }
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }
        public static async Task EditStudentAsync(int Id, string Name, string ClassName, string GradeName, string StudentID, string image, string DeviceSerial, string PhoneNum = null) {
            using (StudentService student = new StudentService()) {
                var Grad = GradeManager.QueryGrad(GradeName);
                var Classes = ClassManager.QueryClass(ClassName);
                foreach (var item in Classes) {
                    if (item.GradeID == Grad.Id) {
                        await student.Edit(new Models.Student() {
                            Id = Id,
                            Name = Name,
                            ClassName = ClassName,
                            GradeName = GradeName,
                            StudentID = StudentID,
                            PhoneNum = PhoneNum,
                            DeviceSerial = DeviceSerial,
                            ClassId = item.Id,
                            Image = image
                        }, true);
                    }
                }

            }
        }

        /// <summary>
        /// 分页搜索查询学生对象
        /// </summary>
        /// <param name="SchoolID">学生所属的学校id</param>
        /// <param name="PageSize">一页的数量</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="Name">学生名字</param>
        /// <returns></returns>
        public static StudentDTO QueryCount(int SchoolID, int PageSize, int PageIndex, string Name = null) {
            #region 函数注释
            /*
         这个函数在做的事情：查询学生对象，分页是必须的，可以加名字搜索
         1.通过学校id获取这个学校所有的班
         2.定义一个count用于存放总学生的数量，arrlist暂存器，StudentDTO要返回的数据类
         4.遍历所有的班级,定义一个temp临时存放学生对象的数组
         3.判断要不要搜索名字
         5.第一层循环过后temp里面装的是一个班的学生
         6.因为没办法将数组合并只好再次遍历学生数组，再一一添加至arraylist
         7.合计学生总数量（每个班级都有一个学生数量）
         8.arrayList分页
         还需：学生Model里面添加班级名和年级名字段
         */
            #endregion
            using (DAL.StudentService student = new DAL.StudentService()) {
                var result = ClassManager.QueryClassList(SchoolID);
                int count = 0;
                ArrayList arrayList = new ArrayList();
                StudentDTO dTO = new StudentDTO();
                foreach (var item in result) {
                    List<Student> temp;
                    if (Name != null) {
                        temp = student.GetAll(m => m.ClassId == item.Id && m.IsDelete == false && m.Name == Name, true).ToList();//获取学生列表，temp是一个数组
                    }
                    else {

                        temp = student.GetAll(m => m.ClassId == item.Id && m.IsDelete == false, true).ToList();//获取学生列表，temp是一个数组
                    }
                    foreach (var students in temp) {//
                        students.ClassName = item.Name;
                        students.GradeName = item.GradeName;
                        arrayList.Add(students);
                    }
                    count += item.StudentCount;
                }
                dTO.arr = arrayList.ToArray().Skip(PageIndex * PageSize).Take(PageSize).ToArray();
                dTO.count = count;
                return dTO;
            }
        }

        public static string DeleteStudent(int Id) {
            try {
                using (StudentService studentService = new StudentService()) {
                    Student student = new Student();
                    student.Id = Id;
                    student.IsDelete = true;
                    studentService.Delete(student);
                    return "删除成功";
                }
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取学生的数量
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public static int QueryStudentCount(int ClassID) {
            using (DAL.StudentService student = new DAL.StudentService()) {
                return student.GetAll(m => m.ClassId == ClassID && m.IsDelete == false).Count();
            }
        }
        /// <summary>
        /// 获取最大的一个学生的ID
        /// </summary>
        /// <returns></returns>
        public static int GetStudentIdMax() {
            using (StudentService student = new StudentService()) {
                var reslut = student.GetAll();
                if (reslut.Count() == 0) return 0;
                int num = reslut.Select(m => m.Id).Max();
                return num;
            }
        }
    }

}
