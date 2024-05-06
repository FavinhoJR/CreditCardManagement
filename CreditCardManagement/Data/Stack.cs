using CreditCardManagement.Models;
using System.Collections.Generic;

namespace CreditCardManagement.Data
{
    /// <summary>
    /// Clase que gestiona una pila de transacciones.
    /// Utiliza una estructura de pila para almacenar y procesar transacciones en un orden LIFO (Last In, First Out).
    /// </summary>
    public class TransactionStack
    {
        // Pila para almacenar las transacciones.
        private Stack<Transaction> stack = new Stack<Transaction>();

        /// <summary>
        /// Añade una transacción a la pila.
        /// </summary>
        /// <param name="transaction">La transacción a añadir.</param>
        public void PushTransaction(Transaction transaction)
        {
            stack.Push(transaction);  // Empuja la transacción al tope de la pila.
        }

        /// <summary>
        /// Extrae y devuelve la transacción superior de la pila.
        /// </summary>
        /// <returns>La transacción en el tope de la pila.</returns>
        public Transaction PopTransaction()
        {
            return stack.Pop();  // Devuelve y elimina la transacción del tope de la pila.
        }

        /// <summary>
        /// Obtiene el número de transacciones en la pila.
        /// </summary>
        /// <returns>El número de transacciones actualmente en la pila.</returns>
        public int GetCount()
        {
            return stack.Count;  // Devuelve la cantidad de transacciones en la pila.
        }
    }
}

