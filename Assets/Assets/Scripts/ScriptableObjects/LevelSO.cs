using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LevelSO : ScriptableObject
{
    public int spawnLimit;
    public int targetScore;
    public List<RoadVehiclesSO> roadVehiclesSoList;
    public List<WaterVehiclesSO> waterVehiclesSoList;
}
