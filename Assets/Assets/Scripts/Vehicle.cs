using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    private float speedMultiplier = 1.5f;
    private int spawnerIndex;
    private RoadVehiclesSO vehicleSO;
    private float despawnXPos = 20f;
    private bool isLeftLane;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();   
        DespawnAtEnd();
    }

    private void Move()
    {
        transform.Translate(GetMovementDirection() * vehicleSO.speed * speedMultiplier * Time.deltaTime , Space.World);
    }

    private void DespawnAtEnd()
    {
        float xPos = transform.position.x;

        if(!isLeftLane && xPos > despawnXPos) // this is for right lane
        {
            Destroy(gameObject);
        }
        if(isLeftLane && xPos < -despawnXPos) // this is for left lane
        { 
            Destroy(gameObject);
        }
    }

    private Vector3 GetMovementDirection() 
    {
        switch(spawnerIndex) {
            // Left lane
            case 0:
            case 1:
                isLeftLane = true;
                return new Vector3(-1, 0, 0);

            // Right lane
            case 2:
            case 3:
                isLeftLane = false;
                return new Vector3(1, 0, 0);

            default:
                return Vector3.zero; // just returning a default value, this is will never be executed
        }
    }

    public void SetSpawnerIndex(int index) 
    {
        spawnerIndex = index;
    }

    public void SetVehicleSO(RoadVehiclesSO roadVehicleSO) 
    {
        vehicleSO = roadVehicleSO;
    }
}
