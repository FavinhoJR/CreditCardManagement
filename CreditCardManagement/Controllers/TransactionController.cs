using Microsoft.AspNetCore.Mvc;
using CreditCardManagement.Models;
using CreditCardManagement.Data;
using System.Collections.Generic;

namespace CreditCardManagement.Controllers
{
    /// <summary>
    /// Controlador para gestionar las transacciones de tarjetas de crédito.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        // Pila que almacena y gestiona las transacciones recientes.
        private readonly TransactionStack transactionStack;

        /// <summary>
        /// Constructor para inyectar la pila de transacciones.
        /// </summary>
        /// <param name="transactionStack">Instancia de la pila de transacciones.</param>
        public TransactionController(TransactionStack transactionStack)
        {
            this.transactionStack = transactionStack;
        }

        /// <summary>
        /// Obtiene las transacciones recientes desde la pila.
        /// </summary>
        /// <returns>Una lista de transacciones recientes.</returns>
        [HttpGet("recentTransactions")]
        public IActionResult GetRecentTransactions()
        {
            var transactions = new List<Transaction>();

            // Extrae transacciones hasta que la pila esté vacía.
            while (transactionStack.GetCount() > 0)
            {
                transactions.Add(transactionStack.PopTransaction());
            }

            // Devuelve la lista de transacciones procesadas.
            return Ok(transactions);
        }
    }
}


