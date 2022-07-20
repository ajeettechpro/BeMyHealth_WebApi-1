using BeMyHealth_WebApi.ContextData;
using BeMyHealth_WebApi.Dto;
using BeMyHealth_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeMyHealth_WebApi.Services.SubscriptionService
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly BeMyHealthDbContext _Context;
        public SubscriptionService(BeMyHealthDbContext Context)
        {
            _Context = Context;
        }

        public async Task<bool> DeleteSubscription(int id)
        {
            var data = await _Context.CustomSubscriptions.Where(x => x.SubscriptionID == id).FirstOrDefaultAsync();
            if(data != null)
            {
                _Context.CustomSubscriptions.Remove(data);
                await _Context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<CustomSubscription>> GetSubscription()
        {
           var data= await _Context.CustomSubscriptions.ToListAsync();
            if (data != null)
            {
                return data;
            }
            return null;
        }

        public async Task<bool> AddSubscription(CustomSubscription customSubscription)

        { 
            if(customSubscription != null)
            {
                await _Context.CustomSubscriptions.AddAsync(customSubscription);
                _Context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateSubscription(CustomSubscription customSubscription)
        {
            
            var result = await _Context.CustomSubscriptions.Where(x => x.SubscriptionID == customSubscription.SubscriptionID).FirstOrDefaultAsync();
            if (result != null)
            {
                result.SubscriptionName = customSubscription.SubscriptionName;
                result.Duration = customSubscription.Duration;
                result.Status = customSubscription.Status;
                result.Amount = customSubscription.Amount;  
                
                _Context.Update(result);
                _Context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
