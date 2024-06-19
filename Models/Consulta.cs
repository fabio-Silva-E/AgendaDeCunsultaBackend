using System;
using System.ComponentModel.DataAnnotations;


namespace Models
{
    public class Consulta
    {
        public int IdConsulta { get; set; }
        public int Codigom { get; set; }
        public int Codigop { get; set; }
        public DateTime? Data { get; set; }
        public Consulta()
         {
            this.IdConsulta = 0;
             this.Codigom = 0;
             this.Codigop = 0;
             this.Data = null;
         }
       /* public Consulta()
        {
        }
        public Consulta(int codigom, int codigop, DateTime? data)
        {
            Codigom = codigom;
            Codigom = codigop;
            Data = data;
        }*/
    }
}