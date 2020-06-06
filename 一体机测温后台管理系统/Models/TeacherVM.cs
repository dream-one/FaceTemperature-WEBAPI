using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaceServer.Models {
    public class TeacherVM {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public string EquipmentNum { get;  set; }
        public string imageContent { get;  set; }
        public int SchoolID { get;  set; }
    }
}