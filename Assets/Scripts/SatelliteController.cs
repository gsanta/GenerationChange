using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteController : MonoBehaviour
{

    public Transform viewPoint;
    public float mouseSensitivity = 1f;
    public CharacterController charController;
    public float moveSpeed = 5f;
    public Transform groundCheckPoint;
    public LayerMask groundLayers;
    public float jumpForce = 12f, gravityMod = 2.5f;
    public GameObject bikeCharacter;

    private Vector3 moveDir, movement;
    private bool isGrounded;
    private DroneMovement droneMovement;

    SatelliteController()
    {
        droneMovement = new DroneMovement(this);
    }


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        ServiceProvider.Instance().activeCharacter.SetDroneCharacter(gameObject);
    }

    void Update()
    {
        if (ServiceProvider.Instance().activeCharacter.activeCharacter == gameObject)
        {
            droneMovement.Move();
        }
    }

    private void HandleInputs()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        //Debug.Log(Input.GetAxisRaw("Horizontal"));

        float y = 0f;

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Space is pressed");
            y = 1;
        }

        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), y, Input.GetAxisRaw("Vertical"));

        movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x) + (transform.up * y)).normalized * moveSpeed;

        isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, .25f, groundLayers);

        //if (Input.GetButtonDown("Jump"))
        //{
        //    movement.y = jumpForce;
        //}
        //else if (charController.isGrounded)
        //{
        //    movement.y = 0;
        //}
        //else
        //{
        //    movement.y += Physics.gravity.y * Time.deltaTime * gravityMod;
        //}

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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ServiceProvider.Instance().activeCharacter.UpdateActiveCharacter(ServiceProvider.Instance().activeCharacter.GetOtherCharacter(gameObject));
        }
    }
}
