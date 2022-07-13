using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenSuburbsScript : MonoBehaviour
{
    private LevelManagement levelManagement;

    [SerializeField] private AudioClip frozenSuburbsMusic;

    void Awake()
    {
        levelManagement = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        levelManagement.notMenu = true;
    }

    void Start()
    {
        AudioManager.Instance.PlayMusicWithFade(frozenSuburbsMusic);
        FadeManager.Instance.FadeIn(1.5f, Color.black);
    }

    void Update()
    {

    }
}
