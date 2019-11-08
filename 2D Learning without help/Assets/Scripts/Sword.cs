using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public  void OnInteract()
    {
        Renderer render=gameObject.GetComponent<Renderer>();
        if(render.enabled)
        {
            render.enabled = false;
        }
        else
        {
            render.enabled = true;
        }
    }
}
