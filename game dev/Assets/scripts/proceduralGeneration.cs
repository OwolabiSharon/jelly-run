using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proceduralGeneration : MonoBehaviour
{
    [SerializeField] Transform ground_0;
    [SerializeField] List<Transform> grounds;
    [SerializeField] Transform player;
    
    const float playerDistanceFromEnd = 100f;
    Vector2 lastEndPosition;
    // Start is called before the first frame update

    void Start()
    {
        lastEndPosition = ground_0.Find("EndPosition").position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position,lastEndPosition) <playerDistanceFromEnd)
        {
            SpawnLevelPart();
        }
    }


    void SpawnLevelPart(){
        Transform lastLevelPartTransform = SpawnLevelPart(lastEndPosition); 
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }

    Transform SpawnLevelPart(Vector3 spawnPosition) 
    {
        Transform levelPartTransform = Instantiate(grounds[Random.Range(0,grounds.Count)],spawnPosition,Quaternion.identity);
        return levelPartTransform;
    }
}