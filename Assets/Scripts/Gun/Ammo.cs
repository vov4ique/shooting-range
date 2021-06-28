using System.Collections;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private const float SecondsToDestroy = 4f;

    public Rigidbody Rigidbody => _rigidbody;
    
    private void Start()
    {
        StartCoroutine(DestroyAfterFewSeconds());
    }

    private IEnumerator DestroyAfterFewSeconds()
    {
        yield return new WaitForSecondsRealtime(SecondsToDestroy);
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Target"))
        {
            Destroy(gameObject);
        }
    }
}
