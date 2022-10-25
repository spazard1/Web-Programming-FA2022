using CloudStorage.Services;
using Microsoft.AspNetCore.Builder;

namespace CloudStorage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IImageTableStorage, ImageTableStorage>();
            builder.Services.AddSingleton<IUserNameProvider, UserNameProvider>();
            builder.Services.AddSingleton<IBlobServiceClientProvider, BlobServiceClientProvider>();
            builder.Services.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();
            builder.Services.AddSingleton<KeyVaultProvider>();
            builder.Services.AddSingleton<SecretProvider>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseCors(policy =>
                policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
            );

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Services.GetRequiredService<IImageTableStorage>().StartupAsync();

            app.Run();
        }
    }
}