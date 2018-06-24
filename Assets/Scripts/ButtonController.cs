using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    private AudioManager audio;

    public AudioClip clickClip;

    void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.isLoaded)
            Set();
    }

    void Set()
    {
        var managers = Utils.FindObjectsOfTypeAll<AudioManager>();
        if (managers.Count > 0)
            audio = managers[0];
    }

    public void Play()
    {
        audio.PlayOneShot(clickClip);
    }
}
