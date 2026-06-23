using RegistroEventos.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrar el servicio único global en memoria
builder.Services.AddSingleton<IEventoService, EventoService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Participantes}/{action=Index}/{id?}");

app.Run();
