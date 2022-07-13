using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHealthBar : MonoBehaviour
{
    public EnemyMovement enemyMovement;
    public BossMovement bossMovement;
    public SpriteRenderer spriteRenderer;
    public Vector3 localScale;

    void Start()
    {
        enemyMovement = gameObject.GetComponentInParent<EnemyMovement>();
        bossMovement = gameObject.GetComponentInParent<BossMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthBar()
    {
        if (enemyMovement)
        {
            localScale.x = 4 * (enemyMovement.health / enemyMovement.maxHealth);
        }
        if (bossMovement)
        {
            localScale.x = 4 * (bossMovement.health / bossMovement.maxHealth);
        }

        transform.localScale = localScale;

        if (localScale.x < 1.0f)
        {
            spriteRenderer.color = Color.red;
        }
        else if (localScale.x < 2.0f)
        {
            spriteRenderer.color = Color.yellow;
        }
    }
}
