using BeMyHealth_WebApi.Dto;
using BeMyHealth_WebApi.Models;

namespace BeMyHealth_WebApi.Services.SubscriptionService
{
    public interface ISubscriptionService
    {
        Task<bool> AddSubscription(CustomSubscription customSubscription);
        Task<List<CustomSubscription>> GetSubscription();
        Task<bool> DeleteSubscription(int id);
        Task<bool> UpdateSubscription(CustomSubscription customSubscription);
    }
}
