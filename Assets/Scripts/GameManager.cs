using System;
using System.Collections;
using System.Collections.Generic;
using RSP;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private RSPVariants playerChoosed;
    private RSPVariants aiChoosed;

    private PlayerController player;
    private AIController ai;
    private ScoreController score;
    private Decider decider;

    public static event Action<RSPVariants> PlayerChoosedEvent;
    public static event Action<RSPVariants> AIChoosedEvent;

    public static event Action GameWin;
    public static event Action GameDraw;
    public static event Action GameLoose;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        ai = FindObjectOfType<AIController>();
        score = FindObjectOfType<ScoreController>();
        decider = FindObjectOfType<Decider>();
    }

    public void ReloadRound()
    {
        GameDraw?.Invoke();
    }

    public void PlayerWin()
    {
        GameWin?.Invoke();
    }

    public void AIWin()
    {
        GameLoose?.Invoke();
    }

    internal void SetPlayerDecision(RSPVariants variant)
    {
        playerChoosed = variant;
        PlayerChoosedEvent?.Invoke(playerChoosed);
        StartCoroutine(Waiter());
    }

    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(1f);
        aiChoosed = ai.Choose();
        AIChoosedEvent?.Invoke(aiChoosed);
        yield return new WaitForSeconds(1f);
        decider.Decide(playerChoosed, aiChoosed);
    }

    internal RSPVariants GetPlayerDecision()
    {
        return playerChoosed;
    }
}
