using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class coinCollection : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            characterMovement script = other.gameObject.GetComponent<characterMovement>();
            audioManager audioManager = other.gameObject.GetComponent<audioManager>();
            Transform particle = gameObject.transform.Find("particles");
            Vector3 particleSpawn = particle.position;
            Instantiate(script.collectCoin,particleSpawn,Quaternion.identity);
            Destroy(gameObject);
            script.SendMessage("gainCoinPoints", 500f);
            audioManager.SendMessage("onCoinCollectionFunc");
        }
        
    }
}
