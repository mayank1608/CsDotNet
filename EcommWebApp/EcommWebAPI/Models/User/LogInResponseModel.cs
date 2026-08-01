namespace EcommWebAPI.Models.User
{
    public class LogInResponseModel
    {
        public string? UserName { get; set; }
        public string? AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
