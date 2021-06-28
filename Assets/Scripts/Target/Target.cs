using System;
using cakeslice;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Outline _outline;
    
    public TextMesh textMesh;
    public event Action<bool> Hit = delegate { };

    private bool _isHighlighted;
    
    public void Highlight(bool highlighted)
    {
        _isHighlighted = highlighted;

        _outline.enabled = highlighted;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Ammo"))
        {
            Hit(_isHighlighted);
        }
    }
}
