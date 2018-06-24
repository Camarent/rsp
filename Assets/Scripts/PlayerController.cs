using System.Collections;
using System.Collections.Generic;
using RSP;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private RSPVariants choose;

    private GameManager gameManager;

	// Use this for initialization
	void Start ()
	{
	    gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Choose(int variant)
    {
        var rspVariant = (RSPVariants) variant;
        gameManager.SetPlayerDecision(rspVariant);
    }
}
