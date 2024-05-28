//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.DependencyInjection;
//using System.Linq;
//using System.Threading.Tasks;

//public class TokenMiddleware
//{
//    private readonly RequestDelegate _next;

//    public TokenMiddleware(RequestDelegate next)
//    {
//        _next = next;
//    }

//    public async Task Invoke(HttpContext context)
//    {
//        // Swagger endpoint'i isteği olduğunda middleware'i atla
//        if (context.Request.Path.StartsWithSegments("/swagger"))
//        {
//            await _next(context);
//            return;
//        }

//        // Token kontrolü yapılabilir
//        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

//        if (string.IsNullOrEmpty(token))
//        {
//            context.Response.StatusCode = 401;
//            await context.Response.WriteAsync("Token is missing.");
//            return;
//        }

//        // Token varsa, devam et
//        await _next(context);
//    }
//}

//public class Startup
//{
//    public void ConfigureServices(IServiceCollection services)
//    {
//        // Diğer servislerin eklenmesi

//        // Swagger servislerinin eklenmesi (burada eklemiyoruz)

//        // Diğer konfigürasyonlar
//    }

//    public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
//    {
//        // Ortamın özelliklerine göre hata yönetimi ve diğer middleware'lerin eklenmesi

//        app.UseMiddleware<TokenMiddleware>();

//        // Swagger UI'ın controller üzerinden servis edilmesi
//        app.UseMvc(routes =>
//        {
//            routes.MapRoute(
//                name: "swagger_route",
//                template: "swagger/{*path}",
//                defaults: new { controller = "Swagger", action = "Index" }
//            );
//        });

//        // Diğer middleware'lerin eklenmesi ve sonlandırılması
//    }
//}

//// Swagger UI'ını servis edecek olan controller
//public class SwaggerController : Controller
//{
//    public IActionResult Index()
//    {
//        return new RedirectResult("~/swagger/index.html");
//    }
//}
