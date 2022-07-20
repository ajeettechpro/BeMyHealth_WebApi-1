using System.ComponentModel.DataAnnotations;

namespace BeMyHealth_WebApi.Models
{
    public class CustomSubscription
    {
        [Key]
        public int SubscriptionID { get; set; }
        public string? SubscriptionName { get; set; }
        public bool Status { get; set; }
        public DateTime Duration { get; set; } =DateTime.Now;
        public int Amount { get; set; }

    }
}
