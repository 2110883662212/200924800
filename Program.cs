using RegistroEventos.Services;

var builder = WebApplication.CreateBuilder(args);

// AGREGAR ESTA LÍNEA (Inyección de dependencias)
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

// Ruta por defecto dirigida a nuestro controlador
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Participantes}/{action=Index}/{id?}");

app.Run();