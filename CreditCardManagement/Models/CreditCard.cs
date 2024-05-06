namespace CreditCardManagement.Models
{
    /// <summary>
    /// Representa una tarjeta de crédito en el sistema de gestión de tarjetas.
    /// </summary>
    public class CreditCard
    {
        /// <summary>
        /// Identificador único de la tarjeta de crédito.
        /// </summary>
        public int CreditCardId { get; set; }

        /// <summary>
        /// Número de la tarjeta de crédito.
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Nombre del titular de la tarjeta.
        /// </summary>
        public string CardHolder { get; set; }

        /// <summary>
        /// Límite de crédito actual de la tarjeta.
        /// </summary>
        public decimal CurrentLimit { get; set; }

        /// <summary>
        /// Saldo actual de la tarjeta, refleja el total de gastos contra el límite de crédito.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Indica si la tarjeta está bloqueada o no.
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Número de identificación personal (PIN) asociado a la tarjeta.
        /// </summary>
        public string Pin { get; set; }
    }
}

