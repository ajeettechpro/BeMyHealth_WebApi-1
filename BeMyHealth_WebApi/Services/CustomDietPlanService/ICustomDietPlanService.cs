using BeMyHealth_WebApi.Models;

namespace BeMyHealth_WebApi.Services.CustomDietPlanService
{
    public interface ICustomDietPlanService
    {
        Task<bool> AddCustomDietPlan(CustomDietPlan customDietPlan);
        Task<List<CustomDietPlan>> GetCustomDietPlan();
        Task<bool> DeleteDietPlan(int id);
        Task<bool> UpdateCustomDietPlan(CustomDietPlan customDietPlan);
    }
}
