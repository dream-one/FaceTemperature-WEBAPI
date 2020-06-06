using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FaceServer.Models {
    public class EqVM {

        [Required]
        /// <summary>
        /// 设备序列号
        /// </summary>
        public string EquipmentNum { get; set; }


        public string Local { get; set; }//布设地点



    }
}