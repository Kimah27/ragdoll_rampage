using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public List<Sprite> walkingLeft;
    public List<Sprite> walkingRight;
    public List<Sprite> attackingLeft;
    public List<Sprite> attackingRight;
    public List<Sprite> specials;

    public EnemyMovement enemyMovement;
    public BossMovement bossMovement;
    public SpriteRenderer spriteRenderer;

    public float shadyFadeRate;
    public float teleportFadeRate;

    public int animationMax;
    public int animationIndex;
    public int frameLoop;
    public int frameCount;

    public int hitsTaken;

    public bool attackSpriteChosen;


    void Start()
    {
        enemyMovement = gameObject.GetComponentInParent<EnemyMovement>();
        bossMovement = gameObject.GetComponentInParent<BossMovement>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        shadyFadeRate = 5.0f;
        teleportFadeRate = 2.5f;
        hitsTaken = 0;
        animationMax = 3;
        animationIndex = 0;
        frameLoop = 3;
        frameCount = 0;
    }

    void Update()
    {
        CheckIfHit();
        if (enemyMovement)
        {
            frameLoop = enemyMovement.isEngaged ? 3 : 5;
        }
    }

	private void FixedUpdate()
	{
        MovementAnimation();
	}

	public void CheckIfHit()
    {
        if (enemyMovement && enemyMovement.isHit)
        {
            enemyMovement.isHit = false;
            hitsTaken += 1;
            StartCoroutine(HitFlash());
        }
        if (bossMovement && bossMovement.isHit && !bossMovement.isInvulnerable)
        {
            bossMovement.isHit = false;
            hitsTaken += 1;
            StartCoroutine(HitFlash());
        }
    }
    public IEnumerator HitFlash()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        hitsTaken -= 1;

        if (hitsTaken == 0 && ((enemyMovement && enemyMovement.isAlive) || (bossMovement && bossMovement.isAlive)))
        {
            spriteRenderer.color = Color.white;
        }
    }

    public IEnumerator DeathFlash()
    {
        //gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        float alpha = 1.0f;

        while (alpha > 0.0f)
        {
            if (bossMovement)
            {
                alpha -= Time.deltaTime / 2.0f;
            }
            else
            {
                alpha -= Time.deltaTime;
            }
            
            spriteRenderer.color = new Color(1.0f, 0.0f, 0.0f, alpha);

            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator FeralDive()
    {
        yield return new WaitForSeconds(0.6f);
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(25.0f, 0.0f, enemyMovement.facingRight ? -90.0f : 90.0f));
        yield return new WaitForSeconds(1.2f);
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(25.0f, 0.0f, 0.0f));
    }

    public IEnumerator FadeIn()
    {
        float alpha = 0.0f;
        yield return new WaitForSeconds(0.2f);
        while (alpha < 1.0f)
        {
            alpha += shadyFadeRate * 3.0f * Time.deltaTime;
            Mathf.Clamp01(alpha);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);

            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator FadeOut()
    {
        float alpha = 1.0f;
        while (alpha > 0.0f)
        {
            alpha -= shadyFadeRate * Time.deltaTime;
            Mathf.Clamp01(alpha);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);

            yield return new WaitForFixedUpdate();
        }
    }
    public IEnumerator Backflip()
    {
        float timer = 0.0f;
        float NewRotation = 0.0f;

        while (timer < 1.4f)
        {
            if (enemyMovement.facingRight)
            {
                NewRotation = (220.0f * Time.deltaTime) + gameObject.transform.rotation.eulerAngles.z;
                NewRotation = NewRotation % 360.0f;
            }
            else
            {
                NewRotation = (-220.0f * Time.deltaTime) + gameObject.transform.rotation.eulerAngles.z;
                NewRotation = NewRotation % 360.0f;
            }
            
            var rot = gameObject.transform.rotation.eulerAngles;
            rot.Set(0.0f, 0.0f, NewRotation);
            gameObject.transform.rotation = Quaternion.Euler(rot);

            timer += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(25.0f, 0.0f, 0.0f));
    }

    public IEnumerator TeleportFadeOut()
    {
        float alpha = 1.0f;

        yield return new WaitForSeconds(0.1f);

        while (alpha > 0.0f)
        {
            alpha -= teleportFadeRate * Time.deltaTime;
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, alpha);

            yield return new WaitForFixedUpdate();
        }

        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    public IEnumerator TeleportFadeIn()
    {
        float alpha = 0.0f;

        yield return new WaitForSeconds(0.2f);

        while (alpha < 1.0f)
        {
            alpha += teleportFadeRate * Time.deltaTime;
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, alpha);

            yield return new WaitForFixedUpdate();
        }

        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public IEnumerator MorphAnimation()
    {
        float counter = 0.0f;
        float a = 0.4f;
        float b = 0.05f;

        spriteRenderer.sprite = specials[1];

        yield return new WaitForSeconds(0.5f);

        while (a > 0.0f)
        {
            if (counter < a)
            {
                spriteRenderer.sprite = specials[1];
            }
            else if (counter <= a + b)
            {
                spriteRenderer.sprite = specials[2];
            }
            else
            {
                counter = 0.0f;
                a -= 0.05f;
			}

            counter += Time.deltaTime;
            yield return new WaitForFixedUpdate();
		}

        spriteRenderer.sprite = specials[2];
        bossMovement.isMorphing = false;
	}

    public void MovementAnimation()
    {
        if (enemyMovement)
        {
            if (enemyMovement.isAttacking)
            {
                if (enemyMovement.enemyType == EnemyMovement.EnemyType.GRUNT && !attackSpriteChosen)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = enemyMovement.facingRight ? attackingRight[Random.Range(0, 3)] : attackingLeft[Random.Range(0, 3)];
                    attackSpriteChosen = true;
                }
                if (enemyMovement.enemyType == EnemyMovement.EnemyType.FLYER)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = specials[0];

                }
                if (enemyMovement.enemyType == EnemyMovement.EnemyType.FERAL)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = enemyMovement.facingRight ? attackingRight[0] : attackingLeft[0];

                }
                if (enemyMovement.enemyType == EnemyMovement.EnemyType.SHADY)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = enemyMovement.facingRight ? attackingRight[0] : attackingLeft[0];

                }
            }
            else if (enemyMovement.isThrowingDagger)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = enemyMovement.facingRight ? attackingRight[1] : attackingLeft[1];
            }
            else if (enemyMovement.isMoving && !enemyMovement.isAttacking)
            {
                if (!enemyMovement.facingRight)
                {
                    if (frameCount >= frameLoop)
                    {
                        animationIndex += 1;
                        if (animationIndex > animationMax)
                        {
                            animationIndex = 0;
                        }

                        gameObject.GetComponent<SpriteRenderer>().sprite = walkingLeft[animationIndex];
                        frameCount = 0;
                    }
                    else
                    {
                        frameCount += 1;
                    }
                }
                if (enemyMovement.facingRight)
                {
                    if (frameCount >= frameLoop)
                    {
                        animationIndex += 1;
                        if (animationIndex > animationMax)
                        {
                            animationIndex = 0;
                        }

                        gameObject.GetComponent<SpriteRenderer>().sprite = walkingRight[animationIndex];
                        frameCount = 0;
                    }
                    else
                    {
                        frameCount += 1;
                    }
                }
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = enemyMovement.facingRight ? walkingRight[0] : walkingLeft[0];
                attackSpriteChosen = false;
            }
        }
        

        if (bossMovement)
        {
            if (bossMovement.isMorphing)
            {
                
			}
            else if (bossMovement.isTeleporting)
            {
                if (bossMovement.phase == BossMovement.Phase.PHASE3)
                {
                    spriteRenderer.sprite = specials[2];
                }
                else
                {
                    spriteRenderer.sprite = specials[1];
                }
                
			}
            else if (bossMovement.isCasting)
            {
                if (bossMovement.phase == BossMovement.Phase.PHASE1 || (bossMovement.phase == BossMovement.Phase.INTERMISSION && !bossMovement.endingPhase2))
                {
                    spriteRenderer.sprite = bossMovement.facingRight ? attackingRight[1] : attackingLeft[1];
                }
                if (bossMovement.phase == BossMovement.Phase.PHASE2 || (bossMovement.phase == BossMovement.Phase.INTERMISSION && bossMovement.endingPhase2))
                {
                    spriteRenderer.sprite = specials[1];
                }
                if (bossMovement.phase == BossMovement.Phase.PHASE3)
                {
                    spriteRenderer.sprite = specials[2];
                }
            }
            else if (bossMovement.isAttacking)
            {
                if (bossMovement.phase == BossMovement.Phase.PHASE1)
                {
                    spriteRenderer.sprite = bossMovement.facingRight ? attackingRight[0] : attackingLeft[0];
                }
                if (bossMovement.phase == BossMovement.Phase.PHASE3)
                {
                    spriteRenderer.sprite = bossMovement.facingRight ? attackingRight[2] : attackingLeft[2];
                }
            }
            else if (bossMovement.isMoving)
            {
                if (!bossMovement.facingRight && bossMovement.phase != BossMovement.Phase.PHASE3)
                {
                    animationMax = 3;
                    if (animationIndex > 3)
                    {
                        animationIndex = 0;
					}
                    if (frameCount >= frameLoop)
                    {
                        animationIndex += 1;
                        if (animationIndex > animationMax)
                        {
                            animationIndex = 0;
                        }

                        spriteRenderer.sprite = walkingLeft[animationIndex];
                        frameCount = 0;
                    }
                    else
                    {
                        frameCount += 1;
                    }
                }
                if (bossMovement.facingRight && bossMovement.phase != BossMovement.Phase.PHASE3)
                {
                    animationMax = 3;
                    if (animationIndex > 3)
                    {
                        animationIndex = 0;
                    }
                    if (frameCount >= frameLoop)
                    {
                        animationIndex += 1;
                        if (animationIndex > animationMax)
                        {
                            animationIndex = 0;
                        }

                        spriteRenderer.sprite = walkingRight[animationIndex];
                        frameCount = 0;
                    }
                    else
                    {
                        frameCount += 1;
                    }
                }
                if (!bossMovement.facingRight && bossMovement.phase == BossMovement.Phase.PHASE3)
                {
                    animationMax = 7;
                    if (animationIndex < 4)
                    {
                        animationIndex = 4;
                    }
                    if (frameCount >= frameLoop)
                    {
                        animationIndex += 1;
                        if (animationIndex > animationMax)
                        {
                            animationIndex = 4;
                        }

                        spriteRenderer.sprite = walkingLeft[animationIndex];
                        frameCount = 0;
                    }
                    else
                    {
                        frameCount += 1;
                    }
                }
                if (bossMovement.facingRight && bossMovement.phase == BossMovement.Phase.PHASE3)
                {
                    animationMax = 7;
                    if (animationIndex < 4)
                    {
                        animationIndex = 4;
                    }
                    if (frameCount >= frameLoop)
                    {
                        animationIndex += 1;
                        if (animationIndex > animationMax)
                        {
                            animationIndex = 4;
                        }

                        spriteRenderer.sprite = walkingRight[animationIndex];
                        frameCount = 0;
                    }
                    else
                    {
                        frameCount += 1;
                    }
                }
            }
            else
            {
                if (bossMovement.phase == BossMovement.Phase.PHASE1 || bossMovement.phase == BossMovement.Phase.PHASE2 || bossMovement.phase == BossMovement.Phase.INTERMISSION)
                {
                    spriteRenderer.sprite = specials[0];
                }
                if (bossMovement.phase == BossMovement.Phase.PHASE3)
                {
                    spriteRenderer.sprite = specials[2];
                }
            }
        }
    }
}
