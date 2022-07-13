using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDBar : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color superFlashColor;
    public Vector3 localScale;

    public float value;
    public float maxValue;
    public float lerpSpeed;
    public float lerpValue;

    public bool configured;
    public bool isHealth;
    public bool isSpecial;
    public bool isSuper;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        localScale = transform.localScale;
        lerpSpeed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void FixedUpdate()
	{
        if (configured)
        {
            localScale.x = 1000 * (value / maxValue);
            transform.localScale = localScale;

            if (isHealth)
            {
                if (localScale.x < 300.0f)
                {
                    spriteRenderer.color = Color.red;
                }
                else if (localScale.x < 600.0f)
                {
                    spriteRenderer.color = Color.yellow;
                }
                else
                {
                    spriteRenderer.color = Color.green;
                }
            }
            if (isSuper && value == maxValue)
            {
                lerpValue += lerpSpeed * Time.deltaTime;
                superFlashColor = Color.Lerp(Color.magenta, new Color(1.0f, 0.77f, 0.0f), Mathf.Sin(lerpValue));
                spriteRenderer.color = superFlashColor;
                if (lerpValue >= 120.0f)
                {
                    lerpValue = 0.0f;
				}
            }
        }
    }
}
