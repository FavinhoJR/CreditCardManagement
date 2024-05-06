using System.Collections.Generic;
using CreditCardManagement.Models;

namespace CreditCardManagement.Data
{
    /// <summary>
    /// Clase que gestiona una cola de notificaciones.
    /// Utiliza una estructura de datos de cola para almacenar y gestionar notificaciones.
    /// </summary>
    public class NotificationQueue
    {
        // Cola para almacenar las notificaciones.
        private Queue<Notification> queue = new Queue<Notification>();

        /// <summary>
        /// Añade una notificación a la cola.
        /// </summary>
        /// <param name="notification">La notificación a encolar.</param>
        public void EnqueueNotification(Notification notification)
        {
            queue.Enqueue(notification);  // Agrega la notificación al final de la cola.
        }

        /// <summary>
        /// Extrae y devuelve la primera notificación de la cola.
        /// </summary>
        /// <returns>La primera notificación en la cola si existe; de lo contrario, null.</returns>
        public Notification DequeueNotification()
        {
            return queue.Count > 0 ? queue.Dequeue() : null;  // Devuelve y elimina la notificación de la cola.
        }

        /// <summary>
        /// Obtiene el número de notificaciones en la cola.
        /// </summary>
        /// <returns>El número de notificaciones en la cola.</returns>
        public int Count()
        {
            return queue.Count;  // Devuelve la cantidad de notificaciones en la cola.
        }
    }
}
