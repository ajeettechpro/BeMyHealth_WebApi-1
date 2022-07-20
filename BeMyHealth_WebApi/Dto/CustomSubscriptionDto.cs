using System.ComponentModel.DataAnnotations;

namespace BeMyHealth_WebApi.Dto
{
    public class CustomSubscriptionDto
    {
       
        public string? SubscriptionName { get; set; }
        public bool Status { get; set; }
        public DateTime Duration { get; set; } = DateTime.Now;
        public int Amount { get; set; }
    }
}
