using Face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.DAL {
    public class CityService : BaseService<City> {
        public CityService() : base(new FaceContext()) {

        }
    }
}
