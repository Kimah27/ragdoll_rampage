using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAnimation : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public EnemyMovement enemyMovement;
    public BossMovement bossMovement;
    public NPCController npcController;

    public Transform actor;
    public SpriteRenderer shadow;

	void Awake()
	{
        playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
        enemyMovement = gameObject.GetComponentInParent<EnemyMovement>();
        bossMovement = gameObject.GetComponentInParent<BossMovement>();
        npcController = gameObject.GetComponentInParent<NPCController>();
        actor = transform.parent;
        shadow = gameObject.GetComponent<SpriteRenderer>();
    }
	void Start()
    {
        
    }

    void Update()
    {
        CheckPosition();
    }

    public void CheckPosition()
    {
        
		if (playerMovement)
		{
            transform.position = new Vector3(actor.transform.position.x, playerMovement.hitGround.point.y + 0.1f, actor.transform.position.z);
            if (playerMovement.cat == PlayerMovement.Cat.TITAN && playerMovement.isSupering)
            {
                
            }
            else
                transform.localScale = new Vector3(0.03f / Mathf.Clamp(playerMovement.distanceToGround, 1.0f, 5.0f) , 0.03f / Mathf.Clamp(playerMovement.distanceToGround, 1.0f, 5.0f));
        }
        else if (enemyMovement)
        {
            transform.position = new Vector3(enemyMovement.hitGround.point.x, enemyMovement.hitGround.point.y + 0.1f, enemyMovement.hitGround.point.z);
            transform.localScale = new Vector3(0.03f / Mathf.Clamp(actor.position.y, 1.0f, 5.0f), 0.03f / Mathf.Clamp(actor.position.y, 1.0f, 5.0f), 1.0f);
        }
        else if (bossMovement)
        {
            transform.position = new Vector3(bossMovement.hitGround.point.x, bossMovement.hitGround.point.y + 0.1f, bossMovement.hitGround.point.z);
            transform.localScale = new Vector3(0.03f / Mathf.Clamp(actor.position.y, 1.0f, 5.0f), 0.03f / Mathf.Clamp(actor.position.y, 1.0f, 5.0f), 1.0f);
        }
        else if (npcController)
        {
            transform.position = new Vector3(npcController.hitGround.point.x, npcController.hitGround.point.y + 0.1f, npcController.hitGround.point.z);
            transform.localScale = new Vector3(0.03f / Mathf.Clamp(actor.position.y, 1.0f, 5.0f), 0.03f / Mathf.Clamp(actor.position.y, 1.0f, 5.0f), 1.0f);
        }
        else
        {
            transform.position = new Vector3(actor.transform.position.x, 0.1f, actor.transform.position.z);
        }
    }

    public IEnumerator DeathFlash()
    {
        //gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        float alpha = 1.0f;

        while (alpha > 0.0f)
        {
            alpha -= Time.deltaTime;
            shadow.color = new Color(1.0f, 0.0f, 0.0f, alpha);

            yield return new WaitForFixedUpdate();
        }
    }
}
