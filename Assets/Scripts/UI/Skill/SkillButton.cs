using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum SkillKeyType { Q, E }

public class SkillButton : MonoBehaviour
{
    [SerializeField]
    private SkillKeyType skillKeyType;
    [SerializeField]
    private PlayerSkill player;
    [SerializeField]
    private SkillData skillData;
    [SerializeField]
    private Image skillImage;
    [SerializeField]
    private Image coolTimeImage;
    [SerializeField]
    private TextMeshProUGUI coolTimeText;
    [SerializeField]
    private TimerSlider timerSlider;

    private void Start()
    {
        skillImage.sprite = skillData.icon;
        coolTimeImage.fillAmount = 0;
        coolTimeText.text = "";
    }

    private void Update()
    {
        switch (skillKeyType)
        {
            case SkillKeyType.Q:
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    OnClickedSkillButton();
                }
                break;
            case SkillKeyType.E:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    OnClickedSkillButton();
                }
                break;
        }
    }

    public void OnClickedSkillButton()
    {
        if (coolTimeImage.fillAmount > 0)
            return;

        if (skillKeyType == SkillKeyType.E)
        {
            timerSlider.SetMaxValue(skillData.coolTime);
            timerSlider.StartDuration(skillData.coolTime);
        }

        player.ActivateSkill(skillData);
        StartCoroutine(StartCoolTimeRoutine());
    }

    private IEnumerator StartCoolTimeRoutine()
    {
        float tick = 1f / skillData.coolTime;
        float curTime = 0;

        coolTimeImage.fillAmount = 1;

        while (coolTimeImage.fillAmount > 0)
        {
            coolTimeImage.fillAmount = Mathf.Lerp(1, 0, curTime);
            curTime += (Time.deltaTime * tick);

            float remainTime = skillData.coolTime * (1 - curTime);
            coolTimeText.text = remainTime.ToString("0");
            yield return null;
        }
        coolTimeText.text = "";
    }
}
