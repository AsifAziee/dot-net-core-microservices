using Edstem.Services.OrderAPI.Data;
using Edstem.Services.OrderAPI.Repository;
using Edstem.Services.OrderAPI.Repository.Impl;
using Microsoft.EntityFrameworkCore;
using Edstem.Services.OrderAPI.Messaging;
using Edstem.Services.OrderAPI.Extension;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var dbContext = builder.Services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IOrderHeaderRepository, OrderHeaderRepository>();


var optionBuilder = new DbContextOptionsBuilder<DataContext>();
optionBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnections"));
builder.Services.AddSingleton<IOrderHeaderRepository, OrderHeaderRepository>(u =>
    new OrderHeaderRepository(optionBuilder.Options));

builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();

builder.Services.AddSwaggerGen();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    using (var context = serviceScope.ServiceProvider.GetService<DataContext>())
    {
        context!.Database.Migrate();
    }
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
    options.DocumentTitle = "Order Swagger";
});

app.UseAzureServiceBusConsumer();
app.Run();