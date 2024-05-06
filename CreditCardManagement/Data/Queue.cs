using CreditCardManagement.Models;
using System.Collections.Generic;

namespace CreditCardManagement.Data
{
    /// <summary>
    /// Clase que gestiona una cola de pagos.
    /// Utiliza una estructura de cola para organizar y procesar pagos de manera secuencial.
    /// </summary>
    public class PaymentQueue
    {
        // Cola para almacenar los pagos.
        private Queue<Payment> queue = new Queue<Payment>();

        /// <summary>
        /// Añade un pago a la cola.
        /// </summary>
        /// <param name="payment">El pago a encolar.</param>
        public void EnqueuePayment(Payment payment)
        {
            queue.Enqueue(payment);  // Agrega el pago al final de la cola.
        }

        /// <summary>
        /// Extrae y devuelve el primer pago de la cola.
        /// </summary>
        /// <returns>El primer pago en la cola si hay alguno; de lo contrario, levanta una excepción.</returns>
        public Payment DequeuePayment()
        {
            return queue.Dequeue();  // Devuelve y elimina el primer pago de la cola.
        }

        /// <summary>
        /// Obtiene el número de pagos en la cola.
        /// </summary>
        /// <returns>El número de pagos actualmente en la cola.</returns>
        public int GetCount()
        {
            return queue.Count;  // Devuelve la cantidad de pagos en la cola.
        }
    }
}

