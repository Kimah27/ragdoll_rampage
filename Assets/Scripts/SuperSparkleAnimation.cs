using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSparkleAnimation : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public List<Sprite> superSparkleAnim;

    public int animationMax = 3;
    public int animationIndex = 0;
    public int frameLoop = 1;
    public int frameCount = 1;

    void Start()
    {
        playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
        if (playerMovement && playerMovement.cat != PlayerMovement.Cat.TITAN)
        {
            gameObject.GetComponent<SpriteRenderer>().color = playerMovement.color;
        }
        
    }

    void Update()
    {
        if (playerMovement && !playerMovement.isSupering)
        {
            Destroy(gameObject);
		}
    }

    private void FixedUpdate()
    {
        AnimateSuperSparkle();
    }

    public void AnimateSuperSparkle()
    {
        if (frameCount == frameLoop)
        {
            animationIndex += 1;
            if (animationIndex > animationMax)
            {
                animationIndex = 0;
            }

            gameObject.GetComponent<SpriteRenderer>().sprite = superSparkleAnim[animationIndex];
            frameCount = 0;
        }
        else
        {
            frameCount += 1;
        }
    }
}
