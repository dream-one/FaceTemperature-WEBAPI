using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Face.Models {
    public class FaceContext : DbContext {
        public FaceContext() : base("name=face") {

        }

        public DbSet<Student> Students { get; set; }//学生
        public DbSet<School> Schools { get; set; }//学校
        public DbSet<Grade> Grades { get; set; }//年级
        //public DbSet<Face> Faces { get; set; }
        public DbSet<Equipment> Equipment { get; set; }//设备
        public DbSet<EnterAndLeave> EnterAndLeaves { get; set; }//进出记录
        public DbSet<Class> Classes { get; set; }//班级
        public DbSet<User> Users { get; set; }//用户
        public DbSet<District> Districts { get; set; }//区
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Attendance> Attendances { get; set; }//考勤表
    }
}
