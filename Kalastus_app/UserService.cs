using System.Collections.Generic;

public class UserService
{
    private readonly List<User> _users = new();

    public void RegisterUser(string name, string email, string password)
    {
        var user = new User(name, email, password);
        _users.Add(user);
    }

    public List<User> GetUsers()
    {
        return _users;
    }

    public class User
    {
        public string Name { get; }
        public string Email { get; }
        public string Password { get; }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}