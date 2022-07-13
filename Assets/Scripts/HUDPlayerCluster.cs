using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDPlayerCluster : MonoBehaviour
{
    public LevelManagement levelManagement;
    public Transform player;

    public List<Sprite> sprites;
    public SpriteRenderer catSprite;
    public TMP_Text name;
    public HUDBar health;
    public HUDBar special;
    public HUDBar super;
    public SpriteRenderer leftArrow;
    public SpriteRenderer rightArrow;

    public bool configured;

    void Start()
    {
        if (GameObject.Find("LevelManagement"))
        {
            levelManagement = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        }

        catSprite = transform.Find("CatSprite").GetComponent<SpriteRenderer>();
        leftArrow = transform.Find("ArrowLeft").GetComponent<SpriteRenderer>();
        rightArrow = transform.Find("ArrowRight").GetComponent<SpriteRenderer>();
        name = transform.Find("NameText").GetComponent<TMP_Text>();
        health = transform.Find("HealthBar").GetComponent<HUDBar>();
        special = transform.Find("SpecialBar").GetComponent<HUDBar>();
        super = transform.Find("SuperBar").GetComponent<HUDBar>();

        configured = false;
    }

    void Update()
    {
        if (!levelManagement && GameObject.Find("LevelManagement"))
        {
            levelManagement = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        }
    }

	private void FixedUpdate()
	{
        if (!configured)
        {
            ConfigureBars();
		}
        else
        {
            UpdateBars();
        }
	}

    public void ConfigureBars()
    {
        if (player)
        {
            if (player.gameObject.GetComponent<PlayerMovement>())
            {
                gameObject.SetActive(true);
                switch (player.gameObject.GetComponent<PlayerMovement>().cat)
                {
                    case PlayerMovement.Cat.ANGUS:
                        catSprite.sprite = sprites[0];
                        break;
                    case PlayerMovement.Cat.BAILEY:
                        catSprite.sprite = sprites[1];
                        break;
                    case PlayerMovement.Cat.MIA:
                        catSprite.sprite = sprites[2];
                        break;
                    case PlayerMovement.Cat.TITAN:
                        catSprite.sprite = sprites[3];
                        break;
                }
                name.text = player.gameObject.GetComponent<PlayerMovement>().catName;
                name.faceColor = player.gameObject.GetComponent<PlayerMovement>().color;
                health.maxValue = player.gameObject.GetComponent<PlayerMovement>().maxHealth;
                health.configured = true;
                special.maxValue = 100.0f;
                special.configured = true;
                super.maxValue = 100.0f;
                super.configured = true;

                configured = true;
            }
            if (player.gameObject.GetComponent<PlayerPrefabs>())
            {
                gameObject.SetActive(true);
                switch (levelManagement.p2Selection)
                {
                    case 0:
                        catSprite.sprite = sprites[0];
                        name.text = "Angus";
                        name.faceColor = new Color(1.0f, 1.0f, 0.0f, 1.0f);
                        break;
                    case 1:
                        catSprite.sprite = sprites[1];
                        name.text = "Bailey";
                        name.faceColor = new Color(1.0f, 0.5613f, 0.9542f, 1.0f);
                        break;
                    case 2:
                        catSprite.sprite = sprites[2];
                        name.text = "Mia";
                        name.faceColor = new Color(0.7052f, 0.0f, 1.0f, 1.0f);
                        break;
                    case 3:
                        catSprite.sprite = sprites[3];
                        name.text = "Titan";
                        name.faceColor = new Color(0.0f, 0.6040f, 1.0f, 1.0f);
                        break;
                }
            }
        }
	}

    public void UpdateBars()
    {
        if (player)
        {
            health.value = player.gameObject.GetComponent<PlayerMovement>().health;
            special.value = player.gameObject.GetComponent<PlayerMovement>().specialMeter;
            super.value = player.gameObject.GetComponent<PlayerMovement>().superMeter;
        }
    }

    public void ToggleArrows()
    {
        if (leftArrow && rightArrow)
        {
            leftArrow.enabled = !leftArrow.enabled;
            rightArrow.enabled = !rightArrow.enabled;
		}
	}
}
