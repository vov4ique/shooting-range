using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private TargetSpawner _spawner;
    [SerializeField] private MouseController _mouseController;
    [SerializeField] private ShootingController _shootingController;

    private TargetController TargetController { get; set; }

    private void Awake()
    {
        _spawner = Instantiate(_spawner, Vector3.zero, Quaternion.identity);
        _spawner.TargetsReady += SpawnerOnTargetsReady;
        
        _mouseController = Instantiate(_mouseController, Vector3.zero, Quaternion.identity);
        _shootingController = Instantiate(_shootingController, Vector3.zero, Quaternion.identity, _mouseController.Camera.transform);
        _shootingController.Init(_mouseController);
        
        GameStart();
    }

    private void SpawnerOnTargetsReady(List<Target> targets, float radius)
    {
        TargetController = new TargetController(targets, radius);
    }

    private void GameStart()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
