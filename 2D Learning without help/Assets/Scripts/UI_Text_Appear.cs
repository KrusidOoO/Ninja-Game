using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Text_Appear : MonoBehaviour
{
    public GameObject uiObject;
    public GameObject uiPicture;

    // Start is called before the first frame update
    void Start()
    {
        uiObject.SetActive(false);
        uiPicture.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            
            uiObject.SetActive(true);
            uiPicture.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }
    

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        Destroy(uiObject);
        Destroy(uiPicture);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
