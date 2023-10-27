using Microsoft.EntityFrameworkCore;
using RestaurantWebAPI.Data;
using RestaurantWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Repository
{
    public class FoodRepository : IFoodRepository
    {
        private readonly PubContext pubContext;

        public FoodRepository(PubContext pubContext)
        {
            this.pubContext = pubContext;
        }
        public async Task<Food> AddFood(Food food)
        {
            var result = await pubContext.Foods.AddAsync(food);
            await pubContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteFood(int foodId)
        {
            var result = await pubContext.Foods.FirstOrDefaultAsync(e => e.FoodId == foodId);
            if(result!=null)
            {
                pubContext.Foods.Remove(result);
                await pubContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Food>> GetFoods()
        {
            return await pubContext.Foods.ToListAsync();
        }

        public async Task<Food> UpdateFood(Food food)
        {
            var result = await pubContext.Foods.FirstOrDefaultAsync(e => e.FoodId == food.FoodId);
            if(result!=null)
            {
                result.FoodName = food.FoodName;
                result.price = food.price;
                if(food.FoodId!=0)
                {
                    result.FoodId = food.FoodId;
                }
                await pubContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
