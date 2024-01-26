using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    [SerializeField]
    private float returnTime;

    public PoolManager poolManager;

    private void Start()
    {
        poolManager = FindObjectOfType<PoolManager>();
    }

    public void OnEnable()
    {
        StartCoroutine(DelayToReturn());
    }

    private IEnumerator DelayToReturn()
    {
        yield return new WaitForSeconds(returnTime);

        poolManager.Release(gameObject);
    }
}
