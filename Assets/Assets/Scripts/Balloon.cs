using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private Vector3 targetPos;
    private float speed = 15f;
    [SerializeField] private GameObject waterSplashParticles;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
        CheckForDespawn();
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
        const string TAG_ROAD = "Road";
        const string TAG_VEHICLE = "Vehicle";
        
        if(obj.gameObject.tag == TAG_ROAD) {
            Despawn();
        }

        if(obj.gameObject.tag == TAG_VEHICLE) {
            Despawn();

            WaterVehicle waterVehicle;
            obj.transform.TryGetComponent<WaterVehicle>( out waterVehicle);
            if(waterVehicle != null) {
                waterVehicle.VehicleHit();
            }

            RoadVehicle roadVehicle;
            obj.transform.TryGetComponent<RoadVehicle>( out roadVehicle);
            if(roadVehicle != null) {
                roadVehicle.VehicleHit();
            }
        }
    }

    private void CheckForDespawn() 
    {
        if(gameObject.GetComponent<Rigidbody>().velocity.magnitude == 0f) {
            if(transform.position.y <= 3.6f ) {
                Despawn();
            }
       }
    }

    private void Despawn() {
        GameObject splash = Instantiate(waterSplashParticles, transform.position, Quaternion.Euler(-90, 0, 0));
        Destroy(splash, 2f);
        Destroy(gameObject);
    }
}
