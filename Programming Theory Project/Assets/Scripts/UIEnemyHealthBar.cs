using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyHealthBar : MonoBehaviour
{
    public Slider enemySlider;
    float timeUntilBarIsHidden = 0;

    public void SetHealth(int health)
    {
        enemySlider.value = health;
    }

    public void SetMaxHealth(int maxHealth)
    {
        enemySlider.maxValue = maxHealth;
        enemySlider.value = maxHealth;
    }

    private void Update()
    {
        if (enemySlider != null)
        {
            if (enemySlider.value <= 0)
            {
                Destroy(enemySlider.gameObject);
            }
        }
    }

    // added to make the healthbar able to move accordingly to your position
    private void LateUpdate()
    {
        if(enemySlider != null)
        {
            transform.rotation = Quaternion.LookRotation((transform.position - Camera.main.transform.position).normalized);
        }
    }
}
