using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnFireBall : MonoBehaviour
{
    [SerializeField] InputActionReference _secondAttackInput;
    [SerializeField] GameObject _fireBall;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] Animator _animator;
    [SerializeField] float _cooldown;

    bool _isAttack2;
    [SerializeField] float _timer;

    private void Start()
    {
        //attack2
        _secondAttackInput.action.started += StartAttack2;
        _secondAttackInput.action.canceled += EndAttack2;
    }


    private void FixedUpdate()
    {
        _animator.SetBool("IsAttack2", _isAttack2);

        if(_timer > 0)
        {
            _timer -= Time.fixedDeltaTime;
        }

        if(_secondAttackInput.action.IsPressed())
        {
            StartAttack2();
        }
    }

    private void StartAttack2(InputAction.CallbackContext obj) => StartAttack2();
    private void StartAttack2()
    {
        if (_timer > 0) return;

        Debug.Log("J'ai attaqué : attaque 2");
        _isAttack2 = true;
        GameObject.Instantiate(_fireBall, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint);
        _timer = _cooldown;
    }

    private void EndAttack2(InputAction.CallbackContext obj)
    {
        Debug.Log("J'ai fini mon attaque 2");
        _isAttack2 = false;
    }
}
