using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpellAnimation : MonoBehaviour
{
    public List<Sprite> iceSpellAnim;

    public float growSpeed;

    public int animationMax;
    public int animationIndex;
    public int frameLoop;
    public int frameCount;

    void Start()
    {
        growSpeed = 0.8f;
        animationMax = 4;
        animationIndex = 0;
        frameLoop = 3;
        frameCount = 0;
    }

    void Update()
    {
        if (transform.position.y <= 0.6f)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = iceSpellAnim[5];
            transform.localScale = transform.localScale + Vector3.one * growSpeed * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        AnimateIceSpell();
    }

    public void AnimateIceSpell()
    {
        if (frameCount >= frameLoop && animationIndex < animationMax)
        {
            animationIndex += 1;

            gameObject.GetComponent<SpriteRenderer>().sprite = iceSpellAnim[animationIndex];
            frameCount = 0;
        }
        else
        {
            frameCount += 1;
        }
    }
}
