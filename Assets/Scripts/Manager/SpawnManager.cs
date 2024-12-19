using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : SingleTon<SpawnManager>
{
    [SerializeField]
    private GameObject bossPrefab;
    [SerializeField]
    private GameObject bossHPBar;
    [SerializeField]
    private Transform[] spawnPoints;
    [Space]
    [SerializeField]
    private float spawnInterval;
    public float spawnDuration;
    public float waitDuration;
    public int waveCount = 0;
    public bool isSpawnStart = false;

    private float timer = 0;
    private int spawnPointNum = 0;
    private GetPoolObject getPool;
    private List<GameObject> spawnGoblinList = new List<GameObject>();

    private void Start()
    {
        getPool = GameObject.Find("GetPoolObject").GetComponent<GetPoolObject>();
        spawnPointNum = 0;
        waveCount = 0;
        bossPrefab.SetActive(false);
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitUntil(() => isSpawnStart);

        while (waveCount < 3)
        {
            spawnPointNum = 0;
            waveCount++;
            timer = 0;
            while (timer < spawnDuration)
            {
                if (spawnPointNum >= spawnPoints.Length)
                    spawnPointNum = 0;

                spawnGoblinList.Add(getPool.GetPool("Goblin", spawnPoints[spawnPointNum].position, spawnPoints[spawnPointNum].rotation));
                spawnPointNum++;
                yield return new WaitForSeconds(spawnInterval);
                timer += spawnInterval;
            }
            ClearGoblins();
            yield return new WaitForSeconds(1.5f);
            yield return new WaitForSeconds(waitDuration);
        }

        if (waveCount >= 3)
        {
            SpawnBoss();
        }
    }

    private void ClearGoblins()
    {
        foreach (var goblin in spawnGoblinList)
        {
            GoblinController go = goblin.GetComponent<GoblinController>();
            if (go != null && go.state != GoblinController.State.Die)
            {
                go.state = GoblinController.State.Die;
            }
        }
        spawnGoblinList.Clear();
    }

    private void SpawnBoss()
    {
        bossHPBar.SetActive(true);
        bossPrefab.SetActive(true);
    }
}
