using CreditCardManagement.Models;
using System.Text.Json;
using System.IO;
using CreditCardManagement.Data;
using Microsoft.OpenApi.Models;

// Inicializaci�n del constructor de aplicaciones Web
var builder = WebApplication.CreateBuilder(args);

// Registro de controladores en el contenedor de servicios.
builder.Services.AddControllers();

// Registro de estructuras de datos como servicios singleton para garantizar una �nica instancia en toda la aplicaci�n.
builder.Services.AddSingleton<LinkedList>();
builder.Services.AddSingleton<BinarySearchTree>();
builder.Services.AddSingleton<TransactionStack>();
builder.Services.AddSingleton<CreditLimitRequestStack>();

// Configuraci�n para explorar puntos finales y generar documentaci�n Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CreditCardManagement", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

// Construcci�n de la aplicaci�n.
var app = builder.Build();

// Obtener la instancia de TransactionStack desde el contenedor de servicios
var transactionStack = app.Services.GetRequiredService<TransactionStack>();

// Configuraci�n de desarrollo espec�fica para manejo de errores y documentaci�n API.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = ""; // Establece el prefijo de ruta de la UI de Swagger en la ra�z
    });

    // Redirigir desde la ra�z a Swagger UI
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

// M�todo para cargar datos iniciales desde un archivo JSON.
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

            //si TransactionStack tiene un m�todo para agregar transacciones iniciales
            transactionStack.AddInitialTransactions(cards);
        }
        else
        {
            Console.WriteLine("Archivo JSON no encontrado en la ruta especificada: " + filePath);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ocurri� un error al cargar los datos iniciales: " + ex.Message);
    }
}