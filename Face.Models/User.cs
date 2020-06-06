using Face.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.Models {
    public class User : BaseEntity { 
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PassWord { get; set; }

        [Required]
        /// <summary>
        /// 等级 1:校级 2：区级 3：市级
        /// </summary>
        public int Level { get; set; }

        [Required]
        /// <summary>
        /// 所属单位ID 
        /// </summary>
        public int organizationID { get; set; }
    }
}
