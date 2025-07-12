using SurveyBasket.Authentication;

namespace Survey_Basket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddEntityFrameworkStores<AppDbContext>();
            builder.Services.Configure<JwtOptions>(
                builder.Configuration.GetSection("Jwt"));
            builder.Services.AddDependencies(builder.Configuration);
            TypeAdapterConfig.GlobalSettings.Scan(typeof(Program).Assembly);
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthorization();
            //app.MapIdentityApi<ApplicationUser>();
            app.MapControllers();
            app.UseExceptionHandler();

            app.Run();
        }
    }
}
