using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField] private InputActionReference jumpInput;
    [SerializeField] private float jumpForce = 200f;
    private CharacterController character;
    private Vector3 velocity;
    private float gravity = -9.81f;

    void Start()
    {
        character = GetComponent<CharacterController>();
        jumpInput.action.Enable();
        jumpInput.action.performed += Jumping;
    }

    void Jumping(InputAction.CallbackContext context)
    {
        if (character.isGrounded)
        {
            velocity = Vector3.zero; // Reset velocity
            velocity.y = jumpForce;
        }
    }

    void Update()
    {
        if (character.isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        velocity.y += gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime);
    }
}