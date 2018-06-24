using System.Collections;
using System.Collections.Generic;
using RSP;
using UnityEngine;

public class UIDecideController : MonoBehaviour
{
    public Dictionary<RSPVariants,GameObject> playerIcons;
    public Dictionary<RSPVariants, GameObject> aiIcons;

    // Use this for initialization
    void Start ()
	{
	    GameManager.AIChoosedEvent += AIDecide;
	    GameManager.PlayerChoosedEvent += PlayerDecide;

	    GameManager.GameDraw += Reset;
	    GameManager.GameLoose += Reset;
	    GameManager.GameWin += Reset;
    }

    public void AIDecide(RSPVariants decision)
    {
        aiIcons[decision].SetActive(true);
    }

    public void PlayerDecide(RSPVariants decision)
    {
        playerIcons[decision].SetActive(true);
    }

    public void Reset()
    {
        foreach (var icon in playerIcons)
        {
            icon.Value.SetActive(false);
        }
        foreach (var icon in aiIcons)
        {
            icon.Value.SetActive(false);
        }
    }

    void OnDestroy()
    {
        GameManager.AIChoosedEvent -= AIDecide;
        GameManager.PlayerChoosedEvent -= PlayerDecide;
    }
}
