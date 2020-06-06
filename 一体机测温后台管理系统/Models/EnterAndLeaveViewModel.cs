using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FaceServer.Models {
    public class EnterAndLeaveViewModel {
        /// <summary>
        /// 设备系列号
        /// </summary>
        [Required]
        public string deviceSerial { get; set; }
        /// <summary>
        /// 设备 id（可选）
        /// </summary>
        public string deviceID { get; set; }
        /// <summary>
        /// 人脸 id
        /// </summary>
        [Required]
        public string faceID { get; set; }
        /// <summary>
        /// 人脸名称
        /// </summary>
        [Required]
        public string faceName { get; set; }
        /// <summary>
        /// 验证类型： ICCard：IC 卡 QRCode：二维码 FaceReco：人脸识别
        /// </summary>
        [Required]
        public string authType { get; set; }
        /// <summary>
        /// 进出类型。1：进；2：出
        /// </summary>
        [Required]
        public int inOutType { get; set; }
        /// <summary>
        /// 抓图的 URL。（与 snapshotContent 参数 2 选 1）
        /// </summary>
        public string snapshotUrl { get; set; }
        /// <summary>
        /// 抓图的内容，base64 编码。
        /// </summary>
        public string snapshotContent { get; set; }
        /// <summary>
        /// 温度。（可选，带测温功能的机型才 有）
        /// </summary>
        public float? temperature { get; set; }
        /// <summary>
        /// 人脸识别通行时间。时间格式为 “yyyy-MM-dd HH:mm:ss”
        /// </summary>
        [Required]
        public string time { get; set; }
    }
}