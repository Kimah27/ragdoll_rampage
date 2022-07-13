using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackAnimation : MonoBehaviour
{
    public List<Sprite> swordAttackAnim;

    public int animationMax;
    public int animationIndex;
    public int frameLoop;
    public int frameCount;

    void Start()
    {
        animationMax = 2;
        animationIndex = 0;
        frameLoop = 2;
        frameCount = 0;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        AnimateSwordAttack();
    }

    public void AnimateSwordAttack()
    {
        if (frameCount >= frameLoop && animationIndex < animationMax)
        {
            animationIndex += 1;

            gameObject.GetComponent<SpriteRenderer>().sprite = swordAttackAnim[animationIndex];
            frameCount = 0;
        }
        else if (frameCount >= frameLoop && animationIndex == animationMax)
        {
            animationIndex = 0;

            gameObject.GetComponent<SpriteRenderer>().sprite = swordAttackAnim[animationIndex];
            frameCount = 0;
        }
        else
        {
            frameCount += 1;
        }
    }
}
