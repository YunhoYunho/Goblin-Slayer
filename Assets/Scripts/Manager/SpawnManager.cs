using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : SingleTon<SpawnManager>
{
    [SerializeField]
    private GetPoolObject getPool;
    [SerializeField]
    private Transform[] spawnPoints;
    [Space]
    [SerializeField]
    private float timer = 0;
    [SerializeField]
    private float spawnInterval;
    [SerializeField]
    private float spawnDuration;
    [SerializeField]
    private float waitDuration;

    private int spawnPointNum = 0;

    private void Start()
    {
        getPool = GameObject.Find("GetPoolObject").GetComponent<GetPoolObject>();
        spawnPointNum = 0;
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            timer = 0;
            while (timer < spawnDuration)
            {
                if (spawnPointNum == spawnPoints.Length - 1)
                    spawnPointNum = 0;

                getPool.GetPool("Goblin", spawnPoints[spawnPointNum].position, spawnPoints[spawnPointNum].rotation);

                spawnPointNum++;
                yield return new WaitForSeconds(spawnInterval);
                timer += spawnInterval;
            }
            yield return new WaitForSeconds(waitDuration);
        }
    }
}
