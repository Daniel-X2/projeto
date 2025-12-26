
using static System.Console;




namespace api
{
    

    class Routers
    {
        public static void Router_Home(WebApplication app)
        {
            
            app.MapGet("/", () =>
            {
                WriteLine("alo");
                return "ola mano";
            });
            
        }
        
        
    }
    class Exec
    {
        public static void Main()
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder();
            var app = builder.Build();
            Routers.Router_Home(app);
            app.Run();
            
        }
    }
}


    


