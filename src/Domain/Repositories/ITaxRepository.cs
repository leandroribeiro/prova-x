using Domain.Entities;

namespace Domain.Repositories
{
    public interface ITaxRepository
    {
        bool Update(Tax model);
        Tax GetByName(string name);
    }
}