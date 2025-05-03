using System.ComponentModel;
using System.Linq;

namespace InventorySystem.Repositories
{
    public interface IGenericRepository<T> //where T:class
    {
        //CRUD Operation
        void Add(T obj);
        void Update(T obj);
        void Delete(int id);
        List<T> GetAll();
        T GetById(int id);
        void Save();


    }
}
