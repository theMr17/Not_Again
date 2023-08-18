using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitScoreItem : MonoBehaviour
{
    [SerializeField] private TMP_Text deltaScoreText;
    [SerializeField] private TMP_Text vehicleNameText;
    [SerializeField] private Color positiveColor;
    [SerializeField] private Color negativeColor;

    private float lifeTimeInSeconds = 3f;
    private float timeLeftInSeconds = 0f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(timeLeftInSeconds >= lifeTimeInSeconds) {
            Destroy(gameObject);
        } else {
            timeLeftInSeconds += Time.deltaTime;
        }
    }

    public void SetDeltaScoreText(int deltaScore) {
        if(deltaScore > 0) {
            deltaScoreText.text = "+" + deltaScore.ToString();
            deltaScoreText.color = positiveColor;
        } else {
            deltaScoreText.text = deltaScore.ToString();
            deltaScoreText.color = negativeColor;
        }
    }

    public void SetVehicleNameText(string vehicleName) {
        vehicleNameText.text = vehicleName;
    }
}
