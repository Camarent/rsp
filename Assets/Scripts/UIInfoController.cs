using System.Collections;
using System.Collections.Generic;
using RSP;
using Sirenix.OdinInspector;
using UnityEngine;

public class UIInfoController : SerializedMonoBehaviour {
    public Dictionary<Game, GameObject> icons;
    private GameManager gameManager;

    public AudioClip clip;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        Hide();
        GameManager.GameState += Show;
        GameManager.GameReload += GameWasReload;
    }

    public void Show(Game game)
    {
        gameObject.SetActive(true);
        icons[game].SetActive(true);
    }

    public void ReloadGame()
    {
        AudioManager.PlayOneShot(clip);
        gameManager.ReloadRound();      
    }

    public void GameWasReload()
    {
        Hide();
    }

    private void Hide()
    {
        foreach (var icon in icons)
        {
            icon.Value.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        GameManager.GameState -= Show;
        GameManager.GameReload -= GameWasReload;
    }
}
