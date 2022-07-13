using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubspaceScript : MonoBehaviour
{
    private LevelManagement levelManagement;

    [SerializeField] private AudioClip subspaceMusic;
    [SerializeField] private AudioClip phase1Music;
    [SerializeField] private AudioClip phase2Music;
    [SerializeField] private AudioClip phase3Music;
    [SerializeField] private AudioClip endMusic;

    void Awake()
    {
        levelManagement = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        levelManagement.notMenu = true;
    }

    void Start()
    {
        AudioManager.Instance.PlayMusic(subspaceMusic);
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

    public void Phase1BGM()
    {
        AudioManager.Instance.PlayMusicWithFade(phase1Music);
    }

    public void Phase2BGM()
    {
        AudioManager.Instance.PlayMusic(phase2Music);
    }

    public void Phase3BGM()
    {
        AudioManager.Instance.PlayMusic(phase3Music);
    }

    public void EndBGM()
    {
        AudioManager.Instance.PlayMusic(endMusic);
    }
}
