using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeirdStuffScript : MonoBehaviour
{
    public List<Sprite> weirdAnim;

    public int animationMax;
    public int animationIndex;
    public int frameLoop;
    public int frameCount;

    // Start is called before the first frame update
    void Start()
    {
        animationMax = 2;
        animationIndex = 0;
        frameLoop = 12;
        frameCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        AnimateWeird();
    }

    public void AnimateWeird()
    {
        if (frameCount >= frameLoop && animationIndex < animationMax)
        {
            animationIndex += 1;

            gameObject.GetComponent<SpriteRenderer>().sprite = weirdAnim[animationIndex];
            frameCount = 0;
        }
        else if (frameCount >= frameLoop && animationIndex == animationMax)
        {
            animationIndex = 0;

            gameObject.GetComponent<SpriteRenderer>().sprite = weirdAnim[animationIndex];
            frameCount = 0;
        }
        else
        {
            frameCount += 1;
        }
    }
}
