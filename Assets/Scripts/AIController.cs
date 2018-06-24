using System;
using System.Collections;
using System.Collections.Generic;
using RandomGenerators;
using RSP;
using Sirenix.OdinInspector;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public RandomGenerator<Game>.Type aiType;
    [Range(0,1)]
    [OnValueChanged(nameof(ChangePossibility))]
    public float Possibility = 0.5f; 

    private RandomGenerator<Game> generator;
    private GameManager gameManager;

    // Use this for initialization
    void Start () {
        gameManager = FindObjectOfType<GameManager>();
        var dictionary = new Dictionary<Game, float>(){{ Game.Win, 1f/3f}, { Game.Draw, 1f / 3f } , { Game.Loose, 1f / 3f } };
        generator = new RandomGenerator<Game>(aiType, dictionary);

        ChangeAIType(aiType, Possibility);
	}

    public void ChangePossibility(float newPossibility)
    {
        var possibility = Mathf.Clamp01(newPossibility);
        var weights = new float[3] { possibility, (1 - possibility) / 2, (1 - possibility) / 2 };
        generator.SetTypeAndWeight(aiType, weights);
    }

    public void ChangeAIType(RandomGenerator<Game>.Type newType, float newPossibility)
    {
        var possibility = Mathf.Clamp01(newPossibility);
        var weights = new float[3] {possibility, (1 - possibility)/2, (1 - possibility) / 2};
        generator.SetTypeAndWeight(newType,weights);
    }
	
	// Update is called once per frame
	public RSPVariants Choose ()
	{
	    var playerChoose = gameManager.GetPlayerDecision();
	    var game = generator.Generate();
	    switch (playerChoose)
	    {
	        case RSPVariants.Rock:
	            switch (game)
	            {
	                case Game.Win:
	                    return RSPVariants.Paper;
	                case Game.Draw:
	                    return RSPVariants.Rock;
	                case Game.Loose:
	                    return RSPVariants.Scissors;
                    default:
	                    throw new ArgumentOutOfRangeException();
	            }
	        case RSPVariants.Scissors:
	            switch (game)
	            {
	                case Game.Win:
	                    return RSPVariants.Rock;
	                case Game.Draw:
	                    return RSPVariants.Scissors;
	                case Game.Loose:
	                    return RSPVariants.Paper;
	                default:
	                    throw new ArgumentOutOfRangeException();
	            }
            case RSPVariants.Paper:
                switch (game)
                {
                    case Game.Win:
                        return RSPVariants.Scissors;
                    case Game.Draw:
                        return RSPVariants.Paper;
                    case Game.Loose:
                        return RSPVariants.Rock;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            default:
	            throw new ArgumentOutOfRangeException(nameof(playerChoose), playerChoose, null);
	    }
    }
}
