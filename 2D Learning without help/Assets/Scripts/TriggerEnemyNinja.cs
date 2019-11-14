using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemyNinja : MonoBehaviour
{
    //References
    public GameObject ParticleEffect;
    public Transform SmokeStopStart;
    
    void Start()
    {
        ParticleEffect = GameObject.Find("Smoke Bomb");
        ParticleEffect.SetActive(false);
        SmokeStopStart.GetComponent<ParticleSystem>().Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name=="Player")
        {
            ParticleEffect.SetActive(true);
            SmokeStopStart.GetComponent<ParticleSystem>().Play();
            StartCoroutine(KillSwitch());
        }
    }

    IEnumerator KillSwitch()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        Destroy(ParticleEffect);
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
