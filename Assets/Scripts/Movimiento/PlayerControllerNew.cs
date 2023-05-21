using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;


public class PlayerControllerNew : MonoBehaviour
{
    [Header("=== Ajustes de Disparo ===")]
    [SerializeField]
    private float cadencia = 1f;
    [SerializeField, Range(0.001f, 0.999f)]
    private float sensibilidad_ajuste = 0.999f;
    public float sensibilidad;
    private float nextFire = 0f;
    [Header("=== Ajustes del Movimiento de la Nave ===")]
    [SerializeField]
    private float aceleracion = 100f; // W A
    [SerializeField]
    private float aceleracionVertical = 100f; // SPACE SHIFT
    [SerializeField]
    private float aceleracionLateral = 50f; // A D
    [SerializeField]
    private float cabeceoHorizontalTorque = 500f;
    [SerializeField]
    private float cabeceoVerticalTorque = 1000f;
    [SerializeField, Range(0.001f, 0.999f)]
    private float aceleracionReduccion = 0.999f;
    [SerializeField, Range(0.001f, 0.999f)]
    private float upDownReduccion = 0.999f;
    [SerializeField, Range(0.001f, 0.999f)]
    private float leftRightReduccion = 0.999f;
    float glide = 0f;
    float verticalglide = 0f;
    float horizontalglide = 0f;

    public GameObject projectilePrefab; // prefab de la bala que se va a disparar

    private AudioManager audioManager;

    private GameObject playerGun; // gameobject del objeto desde el que se va a disparar

    private Transform gunTransform;

    private int enemyLayerMask = 1 << 7;
    private CinemachineVirtualCamera virtualCamera;
    private GameObject MainCamera;
    Rigidbody rb;

    //Input values
    private float delanteDetras1D;
    private float izquierdaDerecha1D;
    private float arribaAbajo1D;
    private Vector2 cabeceo;
    private float disparo;
    private float apuntar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // bloquear el cursor para que no se muestre
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // obtener el componente audioManager
        audioManager = FindObjectOfType<AudioManager>();

        // obtenemos el objeto PlayerGun e inicializamos la variable
        playerGun = GameObject.FindGameObjectWithTag("PlayerGun");

        virtualCamera = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        // inicializamos gunTransform
        gunTransform = new GameObject("GunTransform").transform;
        gunTransform.parent = playerGun.transform;
        gunTransform.localPosition = Vector3.zero;
        gunTransform.localRotation = Quaternion.identity;

        sensibilidad = sensibilidad_ajuste;
    }

    void FixedUpdate() {
        HandleMovement();
        HandleShoot();
    }

    void HandleMovement(){
        // CabeceoVertical
        rb.AddRelativeTorque(Vector3.right * Mathf.Clamp(-cabeceo.y, -1f, 1f) * cabeceoVerticalTorque * Time.deltaTime);
        // CabeceoHorizontal
        rb.AddRelativeTorque(Vector3.up * Mathf.Clamp(cabeceo.x, -1f, 1f) * cabeceoHorizontalTorque * Time.deltaTime);
        // Mantenemos la rotación sobre el eje z a 0 (para que se mantenga paralelo al suelo)
        /* Descomentar si queremos que el jugador no rote sobre el eje z (cuando se da un giro de 360 grados hacia arriba queda muy raro)
        Vector3 eulerRotation = rb.rotation.eulerAngles;
        eulerRotation.z = 0f;
        rb.rotation = Quaternion.Euler(eulerRotation);
        */

        // Aceleracion delante/detras
        if(delanteDetras1D > 0.1f || delanteDetras1D < -0.1f) //para evitar el drift del mando
        {
            float aceleracionActual = aceleracion;
            rb.AddRelativeForce(Vector3.forward * delanteDetras1D * aceleracionActual * Time.deltaTime);
            glide = aceleracion;
        }else{
            rb.AddRelativeForce(Vector3.forward * glide * Time.deltaTime);
            glide *= aceleracionReduccion;
        }

        // Aceleracion arriba/abajo
        if(arribaAbajo1D > 0.1f || arribaAbajo1D < -0.1f) //para evitar el drift del mando
        {
            rb.AddRelativeForce(Vector3.up * arribaAbajo1D * aceleracionVertical * Time.deltaTime);
            verticalglide = arribaAbajo1D * aceleracionVertical;
        }else{
            rb.AddRelativeForce(Vector3.up * verticalglide * Time.deltaTime);
            verticalglide *= upDownReduccion;
        }

        // Aceleracion izquierda/derecha
        if(izquierdaDerecha1D > 0.1f || izquierdaDerecha1D < -0.1f) //para evitar el drift del mando
        {
            rb.AddRelativeForce(Vector3.right * izquierdaDerecha1D * aceleracionLateral * Time.deltaTime);
            horizontalglide = izquierdaDerecha1D * aceleracionLateral;
        }else{
            rb.AddRelativeForce(Vector3.right * horizontalglide * Time.deltaTime);
            horizontalglide *= leftRightReduccion;
        }
    }

    void HandleShoot(){
        // Apuntado
        if (apuntar > 0.1f){
            float targetFOV = 35f;
            float lerpSpeed = 1.5f; // Velocidad de interpolación
            
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFOV, lerpSpeed * Time.deltaTime);
            sensibilidad = sensibilidad_ajuste / 2;
        }else{
            float targetFOV = 60f;
            float lerpSpeed = 1.5f; // Velocidad de interpolación
            
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFOV, lerpSpeed * Time.deltaTime);
            sensibilidad = sensibilidad_ajuste;
        }


        // Disparos
        if(disparo > 0.1f && Time.time > nextFire){
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

            nextFire = Time.time + (1/cadencia);
        }
    }

    #region Input methods
    
    public void OnDelanteDetras(InputAction.CallbackContext context){
        delanteDetras1D = context.ReadValue<float>();
    }

    public void OnIzquierdaDerecha(InputAction.CallbackContext context){
        izquierdaDerecha1D = context.ReadValue<float>();
    }

    public void OnArribaAbajo(InputAction.CallbackContext context){
        arribaAbajo1D = context.ReadValue<float>();
    }

    public void OnCabeceo(InputAction.CallbackContext context){
        cabeceo = context.ReadValue<Vector2>() * sensibilidad;
    }

    public void OnDisparo(InputAction.CallbackContext context){
        disparo = context.ReadValue<float>();
    }

    public void OnApuntar(InputAction.CallbackContext context){
        apuntar = context.ReadValue<float>();
    }

    #endregion
}