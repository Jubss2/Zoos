using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
//using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private int speed = 10;
    private Rigidbody2D rb;
    private PlayerControls input;
    private Vector3 lastMove;
    private Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = new PlayerControls();
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += OnMove;
        input.Player.Move.started += OnMove;
        input.Player.Move.canceled += OnMove;

    }
    private void OnDisable()
    {
        input.Disable();
        input.Player.Move.performed -= OnMove;
        input.Player.Move.started -= OnMove;
        input.Player.Move.canceled -= OnMove;
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector3 movementDirection = context.ReadValue<Vector2>();
        if(movementDirection.x!=0)
        {
            animator.SetBool("AndandoLados", true);
            animator.SetBool("AndandoFrente", false);
            animator.SetBool("AndandoAtras", false);
            if(movementDirection.x < 0)
            {
                transform.eulerAngles = new Vector3(0, 180f, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        if ((movementDirection.y > 0)&&(movementDirection.x == 0))
        {
            animator.SetBool("AndandoLados", false);
            animator.SetBool("AndandoFrente", true);
            animator.SetBool("AndandoAtras", false);
            transform.eulerAngles = new Vector3(0,0,0);
        }
        if ((movementDirection.y < 0) && (movementDirection.x == 0))
        {
            animator.SetBool("AndandoLados", false);
            animator.SetBool("AndandoFrente", false);
            animator.SetBool("AndandoAtras", true);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if((movementDirection == Vector3.zero))
        {
            animator.SetBool("AndandoLados", false);
            animator.SetBool("AndandoFrente", false);
            animator.SetBool("AndandoAtras", false);
        }
        rb.velocity = new Vector3(movementDirection.x, movementDirection.y, 0f) * speed;
        SetLastMove(movementDirection);
    }
    public Vector3 GetLastMove()
    {
        return lastMove;
    }
    public void SetLastMove(Vector3 movementDirection)
    {
        Vector3 zero = new Vector3(0f, 0f, 0f);
        if (movementDirection != zero)
        {
            lastMove = movementDirection;
        }
    }
}
