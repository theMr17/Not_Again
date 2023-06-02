using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclesManager : MonoBehaviour {

    public static VehiclesManager Instance {get; private set;}
    
    [SerializeField] private VehiclesListSO vehiclesListSo;
    [SerializeField] private SpawnerSO spawnerSO;
    private List<RoadVehiclesSO> roadVehiclesSoList;
    private List<WaterVehiclesSO> waterVehiclesSoList;

    private List<bool> laneHasVehicle;

    // Awake is called before Start
    private void Awake() {
        Instance = this;

        // assigning vehicle list from the Scriptable object we created
        roadVehiclesSoList = vehiclesListSo.roadVehiclesSoList;
        waterVehiclesSoList = vehiclesListSo.waterVehiclesSoList;

        // initializing all 4 lanes as empty
        laneHasVehicle = new List<bool>();
        laneHasVehicle.Add(false);
        laneHasVehicle.Add(false);
        laneHasVehicle.Add(false);
        laneHasVehicle.Add(false);
    }

    // Start is called before the first frame update 
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        SpawnVehicles();
    }

    private void SpawnVehicles() {
        // initializing random class
        System.Random rnd = new System.Random();

        // getting random index for spawn location 
        int spawnerIndex = rnd.Next(0, spawnerSO.spawnerListSO.Count);

        if(laneHasVehicle[spawnerIndex]) // temporary, we modify this later
        {
            return;
        }

        //getting random index of vehicle
        int vehicleIndex = rnd.Next(0, roadVehiclesSoList.Count);

        // storing selected random vehicle and spawn location in variables
        RoadVehiclesSO vehicle = roadVehiclesSoList[vehicleIndex];
        SpawnDetails spawnDetails = spawnerSO.spawnerListSO[spawnerIndex];

        // Spawning the Vehicle on its selected spawn location
        GameObject gameObject = Instantiate(vehicle.vehiclePrefab, spawnDetails.position, spawnDetails.rotation);

        // adjusting the Y rotation ONLY of individual vehicles after spawning
        float yRotationAdjustment = vehicle.rotationAdjustment.eulerAngles.y;
        gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.eulerAngles.y + yRotationAdjustment, 0);

        // sending spawn lane data to vehicle
        gameObject.GetComponent<Vehicle>().SetSpawnerIndex(spawnerIndex);
        gameObject.GetComponent<Vehicle>().SetVehicleSO(vehicle);

        // storing lane data whether it already has a vehicle
        laneHasVehicle[spawnerIndex] = true;
    }

    public void MakeLaneClear(int laneIndex) 
    {
        laneHasVehicle[laneIndex] = false;
    }
}

// leftLaneSpawn1 = new Vector3(16.61f, 3.853f, -2.49f);
// leftLaneSpawn2 = new Vector3(16.61f, 3.853f, -1.83f);
// rightLaneSpawn1 = new Vector3(-16.0f, 3.853f, -0.307f);
// rightLaneSpawn2 = new Vector3(-16.0f, 3.853f, 0.29f);