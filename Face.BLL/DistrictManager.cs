using Face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.BLL {
    public class DistrictManager {
        public static District QueryDistrict(int DistrictID) {
            using (DAL.DistrictService districtService = new DAL.DistrictService()) {
                return districtService.GetAll(i => i.Id == DistrictID).FirstOrDefault();
            }
        }

        public static List<District> QueryDistrictByCity(int CityID) {
            using (DAL.DistrictService districtService = new DAL.DistrictService()) {
                return districtService.GetAll(m => m.CityID == CityID).ToList();
            }
        }
    }
}
