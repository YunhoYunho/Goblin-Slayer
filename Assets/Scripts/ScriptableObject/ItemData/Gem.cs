using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed;

    private Coroutine rotateRoutine;

    private void OnEnable()
    {
        rotateRoutine = StartCoroutine(RotatingRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(rotateRoutine);
    }

    private IEnumerator RotatingRoutine()
    {
        while (true)
        {
            transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
            yield return null;
        }
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
