using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlwindAnimation : MonoBehaviour
{
    public List<Sprite> whirlwindAnim;

    public int animationMax = 2;
    public int animationIndex = 0;
    public int frameLoop = 2;
    public int frameCount = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

	private void FixedUpdate()
	{
        AnimateWhirlwind();
	}

	public void AnimateWhirlwind()
    {
        if (frameCount == frameLoop)
        {
            animationIndex += 1;
            if (animationIndex > animationMax)
            {
                animationIndex = 0;
            }

            gameObject.GetComponent<SpriteRenderer>().sprite = whirlwindAnim[animationIndex];
            frameCount = 0;
        }
        else
        {
            frameCount += 1;
        }
    }
}
