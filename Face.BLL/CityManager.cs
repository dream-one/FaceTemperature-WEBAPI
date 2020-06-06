using Face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.BLL {
    public class CityManager {
        public static City QueryCity(int CityID) {
            using (DAL.CityService cityService = new DAL.CityService()) {
                return cityService.GetAll(i => i.Id == CityID).FirstOrDefault();
            }
        }
    }
}
