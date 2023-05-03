using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // velocidad del personaje
    public float sensitivity = 2.0f; // sensibilidad del ratón
    public float maxYAngle = 80.0f; // límite máximo del ángulo vertical de la cámara

    public GameObject projectilePrefab; // prefab de la bala que se va a disparar

    private Vector2 currentRotation; // rotación actual de la cámara

    private GameObject playerGun; // gameobject del objeto desde el que se va a disparar

    private Transform gunTransform;

    void Start()
    {
        // bloquear el cursor para que no se muestre
        Cursor.lockState = CursorLockMode.Locked;

        // obtenemos el objeto PlayerGun e inicializamos la variable
        playerGun = GameObject.FindGameObjectWithTag("PlayerGun");

        // inicializamos gunTransform
        gunTransform = new GameObject("GunTransform").transform;
        gunTransform.parent = playerGun.transform;
        gunTransform.localPosition = Vector3.zero;
        gunTransform.localRotation = Quaternion.identity;
    }

    void Update()
    {
        // disparos
        if (Input.GetButtonDown("Fire1")) // El botón izquierdo del mouse
        {
            // Crea una instancia del proyectil en la posición del objeto "Gun"
            Instantiate(projectilePrefab, gunTransform.position, gunTransform.rotation);
        }


        // movimiento horizontal
        float hInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * hInput * speed * Time.deltaTime);

        // movimiento vertical
        float vInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * vInput * speed * Time.deltaTime);

        // movimiento hacia arriba
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        // movimiento hacia abajo
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        // movimiento del ratón
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        currentRotation.x += mouseX;
        currentRotation.y -= mouseY;
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle); // limitar el ángulo vertical
        transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
    }
}

