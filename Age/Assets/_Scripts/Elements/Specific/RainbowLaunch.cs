using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowLaunch : MonoBehaviour
{
    [SerializeField] Transform _rbTarget;
    [SerializeField] float _rbAngle = 0f;
    [SerializeField] float _rbGravity = 0f;
    [SerializeField] Transform _rbProjectile;
    private Transform _rbTransform;
    

    private void Awake()
    {
        _rbTransform = transform;
    }

    public void LaunchRainbow()
    {
        StartCoroutine(SimulateProjectile());
    }

    IEnumerator SimulateProjectile()
    {
        yield return new WaitForSeconds(2f);

        _rbProjectile.position = _rbTransform.position + new Vector3(0f, 0f, 0f);

        float _target_Distance = Vector3.Distance(_rbProjectile.position, _rbTarget.position);

        float _projectile_Velocity = _target_Distance / (Mathf.Sin(2 * _rbAngle * Mathf.Deg2Rad)) / 2;

        float xVel = Mathf.Sqrt(_projectile_Velocity) * Mathf.Cos(_rbAngle * Mathf.Deg2Rad);
        float yVel = Mathf.Sqrt(_projectile_Velocity) * Mathf.Sin(_rbAngle * Mathf.Deg2Rad);

        float flightDuration = _target_Distance / xVel;

        _rbProjectile.rotation = Quaternion.LookRotation(_rbTarget.position - _rbProjectile.position);

        float elapse_time = 0f;

        while (elapse_time < flightDuration)
        {
            _rbProjectile.Translate(0f, (yVel - (_rbGravity * elapse_time)) * Time.deltaTime /4f, xVel * Time.deltaTime /4f);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}