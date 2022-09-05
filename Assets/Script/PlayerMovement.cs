using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputActionReference _moveInput;
    [SerializeField] InputActionReference _attackInput;
    [SerializeField] InputActionReference _sprintInput;
    [SerializeField] InputActionReference _actionInput;
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _speed;
    [SerializeField] float _fastForward;

    Vector3 direction;
    Vector3 _aim;
    bool _isRunning;
    bool _isAttack;

    private void Reset()
    {
        _speed = 4f;
        _fastForward = 2.5f;
    }

    private void Start()
    {
        //move
        _moveInput.action.started += StartMove;
        _moveInput.action.performed += StartMove;/*on utilise performed pour savoir ou en est le joystick ou le bouton ,
                                                  * du coup ici on le met dans startmove pour avoir le bouton haut et gauche par exemple appuyé ensemble pour faire une diagonale. */
        _moveInput.action.canceled += EndMove;
        
        //sprint
        _sprintInput.action.started += SprintStart;
        _sprintInput.action.canceled += SprintEnd;

        //attack
        _attackInput.action.started += StartAttack;
        _attackInput.action.performed += StartAttack;
        _attackInput.action.canceled += EndAttack;

        //action
        _actionInput.action.started += BeginInteraction;
        _actionInput.action.canceled += EndInteraction;
    }



    private void FixedUpdate()
    {
        _animator.SetBool("IsRunning", _isRunning);
        _animator.SetBool("IsWalking", direction.magnitude > 0.1f);
        _animator.SetFloat("Horizontal", _aim.x);
        _animator.SetFloat("Vertical", _aim.y);
        _animator.SetBool("IsAttack", _isAttack);

        if (_isRunning)
        {
            _rb.MovePosition(transform.position + (direction  * Time.fixedDeltaTime* (_speed * _fastForward)));
        }
        else
        {
            _rb.MovePosition(transform.position + (direction  * Time.fixedDeltaTime* _speed));
        }

    }

    private void StartMove(InputAction.CallbackContext obj)
    {
        Debug.Log("J'ai bougé");
        direction = obj.ReadValue<Vector2>(); //on lit la direction du bouton/joystick/....
        _aim = direction;
    }
    
    private void EndMove(InputAction.CallbackContext obj)
    {
        direction = Vector2.zero; //on remet le deplacement à zero.
    }

    private void SprintStart(InputAction.CallbackContext obj)
    {
        Debug.Log("Cours FOREST !!! COOUUUUUUURRRRTTTTT");
        _isRunning = true;
        //_isAttack = true;
    }
    private void SprintEnd(InputAction.CallbackContext obj)
    {
        Debug.Log("STOOOOOOOOPPPPP");
        _isRunning = false;
        //_isAttack = false;
    }


    private void StartAttack(InputAction.CallbackContext obj)
    {
        Debug.Log("J'ai attaqué");
        _isAttack = true;
    }


    private void EndAttack(InputAction.CallbackContext obj)
    {
        Debug.Log("J'ai fini mon attaque");
        _isAttack = false;
    }


    private void BeginInteraction(InputAction.CallbackContext obj)
    {
        Debug.Log("J'ai fais une action");
    }


    private void EndInteraction(InputAction.CallbackContext obj)
    {
        Debug.Log("J'ai fini mon action");   
    }
}
