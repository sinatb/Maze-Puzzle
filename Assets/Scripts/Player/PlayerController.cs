using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Camera playerCamera;
    private float rotationX;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    private void Update()
    {
        playerMovementInput();
    }
    private void playerMovementInput()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * (Time.deltaTime * vertical * speed));
        transform.Translate(Vector3.right * (Time.deltaTime * horizontal * speed));
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }
}
