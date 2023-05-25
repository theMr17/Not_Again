using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclesManager : MonoBehaviour {
    
    [SerializeField] private VehiclesListSO vehiclesListSo;
    [SerializeField] private SpawnerSO spawnerSO;
    private List<RoadVehiclesSO> roadVehiclesSoList;
    private List<WaterVehiclesSO> waterVehiclesSoList;

    // Awake is called before Start
    private void Awake() {
        // assigning vehicle list from the Scriptable object we created
        roadVehiclesSoList = vehiclesListSo.roadVehiclesSoList;
        waterVehiclesSoList = vehiclesListSo.waterVehiclesSoList;
    }

    // Start is called before the first frame update 
    void Start() {
        SpawnVehicles();
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void SpawnVehicles() {
        // initializing random class
        System.Random rnd = new System.Random();

        // getting random index for spawn location 
        int spawnerIndex = rnd.Next(0, spawnerSO.spawnerListSO.Count);
        //getting random index of vehicle
        int vehicleIndex = rnd.Next(0, roadVehiclesSoList.Count);

        // storing selected random vehicle and spawn location in variables
        RoadVehiclesSO vehicle = roadVehiclesSoList[vehicleIndex];
        SpawnDetails spawnDetails = spawnerSO.spawnerListSO[spawnerIndex];

        // Spawning the Vehicle on its selected spawn location
        GameObject child = Instantiate(vehicle.vehiclePrefab, spawnDetails.position, spawnDetails.rotation);

        // adjusting the Y rotation ONLY of individual vehicles after spawning
        float yRotationAdjustment = vehicle.rotationAdjustment.eulerAngles.y;
        child.transform.rotation = Quaternion.Euler(0, child.transform.eulerAngles.y + yRotationAdjustment, 0);
    }
}


// leftLaneSpawn1 = new Vector3(16.61f, 3.853f, -2.49f);
// leftLaneSpawn2 = new Vector3(16.61f, 3.853f, -1.83f);
// rightLaneSpawn1 = new Vector3(-16.0f, 3.853f, -0.307f);
// rightLaneSpawn2 = new Vector3(-16.0f, 3.853f, 0.29f);