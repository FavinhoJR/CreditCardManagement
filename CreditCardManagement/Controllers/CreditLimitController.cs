using Microsoft.AspNetCore.Mvc;
using CreditCardManagement.Data;
using CreditCardManagement.Models;

namespace CreditCardManagement.Controllers
{
    /// <summary>
    /// Controlador para la gestión de solicitudes de aumento de límite de crédito.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CreditLimitController : ControllerBase
    {
        // Almacena las solicitudes de aumento de límite en una estructura de datos especializada.
        private readonly CreditLimitRequestStack creditLimitRequestStack;
        // Almacena las tarjetas de crédito en una lista enlazada.
        private readonly LinkedList creditCards;

        /// <summary>
        /// Constructor para inyección de dependencias de las estructuras de datos.
        /// </summary>
        /// <param name="creditLimitRequestStack">Estructura para almacenar y gestionar solicitudes de crédito.</param>
        /// <param name="creditCards">Estructura para almacenar y gestionar tarjetas de crédito.</param>
        public CreditLimitController(CreditLimitRequestStack creditLimitRequestStack, LinkedList creditCards)
        {
            this.creditLimitRequestStack = creditLimitRequestStack;
            this.creditCards = creditCards; // Inyectar en el constructor
        }

        /// <summary>
        /// Endpoint para recibir solicitudes de aumento de límite de crédito.
        /// </summary>
        /// <param name="request">Datos de la solicitud de aumento.</param>
        /// <returns>Confirma recepción de la solicitud.</returns>
        [HttpPost("request")]
        public IActionResult RequestCreditLimitIncrease([FromBody] CreditLimitRequest request)
        {
            creditLimitRequestStack.PushRequest(request);
            return Ok("Solicitud de aumento de límite de crédito recibida.");
        }

        /// <summary>
        /// Obtiene todas las solicitudes de aumento de límite de crédito almacenadas.
        /// </summary>
        /// <returns>Lista de todas las solicitudes.</returns>
        [HttpGet("requests")]
        public IActionResult GetCreditLimitRequests()
        {
            var requests = creditLimitRequestStack.GetAllRequests(); // Utiliza GetAllRequests para obtener todas las solicitudes
            return Ok(requests);
        }

        /// <summary>
        /// Aprueba una solicitud específica de aumento de límite.
        /// </summary>
        /// <param name="requestId">ID de la solicitud a aprobar.</param>
        /// <returns>Resultado de la operación, incluyendo el nuevo límite si fue exitoso.</returns>
        [HttpPost("approve")]
        public IActionResult ApproveRequest(int requestId)
        {
            var request = creditLimitRequestStack.FindRequest(requestId);
            if (request == null)
            {
                return NotFound("Solicitud no encontrada.");
            }

            var card = creditCards.FindCard(request.CardNumber);
            if (card == null)
            {
                return NotFound("Tarjeta de crédito no encontrada.");
            }

            // Si el límite solicitado es mayor, actualiza el balance junto con el límite.
            decimal originalLimit = card.CurrentLimit;
            if (request.RequestedLimit > originalLimit)
            {
                card.Balance += (request.RequestedLimit - originalLimit);  // Aumenta el balance según el incremento de límite.
            }

            card.CurrentLimit = request.RequestedLimit;  // Actualiza el límite de la tarjeta.
            return Ok($"El nuevo límite de crédito para la tarjeta {card.CardNumber} es {card.CurrentLimit}, Balance actualizado: {card.Balance}.");
        }
    }
}

