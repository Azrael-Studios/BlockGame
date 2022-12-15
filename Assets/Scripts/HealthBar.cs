using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;
    public Controller controller;

    public void UpdateHealthBar()
    {
        float newHealth = Mathf.Clamp(controller.health / controller.maxHealth, 0, 1f);
        healthBarImage.fillAmount = newHealth;
    }
}
