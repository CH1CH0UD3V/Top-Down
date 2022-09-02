using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputActionReference _moveInput;
    [SerializeField] InputActionReference _attack;
    [SerializeField] InputActionReference _sprint;
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _speed;
    [SerializeField] float _fastForward;

    Vector3 direction;
    bool _isWalked;

    private void Reset()
    {
        _speed = 4f;
        _fastForward = 2.5f;
    }

    private void Start()
    {
        _moveInput.action.started += StartMove;
        _moveInput.action.performed += StartMove;/*on utilise performed pour savoir ou en est le joystick ou le bouton ,
                                                  * du coup ici on le met dans startmove pour avoir le bouton haut et gauche par exemple appuyé ensemble pour faire une diagonale. */
        _moveInput.action.canceled += EndMove;

        _sprint.action.started += SprintStart;
        _sprint.action.canceled += SprintEnd;
    }

    private void FixedUpdate()
    {
        _animator.SetBool("IsWalked", _isWalked);

        if (_isWalked)
        {
            _rb.MovePosition(transform.position + (direction  * Time.fixedDeltaTime* _speed));
        }
        else
        {
            _rb.MovePosition(transform.position + (direction  * Time.fixedDeltaTime* (_speed * _fastForward)));
        }

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

    private void SprintStart(InputAction.CallbackContext obj)
    {
        Debug.Log("Cours FOREST !!! COOUUUUUUURRRRTTTTT");
        _isWalked = true;
    }
    private void SprintEnd(InputAction.CallbackContext obj)
    {
        Debug.Log("STOOOOOOOOPPPPP");
        _isWalked = false;
    }
}
