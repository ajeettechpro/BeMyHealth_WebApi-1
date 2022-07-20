using System.ComponentModel.DataAnnotations;

namespace BeMyHealth_WebApi.Models
{
    public class CustomDietPlan
    {
        [Key]
        public int SerialNo { get; set; }
        public string? DietName { get; set; }
        public string? Day { get; set; }
        public string? Food { get; set; }
        public string? Quantity { get; set; }
        public DateTime? DateTime { get; set; }

    }
}
