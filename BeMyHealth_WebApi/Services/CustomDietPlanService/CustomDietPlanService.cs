using BeMyHealth_WebApi.ContextData;
using BeMyHealth_WebApi.Dto;
using BeMyHealth_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeMyHealth_WebApi.Services.CustomDietPlanService
{
    public class CustomDietPlanService : ICustomDietPlanService
    {
        private readonly BeMyHealthDbContext _Context;
        public CustomDietPlanService (BeMyHealthDbContext Context)
        {
            _Context = Context;
        }
        public async Task<List<CustomDietPlan>> GetCustomDietPlan()
        {
            var data = await _Context.CustomDietPlans.ToListAsync();
            return data;    
        }

        public async Task<bool> DeleteDietPlan(int id)
        {
            var data = await _Context.CustomDietPlans.Where(x => x.SerialNo == id).FirstOrDefaultAsync();
            if(data != null)
            {
                _Context.CustomDietPlans.Remove(data);
                await _Context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> AddCustomDietPlan(CustomDietPlan customDietPlan)
        {
           var data=await _Context.CustomDietPlans.AddAsync(customDietPlan);
            if(data != null)
            {
                _Context.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateCustomDietPlan(CustomDietPlan customDietPlan)
        {
            var data = await _Context.CustomDietPlans.FirstOrDefaultAsync(x => x.SerialNo == customDietPlan.SerialNo);
            if(data != null)
            {
                data.DietName = customDietPlan.DietName;
                data.Day = customDietPlan.Day;
                data.Food = customDietPlan.Food;
                data.Quantity = customDietPlan.Quantity;
                data.DateTime = customDietPlan.DateTime;

                _Context.Update(data);
                _Context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
    }

