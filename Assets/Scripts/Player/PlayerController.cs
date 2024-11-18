using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _maxspeed;
    [SerializeField] private GameObject _flashLight;
    public Camera playerCamera;
    private float rotationX;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    private Rigidbody _rb;
    private bool _isGameRunning = true;

    public void GameOver()
    {
        _isGameRunning = false;
    }
    public void GameWin()
    {
        _isGameRunning = false;
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        playerMovementInput();
    }
    private void Update()
    {
        playerLookInput();
    }
    private void playerMovementInput()
    {
        if (_isGameRunning)
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var movement = new Vector3(
                Time.deltaTime * horizontal * _maxspeed,
                0,
                Time.deltaTime * vertical * _maxspeed
            );
            _rb.MovePosition(transform.position + transform.TransformDirection(movement));
        }
    }
    private void playerLookInput()
    {   if (_isGameRunning)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            _flashLight.transform.localRotation = Quaternion.Euler(rotationX + 90, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
