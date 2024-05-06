using CreditCardManagement.Models;

namespace CreditCardManagement.Data
{
    /// <summary>
    /// Nodo de lista enlazada que contiene una tarjeta de crédito.
    /// </summary>
    public class Node
    {
        public CreditCard Card { get; set; }
        public Node Next { get; set; }

        /// <summary>
        /// Constructor que inicializa un nodo con una tarjeta de crédito.
        /// </summary>
        /// <param name="card">La tarjeta de crédito que se almacenará en este nodo.</param>
        public Node(CreditCard card)
        {
            this.Card = card;
            this.Next = null; // El siguiente nodo inicialmente es null.
        }
    }

    /// <summary>
    /// Lista enlazada que maneja las tarjetas de crédito.
    /// </summary>
    public class LinkedList
    {
        private Node head; // Referencia al primer nodo de la lista.

        /// <summary>
        /// Agrega una tarjeta de crédito a la lista enlazada.
        /// </summary>
        /// <param name="card">La tarjeta de crédito a agregar.</param>
        public void AddCard(CreditCard card)
        {
            Node newNode = new Node(card); // Crea un nuevo nodo para la tarjeta.
            if (head == null)
            {
                head = newNode; // Si la lista está vacía, el nuevo nodo se convierte en el cabeza.
            }
            else
            {
                Node current = head;
                // Recorre la lista hasta encontrar el último nodo.
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode; // Añade el nuevo nodo al final de la lista.
            }
        }

        /// <summary>
        /// Busca una tarjeta de crédito por su número en la lista enlazada.
        /// </summary>
        /// <param name="cardNumber">Número de la tarjeta de crédito a buscar.</param>
        /// <returns>La tarjeta de crédito si se encuentra, de lo contrario null.</returns>
        public CreditCard FindCard(string cardNumber)
        {
            Node current = head; // Comienza la búsqueda desde la cabeza de la lista.
            // Recorre la lista buscando un nodo cuyo número de tarjeta coincida.
            while (current != null)
            {
                // Compara los números de tarjeta ignorando diferencias en mayúsculas o espacios.
                if (current.Card.CardNumber.Trim().Equals(cardNumber.Trim(), StringComparison.OrdinalIgnoreCase))
                    return current.Card; // Retorna la tarjeta si la encuentra.
                current = current.Next; // Avanza al siguiente nodo.
            }
            return null; // Retorna null si no encuentra la tarjeta.
        }
    }
}