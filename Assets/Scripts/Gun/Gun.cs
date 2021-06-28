using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _muzzleFlash;

    public void Shoot()
    {
        StartCoroutine(ShowMuzzleFlash());
    }
    
    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator ShowMuzzleFlash()
    {
        _muzzleFlash.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        _muzzleFlash.SetActive(false);
    }
}
