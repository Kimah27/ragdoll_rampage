using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaClawAnimation : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public List<Sprite> alphaClawAnim;

    public int animationMax = 4;
    public int animationIndex = 0;
    public int frameLoop = 1;
    public int frameCount = 1;

    void Start()
    {
        playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        AnimateAlphaClaw();
    }

    public void AnimateAlphaClaw()
    {
        if (animationIndex <= animationMax)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = alphaClawAnim[animationIndex];
            animationIndex += 1;
        }
    }
}
