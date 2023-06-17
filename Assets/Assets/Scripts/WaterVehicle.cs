using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterVehicle : MonoBehaviour
{
    private float speedMultiplier = 1.5f;
    private int spawnerIndex;
    private WaterVehiclesSO vehicleSO;
    private float despawnZPos = 15f;
    private bool isLeftLane;
    private bool canMove = true;
    private bool drown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();   
        CheckVehiclePosition();

        if(drown) {
            Drown();
        }

        if(transform.position.y < -11f) {
            DespawnVehicle();
        }
    }

    private void Move()
    {
        if(canMove) {
            // moving the vehicle in proper direction and speed
            transform.Translate(GetMovementDirection() * vehicleSO.speed * speedMultiplier * Time.deltaTime , Space.World);
        }
    }

    private void Drown()
    {
        transform.Translate(new Vector3(0, -1, 0) * vehicleSO.speed * speedMultiplier * Time.deltaTime , Space.World);
    }

    private void CheckVehiclePosition()
    {
        float zPos = transform.position.z;

        if(isLeftLane && zPos > despawnZPos) // this is for right lane
        {
            DespawnVehicle();
        }
        else if(!isLeftLane && zPos < -despawnZPos) // this is for left lane
        { 
            DespawnVehicle();
        }
    }

    private void DespawnVehicle() 
    {
        // clearing lane data before despawing
        VehiclesManager.Instance.MakeWaterLaneClear(spawnerIndex);
        Destroy(gameObject);
    }

    private Vector3 GetMovementDirection() 
    {
        switch(spawnerIndex) {
            // Left lane
            case 0:
                isLeftLane = true;
                return new Vector3(0, 0, 1);

            // Right lane
            case 1:
                isLeftLane = false;
                return new Vector3(0, 0, -1);

            default:
                return Vector3.zero; // just returning a default value, this is will never be executed
        }
    }

    public void SetSpawnerIndex(int index) 
    {
        spawnerIndex = index;
    }

    public void SetVehicleSO(WaterVehiclesSO waterVehiclesSO) 
    {
        vehicleSO = waterVehiclesSO;
    }

    public void VehicleHit() {
        drown = true;
        canMove = false;
        GameManger.Instance.AddScore(vehicleSO.pointsWhenHit);
        //StartCoroutine(WaitForTime(vehicleSO.stopTimeWhenHit));
    }

    private void SetCanMove(bool value) {
        canMove = value;
    }

    IEnumerator WaitForTime(float seconds) {
        yield return new WaitForSeconds(seconds);
        SetCanMove(true);
    }
}
