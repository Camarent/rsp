using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public AudioClip clickClip;

    public void Play()
    {
        AudioManager.PlayOneShot(clickClip);
    }
}
