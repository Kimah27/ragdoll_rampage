using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public SpriteRenderer sprite;

    public float bubbleCounter;
    public float bubbleLoopTime;
    public float bubbleSpeed;

    public bool spriteSet;

    void Start()
    {
        playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        bubbleCounter = 0.0f;
        bubbleLoopTime = 1.0f;
        bubbleSpeed = 0.2f;
        sprite.enabled = false;
        spriteSet = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.isBubbled)
        {
            if (!spriteSet)
            {
                sprite.enabled = true;
                spriteSet = true;
                transform.localPosition = new Vector3(0.0f, -0.125f, 0.0f);
            }

            if (bubbleCounter < bubbleLoopTime / 2.0f)
            {
                transform.localPosition = transform.localPosition + Vector3.up * bubbleSpeed * Time.deltaTime;
			}
            else if (bubbleCounter < bubbleLoopTime)
            {
                transform.localPosition = transform.localPosition + Vector3.down * bubbleSpeed * Time.deltaTime;
            }
            else
            {
                bubbleCounter = 0.0f;
                transform.localPosition = new Vector3(0.0f, -0.125f, 0.0f);
            }

            bubbleCounter += Time.deltaTime;
		}
        if (!playerMovement.isBubbled && spriteSet)
        {
            sprite.enabled = false;
            spriteSet = false;
        }
    }
}
