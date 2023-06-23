using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadVehicle : Vehicle
{
    private RoadVehiclesSO vehicleSO;
    private float despawnXPos = 20f;

    // Start is called before the first frame update
    void Start()
    {
        vehicleType = ROAD_VEHICLE_TYPE;
    }

    // Update is called once per frame
    void Update()
    {
        Move();   
        CheckVehiclePosition(transform.position.x, despawnXPos);
    }

    public override void Move()
    {
        base.Move();
    }

    public override void CheckVehiclePosition(float vehiclePos, float despawnPos)
    {
        base.CheckVehiclePosition(vehiclePos, despawnPos);
    }

    public override void SetSpawnerIndex(int index)
    {
        base.SetSpawnerIndex(index);
    }

    public override Vector3 GetMovementDirection()
    {
        return base.GetMovementDirection();
    }

    public override void VehicleHit(bool canStop = true)
    {
        base.VehicleHit(canStop);
    }

    public void SetVehicleSO(RoadVehiclesSO roadVehicleSO) 
    {
        vehicleSO = roadVehicleSO;

        base.speed = vehicleSO.speed;
        base.pointsWhenHit = vehicleSO.pointsWhenHit;
        base.stopTimeWhenHit = vehicleSO.stopTimeWhenHit;
    }
}
