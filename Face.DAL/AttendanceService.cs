using Face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.DAL {
    public class AttendanceService : BaseService<Attendance> {
        public AttendanceService() : base(new FaceContext()) {

        }
    }
}
