using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Face.Models {
    /// <summary>
    /// 年纪表
    /// </summary>
    public class Grade : BaseEntity {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        ///// <summary>
        ///// 关联区
        ///// </summary>
        //[ForeignKey(nameof(School))]
        //public int SchoolID { get; set; }
        //public School School { get; set; }
    }
}
