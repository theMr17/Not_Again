using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclesManager : MonoBehaviour {

    public static VehiclesManager Instance {get; private set;}
    
    [SerializeField] private VehiclesListSO vehiclesListSo;
    [SerializeField] private SpawnerSO spawnerSO;
    private List<RoadVehiclesSO> roadVehiclesSoList;
    private List<WaterVehiclesSO> waterVehiclesSoList;

    private List<bool> roadLaneHasVehicle;
    private List<bool> waterLaneHasVehicle;

    // Awake is called before Start
    private void Awake() {
        Instance = this;

        // assigning vehicle list from the Scriptable object we created
        roadVehiclesSoList = vehiclesListSo.roadVehiclesSoList;
        waterVehiclesSoList = vehiclesListSo.waterVehiclesSoList;

        // initializing all 4 road lanes as empty
        roadLaneHasVehicle = new List<bool>();
        roadLaneHasVehicle.Add(false);
        roadLaneHasVehicle.Add(false);
        roadLaneHasVehicle.Add(false);
        roadLaneHasVehicle.Add(false);

        // initializing all 2 water lanes as empty
        waterLaneHasVehicle = new List<bool>();
        waterLaneHasVehicle.Add(false);
        waterLaneHasVehicle.Add(false);
    }

    // Start is called before the first frame update 
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        SpawnRoadVehicles();
        SpawnWaterVehicles();
    }

    private void SpawnRoadVehicles() {
        // initializing random class
        System.Random rnd = new System.Random();

        // getting random index for spawn location 
        int spawnerIndex = rnd.Next(0, spawnerSO.roadSpawnerListSO.Count);

        if(roadLaneHasVehicle[spawnerIndex]) // temporary, we modify this later
        {
            return;
        }

        //getting random index of vehicle
        int vehicleIndex = rnd.Next(0, roadVehiclesSoList.Count);

        // storing selected random vehicle and spawn location in variables
        RoadVehiclesSO vehicle = roadVehiclesSoList[vehicleIndex];
        SpawnDetails spawnDetails = spawnerSO.roadSpawnerListSO[spawnerIndex];

        // Spawning the Vehicle on its selected spawn location
        GameObject gameObject = Instantiate(vehicle.vehiclePrefab, spawnDetails.position, spawnDetails.rotation);

        // adjusting the Y rotation ONLY of individual vehicles after spawning
        float yRotationAdjustment = vehicle.rotationAdjustment.eulerAngles.y;
        gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.eulerAngles.y + yRotationAdjustment, 0);

        // sending spawn lane data to vehicle
        gameObject.GetComponent<RoadVehicle>().SetSpawnerIndex(spawnerIndex);
        gameObject.GetComponent<RoadVehicle>().SetVehicleSO(vehicle);

        // storing lane data whether it already has a vehicle
        roadLaneHasVehicle[spawnerIndex] = true;
    }

    private void SpawnWaterVehicles() {
        // initializing random class
        System.Random rnd = new System.Random();

        // getting random index for spawn location 
        int spawnerIndex = rnd.Next(0, spawnerSO.waterSpawnerListSO.Count);

        if(waterLaneHasVehicle[spawnerIndex]) // temporary, we modify this later
        {
            return;
        }

        //getting random index of vehicle
        int vehicleIndex = rnd.Next(0, waterVehiclesSoList.Count);

        // storing selected random vehicle and spawn location in variables
        WaterVehiclesSO vehicle = waterVehiclesSoList[vehicleIndex];
        SpawnDetails spawnDetails = spawnerSO.waterSpawnerListSO[spawnerIndex];

        // Spawning the Vehicle on its selected spawn location
        GameObject gameObject = Instantiate(vehicle.vehiclePrefab, spawnDetails.position, spawnDetails.rotation);

        // adjusting the X rotation ONLY of individual vehicles after spawning
        float xRotationAdjustment = vehicle.rotationAdjustment.eulerAngles.x;
        gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.eulerAngles.x + xRotationAdjustment, gameObject.transform.rotation.eulerAngles.y, 0);

        // sending spawn lane data to vehicle
        gameObject.GetComponent<WaterVehicle>().SetSpawnerIndex(spawnerIndex);
        gameObject.GetComponent<WaterVehicle>().SetVehicleSO(vehicle);

        // storing lane data whether it already has a vehicle
        waterLaneHasVehicle[spawnerIndex] = true;
    }

    public void MakeRoadLaneClear(int laneIndex) 
    {
        roadLaneHasVehicle[laneIndex] = false;
    }

    public void MakeWaterLaneClear(int laneIndex) 
    {
        waterLaneHasVehicle[laneIndex] = false;
    }
}