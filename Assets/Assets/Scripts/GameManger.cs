using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManger : MonoBehaviour
{
    public static GameManger Instance {get; private set;}
    [SerializeField] private TMP_Text Text_Score;
    private int score = 0;

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int deltaScore) {
        score += deltaScore;
        Text_Score.text = "Your Score: " + score.ToString();
    }
}
