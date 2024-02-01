using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum KeyType { Q, E }

public class SkillButton : MonoBehaviour
{
    [SerializeField]
    private KeyType keyType;
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

    private void Start()
    {
        skillImage.sprite = skillData.Icon;
        coolTimeImage.fillAmount = 0;
        coolTimeText.text = "";
    }

    private void Update()
    {
        switch (keyType)
        {
            case KeyType.Q:
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    OnClickedSkillButton();
                }
                break;
            case KeyType.E:
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

        player.ActivateSkill(skillData);
        StartCoroutine(StartCoolTime());
    }

    private IEnumerator StartCoolTime()
    {
        float tick = 1f / skillData.CoolTime;
        float curTime = 0;

        coolTimeImage.fillAmount = 1;

        while (coolTimeImage.fillAmount > 0)
        {
            coolTimeImage.fillAmount = Mathf.Lerp(1, 0, curTime);
            curTime += (Time.deltaTime * tick);

            float remainTime = skillData.CoolTime * (1 - curTime);
            coolTimeText.text = remainTime.ToString("0");
            yield return null;
        }
        coolTimeText.text = "";
    }
}
