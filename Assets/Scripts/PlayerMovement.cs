using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float moveSpeed;

    private CharacterController controller;

    private float gravity = 9.81f;

    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveVelocity = (cam.transform.right * moveInput.x + cam.transform.forward * moveInput.y + Vector3.down * gravity) * Time.deltaTime * moveSpeed;
        controller.Move(moveVelocity);
    }

    public void GetMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
