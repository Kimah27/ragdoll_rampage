using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntAttackAnimation : MonoBehaviour
{
    private EnemyMovement enemyMovement;

    public List<Sprite> gruntAttackAnim;

    public int animationMax;
    public int animationIndex;
    public int frameLoop;
    public int frameCount;

    void Start()
    {
        animationMax = 6;
        animationIndex = 0;
        frameLoop = 1;
        frameCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void FixedUpdate()
	{
        AnimateGruntAttack();
    }

	public void AnimateGruntAttack()
    {
        if (frameCount == frameLoop && animationIndex < animationMax)
        {
            animationIndex += 1;
            if (animationIndex > animationMax)
            {
                animationIndex = 0;
            }

            gameObject.GetComponent<SpriteRenderer>().sprite = gruntAttackAnim[animationIndex];
            frameCount = 0;
        }
        else
        {
            frameCount += 1;
        }
    }
}
