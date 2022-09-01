using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputActionReference _moveInput;
    [SerializeField] InputActionReference _attack;
    [SerializeField] InputActionReference _sprint;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _speed;

    Vector3 direction;

    private void Reset()
    {
        _speed = 4f;
    }

    private void Start()
    {
        _moveInput.action.started += StartMove;
        _moveInput.action.performed += StartMove;
        _moveInput.action.canceled += EndMove;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(transform.position + (direction * _speed * Time.fixedDeltaTime));

    }


    private void StartMove(InputAction.CallbackContext obj)
    {
        Debug.Log("J'ai bougé");
        direction = obj.ReadValue<Vector2>(); //on lit la direction du bouton/joystick/....
    }
    
    
    private void EndMove(InputAction.CallbackContext obj)
    {
        direction = Vector2.zero; //on remet le deplacement à zero.
    }
}
