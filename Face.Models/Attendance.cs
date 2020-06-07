using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.Models {
    /// <summary>
    /// 考勤表
    /// </summary>
    public class Attendance : BaseEntity {
        public int Id { get; set; }

        /// <summary>
        /// 出去时间
        /// </summary>
        public DateTime OutTime { get; set; }
        /// <summary>
        /// 进入时间
        /// </summary>
        public DateTime InTime { get; set; }

        [ForeignKey(nameof(School))]
        public int SchoolID { get; set; }
        public School School { get; set; }
    }
}
