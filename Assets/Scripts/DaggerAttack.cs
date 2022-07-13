using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerAttack : MonoBehaviour
{
    public List<Sprite> daggerAttackAnim;

    public int animationMax;
    public int animationIndex;
    public int frameLoop;
    public int frameCount;

    void Start()
    {
        animationMax = 6;
        animationIndex = 0;
        frameLoop = 0;
        frameCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        AnimateDaggerAttack();
    }

    public void AnimateDaggerAttack()
    {
        if (frameCount == frameLoop && animationIndex < animationMax)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = daggerAttackAnim[animationIndex];
            frameCount = 0;
            animationIndex += 1;
        }
        else
        {
            frameCount += 1;
        }
    }
}
