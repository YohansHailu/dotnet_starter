namespace clean_api;

public class Startup
{

    public void ConfigureServices(IServiceCollection services)
    {
        Console.WriteLine("hello what am I doing here");
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }
}
