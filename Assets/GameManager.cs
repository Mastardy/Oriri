using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject gameOverCanvas;
    
    private bool gameOver;
    
    private void Awake()
    {
        if(instance == null) instance = this;
    }

    public void Update()
    {
        if (!gameOver) return;
        
        foreach(var gamepad in Gamepad.all) if(gamepad.startButton.wasPressedThisFrame) ResetGame();
    }
    
    public void GameOver()
    {
        Time.timeScale = 0.01f;
        canvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        gameOver = true;
    }
    
    public void ResetGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(2);
    }
}