using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    [SerializeField] private TMP_Text Text_TargetScore;
    [SerializeField] private TMP_Text Text_YourScore;
    [SerializeField] private TMP_Text Text_VehiclesLeft;
    [SerializeField] private List<LevelSO> levels;

    public GameState gameState;
    private int score = 0;
    private int currentLevelIndex = 0;
    private LevelSO currentLevelSO;
    private int vehiclesLeft = 0;

    public enum GameState {
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private void Awake() {
        Instance = this;
        currentLevelSO = levels[currentLevelIndex];
        vehiclesLeft = currentLevelSO.spawnLimit;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeGameUI();
        gameState = GameState.CountdownToStart;
    }

    // Update is called once per frame
    void Update()
    {
        switch(gameState) {
            case GameState.CountdownToStart:
                // later we add count down timer here
                gameState = GameState.GamePlaying;
                break;

            case GameState.GamePlaying:
                if(!isGameOver()) {
                    VehiclesManager.Instance.SpawnRoadVehicles(currentLevelSO.roadVehiclesSoList);
                    VehiclesManager.Instance.SpawnWaterVehicles(currentLevelSO.waterVehiclesSoList);
                }
                break;

            case GameState.GameOver:
                Debug.Log("Game Over");
                break;
        }


    }

    private void InitializeGameUI() {
        Text_VehiclesLeft.text = "Vehicles Left: " + vehiclesLeft;
        Text_TargetScore.text = "Target Score: " + currentLevelSO.targetScore;
    }

    private bool isGameOver() {
        if(vehiclesLeft <= 0) {
            return true;
        } else {
            return false;
        }
    }

    public void AddScore(int deltaScore) {
        score += deltaScore;
        Text_YourScore.text = "Your Score: " + score.ToString();
    }

    public void ReduceVehicleCount() {
        vehiclesLeft--;
        Text_VehiclesLeft.text = "Vehicles Left: " + vehiclesLeft;
    }

    public int GetVehicleCount() {
        return vehiclesLeft;
    }
}
