using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    private GameObject healthPotion;
    private GameObject[] healthPotions;
    public HealthBar healthBar;

    private float healthPotionHealing = 30f;

    void Start()
    {
        healthPotions = GameObject.FindGameObjectsWithTag("HealthPotions");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name=="Player"||collision.gameObject.name=="PlayerEvolved")
        {
            GameObject.Find("Health").GetComponent<Health>().healthAmount += healthPotionHealing * 1;
            Destroy(GameObject.FindGameObjectWithTag("HealthPotions"));
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" || collision.gameObject.name == "PlayerEvolved")
        {
            healthBar.GetComponent<Health>().healthRegenAmount = new Health().healthRegenAmount = 30f*1;
            Destroy(GameObject.Find("Health Potion"));
        }
    }
}
