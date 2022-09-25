using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int gameMode;
    public static GameManager Instance; // A static reference to the GameManager instance
    private GameplayManager gameplayManager;
    
    void Awake()
    {
        if (Instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(gameObject); // Keep the GameObject, this component is attached to, across different scenes
            Instance = this;
        }
        else if (Instance != this) // If there is already an instance and it's not `this` instance
        {
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
        }
    }

    
    public Data Data
    {
        get
        {
            return new Data();
        }
    }

    public GameplayManager GameplayManager
    {
        get
        {
            return gameplayManager = FindObjectOfType<GameplayManager>();
        }
    }
}
