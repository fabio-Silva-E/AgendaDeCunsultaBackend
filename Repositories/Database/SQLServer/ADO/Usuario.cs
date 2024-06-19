using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Database.SQLServer.ADO
{
    public class Usuario : ILoge<Models.Usuario>
    {
     
        private readonly SqlConnection conn;
        private readonly SqlCommand cmd;
        private readonly string chaveCache;
        public Usuario(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            chaveCache = "Usuario";
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
        public void add(Models.Usuario usuario)
        {
            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "insert into usuario (login, senha) values (@login,@senha);"; //--SCOPE_IDENTITY()
                    cmd.Parameters.Add(new SqlParameter("@login", System.Data.SqlDbType.VarChar)).Value = usuario.Login;
                    cmd.Parameters.Add(new SqlParameter("@senha", System.Data.SqlDbType.VarChar)).Value = usuario.Senha;

                    cmd.ExecuteScalar();
                }
            }
            Cache.delete(chaveCache);
        }
       public Models.Usuario getLogin(string login,string senha)
        {

            Models.Usuario usuario = new Models.Usuario();

            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM usuario WHERE login = @login AND senha = @senha;";
                    cmd.Parameters.Add(new SqlParameter("@login", System.Data.SqlDbType.VarChar)).Value = login;
                    cmd.Parameters.Add(new SqlParameter("@senha", System.Data.SqlDbType.VarChar)).Value = senha;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {


                            usuario.Login = (string)dr["login"];
                            usuario.Senha = (string)dr["senha"];



                        }
                    }
                }

            }

            return usuario;
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
                    cmd.CommandText = "delete from usuario where login = @login;";
                    cmd.Parameters.Add(new SqlParameter("@login", System.Data.SqlDbType.VarChar)).Value = login;

                    linhasAfetadas = cmd.ExecuteNonQuery();
                }
            }
            Cache.delete(chaveCache);
            return null;
        }
        public List<Models.Usuario> get()
        {

            List<Models.Usuario> usuarios= (List<Models.Usuario>)Cache.get(chaveCache);

            if (usuarios != null)
                return usuarios;
            usuarios = new List<Models.Usuario>();

            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "select login, senha from usuario;";

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Models.Usuario usuario = new Models.Usuario();
                            usuario.Login = (string)dr["login"];
                            usuario.Senha = (string)dr["senha"];

                            usuarios.Add(usuario);
                        }
                    }
                }
            }


            Cache.add(chaveCache, usuarios, 120);

            return usuarios;
        }
        public string update(string login,string senha, Models.Usuario usuario)
        {
            

            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "update usuario set senha = @senha where login = @login;";
                    cmd.Parameters.Add(new SqlParameter("@login", System.Data.SqlDbType.VarChar)).Value = usuario.Login;
                    cmd.Parameters.Add(new SqlParameter("@lsenha", System.Data.SqlDbType.VarChar)).Value = usuario.Senha;
                   
                  
                    cmd.ExecuteNonQuery();
                }
            }
            Cache.delete(chaveCache);
            return null;
        }

    }
}