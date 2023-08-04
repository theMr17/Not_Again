using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameOverMenuController : MonoBehaviour
{
    [SerializeField] private TMP_Text headingText;
    [SerializeField] private TMP_Text levelCountText;
    [SerializeField] private TMP_Text targetScore;
    [SerializeField] private TMP_Text yourScoreText;
    [SerializeField] private Button playButton;
    [SerializeField] private TMP_Text playButtonText;

    private void Awake() {
        levelCountText.text = "Level " + (PlayerPrefs.GetInt(MainMenuController.CURRENT_LEVEL_INDEX, 0) + 1);
        targetScore.text = "Target Score: " + PlayerPrefs.GetInt(MainMenuController.CURRENT_TARGET_SCORE, 0);
        yourScoreText.text = "Your Score: " + PlayerPrefs.GetInt(MainMenuController.YOUR_TARGET_SCORE, 0);

        if(PlayerPrefs.GetInt(MainMenuController.IS_LEVEL_CLEARED, 0) == 1) {
            headingText.text = "Level Cleared";
            playButtonText.text = "Next Level";
        } else {
            headingText.text = "Game Over";
            playButtonText.text = "Try Again";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Play() {
        SceneManager.LoadScene("GameScene");
    }
}
