using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Slider slider;
    public void SetMaxValue(float maxValue) 
    {
        slider.maxValue = maxValue;
    }
    public void SetCurrentValue(float currValue)
    {
        slider.value = currValue;
    }
}
