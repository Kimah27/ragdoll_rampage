using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PressAnyKey : MonoBehaviour
{
    private PlayerInputManager inputManager;

    public AudioSource audioSource;
    public AudioClip clip;
    public float pressAnyKeyVolume = 0.5f;

    private bool started = false;

    void Awake()
    {
        inputManager = GameObject.Find("PlayerManager").GetComponent<PlayerInputManager>();
    }

    void Start()
    {
        FadeManager.Instance.FadeIn(1f, Color.white);
    }

    void Update()
    {
        if (!started)
        {
            if (inputManager.playerCount > 0)
            {
                audioSource.PlayOneShot(clip, pressAnyKeyVolume);
                FadeManager.Instance.FadeOut(1.5f, Color.white, LoadScene);
                started = true;
                inputManager.DisableJoining();
            }
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("CharacterSelect");
    }
}
