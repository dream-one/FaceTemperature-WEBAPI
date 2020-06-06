using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Face.Models {
    /// <summary>
    /// 人脸表
    /// </summary>
    public class Face:BaseEntity {
        [Key]
        public string FaceId { get; set; }
        public string Name { get; set; }
        public bool IsStudent { get; set; }
    }
}
