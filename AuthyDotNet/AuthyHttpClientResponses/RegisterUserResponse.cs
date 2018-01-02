namespace AuthyDotNet.AuthyHttpClientResponses
{
    public class RegisterUserResponse : AuthyResponse
    {
        public UserResponse User { get; set; }
        public int UserId => User.Id;

        public class UserResponse
        {
            public int Id { get; set; }
        }
    }
}
