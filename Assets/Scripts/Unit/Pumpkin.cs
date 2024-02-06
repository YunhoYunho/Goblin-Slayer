using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    private IEnumerator HittedRoutine()
    {
        SpawnManager.Instance.isSpawnStart = true;
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            StartCoroutine(HittedRoutine());
        }
    }
}
