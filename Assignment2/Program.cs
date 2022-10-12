using Assignment2.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Assignment2.Hubs;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DataContext")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "";
    });
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DataContext>();
    
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapControllers();

app.UseAuthorization();

app.MapRazorPages();
app.MapHub<MessageHub>("/messageHub");

app.Run();
