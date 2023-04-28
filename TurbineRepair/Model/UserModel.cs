using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurbineRepair.Model
{
    internal class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronomyc { get; set; }
        public int? Role { get; set; }
        public int? Deportament { get; set; }
        public int? Post { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
