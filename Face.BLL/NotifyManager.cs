using Face.DAL;
using Face.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.BLL {
    public class NotifyManager {
        public static async Task AddNotify(int SchoolID, string faceID, string deviceSerial, string faceName,
            string authType, string inOutType, string time, string State, string deviceID = null, string snapshotUrl = null, string snapshotConten = null, float? temperature = null) {
            try {
                using (var notifyService = new NotifyService()) {
                    await notifyService.CreatAsync(new Models.EnterAndLeave() {
                        DeviceSerial = deviceSerial,
                        FaceId = faceID,
                        FaceName = faceName,
                        AuthType = authType,
                        InOutType = inOutType,
                        Time = time,
                        DeviceId = deviceID,
                        SnapshotContent = snapshotConten,
                        SnapshotUrl = snapshotUrl,
                        Temperature = temperature,
                        SchoolID = SchoolID,
                        State = State,

                    }, true);
                }
            }
            catch (DbEntityValidationException ex) {
                Console.WriteLine(ex.Message); ;
            }
        }

        /// <summary>
        /// 通过名字查询进出记录
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="SchoolID"></param>
        /// <returns></returns>
        public static List<Models.EnterAndLeave> GetNotifyByName(int SchoolID, string Name = null, string StartTime = null, string EndTime = null) {
            try {
                using (var notifyService = new NotifyService()) {
                    var result = notifyService.GetAll(m => m.SchoolID == SchoolID).ToList();
                    if (StartTime != null && EndTime != null) {
                        result = result.Where(m => DateTime.Parse(m.Time) >= DateTime.Parse(StartTime) && DateTime.Parse(m.Time) <= DateTime.Parse(EndTime)).ToList();
                    }

                    if (Name != null) {
                        result = result.Where(m => m.FaceName == Name).ToList();
                    }
                    return result;
                };
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 分页查询进出记录
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SchoolID"></param>
        /// <returns></returns>
        public static List<Models.EnterAndLeave> GetNotify(int PageSize, int PageIndex, int SchoolID) {
            try {
                using (var notifyService = new NotifyService()) {
                    return notifyService.GetAll(m => m.SchoolID == SchoolID, false, PageSize, PageIndex).ToList();
                };
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }
        public static int GetNotifyCount() {
            try {
                using (var notifyService = new NotifyService()) {
                    return notifyService.GetAll().Count();
                };
            }
            catch (Exception) {

                throw;
            }
        }

        /// <summary>
        /// 分页查询进出记录
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SchoolID"></param>
        /// <returns></returns>
        public static List<Models.EnterAndLeave> GetNotifyBySchoolName(string SchoolName, string PageIndex = null, string PageSize = null) {
            try {
                using (var notifyService = new NotifyService()) {
                    var SchoolResult = SchoolManager.QuerySchool(SchoolName);
                    if (SchoolResult == null) throw new Exception("不存在此学校");
                    if (PageIndex == null) {
                        return notifyService.GetAll(m => m.SchoolID == SchoolResult.Id).ToList();
                    }
                    return notifyService.GetAll(m => m.SchoolID == SchoolResult.Id, false, Convert.ToInt32(PageSize), Convert.ToInt32(PageIndex)).ToList();
                };
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取过去七天的通行记录
        /// </summary>
        public static object GetNotifyByTime(int SchoolID) {
            using (NotifyService notifyService = new NotifyService()) {
                DateTime dt = DateTime.Now;
                ArrayList myArray = new ArrayList();//存放日期
                List<int> InOutType = new List<int>();//存放日期对应的数据
                for (int i = -7; i < 0; i++) {
                    var day = dt.AddDays(i).Day;
                    myArray.Add(day);
                    var notifyCount = notifyService.GetAll(m => m.IsDelete == false && m.SchoolID == SchoolID && m.CreatTime.Day == day).Count();
                    InOutType.Add(notifyCount);
                }
                object obj = new {
                    DateArr = myArray,
                    InOutType
                };
                return obj;
            }
        }

        /// <summary>
        /// 获取过去七天迟到早退情况
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <returns></returns>
        public static object GetNotifyStateByTime(int SchoolID) {
            using (NotifyService notifyService = new NotifyService()) {
                DateTime dt = DateTime.Now;
                ArrayList myArray = new ArrayList();
                ArrayList Late = new ArrayList();//迟到
                ArrayList LeaveEarly = new ArrayList();//早退
                ArrayList Normal = new ArrayList();//正常
                for (int i = -12; i < 0; i++) {
                    var month = dt.AddMonths(i).Month;
                    myArray.Add(month);
                    var notify = notifyService.GetAll(m => m.IsDelete == false && m.SchoolID == SchoolID && m.CreatTime.Month == month);
                    Late.Add(notify.Where(m => m.State == "迟到").Count());
                    LeaveEarly.Add(notify.Where(m => m.State == "早退").Count());
                    Normal.Add(notify.Where(m => m.State == "正常").Count());
                }
                object obj = new {
                    DateArr = myArray,
                    Late,
                    LeaveEarly,
                    Normal
                };
                return obj;
            }
        }

    }
}
