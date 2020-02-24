using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class TimeSlideCounter : MonoBehaviour
{
    [SerializeField]private Color finishColor;
    [SerializeField]private float maxTime = 5;
    private Image slider;
    private float currentTime;
    private float ActiveTime = 0;
    void Start()
    {
        slider = GetComponent<Image>();
        currentTime = maxTime;
        StartTime();
    }
    public void ResetTime()
    {
        slider.fillAmount = 1;
        ActiveTime = 0;
        slider.color = Color.white;
    }

    public void StartTime()
    {
        StartCoroutine(SlideTime());
    }

    private IEnumerator SlideTime()
    {
        Color startColor = slider.color;
        while(slider.fillAmount > 0)
        {
            ActiveTime += Time.deltaTime;
            slider.fillAmount = 1 - (ActiveTime / maxTime);
            
            slider.color = Color.Lerp(startColor, finishColor, 1 - slider.fillAmount);
            yield return null;
        }
    }
}
