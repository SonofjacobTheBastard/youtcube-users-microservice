
namespace YoutCubeUsersMicroservice.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }

        public User(string username, string email, string password)
        {
            Id = new Guid();
            Username = username;
            Email = email;

        }
    }
}