using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{
    public GameObject cameraLockTarget;
    public GameObject[] enemyPrefabs;

    public AudioSource audioSource;
    public AudioClip spawnSound;

    public Vector3 spawnTarget;
    public Vector3[] spawnPositions;

    public PlayerInputManager inputManager;

    public HUDController hud;
    public Camera cam;

    public Transform player1;
    public Transform player2;

    public int p1Selection;
    public int p2Selection;
    public bool p1firstSpawn;
    public bool p2firstSpawn;
    public bool p1characterSelected;
    public bool p2characterSelected;
    public bool p1Spawned;
    public bool p2Spawned;
    public bool gameOver;
    public bool notMenu;
    public bool inEncounter;

    public bool multipleDevices;
    public bool runGameOver;
    public bool killEnemies;

    public GameObject[] catPrefabs;

    public float driftSpeed;

    public int activePlayers;
    public int livesRemaining;
    public int encounterEnemies;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        activePlayers = 0;
        livesRemaining = 9;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hud && p1Spawned && notMenu)
        {
            hud = GameObject.Find("HUD").GetComponent<HUDController>();
            Debug.Log("Spawn hud");
        }

        if (!cam)
        {
            cam = FindObjectOfType<Camera>();
        }

        CheckGameOver();
    }

    public void Player1In(Transform p1)
    {
        player1 = p1;
    }

    public void Player1Out()
    {
        player1 = null;
    }

    public void Player2In(Transform p2)
    {
        player2 = p2;
        //activePlayers = 2;
    }

    public void Player2Out()
    {
        player2 = null;
        //activePlayers = 1;
    }

    void OnPlayerJoined(PlayerInput input)
    {
        activePlayers += 1;

        if (activePlayers > 1)
        {
            PlayerInput CreatedPlayer = PlayerInput.Instantiate(catPrefabs[p1Selection], input.playerIndex, input.currentControlScheme, input.splitScreenIndex, input.devices[0]);

            CreatedPlayer.gameObject.transform.position = new Vector3(player1.transform.position.x - 2, 10, 0.0f);
        }
    }

    void OnPlayerLeft(PlayerInput input)
    {
        activePlayers -= 1;
        if (hud.playerCluster2)
        {
            hud.playerCluster2.gameObject.SetActive(false);
        }
    }

    public int P1Selection
    {
        get => p1Selection;
        set => p1Selection = value;
    }

    public int ActivePlayers
    {
        get => activePlayers;
        set => activePlayers = value;
    }

    public IEnumerator SpawnEncounter()
    {
        if (activePlayers <= 1)
        {
            encounterEnemies = 3;
        }
        else if (activePlayers == 2)
        {
            encounterEnemies = 5;
        }        

        while (!cam.GetComponent<CameraMovement>().inPosition)
        {
            yield return new WaitForFixedUpdate();
		}

        for (int spawned = 0; spawned < encounterEnemies; spawned++)
        {
            var enemy = Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, 4)], spawnTarget + spawnPositions[spawned], Quaternion.identity);
            enemy.gameObject.layer = 16;
            audioSource.PlayOneShot(spawnSound, 0.5f);

            yield return new WaitForSeconds(0.4f);
		}

	}

    public IEnumerator SpawnTutorialEncounter()
    {
        if (activePlayers <= 1)
        {
            encounterEnemies = 3;
        }
        else if (activePlayers == 2)
        {
            encounterEnemies = 5;
        }

        while (!cam.GetComponent<CameraMovement>().inPosition)
        {
            yield return new WaitForFixedUpdate();
        }

        for (int spawned = 0; spawned < encounterEnemies; spawned++)
        {
            var enemy = Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, 2)], spawnTarget + spawnPositions[spawned], Quaternion.identity);
            enemy.gameObject.layer = 16;
            audioSource.PlayOneShot(spawnSound, 0.5f);

            yield return new WaitForSeconds(0.4f);
        }

    }

    public IEnumerator SpawnFerals()
    {
        yield return new WaitForSeconds(1.0f);

        if (activePlayers <= 1)
        {
            encounterEnemies = 2;
        }
        else if (activePlayers == 2)
        {
            encounterEnemies = 3;
        }

        for (int spawned = 0; spawned < encounterEnemies; spawned++)
        {
            var enemy = Instantiate(enemyPrefabs[2], spawnTarget + spawnPositions[spawned], Quaternion.identity);
            enemy.gameObject.layer = 16;
            audioSource.PlayOneShot(spawnSound, 0.5f);

            yield return new WaitForSeconds(0.4f);
        }
    }

    public void CheckEncounter()
    {
        if (inEncounter && encounterEnemies <= 0)
        {
            inEncounter = false;
            cam.GetComponent<CameraMovement>().lockedOn = false;
            cam.GetComponent<CameraMovement>().lockOn = null;
        }
	}

    public IEnumerator AllLivesLost()
    {
        yield return new WaitForSeconds(2.0f);

        gameOver = true;
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(5.0f);

        gameOver = true;
	}

    public IEnumerator RunGameOver()
    {
        killEnemies = true;
        yield return new WaitForSeconds(2.2f);

        if (GameObject.Find("Yuna"))
        {
            StartCoroutine(GameObject.Find("Yuna").GetComponent<YunaController>().Rescued());
        }

        yield return new WaitForSeconds(1.2f);

        if (GameObject.Find("SubspaceManager"))
        {
            GameObject.Find("SubspaceManager").GetComponent<SubspaceScript>().EndBGM();
        }

        StartCoroutine(GameOver());
    }

    public void CheckGameOver()
    {
        if (runGameOver)
        {
            runGameOver = false;

            StartCoroutine(RunGameOver());
		}
	}
}
