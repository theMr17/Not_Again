using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDetector : MonoBehaviour
{
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() 
    {
        // touchPos is the x, y coordinates on the screen where it is touched
        Vector3 touchPos = Input.mousePosition;

        // Transform (position, rotation & scale) of the camera
        Transform camTransform = Camera.main.transform;

        float yPos = 0f;
        // y coordinate of the road
        switch(gameObject.tag) {
            case "Road":
                yPos = 3.56f;
                break;
            case "Water":
                yPos = 0.7f;
                break;
        }

        // dot product of two 3D vectors
        // gives the distance between road and camera (maybe)
        float distance = Vector3.Dot(
            new Vector3(0f, yPos - camTransform.position.y, 0f), 
            camTransform.forward
        );

        // setting the z coordinates of touchPos, because it has only x and y as screen is 2D
        touchPos.z = distance;

        // this converts the 2D screen position to real world position where the balloon must hit after throwing
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(touchPos);
        
        // sending the targetPos to other scripts to animate player and throw balloon
        player.GetComponent<Player>().ThrowBalloon(targetPos);
        PlayerAnimationFunctions.Instance.targetPos = targetPos;
    }
}
