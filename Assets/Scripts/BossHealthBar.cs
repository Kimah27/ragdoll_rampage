using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    public BossMovement bossMovement;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer nameRenderer;
    public Vector3 localScale;

    void Start()
    {
        bossMovement = transform.parent.GetComponent<BossMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthBar()
    {
        if (bossMovement)
        {
            localScale.x = 6 * (bossMovement.health / bossMovement.maxHealth);
        }

        transform.localScale = localScale;

        if (localScale.x < 1.5f)
        {
            spriteRenderer.color = Color.red;
        }
        else if (localScale.x < 3.0f)
        {
            spriteRenderer.color = Color.yellow;
        }
    }

    public IEnumerator FadeIn()
    {
        float counter = 0.0f;

        while (counter < 1.0f)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, counter);
            nameRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, counter);
            counter += Time.deltaTime;
            yield return new WaitForFixedUpdate();
		}
	}
}
