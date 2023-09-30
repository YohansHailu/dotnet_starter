using AutoMapper;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class UserView
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class UserFirstName
{
    public string JustName { get; set; }
}



public class UserProfile : Profile
{
    public UserProfile() =>
      CreateMap<User, UserView>();
}
public class UserProfile2 : Profile
{
    public UserProfile2() =>
      CreateMap<User, UserFirstName>();
}
