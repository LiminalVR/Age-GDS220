using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    [SerializeField] private bool _enabled;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _cam;
    private Camera _camCam;

    private void Start()
    {
        _camCam = _cam.GetComponent<Camera>();

        if(!_enabled)
            Destroy(this);
    }

    private void Update()
    {
        RotateCam();
    }

    private void RotateCam()
    {
        Quaternion camRot = _cam.transform.rotation;
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        Vector3 posOnScreen = _camCam.ScreenToWorldPoint(new Vector3(mouseX, mouseY, 10.0f));
        Vector3 newDir = camRot.eulerAngles + posOnScreen.normalized;
        transform.LookAt(posOnScreen);
    }

    private void MoveCam()
    {

    }
}
