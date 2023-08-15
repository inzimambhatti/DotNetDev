using Catelog.Entities;

namespace Catelog.Repositories
{
    public interface IInMemItemRepository{
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
        void CreateItem(Item item);
        void UpdateItem(Item item);
        Item DeleteItem(Guid id);


    }
}