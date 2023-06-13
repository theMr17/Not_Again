using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationFunctions : MonoBehaviour
{
    public static PlayerAnimationFunctions Instance {get; private set;}

    [SerializeField] private GameObject handBalloonSpawner;
    [SerializeField] private GameObject balloonPrefab;

    [HideInInspector] public Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ReleaseBalloon() 
    {
        // Spawning the balloon on spawining location (player right hand)
        GameObject balloon = Instantiate(balloonPrefab, handBalloonSpawner.transform.position, Quaternion.identity);
        // Setting the target position for the spawned balloon
        balloon.GetComponent<Balloon>().SetTargetPos(targetPos);
    }
}
