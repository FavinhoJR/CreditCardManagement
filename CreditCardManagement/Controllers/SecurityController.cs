using Microsoft.AspNetCore.Mvc;
using CreditCardManagement.Models;
using CreditCardManagement.Data;

namespace CreditCardManagement.Controllers
{
    /// <summary>
    /// Controlador para gestionar operaciones de seguridad relacionadas con tarjetas de crédito.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        // Lista enlazada que almacena y gestiona las tarjetas de crédito.
        private readonly LinkedList creditCards;

        /// <summary>
        /// Constructor para inyectar la lista de tarjetas de crédito.
        /// </summary>
        /// <param name="creditCards">Instancia de la lista de tarjetas de crédito.</param>
        public SecurityController(LinkedList creditCards)
        {
            this.creditCards = creditCards;
        }

        /// <summary>
        /// Cambia el PIN de una tarjeta de crédito especificada.
        /// </summary>
        /// <param name="cardNumber">Número de la tarjeta de crédito para la cual cambiar el PIN.</param>
        /// <param name="newPin">Nuevo PIN para la tarjeta.</param>
        /// <returns>Resultado de la operación, indicando éxito o fracaso en encontrar la tarjeta.</returns>
        [HttpPost("changePin")]
        public IActionResult ChangePin(string cardNumber, string newPin)
        {
            // Busca la tarjeta por número en la lista enlazada.
            var card = creditCards.FindCard(cardNumber);
            if (card != null)
            {
                // Establece el nuevo PIN para la tarjeta encontrada.
                card.Pin = newPin;
                return Ok("PIN cambiado correctamente");
            }
            // Retorna un error si no se encuentra la tarjeta.
            return NotFound("Tarjeta no encontrada");
        }
    }
}

