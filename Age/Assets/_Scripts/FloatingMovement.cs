using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatingMovement : MonoBehaviour {

    [SerializeField] private float _height;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _accelaration;
    private Rigidbody _rb;
    private Vector3 _startPos;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _startPos = transform.position;
    }

    private void FixedUpdate()
    {
        if(transform.position.y > _height + _startPos.y && _accelaration > 0)
            _accelaration *= -1;
        else if(transform.position.y < -_height + _startPos.y && _accelaration < 0)
            _accelaration *= -1;

        _rb.AddForce(transform.up * _accelaration * Time.fixedDeltaTime);

        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxSpeed);
    }
}