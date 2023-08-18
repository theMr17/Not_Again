using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class HitScoreContainer : MonoBehaviour {
    public static HitScoreContainer Instance {get; private set;}

    [SerializeField] private Transform hitScoreItemPrefab;

    private void Awake() {
        Instance = this;
    }

    public void InsertHitScoreItem(int deltaScore, string vehicleName) {
        HitScoreItem hitScoreItem = Instantiate(hitScoreItemPrefab, gameObject.transform).GetComponent<HitScoreItem>();
        hitScoreItem.SetDeltaScoreText(deltaScore);
        hitScoreItem.SetVehicleNameText(vehicleName);
    }
}
