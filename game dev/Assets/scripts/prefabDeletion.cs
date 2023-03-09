using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefabDeletion : MonoBehaviour
{
    public float threshold = 100f;
    GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
       player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < player.transform.position.x - threshold)
    {
        Destroy(gameObject);
    }
        
    }
}
