namespace CreditCardManagement.Models
{
    /// <summary>
    /// Representa una solicitud para aumentar el límite de crédito de una tarjeta específica.
    /// </summary>
    public class CreditLimitRequest
    {
        /// <summary>
        /// Identificador único de la solicitud.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Número de la tarjeta de crédito asociada con la solicitud.
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Límite de crédito actual de la tarjeta en el momento de la solicitud.
        /// </summary>
        public decimal CurrentLimit { get; set; }

        /// <summary>
        /// Límite de crédito solicitado por el titular de la tarjeta.
        /// </summary>
        public decimal RequestedLimit { get; set; }

        /// <summary>
        /// Fecha y hora cuando se realizó la solicitud.
        /// </summary>
        public DateTime RequestDate { get; set; }
    }
}
