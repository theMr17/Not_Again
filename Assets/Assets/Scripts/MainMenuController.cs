using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Transform buttonsContainer;
    [SerializeField] private Transform levelsContainer;
    [SerializeField] private LevelSO[] levels;
    [SerializeField] private GameObject levelButtonPrefab;

    private Transform activeContainer;
    public static string UNLOCKED_LEVEL = "unlockedLevel";
    public static string LOAD_LEVEL_INDEX = "loadLevelIndex";

    // Start is called before the first frame update
    void Start()
    {
        activeContainer = buttonsContainer;
        int unlockedLevel = PlayerPrefs.GetInt(UNLOCKED_LEVEL, 1);
        PlayerPrefs.DeleteAll();
        Debug.Log(unlockedLevel);

        for (int i = 0; i < levels.Length; i++) 
        {
            LevelSO level = levels[i];

            GameObject levelButton = Instantiate(levelButtonPrefab);
            levelButton.transform.SetParent(levelsContainer);

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
        setActiveContainer(levelsContainer);
    }

    private void setActiveContainer(Transform container) {       
        activeContainer.gameObject.SetActive(false);
        container.gameObject.SetActive(true);
        activeContainer = container;
    }

    private void LoadLevel(int level) {
        PlayerPrefs.SetInt(LOAD_LEVEL_INDEX, level);
        SceneManager.LoadScene("GameScene");
    }
}
