using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemyNinja : MonoBehaviour
{
    //References
    public GameObject ParticleEffect;
    public GameObject SmokeStopStart;

    
    void Start()
    {
        SmokeStopStart = GameObject.Find("Smoke Bomb");
        SmokeStopStart.GetComponent<ParticleSystem>().Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name=="Player")
        {
            SmokeStopStart.GetComponent<ParticleSystem>().Play();
            StartCoroutine(KillSwitch());
        }
    }

    IEnumerator KillSwitch()
    {
        yield return new WaitForSeconds(1);
        Destroy(SmokeStopStart);
        Destroy(gameObject);
        SmokeStopStart.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
