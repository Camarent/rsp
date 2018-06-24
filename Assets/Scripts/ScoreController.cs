using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int playerScore;
    public int aiScore;

    public TextMeshProUGUI playerText;
    public TextMeshProUGUI aiText;

    public void IncreaseScore(Enums.PlayerType winner)
    {
        if (winner == Enums.PlayerType.AI)
        {
            aiScore++;
            aiText.text = aiScore.ToString();
        }
        else
        {
            playerScore++;
            playerText.text = playerScore.ToString();
        }
    }
}
