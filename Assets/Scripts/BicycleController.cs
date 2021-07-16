using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BicycleController : MonoBehaviour
{
    public Transform viewPoint;
    public float mouseSensitivity = 1f;
    public CharacterController charController;
    public float moveSpeed = 5f;
    public Transform groundCheckPoint;
    public LayerMask groundLayers;
    public float jumpForce = 12f, gravityMod = 2.5f;
    public GameObject satelliteCharacter;

    private Vector3 moveDir, movement;
    private bool isGrounded;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Bicycle Start");

        ServiceProvider.Instance().activeCharacter.SetBikeCharacter(gameObject);
    }

    void Update()
    {
        if (ServiceProvider.Instance().activeCharacter.activeCharacter == gameObject)
        {
            HandleInputs();
        }
    }

    private void HandleInputs()
    {

        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        float yVal = movement.y;
        movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized * moveSpeed;
        movement.y = yVal;

        isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, .25f, groundLayers);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            movement.y = jumpForce;
        }
        else if (charController.isGrounded)
        {
            movement.y = 0;
        }
        else
        {
            movement.y += Physics.gravity.y * Time.deltaTime * gravityMod;
        }

        charController.Move(movement * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Cursor.lockState == CursorLockMode.None)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ServiceProvider.Instance().activeCharacter.UpdateActiveCharacter(ServiceProvider.Instance().activeCharacter.GetOtherCharacter(gameObject));
        }
    }
}
