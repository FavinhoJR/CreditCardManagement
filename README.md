# CreditCardManagement


CreditCardManagement - Documentación para Ejecución

Requisitos Previos:

Asegúrate de tener instalado .NET 6.0 o superior.
El IDE recomendado es Visual Studio 2022 o posterior, aunque puedes usar también Visual Studio Code.
Pasos para ejecutar el programa:

Clonar el repositorio:
Clona el repositorio usando Git con el comando:
bash
Copy code
git clone https://github.com/FavinhoJR/CreditCardManagement.git
Navega al directorio del proyecto.
Abrir el proyecto:
Abre el archivo de solución (.sln) en Visual Studio.
Instalar dependencias:
Asegúrate de que todas las dependencias necesarias están instaladas. Visual Studio normalmente las restaura automáticamente, pero puedes hacerlo manualmente desde la consola de NuGet con:
Copy code
dotnet restore
Ejecutar la aplicación:
Ejecuta el proyecto desde Visual Studio usando Ctrl + F5 para evitar la pausa de la consola si es una aplicación de consola.
Si estás usando Visual Studio Code o una terminal, puedes ejecutar:
arduino
Copy code
dotnet run
Acceder a Swagger UI:
Una vez que la aplicación está corriendo, navega a http://localhost:5018/swagger en tu navegador para acceder a la interfaz de usuario de Swagger, donde podrás probar todas las APIs disponibles.
Pruebas de las APIs con Swagger:

Carga Inicial de Datos:
POST /api/CreditCard/load: Carga los datos de tarjetas de crédito desde un archivo JSON.
Consulta de Saldo:
GET /api/CreditCard/balance/{cardNumber}: Obtiene el saldo actual de una tarjeta específica.
Realización de Pagos:
POST /api/Payment/makePayment: Permite realizar un pago con una tarjeta de crédito.
Generación de Estados de Cuenta:
GET /api/Payment/statement/{cardNumber}: Obtiene el estado de cuenta de una tarjeta específica.
Consulta de Movimientos Recientes:
GET /api/Transaction/recentTransactions: Devuelve los últimos movimientos realizados.
Manejo de Notificaciones:
POST /api/Notification/sendNotification: Envía una notificación.
Cambio de PIN:
POST /api/Security/changePin: Permite cambiar el PIN de una tarjeta.
Bloqueo Temporal de Tarjeta:
POST /api/CreditCard/block/{cardNumber}: Bloquea temporalmente una tarjeta.
Solicitud de Aumento de Límite de Crédito:
POST /api/CreditLimit/request: Registra una solicitud para aumentar el límite de crédito.
Cómo usar Swagger:

En la página de Swagger UI, selecciona la API que deseas probar.
Completa los parámetros necesarios y presiona el botón "Try it out!"
Revisa los resultados directamente en la interfaz de Swagger.
