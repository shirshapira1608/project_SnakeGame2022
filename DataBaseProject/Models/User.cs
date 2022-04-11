using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProject.Models
{               
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int CurrentCharacter { get; set; }
        public int Coins { get; set; }
        public int MaxScore { get; set; }
        public string Mail { get; set; }
        public int CurrentFood { get; set; }
        public int CurrentBackground { get; set; }
    }
}
