using Catelog.Entities;
using Catelog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catelog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsControllers : ControllerBase
    {
        private readonly IInMemItemRepository repository;
        public ItemsControllers(IInMemItemRepository repository)
        {
            this.repository= repository;
        }
        //Get/items    get all items
        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            var items=repository.GetItems();
            return items;
        }
        //Gget/ items get a single item
        [HttpGet("{id}")]
        public ActionResult <Item> GetItem(Guid id)  

          {
            var item =repository.GetItem(id);
            if(item is null){
                return NotFound();
            }
            return item;
        }    
        //Create Item
        [HttpPost]
       public ActionResult <Item>CreateItem(Item item){

           item=new(){
                Id=Guid.NewGuid(),
                Name=item.Name,
                Price=item.Price,
                createdAt=DateTimeOffset.UtcNow
             };
             repository.CreateItem(item);
             return CreatedAtAction(nameof(GetItem),new {id=item.Id},item);

        }
         //update Item
        [HttpPut("{id}")]
       public ActionResult <Item>UpdateItem(Guid id,Item item){
        var existingItem =repository.GetItem(id);
        if(existingItem is null){
            return NotFound();
        }


         Item  updatedItem=existingItem with{
                Name=item.Name,
                Price=item.Price,
                createdAt=DateTimeOffset.UtcNow
             };
             repository.UpdateItem(updatedItem);
             return NoContent();

        }
    }
}