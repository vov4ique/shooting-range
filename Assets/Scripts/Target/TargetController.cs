using System.Collections.Generic;
using UnityEngine;

public class TargetController
{
    private List<Target> _targets;
    private int _hitCount;
    private Dictionary<int, List<int>> _neighboursDict = new Dictionary<int, List<int>>();
    private float _radius;

    public TargetController(List<Target> targets, float radius)
    {
        _targets = targets;
        _radius = radius;
        
        for (var i = 0; i < _targets.Count; i++)
        {
            var index = i;
            _targets[i].Hit += (highlighted) => OnHit(index, highlighted);
        }

        HighlightTarget(Random.Range(0, _targets.Count + 1), true);
        FillNeighboursDict();
    }

    private void FillNeighboursDict()
    {
        for (int i = 0; i < _targets.Count; i++)
        {
            List<int> neighbours = new List<int>();

            for (int j = 0; j < _targets.Count; j++)
            {
                if (j < _targets.Count - 1)
                {
                    var magn = (_targets[i].transform.position - _targets[j].transform.position).magnitude;
                    float dif = _targets.Count / _radius;
                    
                    if (magn < dif)
                    {
                        neighbours.Add(j);
                    }
                }
            }

            _neighboursDict.Add(i, neighbours);
        }
    }
    
    private void OnHit(int number, bool highlighted)
    {
        if (highlighted)
        {
            HighlightTarget(number, false);
            _hitCount++;
            HighlightTarget(FindNewTarget(number), true);
        }
    }

    private void HighlightTarget(int number, bool highlighted)
    {
        _targets[number].Highlight(highlighted);
    }

    private int FindNewTarget(int oldTarget)
    {
        List<int> random = new List<int>();
        if (_neighboursDict.TryGetValue(oldTarget, out List<int> neighbours))
        {
            for (int i = 0; i < _targets.Count; i++)
            {
                if (!neighbours.Contains(i))
                {
                    random.Add(i);
                }
            }
        }
        else
        {
            return Random.Range(0, _targets.Count + 1);
        }

        return Random.Range(0, random.Count + 1);
    }
}
