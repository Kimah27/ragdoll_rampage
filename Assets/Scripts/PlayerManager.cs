using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public LevelManagement levelManagement;
    public PlayerInputManager playerInputManager;
    private HUDController HUDController;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (!levelManagement && GameObject.Find("LevelManagement"))
        {
            levelManagement = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        }

        if (!playerInputManager && GameObject.Find("PlayerManager"))
        {
            playerInputManager = GameObject.Find("PlayerManager").GetComponent<PlayerInputManager>();
        }

        if (!HUDController)
        {
            HUDController = FindObjectOfType<HUDController>();
        }
    }

    void OnPlayerJoined(PlayerInput input)
    {
        Debug.Log("Player Joined. Count: " + playerInputManager.playerCount);
        
        if (HUDController)
        {
            HUDController.RefreshHUD();
        }
    }

    void OnPlayerLeft(PlayerInput input)
    {
        Debug.Log("Player Left Count: " + playerInputManager.playerCount);

        if (HUDController)
        {
            HUDController.RefreshHUD();
        }

        if (levelManagement.livesRemaining == 0)
        {
            playerInputManager.DisableJoining();
        }
        else
        {
            playerInputManager.EnableJoining();
        }
    }
}
