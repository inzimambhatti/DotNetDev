using System;
using System.Collections.Generic;
using Catelog.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Catelog.Repositories
{
    public class InMemItemRepository:IInMemItemRepository
    {
    private readonly  List<Item> items =new()
    {
        new Item{Id=Guid.NewGuid(),Name ="Potion",Price=9,createdAt=DateTimeOffset.UtcNow},
        new Item{Id=Guid.NewGuid(),Name ="Sword",Price=12,createdAt=DateTimeOffset.UtcNow},
        new Item{Id=Guid.NewGuid(),Name ="Car",Price=120,createdAt=DateTimeOffset.UtcNow},

    };

    public IEnumerable<Item> GetItems()
    {
        return items;
    }
    public Item GetItem(Guid id)
    {
         var item=items.Where(item => item.Id == id).SingleOrDefault();
            return  item;
    }

        public IEnumerable<Item> GetIems()
        {
            throw new NotImplementedException();
        }

        public Item GetItem(int id)
        {
            
            throw new NotImplementedException();
        }
        public void CreateItem(Item item)
        {
            items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            var index=items.FindIndex(existingItem=>existingItem.Id==item.Id);
            items[index]=item;
        }

        public Item DeleteItem(Guid id)
        {
            
            throw new NotImplementedException();
        }
    }
}
