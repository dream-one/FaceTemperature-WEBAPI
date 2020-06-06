using Face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.DAL {

    public class EqService : BaseService<Equipment> {
        public EqService() : base(new FaceContext()) {


        }
    }
}
