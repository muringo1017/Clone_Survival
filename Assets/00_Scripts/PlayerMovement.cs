using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")] 
    public float moveSpeed = 5.0f;
    //public float gravity = -9.81f;

    [Space(20f)]
    [Header("Mouse Rotation")]
    public LayerMask groundLayer;
    public float rotateSpeed = 10.0f;
    
    private CharacterController _controller;
    private Animator _animator;
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        RotateTowardsMouse();
    }
    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        
        cameraForward.Normalize();
        cameraRight.Normalize();
        
        Vector3 moveDirection = (cameraForward * vertical) + (cameraRight * horizontal);
        
        _controller.Move(moveDirection * (moveSpeed * Time.deltaTime));

        float currentSpeed = moveDirection.magnitude * moveSpeed;
        _animator.SetFloat("Speed", currentSpeed);

    }

    void RotateTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay((Input.mousePosition));
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            Vector3 targetPosition = hit.point;
            
            Vector3 direction = (targetPosition - transform.position).normalized;
            direction.y = 0.0f;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            }
        }
    }
}

