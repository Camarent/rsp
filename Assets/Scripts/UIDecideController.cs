using System.Collections.Generic;
using RSP;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class UIDecideController : SerializedMonoBehaviour
{
    public Dictionary<RSPVariants,GameObject> playerIcons;
    public Dictionary<RSPVariants, GameObject> aiIcons;

    public Button[] buttons;
    public AudioClip clip;
    // Use this for initialization
    void Start ()
	{
	    GameManager.AIChoosedEvent += AIDecide;
	    GameManager.PlayerChoosedEvent += PlayerDecide;
	    GameManager.GameReload += Reset;
    }

    public void PlayAudio()
    {
        AudioManager.PlayOneShot(clip);
    }

    public void AIDecide(RSPVariants decision)
    {
        PlayAudio();
        aiIcons[decision].SetActive(true);
    }

    public void PlayerDecide(RSPVariants decision)
    {
        foreach (var button in buttons)
        {
            button.interactable = false;
        }
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
        foreach (var button in buttons)
        {
            button.interactable = true;
        }
    }

    void OnDestroy()
    {
        GameManager.AIChoosedEvent -= AIDecide;
        GameManager.PlayerChoosedEvent -= PlayerDecide;
        GameManager.GameReload -= Reset;
    }
}
