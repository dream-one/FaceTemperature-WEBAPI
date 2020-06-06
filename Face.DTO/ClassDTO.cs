using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.DTO {
    public class ClassDTO {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GradeID { get; set; }
        public string GradeName { get; set; }
        /// <summary>
        /// 班级人数
        /// </summary>
        public int StudentCount { get; set; }
    }
}
