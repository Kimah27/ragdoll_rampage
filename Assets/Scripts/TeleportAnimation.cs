using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAnimation : MonoBehaviour
{
    public List<Sprite> teleportAnim;

    public int animationMax;
    public int animationIndex;
    public int frameLoop;
    public int frameCount;

    void Start()
    {
        animationMax = 6;
        animationIndex = 0;
        frameLoop = 3;
        frameCount = 0;

        Destroy(gameObject, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        AnimateTeleport();
    }

    public void AnimateTeleport()
    {
        if (frameCount == frameLoop && animationIndex < animationMax)
        {
            animationIndex += 1;
            if (animationIndex > animationMax)
            {
                animationIndex = 0;
            }

            gameObject.GetComponent<SpriteRenderer>().sprite = teleportAnim[animationIndex];
            frameCount = 0;
        }
        else
        {
            frameCount += 1;
        }
    }
}
