using System;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private Target _target;
    [SerializeField] private int _targetAmount = 100; //todo move to json
    [SerializeField] private float _radius = 20;//todo move to json

    public event Action<List<Target>, float> TargetsReady = delegate { };
    
    private List<Target> _targets = new List<Target>();
    private List<Vector3> _points = new List<Vector3>();

    private void Start()
    {
        GeneratePoints(_targetAmount * 2);
        SpawnTargets();
    }

    /// <summary>
    /// Instantiates targets from previously generated points
    /// </summary>
    private void SpawnTargets()
    {
        for (var i = 0; i < _points.Count; i++) //todo remove textmesh
        {
            var point = _points[i];
            var target = Instantiate(_target, point * _radius, Quaternion.identity, transform);
            //target.textMesh.text = i.ToString(); //debug feature
            _targets.Add(target);
        }

        TargetsReady(_targets, _radius);
    }

    /// <summary>
    /// Generates evenly distributed points on a surface of a sphere
    /// </summary>
    /// <param name="amount"> amount of points to generate</param>
    private void GeneratePoints(int amount)
    {
        float x, y, z, r, phi;
        float inc = Mathf.PI * (3 - Mathf.Sqrt(5));
        float off = 2.0f / amount;
        
        for (int i = 0; i < amount; i++)
        {
            y = i * off - 1 + (off /2);
            r = Mathf.Sqrt(1 - y * y);
            phi = i * inc;
            x = Mathf.Cos(phi) * r;
            z = Mathf.Sin(phi) * r;

            if (z > 0) // condition to generate point in semi sphere
            {
                _points.Add(new Vector3(x, y, z));
            }
        }
    }
}
