using CreditCardManagement.Models;
using System;
using System.Collections.Generic;

public class TransactionStack
{
    /// <summary>
    /// Pila para almacenar transacciones.
    /// </summary>
    private Stack<Transaction> transactions = new Stack<Transaction>();

    /// <summary>
    /// Obtiene el número de transacciones en la pila.
    /// </summary>
    /// <returns>Cantidad de transacciones actualmente en la pila.</returns>
    public int GetCount()
    {
        return transactions.Count;
    }

    /// <summary>
    /// Extrae y devuelve la transacción en la cima de la pila.
    /// </summary>
    /// <returns>La transacción en la cima de la pila.</returns>
    /// <exception cref="InvalidOperationException">Se lanza si la pila está vacía.</exception>
    public Transaction PopTransaction()
    {
        if (transactions.Count > 0)
        {
            return transactions.Pop(); // Retorna y elimina la transacción del tope de la pila.
        }
        else
        {
            throw new InvalidOperationException("No hay transacciones en la pila."); // Manejo de error cuando la pila está vacía.
        }
    }

    /// <summary>
    /// Agrega transacciones iniciales a la pila basadas en una lista de tarjetas de crédito.
    /// Este método podría ser utilizado para inicializar la pila con transacciones predeterminadas al arranque de la aplicación.
    /// </summary>
    /// <param name="cards">Lista de tarjetas de crédito para generar transacciones iniciales.</param>
    public void AddInitialTransactions(List<CreditCard> cards)
    {
        // Suponiendo que cada tarjeta de crédito necesita generar una transacción inicial, como un saldo inicial.
        foreach (var card in cards)
        {
            // Creación de una transacción inicial basada en los datos de la tarjeta.
            var initialTransaction = new Transaction
            {
                CreditCardId = card.CreditCardId,
                Amount = card.Balance, // Supongamos que el balance inicial es la transacción.
                TransactionDate = DateTime.Now, // Fecha actual como fecha de la transacción.
                Description = "Transacción inicial"
            };

            transactions.Push(initialTransaction); // Agregar transacción a la pila.
        }
    }
}

