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

    public Text winnerText;
    public GameObject[] winningLine;         // Hold all the different lines for showing the winner
    public GameObject winnerPanel;



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
            markedSpaces[i] = -999;
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

        markedSpaces[WhichNumber] = whoseTurn + 1;  // Identify which space was mark by which player (1 or 2)
        turnCounter++;

        if (turnCounter > 4)
        {
            WinnerCheck();
        }


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


    void WinnerCheck()      // Check all 8 ways for winning
    {
        /*Game Logic Reference: https://www.youtube.com/watch?v=JoekPMKyIZQ&list=PLWeGoBm1YHVj7cYQSglzBU0gb7ecDNf4g&index=5 */

        // Row
        int WinCondition_1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int WinCondition_2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int WinCondition_3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];

        // Column
        int WinCondition_4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int WinCondition_5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int WinCondition_6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];

        // X
        int WinCondition_7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int WinCondition_8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];

        var solutions = new int[] { WinCondition_1, WinCondition_2, WinCondition_3, WinCondition_4, WinCondition_5, WinCondition_6, WinCondition_7, WinCondition_8 };

        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i] == 3 * (whoseTurn + 1))
            {
                DisplayWinner(i);
                return;
            }
        }

    }


    void DisplayWinner(int indexIn)
    {
        // After winner exist, put a invisible panel on top of everything
        winnerPanel.gameObject.SetActive(true);

        if (whoseTurn == 0)
        {
            winnerText.text = "Player X won!";
        }
        else if (whoseTurn == 1)
        {
            winnerText.text = "Player O won!";
        }

        winningLine[indexIn].SetActive(true);

    }
}
