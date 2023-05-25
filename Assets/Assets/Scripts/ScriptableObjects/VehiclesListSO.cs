using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class VehiclesListSO : ScriptableObject {
    public List<RoadVehiclesSO> roadVehiclesSoList;
    public List<WaterVehiclesSO> waterVehiclesSoList;
}
