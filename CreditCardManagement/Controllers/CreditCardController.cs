using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using CreditCardManagement.Models;
using CreditCardManagement.Data;

namespace CreditCardManagement.Controllers
{
    /// <summary>
    /// Controlador para la gestión de tarjetas de crédito.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CreditCardController : ControllerBase
    {
        // Instancia de LinkedList para almacenar y gestionar tarjetas de crédito.
        private readonly LinkedList creditCards;

        /// <summary>
        /// Constructor que inyecta la dependencia de la lista de tarjetas.
        /// </summary>
        /// <param name="creditCards">Lista enlazada de tarjetas de crédito.</param>
        public CreditCardController(LinkedList creditCards)
        {
            this.creditCards = creditCards;
        }

        /// <summary>
        /// Carga las tarjetas de crédito desde un archivo JSON.
        /// </summary>
        /// <returns>Resultado de la acción indicando si la carga fue exitosa.</returns>
        [HttpPost("load")]
        public IActionResult LoadCards()
        {
            // Obtiene la ruta del archivo JSON que contiene los datos de las tarjetas.
            string fileName = Path.Combine(Directory.GetCurrentDirectory(), "initialData.json");
            // Lee el contenido del archivo JSON en una cadena de texto.
            string jsonString = System.IO.File.ReadAllText(fileName);
            // Deserializa la cadena JSON a una lista de objetos CreditCard.
            var cards = JsonSerializer.Deserialize<List<CreditCard>>(jsonString);

            Console.WriteLine($"Se cargaron {cards.Count} tarjetas de crédito del archivo JSON.");

            // Añade cada tarjeta deserializada a la lista enlazada.
            foreach (var card in cards)
            {
                creditCards.AddCard(card);
                Console.WriteLine($"Tarjeta cargada: {card.CardNumber} - {card.CardHolder}");
            }

            return Ok("Tarjetas cargadas exitosamente");
        }

        /// <summary>
        /// Obtiene el saldo de una tarjeta específica usando su número.
        /// </summary>
        /// <param name="cardNumber">El número de la tarjeta de crédito.</param>
        /// <returns>Saldo de la tarjeta si es encontrada, o un mensaje de no encontrada.</returns>
        [HttpGet("balance/{cardNumber}")]
        public IActionResult GetBalance(string cardNumber)
        {
            // Busca la tarjeta por número en la lista enlazada.
            var card = creditCards.FindCard(cardNumber);
            if (card != null)
            {
                return Ok(card.Balance);
            }
            return NotFound("Tarjeta no encontrada");
        }

        /// <summary>
        /// Bloquea una tarjeta de crédito específica usando su número.
        /// </summary>
        /// <param name="cardNumber">El número de la tarjeta de crédito a bloquear.</param>
        /// <returns>Resultado indicando si la tarjeta fue bloqueada con éxito o no encontrada.</returns>
        [HttpPost("block/{cardNumber}")]
        public IActionResult BlockCard(string cardNumber)
        {
            // Busca la tarjeta por número en la lista enlazada.
            var card = creditCards.FindCard(cardNumber);
            if (card != null)
            {
                card.IsBlocked = true; // Cambia el estado de la tarjeta a bloqueado.
                return Ok("Tarjeta bloqueada con éxito");
            }
            return NotFound("Tarjeta no encontrada");
        }
    }
}
