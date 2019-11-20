using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemyNinja : MonoBehaviour
{
    //References
    public GameObject ParticleEffect;
    public GameObject SmokeStopStart;
    public GameObject EnemyNinja;

    
    void Start()
    {
        SmokeStopStart = GameObject.Find("Smoke Bomb");
        SmokeStopStart.GetComponent<ParticleSystem>().Stop();
        EnemyNinja = GameObject.Find("Enemy Ninja");
        EnemyNinja.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name=="PlayerEvolved"||collision.gameObject.name=="Player")
        {
            SmokeStopStart.GetComponent<ParticleSystem>().Play();
            StartCoroutine(KillSwitch());
            EnemyNinja.SetActive(true);
        }
    }

    IEnumerator KillSwitch()
    {
        yield return new WaitForSeconds(1);
        SmokeStopStart.GetComponent<ParticleSystem>().Stop();
        Destroy(SmokeStopStart);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
