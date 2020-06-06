using System;

namespace Face.Models {
    public class BaseEntity {
        public DateTime CreatTime { get; set; }=DateTime.Now;//默认当前
        public bool IsDelete { get; set; } = false;
    }
}
