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
    [SerializeField] private Animator transitionAnimator;

    [SerializeField] private AudioClip gameOverAudio;
    [SerializeField] private AudioClip levelCompletedAudio;
    [SerializeField] private AudioSource audioPlayer;

    private void Awake() {
        levelCountText.text = "Level " + (PlayerPrefs.GetInt(MainMenuController.CURRENT_LEVEL_INDEX, 0) + 1);
        targetScore.text = "Target Score: " + PlayerPrefs.GetInt(MainMenuController.CURRENT_TARGET_SCORE, 0);
        yourScoreText.text = "Your Score: " + PlayerPrefs.GetInt(MainMenuController.YOUR_TARGET_SCORE, 0);

        if(PlayerPrefs.GetInt(MainMenuController.IS_LEVEL_CLEARED, 0) == 1) {
            audioPlayer.clip = levelCompletedAudio;
            headingText.text = "Level Cleared";
            playButtonText.text = "Next Level";
        } else {
            audioPlayer.clip = gameOverAudio;
            headingText.text = "Game Over";
            playButtonText.text = "Try Again";
        }
        audioPlayer.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainMenu() {
        StartCoroutine(LoadScene("MainMenuScene"));
    }

    public void Play() {
        StartCoroutine(LoadScene("GameScene"));
    }

    IEnumerator LoadScene(string sceneName) {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}