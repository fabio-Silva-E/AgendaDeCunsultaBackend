using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Usuarioad
    {
       

        public string Login { get; set; }
      public string Senha { get; set; }
    public Usuarioad()
        {
            this.Login = "";
            this.Senha = "";
           
        }

    }
}
