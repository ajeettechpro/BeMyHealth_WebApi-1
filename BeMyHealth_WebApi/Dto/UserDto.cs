namespace BeMyHealth_WebApi.Dto
{
    public class UserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DOB { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
