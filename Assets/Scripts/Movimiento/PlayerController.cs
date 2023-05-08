using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // velocidad del personaje
    public float sensitivity = 2.0f; // sensibilidad del ratón
    public float maxYAngle = 80.0f; // límite máximo del ángulo vertical de la cámara

    public GameObject projectilePrefab; // prefab de la bala que se va a disparar

    private AudioManager audioManager;

    private Vector2 currentRotation; // rotación actual de la cámara

    private GameObject playerGun; // gameobject del objeto desde el que se va a disparar

    private GameObject MainCamera;

    private Transform gunTransform;

    private int enemyLayerMask = 1 << 7;

    void Start()
    {
        // bloquear el cursor para que no se muestre
        Cursor.lockState = CursorLockMode.Locked;

        // obtener el componente audioManager
        audioManager = FindObjectOfType<AudioManager>();

        // obtenemos el objeto PlayerGun e inicializamos la variable
        playerGun = GameObject.FindGameObjectWithTag("PlayerGun");

        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");

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
            //Hacemos un RayCast desde el centro de la cámara
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
            RaycastHit hitInfo;

            // Si el raycast ha colisionado con algo se dispara en esa dirección, sino se dispara en la dirección de la cámara
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, enemyLayerMask)) {
                Vector3 bulletSpawnPosition = gunTransform.position;
                Vector3 bulletSpawnDirection = (hitInfo.point - bulletSpawnPosition).normalized;
                Quaternion bulletSpawnRotation = Quaternion.LookRotation(bulletSpawnDirection);
                Instantiate(projectilePrefab, bulletSpawnPosition, bulletSpawnRotation);
            }else{
                // Crea una instancia del proyectil en la posición del objeto "Gun"
                Instantiate(projectilePrefab, gunTransform.position, MainCamera.gameObject.transform.rotation);
            }

            // Hacer sonar el sonido de disparo
            audioManager.ReproducirSonido("shoot");
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

