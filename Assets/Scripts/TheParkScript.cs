using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheParkScript : MonoBehaviour
{
    private LevelManagement levelManagement;

    [SerializeField] private AudioClip theParkMusic;

    void Awake()
    {
        levelManagement = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        levelManagement.notMenu = true;
    }

    void Start()
    {
        AudioManager.Instance.PlayMusicWithFade(theParkMusic);
        FadeManager.Instance.FadeIn(1.5f, Color.black);

        if (levelManagement.player1)
        {
            levelManagement.player1.transform.position = new Vector3(0, 1, 0);
        }
        if (levelManagement.player2)
        {
            levelManagement.player2.transform.position = new Vector3(-1, 1, 0);
        }
    }

    void Update()
    {
        
    }
}
