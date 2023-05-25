using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WaterVehiclesSO : ScriptableObject {
    public float speed;
    public bool drownOnHit;
    public int pointsWhenHit;
    public GameObject vehiclePrefab;
}
