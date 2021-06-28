using System;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private float _mouseSensetivity = 100f; // todo move to settings
    [SerializeField] private float _clampValue = 90f; // todo move to settings
    [SerializeField] private Camera _camera;
    
    public event Action Shoot = delegate { };

    public Camera Camera => _camera;
    
    private float _xRotation = 0f;
    private float _yRotation = 0f;
    
    private void Update()
    {
        ManagePlayerCameraRotation();
        ManagePlayerClick();
    }

    private void ManagePlayerClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void ManagePlayerCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensetivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensetivity * Time.deltaTime;

        _xRotation = Mathf.Clamp(_xRotation - mouseY, -_clampValue, _clampValue);
        _yRotation += mouseX;

        _camera.transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
    }
}
