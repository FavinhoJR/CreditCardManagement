using CreditCardManagement.Models;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Clase que gestiona una pila de solicitudes de aumento de límite de crédito.
/// Utiliza un diccionario para almacenar las solicitudes y asignarles un identificador único.
/// </summary>
public class CreditLimitRequestStack
{
    // Siguiente identificador único a asignar a una nueva solicitud.
    private int nextId = 1;

    // Diccionario que almacena las solicitudes con su ID como clave.
    private Dictionary<int, CreditLimitRequest> requests = new Dictionary<int, CreditLimitRequest>();

    /// <summary>
    /// Añade una solicitud de aumento de límite de crédito a la pila y le asigna un ID único.
    /// </summary>
    /// <param name="request">La solicitud de aumento de límite a añadir.</param>
    /// <returns>El ID asignado a la solicitud.</returns>
    public int PushRequest(CreditLimitRequest request)
    {
        int requestId = nextId++;  // Obtiene y actualiza el próximo ID disponible.
        request.Id = requestId;  // Asigna el ID a la solicitud.
        requests[requestId] = request;  // Almacena la solicitud en el diccionario con su ID como clave.
        return requestId;  // Devuelve el ID asignado a la solicitud.
    }

    /// <summary>
    /// Busca una solicitud de aumento de límite de crédito por su ID.
    /// </summary>
    /// <param name="requestId">El ID de la solicitud a encontrar.</param>
    /// <returns>La solicitud si se encuentra, de lo contrario null.</returns>
    public CreditLimitRequest FindRequest(int requestId)
    {
        if (requests.TryGetValue(requestId, out CreditLimitRequest request))
        {
            return request;  // Devuelve la solicitud si se encuentra.
        }
        return null;  // Devuelve null si no se encuentra la solicitud.
    }

    /// <summary>
    /// Obtiene todas las solicitudes de aumento de límite de crédito almacenadas.
    /// </summary>
    /// <returns>Una lista de todas las solicitudes.</returns>
    public List<CreditLimitRequest> GetAllRequests()
    {
        return requests.Values.ToList();  // Devuelve una lista con todas las solicitudes del diccionario.
    }
}