using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    public Slider barSlider;

    public void SetMaxValue(int fullness)
    {
        barSlider.maxValue = fullness;
        barSlider.value = fullness;
    }

    public void SetValue(int fullness)
    {
        barSlider.value = fullness;
    }
}
