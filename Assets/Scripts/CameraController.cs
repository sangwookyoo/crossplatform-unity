using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float _cameraRotateSpeed = 200;
    private float _xAxis;
    private float _yAxis;

    void LateUpdate()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        _xAxis += h * Time.deltaTime * _cameraRotateSpeed;
        _yAxis += v * Time.deltaTime * _cameraRotateSpeed;

        _yAxis = Mathf.Clamp(_yAxis, -60, 60);

        Vector3 dir = new Vector3(-_yAxis, _xAxis, 0);

        this.transform.eulerAngles = dir;
    }
}
