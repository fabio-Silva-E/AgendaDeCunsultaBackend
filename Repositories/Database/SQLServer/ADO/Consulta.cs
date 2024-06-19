using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Repositories.Database.SQLServer.ADO
{
    public class Consulta : IRepository<Models.Consulta>
    {
        private readonly SqlConnection conn;
        private readonly SqlCommand cmd;


        public Consulta(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand();
        }
        public void add(Models.Consulta consulta)
        {
            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "insert into consulta (codigom,codigop,data) values (@codigom,@codigop,@data);select convert(int,@@identity) as idconsulta;"; //--SCOPE_IDENTITY()
                    cmd.Parameters.Add(new SqlParameter("@codigom", System.Data.SqlDbType.Int)).Value = consulta.Codigom;
                    cmd.Parameters.Add(new SqlParameter("@codigop", System.Data.SqlDbType.Int)).Value = consulta.Codigop;
                    cmd.Parameters.Add(new SqlParameter("@data", System.Data.SqlDbType.DateTime)).Value = consulta.Data;
                    consulta.IdConsulta = (int)cmd.ExecuteScalar();
                }
            }
        }

        public int delete(int id)
        {
            int linhasAfetadas = 0;

            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "delete from consulta where idconsulta = @idconsulta  ; ";
                    cmd.Parameters.Add(new SqlParameter("@idconsulta", System.Data.SqlDbType.Int)).Value = id;
                    linhasAfetadas = cmd.ExecuteNonQuery();
                }
            }

            return linhasAfetadas;
        }
        public List<Models.Consulta> get()
        {
            List<Models.Consulta> consultas = new List<Models.Consulta>();

            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "select idconsulta, codigom, codigop, data from consulta;";

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Models.Consulta consulta = new Models.Consulta();
                            consulta.IdConsulta = (int)dr["idconsulta"];
                            consulta.Codigom = (int)dr["codigom"];
                            consulta.Codigop = (int)dr["codigop"];
                            consulta.Data = (DateTime)dr["data"];


                            consultas.Add(consulta);
                        }
                    }
                }
                
            }
            return consultas;
        }
        public Models.Consulta getById(int id)
        {
            Models.Consulta consulta= new Models.Consulta();

            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "select idconsulta,codigom,codigop, data from consulta where idconsulta = @idconsulta;";
                    cmd.Parameters.Add(new SqlParameter("@idconsulta", System.Data.SqlDbType.Int)).Value = id;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            consulta.IdConsulta = (int)dr["idconsulta"];
                            consulta.Codigom = (int)dr["codigom"];
                            consulta.Codigop = (int)dr["codigop"];
                            consulta.Data = (DateTime)dr["data"];
                        }
                    }
                }
            }

            return consulta;
        }
        public int update(int id, Models.Consulta consulta)
        {
            int linhasAfetadas = 0;

            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "update consulta set data = @data where idconsulta = @idconsulta;";
                    //cmd.Parameters.Add(new SqlParameter("@codigop", System.Data.SqlDbType.Int)).Value = consulta.Codigop;
                    cmd.Parameters.Add(new SqlParameter("@data", System.Data.SqlDbType.DateTime)).Value = consulta.Data;
                    cmd.Parameters.Add(new SqlParameter("@idconsulta", System.Data.SqlDbType.Int)).Value = id;

                     linhasAfetadas = cmd.ExecuteNonQuery();
                }
            }

            return linhasAfetadas;
        }
        public Models.Consulta getNome(string nome)
        {
            return null ;
        }

        }
}