using Face.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.BLL {
    public class AttendanceManager {
        public static async Task SetTimeAsync(string inTime, string outTime, int SchoolID) {
            using (AttendanceService attendance = new AttendanceService()) {
                var att = attendance.GetAll(m => m.SchoolID == SchoolID).FirstOrDefault();
                if (att != null) {
                    await attendance.Edit(new Models.Attendance() { Id = att.Id, InTime = DateTime.Parse(inTime), OutTime = DateTime.Parse(outTime), SchoolID = SchoolID }, true);
                }
                else {
                    attendance.Creat(new Models.Attendance() { InTime = DateTime.Parse(inTime), OutTime = DateTime.Parse(outTime), SchoolID = SchoolID });
                }
            }
        }

        public static Models.Attendance GetTime(int SchoolID) {
            using (AttendanceService attendance = new AttendanceService()) {
                var result = attendance.GetAll(m => m.SchoolID == SchoolID).FirstOrDefault();

                return result;

            }
        }
    }
}
