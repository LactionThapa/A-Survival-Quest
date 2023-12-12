using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 userInput;
    private Transform playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forwardVector = playerCamera.forward;
        forwardVector.y = 0.0f;

        Vector3 rightVector = playerCamera.right;
        rightVector.y = 0.0f;

        Vector3 moveDirection = forwardVector * userInput.y + rightVector * userInput.x;

        rb.velocity = moveDirection * 8.0f;
    }

    public void OnMove(InputValue value)
    {
        userInput = value.Get<Vector2>();
    }
}
