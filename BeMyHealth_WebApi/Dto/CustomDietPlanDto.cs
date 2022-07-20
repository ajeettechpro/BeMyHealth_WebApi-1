using System.ComponentModel.DataAnnotations;

namespace BeMyHealth_WebApi.Dto
{
    public class CustomDietPlanDto
    {
        public string? DietName { get; set; }
        public string? Day { get; set; }
        public string? Food { get; set; }
        public string? Quantity { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
