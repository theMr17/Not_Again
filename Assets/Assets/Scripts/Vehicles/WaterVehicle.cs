using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterVehicle : Vehicle
{
    private WaterVehiclesSO vehicleSO;
    private float despawnZPos = 15f;
    private bool startDrowning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();   
        CheckVehiclePosition(transform.position.z, despawnZPos);

        if(startDrowning) {
            Drown();
        }

        float MAX_DROWN_DISTANCE = 11f;
        if(transform.position.y < -MAX_DROWN_DISTANCE) {
            DespawnVehicle();
        }
    }

    public override void Move()
    {
        base.Move();
    }

    private void Drown()
    {
        transform.Translate(new Vector3(0, -1, 0) * vehicleSO.speed * speedMultiplier * Time.deltaTime , Space.World);
    }

    public override void CheckVehiclePosition(float vehiclePos, float despawnPos)
    {
        base.CheckVehiclePosition(vehiclePos, despawnPos);
    }

    public override Vector3 GetMovementDirection()
    {
        switch(spawnerIndex) {
            // Left lane
            case 0:
                isLeftLane = false;
                return new Vector3(0, 0, 1);

            // Right lane
            case 1:
                isLeftLane = true;
                return new Vector3(0, 0, -1);

            default:
                return Vector3.zero; // just returning a default value, this is will never be executed
        }
    }

    public override void SetSpawnerIndex(int index)
    {
        base.SetSpawnerIndex(index);
    }

    public override void VehicleHit(bool canStop = false) {
        startDrowning = true;
        base.VehicleHit(canStop);
    } 
    
    public void SetVehicleSO(WaterVehiclesSO waterVehiclesSO) 
    {
        vehicleSO = waterVehiclesSO;
        
        base.speed = vehicleSO.speed;
        base.pointsWhenHit = vehicleSO.pointsWhenHit;
    }
}
