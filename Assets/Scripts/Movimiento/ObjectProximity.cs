using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProximity : MonoBehaviour
{
    public float proximityThreshold = 2.0f; // Umbral de proximidad
    public LayerMask proximityLayerMask; // Capas a tener en cuenta en la detección de proximidad

    private Collider[] nearbyColliders; // Lista de colisionadores cercanos
    private List<GameObject> nearbyObjects = new List<GameObject>(); // Lista de objetos cercanos

    // Se llama al inicio del script
    void Start()
    {
        // Se obtienen los colisionadores cercanos
        nearbyColliders = Physics.OverlapSphere(transform.position, proximityThreshold, proximityLayerMask);
        
        // Se añaden los objetos cercanos a la lista
        foreach (Collider collider in nearbyColliders)
        {
            GameObject nearbyObject = collider.gameObject;
            // Se comprueba si el objeto no es este mismo
            if (nearbyObject != gameObject)
            {
                nearbyObjects.Add(nearbyObject);
            }
        }
    }

    // Se llama en cada frame
    void Update()
    {
        // Se comprueba la proximidad de los objetos cercanos
        foreach (GameObject nearbyObject in nearbyObjects)
        {
            float distance = Vector3.Distance(transform.position, nearbyObject.transform.position);
            if (distance < proximityThreshold)
            {
                // Se calcula la dirección y distancia entre los objetos
                Vector3 direction = transform.position - nearbyObject.transform.position;
                float moveDistance = proximityThreshold - distance;
                // Se mueve el objeto en la dirección calculada
                transform.position += direction.normalized * moveDistance;
            }
        }
    }
}
