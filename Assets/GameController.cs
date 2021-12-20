using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int whoseTurn;                    // 0 = X and 1 = O
    public int turnCounter;                  // Counts the number of turn play
    

    public GameObject[] turnIndicators;      // Display Whos turn it is
    public Button[] tictactoeSpaces;         // Playable spaces for our game
    public Sprite[] playerIcon;              // 0 = X icon, 1 = y icon
    public int[] markedSpaces;               // ID's which space was marked by which player

    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }

    void GameSetup()        // Initializing the game
    {
        whoseTurn = 0;
        turnCounter = 0;
        turnIndicators[0].SetActive(true);
        turnIndicators[1].SetActive(false);

        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;

        }

        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TicTacToeButton(int WhichNumber)
    {
        tictactoeSpaces[WhichNumber].image.sprite = playerIcon[whoseTurn];
        tictactoeSpaces[WhichNumber].interactable = false;

        markedSpaces[WhichNumber] = whoseTurn;  // Identify which space was mark by which player
        turnCounter++;

        if (whoseTurn == 0)
        {
            whoseTurn = 1;
            turnIndicators[0].SetActive(false);
            turnIndicators[1].SetActive(true);
        }
        else
        {
            whoseTurn = 0;
            turnIndicators[0].SetActive(true);
            turnIndicators[1].SetActive(false);
        }

    }
}
