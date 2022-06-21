using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuryBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxFury()
    {
        slider.maxValue = 15;
        slider.value = 0;

        fill.color = gradient.Evaluate(0);
    }

    public void SetFuryLevel(int furyAmount)
    {
        slider.value = furyAmount;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
