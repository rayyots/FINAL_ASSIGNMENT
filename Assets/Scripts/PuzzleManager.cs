using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;
    private int correctCount = 0;

    public GameObject winText;
    public GameObject restartButton;

    void Awake()
    {
        instance = this;
    }

    public void RegisterPlacement()
    {
        correctCount++;
        if (correctCount == 3)
        {
            winText.SetActive(true);
            restartButton.SetActive(true);
        }
    }

    public void RestartPuzzle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
