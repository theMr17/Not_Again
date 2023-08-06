using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    [SerializeField] private TMP_Text Text_TargetScore;
    [SerializeField] private TMP_Text Text_YourScore;
    [SerializeField] private TMP_Text Text_VehiclesLeft;
    [SerializeField] private TMP_Text Text_Level;
    [SerializeField] private List<LevelSO> levels;
    [SerializeField] private Animator transitionAnimator;

    public GameState gameState;
    private int score = 0;
    private int currentLevelIndex = 0;
    private LevelSO currentLevelSO;
    private int vehiclesLeft = 0;
    private bool nextLevelUnlocked = false;

    public enum GameState {
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private void Awake() {
        Instance = this;

        currentLevelIndex = PlayerPrefs.GetInt(MainMenuController.LOAD_LEVEL_INDEX, 0);
        Debug.Log(currentLevelIndex+1);
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
                nextLevelUnlocked = false;
                if(!nextLevelUnlocked && score >= currentLevelSO.targetScore) {
                    if(currentLevelIndex+1 == PlayerPrefs.GetInt(MainMenuController.UNLOCKED_LEVEL, 1))
                    PlayerPrefs.SetInt(MainMenuController.UNLOCKED_LEVEL, currentLevelIndex+2);
                    PlayerPrefs.SetInt(MainMenuController.LOAD_LEVEL_INDEX, currentLevelIndex+1);
                    nextLevelUnlocked = true;
                }
                LoadGameOverMenu();
                break;
        }


    }

    private void InitializeGameUI() {
        Text_VehiclesLeft.text = "Vehicles Left: " + vehiclesLeft;
        Text_TargetScore.text = "Target Score: " + currentLevelSO.targetScore;
        Text_Level.text = "Level: " + (currentLevelIndex + 1);
    }

    private bool isGameOver() {
        if(vehiclesLeft <= 0) {
            return true;
        } else {
            return false;
        }
    }

    private void LoadGameOverMenu() {
        PlayerPrefs.SetInt(MainMenuController.CURRENT_TARGET_SCORE, currentLevelSO.targetScore);
        PlayerPrefs.SetInt(MainMenuController.YOUR_TARGET_SCORE, score);
        PlayerPrefs.SetInt(MainMenuController.CURRENT_LEVEL_INDEX, currentLevelIndex);
        PlayerPrefs.SetInt(MainMenuController.IS_LEVEL_CLEARED, Convert.ToInt32(nextLevelUnlocked));
        StartCoroutine(LoadScene("GameOverScene"));
    }

    IEnumerator LoadScene(string sceneName) {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
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
