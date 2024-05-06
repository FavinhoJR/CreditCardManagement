using CreditCardManagement.Models;
using System.Text.Json;
using System.IO;
using CreditCardManagement.Data;
using Microsoft.OpenApi.Models;

// Inicialización del constructor de aplicaciones Web
var builder = WebApplication.CreateBuilder(args);

// Registro de controladores en el contenedor de servicios.
builder.Services.AddControllers();

// Registro de estructuras de datos como servicios singleton para garantizar una única instancia en toda la aplicación.
builder.Services.AddSingleton<LinkedList>();
builder.Services.AddSingleton<BinarySearchTree>();
builder.Services.AddSingleton<TransactionStack>();
builder.Services.AddSingleton<CreditLimitRequestStack>();

// Configuración para explorar puntos finales y generar documentación Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CreditCardManagement", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

// Construcción de la aplicación.
var app = builder.Build();

// Obtener la instancia de TransactionStack desde el contenedor de servicios
var transactionStack = app.Services.GetRequiredService<TransactionStack>();

// Configuración de desarrollo específica para manejo de errores y documentación API.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = ""; // Establece el prefijo de ruta de la UI de Swagger en la raíz
    });

    // Redirigir desde la raíz a Swagger UI
    app.Use(async (context, next) =>
    {
        if (context.Request.Path == "/")
        {
            context.Response.Redirect("/swagger", permanent: false);
            return;
        }
        await next();
    });
}

// Se asegura de que el archivo existe antes de cargarlo
LoadInitialData(app.Environment.ContentRootPath, transactionStack);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();

// Método para cargar datos iniciales desde un archivo JSON.
void LoadInitialData(string basePath, TransactionStack transactionStack)
{
    try
    {
        var filePath = Path.Combine(basePath, "initialData.json");
        Console.WriteLine("Intentando cargar el archivo JSON desde: " + filePath);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            var cards = JsonSerializer.Deserialize<List<CreditCard>>(json);

            //si TransactionStack tiene un método para agregar transacciones iniciales
            transactionStack.AddInitialTransactions(cards);
        }
        else
        {
            Console.WriteLine("Archivo JSON no encontrado en la ruta especificada: " + filePath);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ocurrió un error al cargar los datos iniciales: " + ex.Message);
    }
}