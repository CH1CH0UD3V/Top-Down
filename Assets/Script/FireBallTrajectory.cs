using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallTrajectory : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _speed;
    [SerializeField] Vector3 _direction;

    private void Start()
    {
        _rb.AddForce(_direction * Time.fixedDeltaTime * _speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
