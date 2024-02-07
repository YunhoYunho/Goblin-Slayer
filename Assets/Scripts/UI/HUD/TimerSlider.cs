using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerSlider : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup sliderGroup;
    public ItemButton itemButton;

    private Slider slider;
    private Coroutine durationRoutine;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        sliderGroup.alpha = 0;
    }

    public void SetMaxValue(float duration)
    {
        slider.maxValue = duration;
        slider.value = duration;
    }

    public void StartDuration(float duration)
    {
        sliderGroup.alpha = 1;
        transform.SetAsFirstSibling();
        if (durationRoutine != null)
            StopCoroutine(durationRoutine);

        durationRoutine = StartCoroutine(DurationCoroutine(duration));
    }

    private IEnumerator DurationCoroutine(float duration)
    {
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            slider.value = duration;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        sliderGroup.alpha = 0;
    }

}
