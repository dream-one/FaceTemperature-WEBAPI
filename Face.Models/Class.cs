using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Face.Models {
    public class Class:BaseEntity {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 是否毕业
        /// </summary>
        public bool IsGraduation { get; set; }

        /// <summary>
        /// 年纪编号
        /// </summary>
        [ForeignKey(nameof(Grade))]
        public int GradeID { get; set; }
        public Grade Grade { get; set; }

        [ForeignKey(nameof(School))]
        public int SchoolID { get; set; }
        public School School { get; set; }
    }
}
