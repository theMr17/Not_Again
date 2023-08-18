using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle: MonoBehaviour
{
    protected float speedMultiplier = 1.5f;
    protected int spawnerIndex;
    protected bool isLeftLane;
    private bool canMove = true;

    [HideInInspector] public string vehicleName;
    [HideInInspector] public float speed;
    [HideInInspector] public int pointsWhenHit;
    [HideInInspector] public float stopTimeWhenHit;

    protected int vehicleType = 0; // 0 for road, 1 for water
    protected const int ROAD_VEHICLE_TYPE = 0;
    protected const int WATER_VEHICLE_TYPE = 1;

    public virtual void Move()
    {
        if(canMove) {
            // moving the vehicle in proper direction and speed
            transform.Translate(GetMovementDirection() * speed * speedMultiplier * Time.deltaTime , Space.World);
        }
    }

    public virtual void CheckVehiclePosition(float vehiclePos, float despawnPos)
    {
        //float xPos = transform.position.x;

        if(!isLeftLane && vehiclePos > despawnPos) // this is for right lane
        {
            DespawnVehicle();
        }
        else if(isLeftLane && vehiclePos < -despawnPos) // this is for left lane
        { 
            DespawnVehicle();
        }
    }

    public virtual void DespawnVehicle() 
    {
        // clearing lane data before despawing according to vehicle type
        switch(vehicleType) {
            case ROAD_VEHICLE_TYPE:
                VehiclesManager.Instance.MakeRoadLaneClear(spawnerIndex);
                break;
            
            case WATER_VEHICLE_TYPE:
                VehiclesManager.Instance.MakeWaterLaneClear(spawnerIndex);
                break;
        }

        Destroy(gameObject);
    }

    public virtual Vector3 GetMovementDirection() 
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

    public virtual void SetSpawnerIndex(int index) 
    {
        spawnerIndex = index;
    }

    public virtual void VehicleHit(bool canStop) {
        SetCanMove(false);
        GameManager.Instance.AddScore(pointsWhenHit);
        HitScoreContainer.Instance.InsertHitScoreItem(pointsWhenHit, vehicleName);

        if(canStop)
            StartCoroutine(WaitForTime(stopTimeWhenHit));
    }

    public virtual void SetCanMove(bool value) {
        canMove = value;
    }

    IEnumerator WaitForTime(float seconds) {
        yield return new WaitForSeconds(seconds);
        SetCanMove(true);
    }
}
