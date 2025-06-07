using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    [System.Serializable]
    public class PuzzlePiece
    {
        public GameObject cube;
        [HideInInspector] public Vector3 startPos;
        [HideInInspector] public bool isPlaced = false;
    }

    public List<PuzzlePiece> pieces = new List<PuzzlePiece>();
    public TMP_Text scoreText;
    public TMP_Text winText;

    public AudioClip correctSound;
    public AudioClip incorrectSound;
    public AudioSource audioSource;

    private int totalPlaced = 0;

    void Start()
    {
        foreach (var piece in pieces)
        {
            piece.startPos = piece.cube.transform.position;
            piece.isPlaced = false;
        }

        winText.gameObject.SetActive(false);
        UpdateScore();
    }

    public void CheckPlacement(GameObject cube, GameObject socket)
    {
        var cubeTag = cube.tag;
        var socketTag = socket.tag;

        var piece = pieces.Find(p => p.cube == cube);

        if (cubeTag == socketTag)
        {
            if (!piece.isPlaced)
            {
                piece.isPlaced = true;
                totalPlaced++;
                PlaySound(correctSound);
                UpdateScore();

                if (totalPlaced == pieces.Count)
                    ShowWinUI();
            }
        }
        else
        {
            PlaySound(incorrectSound);
        }
    }

    void UpdateScore()
    {
        scoreText.text = $"{totalPlaced}/{pieces.Count} boxes placed";
    }

    void ShowWinUI()
    {
        winText.gameObject.SetActive(true);
    }

    public void RestartPuzzle()
    {
        foreach (var piece in pieces)
        {
            piece.cube.transform.position = piece.startPos;
            piece.isPlaced = false;
        }

        totalPlaced = 0;
        winText.gameObject.SetActive(false);
        UpdateScore();
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource && clip)
            audioSource.PlayOneShot(clip);
    }
}
