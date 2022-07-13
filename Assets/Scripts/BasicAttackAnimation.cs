using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackAnimation : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public List<Sprite> basicAttackAnim;

    public int animationMax = 3;
    public int animationIndex = 0;
    public int frameLoop = 0;
    public int frameCount = 0;

    void Start()
    {
        frameLoop = 0;
        frameCount = 0;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        AnimateBasicAttack();
    }

    public void AnimateBasicAttack()
    {
        if (frameCount == frameLoop && animationIndex < animationMax)
        {
            animationIndex += 1;
            if (animationIndex > animationMax)
            {
                animationIndex = 0;
            }

            gameObject.GetComponent<SpriteRenderer>().sprite = basicAttackAnim[animationIndex];
            frameCount = 0;
        }
        else
        {
            frameCount += 1;
        }
    }
}
