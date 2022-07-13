using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerPrefabs : MonoBehaviour
{
    private CharacterSelect characterSelect;
    private LevelManagement levelManagement;
    private PlayerInputManager playerInputManager;
    private PlayerInput playerInput;
    public HUDController hud;

    public GameObject[] catPrefabs;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        playerInputManager = GameObject.Find("PlayerManager").GetComponent<PlayerInputManager>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (!hud)
        {
            hud = FindObjectOfType<HUDController>();
        }

        if (SceneManager.GetActiveScene().name == "CharacterSelect")
        {
            if (!characterSelect && GameObject.Find("CharacterSelectionController"))
            {
                characterSelect = GameObject.Find("CharacterSelectionController").GetComponent<CharacterSelect>();
            }
        }

        if (!levelManagement && GameObject.Find("LevelManagement"))
        {
            levelManagement = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        }

        if (SceneManager.GetActiveScene().name == "FrozenSuburbs")
        {
            if (!levelManagement.p1Spawned && !levelManagement.p1firstSpawn && GameObject.Find("LevelManagement"))
            {
                levelManagement.activePlayers = 1;

                PlayerInput CreatedPlayer = PlayerInput.Instantiate(catPrefabs[levelManagement.p1Selection], playerInput.playerIndex, playerInput.currentControlScheme, playerInput.splitScreenIndex, playerInput.devices[0]);
                levelManagement.p1Spawned = true;
                levelManagement.p1firstSpawn = true;

                for (int i = 0; i < playerInput.devices.Count; i++)
                {
                    Debug.Log("Input device: " + playerInput.devices[i].ToString());
                }

                Debug.Log("P1 spawned");

                Destroy(this.gameObject);
            }
        }

        if (GameObject.Find("LevelManagement"))
        {
            if (!levelManagement.p2Spawned && playerInputManager.playerCount == 2 && !levelManagement.p2firstSpawn && !levelManagement.p2characterSelected)
            {
                levelManagement.activePlayers = 2;
                levelManagement.livesRemaining -= 1;
                levelManagement.p2firstSpawn = true;
                levelManagement.player2 = this.gameObject.transform;
                
                if (levelManagement.p1Selection == 0)
                {
                    levelManagement.p2Selection = 1;
                }

                hud.RefreshHUD();
            }

            if (!levelManagement.p2Spawned && playerInputManager.playerCount == 2 && levelManagement.p2firstSpawn && levelManagement.p2characterSelected && levelManagement.livesRemaining > 0)
            {
                PlayerInput CreatedPlayer = PlayerInput.Instantiate(catPrefabs[levelManagement.p2Selection], playerInput.playerIndex, playerInput.currentControlScheme, playerInput.splitScreenIndex, playerInput.devices[0]);
                CreatedPlayer.gameObject.transform.position = new Vector3(levelManagement.player1.transform.position.x - 2, 10, 0.0f);
                levelManagement.p2Spawned = true;
                levelManagement.activePlayers = 2;
                levelManagement.livesRemaining -= 1;

                for (int i = 0; i < playerInput.devices.Count; i++)
                {
                    Debug.Log("Input device: " + playerInput.devices[i].ToString());
                }

                Debug.Log("Player 2 spawned");

                Destroy(this.gameObject);
            }
        }
    }

    public void Quit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }

    public void LeftArrow(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().name == "CharacterSelect")
        {
            if (context.performed)
            {
                characterSelect.LeftArrow(context);
            }
        }
        else if (levelManagement.notMenu && levelManagement.p2firstSpawn && !levelManagement.p2characterSelected)
        {
            if (context.performed)
            {
                Debug.Log("character left");
                levelManagement.p2Selection -= 1;

                if (levelManagement.p2Selection == levelManagement.p1Selection)
                {
                    levelManagement.p2Selection -= 1;
                }

                if (levelManagement.p2Selection < 0)
                {
                    levelManagement.p2Selection = 3;
                }

                hud.RefreshHUD();
            }
        }
    }

    public void RightArrow(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().name == "CharacterSelect")
        {
            if (context.performed)
            {
                characterSelect.RightArrow(context);
            }
        }
        else if (levelManagement.notMenu && levelManagement.p2firstSpawn && !levelManagement.p2characterSelected)
        {
            if (context.performed)
            {
                Debug.Log("character right");
                levelManagement.p2Selection += 1;

                if (levelManagement.p2Selection > 3)
                {
                    levelManagement.p2Selection = 0;
                }

                if (levelManagement.p2Selection == levelManagement.p1Selection)
                {
                    levelManagement.p2Selection += 1;
                }

                hud.RefreshHUD();
            }
        }
    }

    public void Confirm(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().name == "CharacterSelect")
        {
            if (context.performed)
            {
                characterSelect.Confirm(context);
            }
        }
        else if (levelManagement.notMenu && levelManagement.p2firstSpawn && !levelManagement.p2characterSelected)
        {
            if (context.performed)
            {
                Debug.Log("Confirm in game");
                PlayerInput CreatedPlayer = PlayerInput.Instantiate(catPrefabs[levelManagement.p2Selection], playerInput.playerIndex, playerInput.currentControlScheme, playerInput.splitScreenIndex, playerInput.devices[0]);
                CreatedPlayer.gameObject.transform.position = new Vector3(levelManagement.player1.transform.position.x - 2, 10, 0.0f);
                
                levelManagement.p2Spawned = true;
                levelManagement.p2characterSelected = true;

                for (int i = 0; i < playerInput.devices.Count; i++)
                {
                    Debug.Log("Input device: " + playerInput.devices[i].ToString());
                }

                Debug.Log("Player 2 spawned");
                hud.playerCluster2.ToggleArrows();
                playerInputManager.DisableJoining();

                Destroy(this.gameObject);
            }
        }
    }
}
