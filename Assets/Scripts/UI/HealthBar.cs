using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : PanelUI
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        if (health < slider.value) // Hit
        {
            // Play wound animation
            StartCoroutine(ColorEffect(.1f, Color.white));
        }
        else // Cure
        {
            // Play cure animation
            StartCoroutine(ColorEffect(.1f, Color.green));
        }

        slider.value = health;
    }

    IEnumerator ColorEffect(float delay, Color color)
    {
        fill.color = color;
        yield return new WaitForSeconds(delay);
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
