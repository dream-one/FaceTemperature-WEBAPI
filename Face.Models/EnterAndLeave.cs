using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Face.Models {
    /// <summary>
    /// 进出记录表
    /// </summary>
    public class EnterAndLeave : BaseEntity {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DeviceSerial { get; set; }//设备系列号
        [Required]
        public string FaceId { get; set; }//人脸ID

        public string DeviceId { get; set; }//设备ID

        [Required]
        public string FaceName { get; set; }//名称

        [Required]
        public string AuthType { get; set; }//验证类型

        [Required]
        public string InOutType { get; set; }//进出类型 1 进  2 出

        public string SnapshotUrl { get; set; }//抓图URL

        public string SnapshotContent { get; set; }//抓图内容

        public float? Temperature { get; set; }//温度
        public string State { get; set; }//状态

        [Required]
        public string Time { get; set; }//通行时间

        //[ForeignKey(nameof(Face))]
        //public string FaceId { get; set; }

        //public Face Face { get; set; }



        [ForeignKey(nameof(School))]
        public int SchoolID { get; set; }
        public School School { get; set; }
    }
}
