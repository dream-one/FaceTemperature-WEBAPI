using Face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.DAL {
    public class ClassService : BaseService<Class> {
        public ClassService() : base(new FaceContext()) {

        }
        /// <summary>
        /// 判断班级是否已经存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsCunZai(string name, int GradeID, int SchoolID) {
            using (FaceContext FC = new FaceContext()) {
                var result = FC.Set<Class>().Any(i => i.Name == name && i.SchoolID == SchoolID && i.GradeID == GradeID && i.IsDelete == false);
                return result;
            }
        }
    }
}
