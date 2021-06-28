using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private Ammo _ammo;
    [SerializeField] private float _force = 1000f; //todo move to json
    
    private MouseController _mouseController;
    private Camera _camera;
    public void Init(MouseController mouseController)
    {
        _mouseController = mouseController;
        _camera = mouseController.Camera;
        
        _mouseController.Shoot += OnShoot;
    }

    private void OnDestroy()
    {
        _mouseController.Shoot += OnShoot;
    }

    private void OnShoot()
    {
        _gun.Shoot();
        
        var ammo = Instantiate(_ammo, _gun.transform.position + new Vector3(0, 0, 0.5f), Quaternion.identity);
        ammo.Rigidbody.AddForce(_camera.transform.forward * _force);
    }
}
