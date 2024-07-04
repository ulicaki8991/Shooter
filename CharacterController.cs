using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public GameObject bulletPrefab; // Assign your bullet prefab here
    public GameObject impactEffectPrefab; // Assign your impact effect prefab here
    public Transform gunEnd; // The transform representing the end of the gun barrel

    private CharacterController characterController;
    private Transform cameraTransform;
    private float verticalRotation = 0f;

    [Header("Gun")]
    [SerializeField] Animator Gun;
    [SerializeField] int bullets;

    [Header("UI")]
    [SerializeField] Text BulletsText;
    [SerializeField] Text HPText;

    [Header("General")]
    [SerializeField] int Hp;

    void Start()
    {
        BulletsText.text = "" + bullets;
        HPText.text = "HP: " + Hp;
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        // Lock cursor to center of screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void AddBullets (int newBulllets)
    {
        bullets += newBulllets;
        BulletsText.text = "" + bullets;
    }

    public void MinusHp ()
    {
        Hp -= 7;
        HPText.text = "HP: " + Hp;
        if (Hp <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    void Update()
    {
        // Player Movement
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float verticalMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        Vector3 moveDirection = transform.TransformDirection(new Vector3(horizontalMovement, 0f, verticalMovement));
        characterController.Move(moveDirection);

        // Camera Rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation += mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            if(bullets > 0)
            Shoot();
        }
    }

    void Shoot()
    {
        bullets--;
        BulletsText.text = "" + bullets;
        Gun.SetTrigger("shoot");
        RaycastHit hit;
        Vector3 rayOrigin = cameraTransform.position;
        Vector3 rayDirection = cameraTransform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, Mathf.Infinity, ~LayerMask.GetMask("Player")))
        {
            Debug.Log("Hit: " + hit.transform.name);

            // Instantiate impact effect at hit point
            Instantiate(impactEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));


            if(hit.transform.gameObject.CompareTag("Enemy"))
            {
                hit.transform.gameObject.GetComponent<EnemyAI>().Hit();
            }

        }
        else
        {
            Debug.Log("Missed");
        }
    }
}
