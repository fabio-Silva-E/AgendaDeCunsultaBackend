using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Database.SQLServer.ADO
{
    public class Usuarioad : ILoge<Models.Usuarioad>
    {
     
        private readonly SqlConnection conn;
        private readonly SqlCommand cmd;
        private readonly string chaveCache;
        public Usuarioad(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            chaveCache = "usuarioad";
        }
        /*   public void add(Models.Usuario usuario) { }
           public int delete(int id) {
               return id;
           }
           public List<Models.Usuario> get() {
               return null;
           }
           public Models.Usuario getNome(string nome) {
               return null;
           }
           public Models.Usuario getById(int id) {
               return null;
           }
           public int update(int id, Models.Usuario usuario) {
               return id;
           }*/
        public void add(Models.Usuarioad usuarioad)
        {
            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "insert into usuarioad (login, senha) values (@login,@senha);"; //--SCOPE_IDENTITY()
                    cmd.Parameters.Add(new SqlParameter("@login", System.Data.SqlDbType.VarChar)).Value = usuarioad.Login;
                    cmd.Parameters.Add(new SqlParameter("@senha", System.Data.SqlDbType.VarChar)).Value = usuarioad.Senha;

                    cmd.ExecuteScalar();
                }
            }
            Cache.delete(chaveCache);
        }
       public Models.Usuarioad getLogin(string login,string senha)
        {

            Models.Usuarioad usuarioad = new Models.Usuarioad();

            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM usuarioad WHERE login = @login AND senha = @senha;";
                    cmd.Parameters.Add(new SqlParameter("@login", System.Data.SqlDbType.VarChar)).Value = login;
                    cmd.Parameters.Add(new SqlParameter("@senha", System.Data.SqlDbType.VarChar)).Value = senha;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {


                            usuarioad.Login = (string)dr["login"];
                            usuarioad.Senha = (string)dr["senha"];



                        }
                    }
                }

            }

            return usuarioad;
        }
        public string delete(string login)
        {
            int linhasAfetadas = 0;

            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "delete from usuarioad where login = @login;";
                    cmd.Parameters.Add(new SqlParameter("@login", System.Data.SqlDbType.VarChar)).Value = login;

                    linhasAfetadas = cmd.ExecuteNonQuery();
                }
            }
            Cache.delete(chaveCache);
            return null;
        }
        public List<Models.Usuarioad> get()
        {

            List<Models.Usuarioad> usuarioads= (List<Models.Usuarioad>)Cache.get(chaveCache);

            if (usuarioads != null)
                return usuarioads;
            usuarioads = new List<Models.Usuarioad>();

            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "select login, senha from usuarioad;";

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Models.Usuarioad usuarioad = new Models.Usuarioad();
                            usuarioad.Login = (string)dr["login"];
                            usuarioad.Senha = (string)dr["senha"];

                            usuarioads.Add(usuarioad);
                        }
                    }
                }
            }


            Cache.add(chaveCache, usuarioads, 120);

            return usuarioads;
        }
        public string update(string login,string senha, Models.Usuarioad usuarioad)
        {
            

            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "update usuarioad set senha = @senha where login = @login;";
                    cmd.Parameters.Add(new SqlParameter("@login", System.Data.SqlDbType.VarChar)).Value = usuarioad.Login;
                    cmd.Parameters.Add(new SqlParameter("@lsenha", System.Data.SqlDbType.VarChar)).Value = usuarioad.Senha;
                   
                  
                    cmd.ExecuteNonQuery();
                }
            }
            Cache.delete(chaveCache);
            return null;
        }

    }
}