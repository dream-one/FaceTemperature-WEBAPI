using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaceServer.Models {
    public class Student {
        public int Id { get; set; }
        public string Name { get; set; }
        public string classValue { get; set; }
        public string gradeValue { get; set; }
        public string StudentID { get; set; }
        public string imageContent { get; set; }
        public string  Caller { get; set; }
        public int PhoneNum { get; set; }
        public string EquipmentIP { get; set; }
        public string EquipmentNum { get; set; }
    }
}