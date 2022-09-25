using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [Header("Idle")]
    [SerializeField] private GameObject idleMenu;

    [Header("Game")]
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private TMP_Text lengtText;
    [SerializeField] private TMP_Text oxygenLevelText;
    [SerializeField] private Image oxyegnLevelMeter;
    private int maxLength;
    private int currentLength;
    private int oxygenMaxLevel;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private TMP_Text collectCashText;
    [SerializeField] private Button homeButton;
    private int collectedCash;

    public int chunkCount;
    public int maxChunk;
    public Image levelImage;

    private void Start()
    {
        OnGameIdle();
        homeButton.onClick.RemoveAllListeners();
        homeButton.onClick.AddListener(OnGameOverHomeButtonClick);
        chunkCount = maxChunk;
    }

    private void Update()
    {
        if (GameManager.Instance.gameMode == 1)
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Chunk");
            chunkCount = objs.Length;
            levelImage.fillAmount = (float)(1 * chunkCount) / maxChunk;
        }
    }
    public void OnGameIdle()
    {
        GameManager.Instance.gameMode = 0;
        ActiveIdleMenu();
    }

    public void ActiveIdleMenu()
    {
        DisableAllMenu();
        idleMenu.SetActive(true);
        collectedCash = 0;
    }
    public void OnGameStart()
    {
        GameManager.Instance.gameMode = 1;
        DisableAllMenu();
        gameMenu.SetActive(true);
        oxygenMaxLevel = GameManager.Instance.Data.GetPlayerdata().oxygen;
        maxLength = GameManager.Instance.Data.GetPlayerdata().length;
    }

    public void UpdateLength(int length)
    {
        lengtText.text = length + "/" + maxLength;
    }

    public void UpdateOxygen(int oxygenLevel)
    {

        if (oxygenLevel > 0)
        {
            oxygenLevelText.text = oxygenLevel.ToString() + "%";
            oxyegnLevelMeter.fillAmount =(float)oxygenLevel / 100;
        }
        else
        {
            GameOver();
        }

    }



    public void AddCash()
    {
        collectedCash += 100;
    }

    void GameOver()
    {
        GameManager.Instance.gameMode = 2;
        DisableAllMenu();
        gameOverMenu.SetActive(true);
        collectCashText.text = collectedCash.ToString();
    }

    void OnGameOverHomeButtonClick()
    {
        Player player = GameManager.Instance.Data.GetPlayerdata();
        if (collectedCash > 0)
        {
            player.currency += collectedCash;
            GameManager.Instance.Data.SetData(player);
        }
        SceneManager.LoadScene(0);

    }
    void DisableAllMenu()
    {
        idleMenu.SetActive(false);
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }
}
