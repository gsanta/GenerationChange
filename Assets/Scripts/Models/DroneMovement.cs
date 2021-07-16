using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement
{

    private SatelliteController droneController;
    public float mouseSensitivity = 1f;
    public float moveSpeed = 5f;
    private Vector3 moveDir, movement;
    private bool isGrounded;
    public float yPos = 3f;

    public DroneMovement(SatelliteController _droneController)
    {
        droneController = _droneController;
    }

    public void Move()
    {
        Transform transform = droneController.transform;
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        //Debug.Log(Input.GetAxisRaw("Horizontal"));

        float y = 0f;

        if (Input.GetKey(KeyCode.Space))
        {
            y = 1;
        } else if (droneController.groundCheckPoint.position.y > yPos)
        {
            y = -1;
        }

        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), y, Input.GetAxisRaw("Vertical"));

        movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x) + (transform.up * y)).normalized * moveSpeed;

        isGrounded = Physics.Raycast(droneController.groundCheckPoint.position, Vector3.down, .25f, droneController.groundLayers);

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

        droneController.charController.Move(movement * Time.deltaTime);

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
            ServiceProvider.Instance().activeCharacter.UpdateActiveCharacter(ServiceProvider.Instance().activeCharacter.GetOtherCharacter(droneController.gameObject));
        }
    }
}
