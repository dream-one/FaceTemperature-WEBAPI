using Face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core;
namespace Face.DAL {
    public class UserService :BaseService<User>{
        public UserService():base(new FaceContext()) {

        }
    }
}
