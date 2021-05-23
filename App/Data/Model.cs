using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGenCSharp.App.Data
{
    public class Model
    {
        public int ID { get; set; }
        public string Platform { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public string Nickname { get; set; }

        public Model(int id, string platform, string email, string password, 
            string descr, string nick) {

            ID = id;
            Platform = platform;
            Email = email;
            Password = password;
            Description = descr;
            Nickname = nick;
        }

        public Model() {

        }
    }
}
