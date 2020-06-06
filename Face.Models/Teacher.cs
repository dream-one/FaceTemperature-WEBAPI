using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.Models {
    public class Teacher : BaseEntity {
        [Key]
      [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNum { get; set; }

        public string Image { get; set; }//抓图url

        [ForeignKey(nameof(School))]
        public int SchoolId { get; set; }
        public School School { get; set; }
        public string DeviceSerial { get; set; }
    }
}
