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
    [SerializeField]
    private TextMeshProUGUI timeRemainText;
    [SerializeField]
    private TextMeshProUGUI fightTimeText;
    [SerializeField]
    private TextMeshProUGUI waitTimeText;
    [SerializeField]
    private TextMeshProUGUI bossTimeText;
    [Space]
    [SerializeField]
    private float remainTime;
    [SerializeField]
    private float spawnInterval;
    [SerializeField]
    private float spawnDuration;
    [SerializeField]
    private float waitDuration;
    [SerializeField]
    private int waveCount = 0;

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
        StartCoroutine(SpawnRoutine());
        StartCoroutine(TimerRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitUntil(() => isSpawnStart);

        while (waveCount < 3)
        {
            waveCount++;
            timer = 0;
            while (timer < spawnDuration)
            {
                if (spawnPointNum == spawnPoints.Length - 1)
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

    private IEnumerator TimerRoutine()
    {
        yield return new WaitUntil(() => isSpawnStart);

        while (waveCount < 3)
        {
            yield return StartCoroutine(UpdateTimeRoutine(spawnDuration, true));
            yield return new WaitForSeconds(1.5f);
            yield return StartCoroutine(UpdateTimeRoutine(waitDuration, false));
        }
    }

    private IEnumerator UpdateTimeRoutine(float duration, bool isSpawnTime)
    {
        fightTimeText.gameObject.SetActive(isSpawnTime);
        waitTimeText.gameObject.SetActive(!isSpawnTime);
        remainTime = 0;
        while (remainTime < duration)
        {
            timeRemainText.text = (duration - remainTime).ToString("0");
            remainTime += Time.deltaTime;
            yield return null;
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
        bossPrefab.SetActive(true);
        StopCoroutine(TimerRoutine());
        waitTimeText.gameObject.SetActive(false);
        bossTimeText.gameObject.SetActive(true);
    }
}
