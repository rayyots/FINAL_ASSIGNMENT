using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public XRSocketInteractor redSocket;
    public XRSocketInteractor greenSocket;
    public XRSocketInteractor blueSocket;

    public GameObject winText;
    public GameObject restartButton;

    private bool puzzleComplete = false;

    void Update()
    {
        if (puzzleComplete) return;

        bool redPlaced = redSocket.hasSelection && redSocket.firstInteractableSelected.transform.name == "RedCube";
        bool greenPlaced = greenSocket.hasSelection && greenSocket.firstInteractableSelected.transform.name == "GreenCube";
        bool bluePlaced = blueSocket.hasSelection && blueSocket.firstInteractableSelected.transform.name == "BlueCube";

        if (redPlaced && greenPlaced && bluePlaced)
        {
            puzzleComplete = true;
            winText.SetActive(true);
            restartButton.SetActive(true);
        }
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
