using Microsoft.AspNetCore.Mvc;
using CreditCardManagement.Models;
using CreditCardManagement.Data;

namespace CreditCardManagement.Controllers
{
    /// <summary>
    /// Controlador para gestionar operaciones de pago.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        // Cola para gestionar los pagos realizados.
        private readonly PaymentQueue paymentQueue = new PaymentQueue();
        // Árbol binario para almacenar los estados de cuenta.
        private readonly BinarySearchTree accountStatements;
        // Lista enlazada para almacenar las tarjetas de crédito.
        private readonly LinkedList creditCards;

        /// <summary>
        /// Constructor para inyectar las dependencias necesarias.
        /// </summary>
        /// <param name="creditCards">Instancia de la lista de tarjetas de crédito.</param>
        /// <param name="accountStatements">Instancia del árbol binario de estados de cuenta.</param>
        public PaymentController(LinkedList creditCards, BinarySearchTree accountStatements)
        {
            this.creditCards = creditCards;
            this.accountStatements = accountStatements;
        }

        /// <summary>
        /// Procesa un pago y lo añade a la cola de pagos.
        /// </summary>
        /// <param name="payment">Datos del pago a procesar.</param>
        /// <returns>Resultado de la acción indicando si el pago fue procesado con éxito.</returns>
        [HttpPost("makePayment")]
        public IActionResult MakePayment([FromBody] Payment payment)
        {
            // Buscar la tarjeta por número en la lista enlazada.
            var card = creditCards.FindCard(payment.CardNumber);
            if (card == null)
            {
                return NotFound("Tarjeta de crédito no encontrada.");
            }

            // Actualizar el saldo de la tarjeta descontando el monto del pago.
            card.Balance -= payment.Amount;

            // Buscar o crear un estado de cuenta para la tarjeta.
            var accountStatement = accountStatements.Search(card.CardNumber);
            if (accountStatement == null)
            {
                accountStatement = new AccountStatement(card);
                accountStatements.InsertOrUpdate(card);
            }
            accountStatement.Transactions.Add(payment); // Agregar la transacción al estado de cuenta.

            // Encolar el pago para procesamiento posterior.
            paymentQueue.EnqueuePayment(payment);

            // Actualizar el saldo de la tarjeta en el árbol binario.
            accountStatements.UpdateBalance(card.CardNumber, card.Balance);

            return Ok("Pago procesado y puesto en cola.");
        }

        /// <summary>
        /// Obtiene el estado de cuenta de una tarjeta específica.
        /// </summary>
        /// <param name="cardNumber">Número de la tarjeta cuyo estado se solicita.</param>
        /// <returns>Estado de cuenta de la tarjeta si existe; de lo contrario, no encontrado.</returns>
        [HttpGet("statement/{cardNumber}")]
        public IActionResult GetStatement(string cardNumber)
        {
            // Buscar el estado de cuenta en el árbol binario.
            var statement = accountStatements.Search(cardNumber);
            if (statement != null)
            {
                return Ok(statement);
            }
            return NotFound("Estado no encontrado");
        }
    }
}
