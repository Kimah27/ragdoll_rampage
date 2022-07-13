using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public LevelManagement levelManagement;
    public HUDPlayerCluster playerCluster1;
    public HUDPlayerCluster playerCluster2;
    public TMP_Text lives;
    public Camera cam;
    public Canvas canvas;
    public SpriteRenderer gameOverText;
    public SpriteRenderer pressStartText;
    public SpriteRenderer p1PressStart;
    public SpriteRenderer p1ToJoin;
    public SpriteRenderer p2PressStart;
    public SpriteRenderer p2ToJoin;

    public float textSpeed;
    public float counter;
    public float p1inactiveCounter;
    public float p2inactiveCounter;

    public bool pressStart;

    void Awake()
    {

    }

	void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        if (GameObject.Find("LevelManagement"))
        {
            levelManagement = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        }

        canvas = GameObject.Find("HUD").GetComponent<Canvas>();
        textSpeed = 2.0f;

        playerCluster1 = transform.Find("P1-Cluster").GetComponent<HUDPlayerCluster>();
        playerCluster2 = transform.Find("P2-Cluster").GetComponent<HUDPlayerCluster>();
        lives = transform.Find("LivesText").GetComponent<TMP_Text>();
        gameOverText = transform.Find("GameOverText").GetComponent<SpriteRenderer>();
        pressStartText = transform.Find("PressStartText").GetComponent<SpriteRenderer>();
        p1PressStart = transform.Find("P1PressStartText").GetComponent<SpriteRenderer>();
        p2PressStart = transform.Find("P2PressStartText").GetComponent<SpriteRenderer>();
        p1ToJoin = transform.Find("P1ToJoinText").GetComponent<SpriteRenderer>();
        p2ToJoin = transform.Find("P2ToJoinText").GetComponent<SpriteRenderer>();
        RefreshHUD();
    }

    // Update is called once per frame
    void Update()
    {
        if (!cam)
        {
            cam = FindObjectOfType<Camera>();
            canvas.worldCamera = cam;
        }

        CheckGameOver();
        ToJoinText();
    }

	private void FixedUpdate()
	{
        
        
	}

    public void RefreshHUD()
    {
        if (levelManagement)
        {
            playerCluster1.player = levelManagement.player1;
            playerCluster1.gameObject.SetActive(playerCluster1.player ? true : false);
            playerCluster2.player = levelManagement.player2;
            playerCluster2.gameObject.SetActive(playerCluster2.player ? true : false);
            lives.text = "×" + levelManagement.livesRemaining.ToString();
        }
        
	}

    public void RefreshLives()
    {
        lives.text = "×" + levelManagement.livesRemaining.ToString();
    }

    public void CheckGameOver()
    {
        if (levelManagement.gameOver)
        {
            if (gameOverText.transform.localPosition.y < 0.0f)
            {
                gameOverText.transform.position = gameOverText.transform.position + Vector3.up * textSpeed * Time.deltaTime;
            }

            counter += Time.deltaTime;

            if (counter > 5.0f)
            {
                pressStart = true;
                if (counter < 6.0f)
                {
                    pressStartText.enabled = true;
                }
                else if (counter < 7.0f)
                {
                    pressStartText.enabled = false;
                }
                else
                    counter = 5.0f;
			}
		}
	}

    public void ToJoinText()
    {
        if (!playerCluster1.gameObject.activeSelf && levelManagement.livesRemaining > 0)
        {
            if (p1inactiveCounter < 0.5f)
            {
                p1PressStart.enabled = true;
                p1ToJoin.enabled = false;
            }
            else if (p1inactiveCounter < 1.0f)
            {
                p1PressStart.enabled = false;
                p1ToJoin.enabled = true;
            }
            else if (p1inactiveCounter <= 2.0f)
            {
                p1PressStart.enabled = false;
                p1ToJoin.enabled = false;
            }
            else
                p1inactiveCounter = 0.0f;

            p1inactiveCounter += Time.deltaTime;
        }
        else
        {
            p1PressStart.enabled = false;
            p1ToJoin.enabled = false;
            p1inactiveCounter = 1.0f;
        }

        if (!playerCluster2.gameObject.activeSelf && levelManagement.livesRemaining > 0)
        {
            if (p2inactiveCounter < 0.5f)
            {
                p2PressStart.enabled = true;
                p2ToJoin.enabled = false;
            }
            else if (p2inactiveCounter < 1.0f)
            {
                p2PressStart.enabled = false;
                p2ToJoin.enabled = true;
            }
            else if (p2inactiveCounter <= 2.0f)
            {
                p2PressStart.enabled = false;
                p2ToJoin.enabled = false;
            }
            else
                p2inactiveCounter = 0.0f;

            p2inactiveCounter += Time.deltaTime;
        }
        else
        {
            p2PressStart.enabled = false;
            p2ToJoin.enabled = false;
            p2inactiveCounter = 1.0f;
        }
    }
}
