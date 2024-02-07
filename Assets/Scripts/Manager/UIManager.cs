using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : SingleTon<UIManager>
{
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
    
    private void Start()
    {
        StartCoroutine(TimerRoutine());
    }

    private IEnumerator TimerRoutine()
    {
        yield return new WaitUntil(() => SpawnManager.Instance.isSpawnStart);

        while (SpawnManager.Instance.waveCount < 3)
        {
            yield return StartCoroutine(UpdateTimeRoutine(SpawnManager.Instance.spawnDuration, true));
            yield return new WaitForSeconds(1.5f);
            yield return StartCoroutine(UpdateTimeRoutine(SpawnManager.Instance.waitDuration, false));
        }
        waitTimeText.gameObject.SetActive(false);
        bossTimeText.gameObject.SetActive(true);
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
}
