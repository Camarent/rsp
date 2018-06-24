using System.Collections;
using System.Collections.Generic;
using RSP;
using UnityEngine;

public class Decider : MonoBehaviour
{
    public GameManager gameManager;
    public ScoreController scoreController;

    public void Decide(RSPVariants player, RSPVariants ai)
    {
        if (player == ai)
            gameManager.ReloadRound();
        else
        {
            if (player == RSPVariants.Rock && ai == RSPVariants.Scissors ||
                player == RSPVariants.Scissors && ai == RSPVariants.Paper ||
                player == RSPVariants.Paper && ai == RSPVariants.Rock)
            {
                scoreController.IncreaseScore(PlayerType.Player);
                gameManager.PlayerWin();
            }
            else
            {
                scoreController.IncreaseScore(PlayerType.AI);
                gameManager.AIWin();
            }
        }
    }

}
