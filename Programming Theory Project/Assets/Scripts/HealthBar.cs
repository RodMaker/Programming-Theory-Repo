using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject playerUI;
    public Slider slider;
    public Slider playerUISlider;

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        playerUISlider.maxValue = maxHealth;
        slider.value = maxHealth;
        playerUISlider.value = maxHealth;
    }

    public void SetCurrentHealth(int currentHealth)
    {
        slider.value = currentHealth;
        playerUISlider.value = currentHealth;
    }

    private void Update()
    {
        if (playerUISlider != null)
        {
            if (playerUISlider.value <= 0)
            {
                Destroy(playerUI.gameObject);
            }
        }
    }

    // added to make the healthbar able to move accordingly to your position
    private void LateUpdate()
    {
        if(playerUISlider != null)
        {
            playerUI.transform.rotation = Quaternion.LookRotation((playerUI.transform.position - Camera.main.transform.position).normalized);
        }
    }
}
