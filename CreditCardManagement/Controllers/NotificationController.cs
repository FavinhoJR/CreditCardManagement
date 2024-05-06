using Microsoft.AspNetCore.Mvc;
using CreditCardManagement.Models;
using CreditCardManagement.Data;

namespace CreditCardManagement.Controllers
{
    /// <summary>
    /// Controlador para la gestión de notificaciones.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        // Instancia de NotificationQueue para manejar la cola de notificaciones.
        private NotificationQueue notificationQueue = new NotificationQueue();

        /// <summary>
        /// Recibe una notificación y la añade a la cola de procesamiento.
        /// </summary>
        /// <param name="notification">Objeto de tipo Notification que contiene los datos de la notificación.</param>
        /// <returns>Respuesta indicando que la notificación ha sido encolada.</returns>
        [HttpPost("sendNotification")]
        public IActionResult SendNotification([FromBody] Notification notification)
        {
            // Añade la notificación a la cola.
            notificationQueue.EnqueueNotification(notification);
            // Retorna un mensaje indicando que la notificación ha sido encolada.
            return Ok("Notificación en cola");
        }
    }
}
