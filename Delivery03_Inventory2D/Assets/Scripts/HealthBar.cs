using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    public Gradient ColorGradient;
    public Image FillImage;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        SkullHealth.OnChangeHealth += SetValue; // Suscribirse en Awake
    }

    private void OnDestroy()
    {
        SkullHealth.OnChangeHealth -= SetValue; // Desuscribirse en OnDestroy
    }

    void Start()
    {
        slider = GetComponent<Slider>();
        SetValue(1.0f);
    }

    public void SetValue(float fractionHealth)
    {
        slider.value = fractionHealth;
        FillImage.color = ColorGradient.Evaluate(fractionHealth);
        slider.gameObject.SetActive(true);
    }
}
