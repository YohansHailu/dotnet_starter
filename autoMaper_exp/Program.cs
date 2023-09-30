using AutoMapper;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();


//app.MapGet("/", () => new User { Id = 1, FirstName = "John", LastName = "Doe" });

app.MapGet("/", (IMapper _mapper) =>
{
    var user = new User { Id = 1, FirstName = "John", LastName = "Doe" };
    var userViewModel = _mapper.Map<UserFirstName>(user);
    return userViewModel;

});

app.Run();
