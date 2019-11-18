using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private GameObject playerEvolved;
    private GameObject player;
    private GameObject CameraController;
    // Start is called before the first frame update
    void Start()
    {
        CameraController = GameObject.Find("CameraController");
        player = GameObject.Find("Player");
        playerEvolved = GameObject.Find("PlayerEvolved");
        playerEvolved.GetComponent<SpriteRenderer>().enabled = false;
        playerEvolved.GetComponent<BoxCollider2D>().enabled = false;
        playerEvolved.GetComponent<CircleCollider2D>().enabled = false;
        playerEvolved.GetComponent<Rigidbody2D>().gravityScale = 0;


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name=="Player")
        {
            player.GetComponent<Player>().CanMove = false;
            Destroy(GameObject.Find("Sword"));
            player.SetActive(false);
            playerEvolved.GetComponent<SpriteRenderer>().enabled = true;
            playerEvolved.GetComponent<BoxCollider2D>().enabled = true;
            playerEvolved.GetComponent<CircleCollider2D>().enabled = true;
            playerEvolved.GetComponent<Rigidbody2D>().gravityScale = 3;
            playerEvolved.transform.position = player.transform.position;
            CameraController.GetComponent<CameraController>().player = playerEvolved;
            Debug.Log("Player has been transformed");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
