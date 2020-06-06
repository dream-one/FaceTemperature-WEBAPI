using Face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.DAL {
    public class SchoolService:BaseService<School> {
        public SchoolService():base(new FaceContext()) {

        }
    }
}
