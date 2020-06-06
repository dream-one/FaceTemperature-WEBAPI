using Face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.DAL {
    public class GradeService : BaseService<Grade> {
        public GradeService() : base(new FaceContext()) {

        }
    }
}
