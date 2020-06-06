using Face.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.BLL {
    public class EqManager {
        public static async Task AddEq(string EquipmentNum, string Local, int SchoolID) {
            try {
                using (EqService es = new EqService()) {
                    await es.CreatAsync(new Models.Equipment() {  EquipmentNum = EquipmentNum, Local = Local, SchoolId = SchoolID }, true);
                }
            }
            catch (Exception ex) {

                throw;
            }

        }
        public static List<Models.Equipment> GetEq(int SchoolID) {
            try {
                using (EqService es = new EqService()) {
                    return es.GetAll(m => m.IsDelete == false && m.SchoolId == SchoolID, false).ToList();
                };
            }
            catch (Exception) {

                throw;
            }
        }

        /// <summary>
        /// 通过设备系列号查询学校ID
        /// </summary>
        /// <param name="EqNum"></param>
        /// <returns></returns>
        public static int GetSchoolIdByEqNum(string EqNum) {
            using (EqService eq = new EqService()) {
                var result = eq.GetAll(m => m.EquipmentNum == EqNum && m.IsDelete == false).FirstOrDefault();
                if (result != null) {
                    return result.SchoolId;
                }
                else {
                    throw new Exception("没有找到设备");
                }
            }
        }


        public static void DelEq(int Id, int SchoolId) {
            using (EqService es = new EqService()) {
                Models.Equipment eq = new Models.Equipment() { Id = Id };
                eq.IsDelete = true;
                es.Delete(eq);
            }
        }
    }
}
