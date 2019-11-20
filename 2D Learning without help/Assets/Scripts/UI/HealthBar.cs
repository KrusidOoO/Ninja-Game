using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey;

public class HealthBar : MonoBehaviour
{
    private Health health;
    private Image barImage;
    private Button button;
    private void Awake()
    {
        barImage = transform.Find("Health").GetComponent<Image>();
        health = new Health();

        CMDebug.ButtonUI(new Vector2(-500, -190), "Inflict Damage", () =>
           {
               health.TrySpendHealth(20);
           });
    }
    private void Update()
    {
        health.Update();
        barImage.fillAmount = health.GetHealthNormalized();
    }
}
    public class Health
    {
        public const int Health_Max = 100;
        private float healthAmount;
        private float healthRegenAmount;
        public Health()
        {
            healthAmount = 100f;
            healthRegenAmount = 0.5f;
        }
        public void Update()
        {
            healthAmount += healthRegenAmount * Time.deltaTime;
            healthAmount = Mathf.Clamp(healthAmount, 0f, Health_Max);
        }
        public void TrySpendHealth(int amount)
        {
            if (healthAmount>=amount)
            {
                healthAmount -= amount;
            }
        }
        public float GetHealthNormalized()
        {
            return healthAmount / Health_Max;
        }
    }
