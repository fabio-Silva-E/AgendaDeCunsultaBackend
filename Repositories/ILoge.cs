using System;
using System.Collections.Generic;
using System.Linq;



namespace Repositories
{
    public interface ILoge<T> where T : class
    {
        List<T> get();
        string delete(string login);
     
         T getLogin(string login,string senha);

         void add(T entity);
        string update(string login,string senha, T entity);
    }
}
