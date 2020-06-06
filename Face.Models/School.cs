using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Face.Models {
    public class School : BaseEntity {
        public string Name { get; set; }
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 关联区
        /// </summary>
        [ForeignKey(nameof(District))]
        public int DistrictID { get; set; }
        public District District { get; set; }

        //public ICollection<EnterAndLeave> EnterAndLeaves { get; set; }
    }
}
