using Face.DAL;
using Face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Face.DTO;
namespace Face.BLL {
    public class OptionsManager {

        /// <summary>
        /// 查询属于这个区的所有学校
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public static List<School> GetSchoolsByD(int organizationID) {
            using (SchoolService school = new SchoolService()) {
                return school.GetAll(m => m.IsDelete == false && m.DistrictID == organizationID).ToList();
            }
        }
        /// <summary>
        /// 查询某个市的所有学校
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public static Array GetSchoolsByC(int organizationID) {
            //得到城市ID，首先获取这个城市所有的区的ID，遍历区获取所有的学校
            using (SchoolService school = new SchoolService()) {
                var district = DistrictManager.QueryDistrictByCity(organizationID);
                List<OptionDTO> optionDTOs = new List<OptionDTO>();
                foreach (var item in district) {
                    OptionDTO o = new OptionDTO();
                    var result = school.GetAll(m => m.IsDelete == false && m.DistrictID == item.Id).ToArray();
                    if (result.Length == 0) continue;
                    o.label = item.Name;
                    o.options = result;
                    optionDTOs.Add(o);
                }
                return optionDTOs.ToArray();
            }
        }
    }
}
