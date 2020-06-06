using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Face.Models {
    public class Student : BaseEntity {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public int Id { get; set; }

        public string Name { get; set; }
        public string ClassName { get; set; }
        public string GradeName { get; set; }
        public string StudentID { get; set; }//学号
        public string PhoneNum { get; set; }//电话

        public string Image { get; set; }//抓图url

        [ForeignKey(nameof(Class))]
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public string DeviceSerial { get; set; }
    }
}
