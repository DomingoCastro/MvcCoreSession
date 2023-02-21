var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();
//TAMBIEN DEBEMOS INDICAR E LTIEMPO QUE DURARA LA SESSION SIN UTILIZAR
builder.Services.AddSession(options =>
{
    options.IdleTimeout= TimeSpan.FromMinutes(10);
});
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
//DEBEMOS INDICAR AL SERVIDOR QUE UTILIZAMOS SESSION
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
