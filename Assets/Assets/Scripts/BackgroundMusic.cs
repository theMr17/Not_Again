using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {
    public static BackgroundMusic Instance {get; private set;}

    private AudioSource audioSource;

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat(MainMenuController.VOLUME_LEVEL, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateVolume() {
        audioSource.volume = PlayerPrefs.GetFloat(MainMenuController.VOLUME_LEVEL, 1f);
    }
}
