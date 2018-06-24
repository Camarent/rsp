using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decider : MonoBehaviour
{
    public GameManager gameManager;
    public ScoreController scoreController;

    public void Decide(Enums.RSPVariants player, Enums.RSPVariants ai)
    {
        if (player == ai)
            gameManager.ReloadRound();
        else
        {
            if (player == Enums.RSPVariants.Rock && ai == Enums.RSPVariants.Scissors ||
                player == Enums.RSPVariants.Scissors && ai == Enums.RSPVariants.Paper ||
                player == Enums.RSPVariants.Paper && ai == Enums.RSPVariants.Rock)
            {
                scoreController.IncreaseScore(Enums.PlayerType.Player);
                gameManager.PlayerWin();
            }
            else
            {
                scoreController.IncreaseScore(Enums.PlayerType.AI);
                gameManager.AIWin();
            }
        }
    }

}
