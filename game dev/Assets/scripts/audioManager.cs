using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public Transform onBalloonBounce;
    public Transform onAirSlam;
    public Transform onDamage;
    public Transform onCoinCollection;
    public Transform onDeath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBalloonBounceFunc()
    {
        Instantiate(onBalloonBounce,transform.position,Quaternion.identity);
    }

    public void onAirSlamFunc()
    {
        Instantiate(onAirSlam,transform.position,Quaternion.identity);
    }

    public void onDamageFunc()
    {
        Instantiate(onDamage,transform.position,Quaternion.identity);
    }

    public void onCoinCollectionFunc()
    {
        Instantiate(onCoinCollection,transform.position,Quaternion.identity);
    }

    public void onDeathFunc()
    {
        Instantiate(onDeath,transform.position,Quaternion.identity);
    }
}
