using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using UnityEditor.Rendering.Universal.ShaderGUI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Transform buttonsContainer;
    [SerializeField] private Transform levelsContainer;
    [SerializeField] private Transform rulesContainer;
    [SerializeField] private Transform settingsContainer;
    [SerializeField] private Transform levelButtonsContainer;
    [SerializeField] private LevelSO[] levels;
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private Slider volumeSlider;

    private Transform activeContainer;
    public static string UNLOCKED_LEVEL = "unlockedLevel";
    public static string LOAD_LEVEL_INDEX = "loadLevelIndex";
    public static string CURRENT_TARGET_SCORE = "currentTargetScore";
    public static string YOUR_TARGET_SCORE = "yourTargetScore";
    public static string CURRENT_LEVEL_INDEX = "currentLevelIndex";
    public static string IS_LEVEL_CLEARED = "isLevelCleared";
    public static string VOLUME_LEVEL = "volumeLevel";

    private void Awake() {
        volumeSlider.value = PlayerPrefs.GetFloat(VOLUME_LEVEL, 1f);
        volumeSlider.onValueChanged.AddListener((float volume) => {
            PlayerPrefs.SetFloat(VOLUME_LEVEL, volume);
            BackgroundMusic.Instance.UpdateVolume();
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        activeContainer = buttonsContainer;
        int unlockedLevel = PlayerPrefs.GetInt(UNLOCKED_LEVEL, 1);
        //PlayerPrefs.DeleteAll();
        Debug.Log(unlockedLevel);

        for (int i = 0; i < levels.Length; i++) 
        {
            LevelSO level = levels[i];

            GameObject levelButton = Instantiate(levelButtonPrefab);
            levelButton.transform.SetParent(levelButtonsContainer);

            levelButton.GetComponentInChildren<TMP_Text>().text = (i+1).ToString();

            if(i+1 <= unlockedLevel) 
            {
                Button button = levelButton.GetComponent<Button>();
                button.interactable = true;
                button.onClick.AddListener(() => LoadLevel(button.transform.GetSiblingIndex()));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play() {
        SetActiveContainer(levelsContainer);
    }

    public void Rules() {
        SetActiveContainer(rulesContainer);
    }

    public void Settings() {
        SetActiveContainer(settingsContainer);
    }

    public void Exit() {
        Application.Quit();
    }

    public void Back() {
        SetActiveContainer(buttonsContainer);
    }

    private void SetActiveContainer(Transform container) {       
        activeContainer.gameObject.SetActive(false);
        container.gameObject.SetActive(true);
        activeContainer = container;
    }

    private void LoadLevel(int level) {
        PlayerPrefs.SetInt(LOAD_LEVEL_INDEX, level);
        StartCoroutine(LoadScene("GameScene"));
    }

    IEnumerator LoadScene(string sceneName) {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}
