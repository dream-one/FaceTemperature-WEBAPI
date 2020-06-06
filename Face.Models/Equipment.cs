using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Face.Models {
    /// <summary>
    /// 设备表
    /// </summary>
    public class Equipment : BaseEntity {



        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 设备序列号
        /// </summary>
        public string EquipmentNum { get; set; }

        /// <summary>
        /// 设备IP地址
        /// </summary>
        public string EquipmentIP { get; set; }

        public string Local { get; set; }//布设地点


        [ForeignKey(nameof(School))]
        public int SchoolId { get; set; }
        public School School { get; set; }
    }
}
