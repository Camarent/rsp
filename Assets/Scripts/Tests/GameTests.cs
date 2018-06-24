using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using RandomGenerators;
using RSP;
using UnityEngine.SceneManagement;

public class GameTests
{

    private GameManager gameManager;
    private AIController ai;
    private PlayerController player;

    private float percent;
    private bool reloaded;


    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    [Timeout(100000000)]
    public IEnumerator GameTestsPure()
    {
        yield return LoadScene();

        ai = Utils.FindObjectsOfTypeAll<AIController>()[0];
        ai.ChangeAIType(RandomGenerator<Game>.Type.Pure, 0.5f);

        yield return StartTest(10);
    }

    [UnityTest]
    [Timeout(100000000)]
    public IEnumerator GameTestsWeightWin()
    {
        yield return LoadScene();

        ai = Utils.FindObjectsOfTypeAll<AIController>()[0];
        ai.ChangeAIType(RandomGenerator<Game>.Type.Weight, 1f);

        yield return StartTest(10);

        Assert.Greater(percent, 0.98f);
    }

    [UnityTest]
    [Timeout(100000000)]
    public IEnumerator GameTestsWeightLoose()
    {
        yield return LoadScene();

        ai = Utils.FindObjectsOfTypeAll<AIController>()[0];
        ai.ChangeAIType(RandomGenerator<Game>.Type.Weight, 0f);

        yield return StartTest(10);

        Assert.Less(percent, 0.02f);
    }

    [UnityTest]
    [Timeout(100000000)]
    public IEnumerator GameTestsWeight_0_65()
    {
        yield return LoadScene();

        ai = Utils.FindObjectsOfTypeAll<AIController>()[0];
        ai.ChangeAIType(RandomGenerator<Game>.Type.Weight, 0.65f);

        yield return StartTest(50);

        Assert.Less(percent, 0.7f);
        Assert.Greater(percent, 0.6f);
    }

    private IEnumerator LoadScene()
    {
        yield return null;
        SceneManager.LoadScene("Play");
        yield return null;
        yield return null;
    }

    public IEnumerator StartTest(float counter)
    {
        player = Utils.FindObjectsOfTypeAll<PlayerController>()[0];
        gameManager = Utils.FindObjectsOfTypeAll<GameManager>()[0];

        GameManager.GameState += WaitForRoundEnd;
        GameManager.GameReload += Reloaded;


        var count = counter;
        while (count > 0)
        {
            PlayerDecide();
            yield return new WaitUntil(() => reloaded);
            reloaded = false;
            count--;
        }
        Debug.Log("Passed");
        var score = Utils.FindObjectsOfTypeAll<ScoreController>()[0];
        percent = (float)score.aiScore/(counter);

        GameManager.GameState -= WaitForRoundEnd;
        GameManager.GameReload -= Reloaded;
    }

    public void Reloaded()
    {
        reloaded = true;
    }

    public void PlayerDecide()
    {
        var random = Random.Range(0, 3);
        player.Choose(random);
    }

    public void WaitForRoundEnd(Game game)
    {
        gameManager.ReloadRound();
    }
}
