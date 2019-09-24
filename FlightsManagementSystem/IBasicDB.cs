using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public interface IBasicDB<T> where T : IPoco
    {
        T Get(int id);       
        IList<T> GetAll();
        void Add(T t);
        void Remove(T t);
        void Update(T t);
    }
}
