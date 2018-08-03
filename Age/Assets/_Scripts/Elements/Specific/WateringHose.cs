using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringHose : MonoBehaviour 
{
	[SerializeField] private float _accel = 0.8f, _inertia = 0.9f, _speedLimit = 10.0f, _minSpeed = 1.0f, _stopTime = 1.0f, rotationDamping = 6.0f;
	private float _currSpeed = 0.0f, _state = 0;
    [SerializeField] private ParticleSystem _waterHose;

    [SerializeField] private bool smoothRotation = true;
    private bool _accelState, _slowState;

	private Transform waypoint;
    [SerializeField] private Transform[] waypoints;

	private int WPindexPointer;

	private void Start( )
	{
		_state = 0;
	}

	void Update ()
	{
        if (_waterHose.isPlaying)
        {
            if (_state == 0)
            {
                Accell();
            }

            if (_state == 1)
            {
                StartCoroutine(Slow());
            }

            waypoint = waypoints[WPindexPointer];
        }
	}

    public void BeginEffect()
    {
        _waterHose.Play();
    }

	// I declared "Accell()".
	private void Accell ()
	{
		if (_accelState == false)
		{
			//If Accell() is running, Slow() can not run
			_accelState = true;
			_slowState = false;
		}

		if (waypoint)
		{
			if (smoothRotation)
			{
				var rotation = Quaternion.LookRotation(waypoint.position - transform.position);

                //Smooth look rotation
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
			}
		}

		_currSpeed = _currSpeed + _accel * _accel;
		transform.Translate (0,0,Time.deltaTime * _currSpeed);

		if (_currSpeed >= _speedLimit)
		{
			_currSpeed = _speedLimit;
		}
	}

    //Each waypoint has a trigger collider
	private void OnTriggerEnter ()
	{
		_state = 1;

        //Reach waypoint then move to next
		WPindexPointer++;

		if (WPindexPointer >= waypoints.Length)
		{
            WPindexPointer = 0;
            StartCoroutine(DestroyDelay());
		}
	}

    

	private IEnumerator Slow()
	{
		if (_slowState == false) //
		{
            //If Slow() is running, Accell() can not run
            _accelState = false;
			_slowState = true;
		}

		_currSpeed = _currSpeed * _inertia;
		transform.Translate (0,0,Time.deltaTime * _currSpeed);

		if (_currSpeed <= _minSpeed)
		{
			_currSpeed = 0.0f;

			yield return new WaitForSeconds(_stopTime);

            _state = 0;
		}
	}

    private IEnumerator DestroyDelay()
    {
        _waterHose.Stop();

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);

        yield return null;
    }

}

