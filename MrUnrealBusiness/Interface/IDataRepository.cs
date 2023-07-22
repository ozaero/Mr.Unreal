using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrUnrealBusiness.DataRepository
{
    public interface IDataRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetOne(int id);

        void NewOne(T entity);

        void Put(T entity);

        void Delete(int id);
    }
}
