using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string volumeSceneName = "Volume";
    public string playeSceneName = "Play";

    // Use this for initialization
    void Start ()
	{
	    SceneManager.LoadSceneAsync(volumeSceneName,LoadSceneMode.Additive);
    }
	
	// Update is called once per frame
	public void Play ()
	{
	    AsyncOperation operation = SceneManager.LoadSceneAsync(playeSceneName, LoadSceneMode.Additive);
	    operation.completed += UnloadSelf;
	}

    private void UnloadSelf(AsyncOperation obj)
    {
        SceneManager.UnloadSceneAsync(0);
    }
}
