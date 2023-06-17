using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private Vector3 targetPos;
    private float speed = 15f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        // moves the balloon in a straight line to the target position (clicked position)
        transform.position = Vector3.MoveTowards(
            transform.position, 
            targetPos, 
            speed * Time.deltaTime
        );
    }

    public void SetTargetPos(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }

    private void OnCollisionEnter(Collision obj) {
        const int LAYER_ENVIRONMENT = 6;
        const string TAG_ROAD = "Road";
        const string TAG_VEHICLE = "Vehicle";
        if(obj.gameObject.tag == TAG_ROAD) {
            Destroy(gameObject);
        }

        if(obj.transform.parent != null && obj.transform.parent.gameObject.tag == TAG_VEHICLE) {
            Destroy(gameObject);
            obj.transform.parent.GetComponent<Vehicle>().VehicleHit();
        }

        if(obj.gameObject.tag == TAG_VEHICLE) {
            Destroy(gameObject);
            obj.transform.GetComponent<WaterVehicle >().VehicleHit();
        }
    }
}
