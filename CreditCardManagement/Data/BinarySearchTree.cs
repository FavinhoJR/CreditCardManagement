using CreditCardManagement.Models;
using System;
using System.Collections.Generic;

namespace CreditCardManagement.Data
{
    /// <summary>
    /// Representa el estado de cuenta de una tarjeta de crédito, incluyendo todas las transacciones realizadas.
    /// </summary>
    public class AccountStatement
    {
        public CreditCard Card { get; set; }
        public List<Payment> Transactions { get; set; }

        /// <summary>
        /// Constructor que inicializa un nuevo estado de cuenta con una tarjeta específica.
        /// </summary>
        /// <param name="card">La tarjeta de crédito asociada a este estado de cuenta.</param>
        public AccountStatement(CreditCard card)
        {
            Card = card;
            Transactions = new List<Payment>();
        }
    }

    /// <summary>
    /// Nodo para el árbol binario de búsqueda que almacena estados de cuenta.
    /// </summary>
    public class TreeNode
    {
        public AccountStatement Statement { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        /// <summary>
        /// Constructor que inicializa un nodo con un estado de cuenta.
        /// </summary>
        /// <param name="statement">El estado de cuenta asociado con este nodo.</param>
        public TreeNode(AccountStatement statement)
        {
            Statement = statement;
            Left = null;
            Right = null;
        }
    }

    /// <summary>
    /// Árbol binario de búsqueda que almacena y gestiona los estados de cuenta de las tarjetas de crédito.
    /// </summary>
    public class BinarySearchTree
    {
        private TreeNode root;

        /// <summary>
        /// Inserta o actualiza un nodo en el árbol con un nuevo estado de cuenta o actualización del saldo existente.
        /// </summary>
        /// <param name="card">La tarjeta de crédito a insertar o actualizar.</param>
        public void InsertOrUpdate(CreditCard card)
        {
            root = InsertOrUpdate(root, card);
        }

        private TreeNode InsertOrUpdate(TreeNode node, CreditCard card)
        {
            if (node == null)
            {
                return new TreeNode(new AccountStatement(card));
            }

            if (card.CardNumber.Equals(node.Statement.Card.CardNumber, StringComparison.OrdinalIgnoreCase))
            {
                node.Statement.Card.Balance = card.Balance;  // Actualiza el saldo si el número de tarjeta coincide.
                return node;
            }

            if (card.CardNumber.CompareTo(node.Statement.Card.CardNumber) < 0)
                node.Left = InsertOrUpdate(node.Left, card);
            else
                node.Right = InsertOrUpdate(node.Right, card);

            return node;
        }

        /// <summary>
        /// Busca un estado de cuenta en el árbol por número de tarjeta.
        /// </summary>
        /// <param name="cardNumber">Número de tarjeta para buscar el estado de cuenta asociado.</param>
        /// <returns>El estado de cuenta si se encuentra, de lo contrario null.</returns>
        public AccountStatement Search(string cardNumber)
        {
            return Search(root, cardNumber);
        }

        private AccountStatement Search(TreeNode node, string cardNumber)
        {
            if (node == null)
                return null;

            if (cardNumber.Equals(node.Statement.Card.CardNumber, StringComparison.OrdinalIgnoreCase))
                return node.Statement;

            if (cardNumber.CompareTo(node.Statement.Card.CardNumber) < 0)
                return Search(node.Left, cardNumber);
            else
                return Search(node.Right, cardNumber);
        }

        /// <summary>
        /// Actualiza el saldo de una tarjeta en el árbol.
        /// </summary>
        /// <param name="cardNumber">Número de tarjeta para actualizar.</param>
        /// <param name="newBalance">Nuevo saldo a establecer.</param>
        public void UpdateBalance(string cardNumber, decimal newBalance)
        {
            UpdateBalance(root, cardNumber, newBalance);
        }

        private void UpdateBalance(TreeNode node, string cardNumber, decimal newBalance)
        {
            if (node == null)
                return;

            if (cardNumber.Equals(node.Statement.Card.CardNumber, StringComparison.OrdinalIgnoreCase))
            {
                node.Statement.Card.Balance = newBalance;  // Actualiza el saldo si el número de tarjeta coincide.
                return;
            }

            if (cardNumber.CompareTo(node.Statement.Card.CardNumber) < 0)
                UpdateBalance(node.Left, cardNumber, newBalance);
            else
                UpdateBalance(node.Right, cardNumber, newBalance);
        }
    }
}