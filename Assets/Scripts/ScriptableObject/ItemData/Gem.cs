using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed;

    private Collider coll;
    private Coroutine rotateRoutine;
    private Coroutine getRoutine;

    private void Awake()
    {
        coll = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        coll.isTrigger = false;
        rotateRoutine = StartCoroutine(RotatingRoutine());
        getRoutine = StartCoroutine(GetRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(rotateRoutine);
        StopCoroutine(getRoutine);
    }

    private IEnumerator RotatingRoutine()
    {
        while (true)
        {
            transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
            yield return null;
        }
    }

    private IEnumerator GetRoutine()
    {
        yield return new WaitForSeconds(2f);
        coll.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int money = Random.Range(10, 51);
            ShopManager.Instance.coins += money;
            ShopManager.Instance.UpdateCoin();
            PoolManager.Instance.Release(gameObject);
        }
    }
}
