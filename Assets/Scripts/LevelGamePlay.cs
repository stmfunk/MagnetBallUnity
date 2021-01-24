using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelGamePlay : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    TextMeshProUGUI text;
    GameState gameState;


    // Could also access using Findobjectoftype
    [SerializeField] int blocksRemaining;
    int totalBlocks = 0;


    public void Start() { 
        text = FindObjectOfType<TextMeshProUGUI>();
        gameState = FindObjectOfType<GameState>();
        text.text = gameState.currentScore.ToString();
    }

    public void BlockBroken()
    {
        blocksRemaining--;
        text.text = gameState.currentScore.ToString();

        if (blocksRemaining == 0)
        {
            sceneLoader.LoadNextScene();
        }

    }

    public void AddBlock()
    {
        this.blocksRemaining++;
        this.totalBlocks++;
    }
}
