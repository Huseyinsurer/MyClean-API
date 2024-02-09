public class LoginResponse
{
    public bool IsSuccessful { get; set; }
    public string Token { get; set; }
    public Guid UserId { get; set; }
}