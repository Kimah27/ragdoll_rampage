using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;

    public List<Sprite> walkingLeft;
    public List<Sprite> walkingRight;
    public List<Sprite> angelLeft;
    public List<Sprite> angelRight;
    public List<Sprite> attackingLeft;
    public List<Sprite> attackingRight;
    public List<Sprite> specials;

    public float fadeRate;
    public float appearRate;
    public float nyanFadeRate;

    public int animationMax;
    public int animationIndex;
    public int frameLoop;
    public int frameCount;

    public bool isAngel;
    public bool isHowling;
    public bool isWhirlwinding;
    public bool isAlphaClawing;
    public bool isGroundPounding;
    public bool isStaredowning;
    public bool isPayDaying;
    public bool isNyanSlashing;
    public bool damaged;

    void Start()
    {
        playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        fadeRate = 30.0f;
        appearRate = 80.0f;
        nyanFadeRate = 5.0f;
        isAngel = false;
        isHowling = false;
        isWhirlwinding = false;
        isAlphaClawing = false;
        isGroundPounding = false;
        isStaredowning = false;
        isNyanSlashing = false;
        damaged = false;
        animationMax = 3;
        animationIndex = 0;
        frameLoop = 3;
        frameCount = 0;
    }

    void Update()
    {
        frameLoop = playerMovement.isRunning ? 3 : 5;
    }

	private void FixedUpdate()
	{
        MovementAnimation();
	}

	private void MovementAnimation()
    {
        if (!playerMovement.isAlive)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.6f);
	    }
        if (playerMovement.isBubbled)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = playerMovement.isAlive ? specials[6] : specials[9];

        }
        else if (playerMovement.isSupering)
        {
            if (playerMovement.cat == PlayerMovement.Cat.ANGUS && !isStaredowning)
            {
                
                if (playerMovement.staredowning)
                {
                    isStaredowning = true;
                    gameObject.GetComponent<SpriteRenderer>().sprite = playerMovement.facingRight ? specials[4] : specials[3];
                    StartCoroutine(Staredown());
                }
                    
            }
            if (playerMovement.cat == PlayerMovement.Cat.BAILEY && isPayDaying)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = playerMovement.facingRight ? specials[8] : specials[7];
            }
            if (playerMovement.cat == PlayerMovement.Cat.MIA && !isNyanSlashing)
            {
                isNyanSlashing = true;
                
                StartCoroutine(NyanSlash());
            }
            if (playerMovement.cat == PlayerMovement.Cat.TITAN && playerMovement.megaSlaming)
            {
                playerMovement.megaSlaming = false;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(25.0f, 0.0f, 0.0f));
                gameObject.GetComponent<SpriteRenderer>().sprite = specials[2];
            }
            if (playerMovement.cat == PlayerMovement.Cat.TITAN && !playerMovement.megaSlamApex && !playerMovement.rb.useGravity)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(25.0f, 0.0f, 90.0f));
            }
        }
        else if (playerMovement.isSpecialing)
        {
            if (playerMovement.cat == PlayerMovement.Cat.ANGUS)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = playerMovement.facingRight ? attackingRight[1] : attackingLeft[1];
            }
            if (playerMovement.cat == PlayerMovement.Cat.BAILEY && !isWhirlwinding)
            {
                isWhirlwinding = true;
                StartCoroutine(Whirlwind(playerMovement.whirlwindDuration));
			}
            if (playerMovement.cat == PlayerMovement.Cat.MIA && !isAlphaClawing)
            {
                isAlphaClawing = true;
                StartCoroutine(AlphaClaw(playerMovement.alphaClawStartup, playerMovement.alphaClawDashDuration, playerMovement.alphaClawRecovery));
            }
            if (playerMovement.cat == PlayerMovement.Cat.TITAN && playerMovement.groundPoundApex)
            {
                gameObject.transform.rotation = playerMovement.facingRight ? Quaternion.Euler(new Vector3(25.0f, 0.0f, -90.0f)) : Quaternion.Euler(new Vector3(25.0f, 0.0f, 90.0f));
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 1.0f);
                playerMovement.groundPoundApex = false;
            }
            if (playerMovement.cat == PlayerMovement.Cat.TITAN && !playerMovement.groundPounding)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(25.0f, 0.0f, 0.0f));
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, transform.parent.position.z);
            }
        }

        else if (playerMovement.isAttacking)
        {
            animationIndex = 0;
            frameCount = 0;

            if (playerMovement.combo == 1)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = playerMovement.facingRight ? attackingRight[0] : attackingLeft[0];
            }
            if (playerMovement.combo == 2)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = playerMovement.facingRight ? attackingRight[1] : attackingLeft[1];
            }
            if (playerMovement.combo == 3)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = playerMovement.facingRight ? attackingRight[2] : attackingLeft[2];
            }
        }

        else if (playerMovement.isDodging)
        {
            

        }

        else if (!playerMovement.isMoving && !playerMovement.isSpecialing)
        {
            animationIndex = 0;
            frameCount = 0;

            if (!playerMovement.facingRight)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = playerMovement.isAlive ? walkingLeft[animationIndex] : angelLeft[animationIndex];
            }
            if (playerMovement.facingRight)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = playerMovement.isAlive ? walkingRight[animationIndex] : angelRight[animationIndex];
            }
        }

        else if (playerMovement.isMoving)
        {
            if (!playerMovement.facingRight)
            {
                if (playerMovement.isJumping)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = playerMovement.isAlive ? walkingLeft[1] : angelLeft[1];
                }
                else if (frameCount >= frameLoop)
                {
                    animationIndex += 1;
                    if (animationIndex > animationMax)
                    {
                        animationIndex = 0;
                    }

                    gameObject.GetComponent<SpriteRenderer>().sprite = playerMovement.isAlive ? walkingLeft[animationIndex] : angelLeft[animationIndex];
                    frameCount = 0;
                }
                else
                {
                    frameCount += 1;
                }
            }
            if (playerMovement.facingRight)
            {
                if (playerMovement.isJumping)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = playerMovement.isAlive ? walkingRight[3] : angelRight[3];
                }
                else if (frameCount >= frameLoop)
                {
                    animationIndex += 1;
                    if (animationIndex > animationMax)
                    {
                        animationIndex = 0;
                    }

                    gameObject.GetComponent<SpriteRenderer>().sprite = playerMovement.isAlive ? walkingRight[animationIndex] : angelRight[animationIndex];
                    frameCount = 0;
                }
                else
                {
                    frameCount += 1;
                }
            }
        }
    }

    public void CheckState()
    {
        if (playerMovement.recentlyDamaged)
        {
            damaged = true;
            StartCoroutine(Damaged());
		}
    }

    public IEnumerator Dodging()
    {
        float timer = 0.0f;
        gameObject.GetComponent<SpriteRenderer>().sprite = specials[6];
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 0.4f);

        while (timer < playerMovement.dodgeDuration)
        {
            float NewRotation;
            if (playerMovement.facingRight)
            {
                NewRotation = (-720.0f * Time.deltaTime) + gameObject.transform.rotation.eulerAngles.z;
                NewRotation = NewRotation % 360.0f;
            }
            else
            {
                NewRotation = (720.0f * Time.deltaTime) + gameObject.transform.rotation.eulerAngles.z;
                NewRotation = NewRotation % 360.0f;
            }
            timer += Time.deltaTime;

            var rot = gameObject.transform.rotation.eulerAngles;
            rot.Set(0.0f, 0.0f, NewRotation);
            gameObject.transform.rotation = Quaternion.Euler(rot);
            yield return new WaitForFixedUpdate();
		}

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(25.0f, 0.0f, 0.0f));
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public IEnumerator BubbleAttacking()
    {
        float timer = 0.0f;

        while (timer < playerMovement.bubbleAttackDuration)
        {
            float NewRotation;
            if (playerMovement.facingRight)
            {
                NewRotation = (-720.0f * Time.deltaTime) + gameObject.transform.rotation.eulerAngles.z;
                NewRotation = NewRotation % 360.0f;
            }
            else
            {
                NewRotation = (720.0f * Time.deltaTime) + gameObject.transform.rotation.eulerAngles.z;
                NewRotation = NewRotation % 360.0f;
            }
            timer += Time.deltaTime;

            var rot = gameObject.transform.rotation.eulerAngles;
            rot.Set(0.0f, 0.0f, NewRotation);
            gameObject.transform.rotation = Quaternion.Euler(rot);
            yield return new WaitForFixedUpdate();
        }

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(25.0f, 0.0f, 0.0f));

    }

    public IEnumerator Damaged()
    {   
        if (damaged)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;

            yield return new WaitForSeconds(1.0f);

            playerMovement.recentlyDamaged = false;
            damaged = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public IEnumerator Whirlwind(float time)
    {
        float TimeRotating = 0.0f;
        gameObject.GetComponent<SpriteRenderer>().sprite = specials[0];
        //playerMovement.rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        while (TimeRotating < time)
        {
            if (gameObject.transform.rotation.eulerAngles.y > 180.0f)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = specials[1];
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = specials[0];
            }

            float NewRotation = (720.0f * Time.deltaTime) + gameObject.transform.rotation.eulerAngles.y;
            NewRotation = NewRotation % 360.0f;

            var rot = gameObject.transform.rotation.eulerAngles;
            rot.Set(0.0f, NewRotation, 0.0f);
            gameObject.transform.rotation = Quaternion.Euler(rot);

            TimeRotating += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(25.0f, 0.0f, 0.0f));
        isWhirlwinding = false;
        //playerMovement.rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public IEnumerator AlphaClaw(float delay, float dash, float finish)
    {
        float timeDashing = 0.0f;
        float alpha = 1.0f;
        gameObject.GetComponent<SpriteRenderer>().sprite = playerMovement.facingRight ? walkingRight[3] : walkingLeft[3];

        yield return new WaitForSeconds(delay);

        gameObject.GetComponent<SpriteRenderer>().sprite = playerMovement.facingRight ? attackingRight[1] : attackingLeft[1];

        while (timeDashing < dash / 2)
        {
            alpha -= fadeRate * Time.deltaTime;
            Mathf.Clamp01(alpha);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
            timeDashing += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }
        while (timeDashing >= dash / 2 && timeDashing < dash)
        {
            alpha += appearRate * Time.deltaTime;
            Mathf.Clamp01(alpha);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
            timeDashing += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(finish);
        isAlphaClawing = false;
    }

    public IEnumerator PayDay()
    {
        float time = 0.0f;
        float duration = 6.0f;
        float counter = 0.25f;
        while (time < duration)
        {
            if (counter <= 0.5f)
            {
                float NewRotation = (-150.0f * Time.deltaTime) + transform.rotation.eulerAngles.z;
                NewRotation = NewRotation % 360.0f;

                var rot = gameObject.transform.rotation.eulerAngles;
                rot.Set(25.0f, 0.0f, NewRotation);
                transform.rotation = Quaternion.Euler(rot);
                counter += Time.deltaTime;
            }
            else if (counter <= 1.0f)
            {
                float NewRotation = (150.0f * Time.deltaTime) + transform.rotation.eulerAngles.z;
                NewRotation = NewRotation % 360.0f;

                var rot = gameObject.transform.rotation.eulerAngles;
                rot.Set(25.0f, 0.0f, NewRotation);
                transform.rotation = Quaternion.Euler(rot);
                counter += Time.deltaTime;
            }
            else
            {
                counter = 0.0f;
			}

            time += Time.deltaTime;
            
            yield return new WaitForFixedUpdate();
		}

        var idle = new Vector3(25.0f, 0.0f, 0.0f);
        transform.rotation = Quaternion.Euler(idle);
    }

    public IEnumerator NyanSlash()
    {
        float alpha = 1.0f;
        yield return new WaitForSeconds(1.0f);
        while (alpha > 0.0f)
        {
            alpha -= nyanFadeRate * Time.deltaTime;
            Mathf.Clamp01(alpha);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);

            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(2.4f);
        gameObject.GetComponent<SpriteRenderer>().sprite = specials[5];
        while (alpha < 1.0f)
        {
            alpha += nyanFadeRate * 5.0f * Time.deltaTime;
            Mathf.Clamp01(alpha);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);

            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(2.4f);
        isNyanSlashing = false;
    }

    public IEnumerator Staredown()
    {
        yield return new WaitForSeconds(5.0f);
        isStaredowning = false;
    }

    public IEnumerator HitFlash()
    {
        float timer = 0.0f;
        float flash = 0.0f;

        while (timer < 1.5f)
        {
            timer += Time.deltaTime;
            flash += Time.deltaTime;

            if (flash < 0.1f)
            {
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
            else if (flash < 0.2f)
            {
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else
            {
                flash = 0.0f;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator Frozen()
    {
        float timer = 0.0f;
        spriteRenderer.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);

        while (timer < 5.0f && playerMovement.isFrozen)
        {
            timer += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public IEnumerator EnterPortalAnimation()
    {
        float timer = 0.0f;
        float timerMax = 1.5f;

        while (timer < timerMax)
        {
            float NewRotation = (540.0f * Time.deltaTime) + transform.rotation.eulerAngles.z;
            NewRotation = NewRotation % 360.0f;

            var rot = transform.rotation.eulerAngles;
            rot.Set(25.0f, 0.0f, NewRotation);
            transform.rotation = Quaternion.Euler(rot);

            transform.localScale = transform.localScale - Vector3.one * 0.6f * Time.deltaTime;

            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
		}

        timer = 0.0f;

        while (timer < timerMax)
        {
            float NewRotation = (540.0f * Time.deltaTime) + transform.rotation.eulerAngles.z;
            NewRotation = NewRotation % 360.0f;

            var rot = transform.rotation.eulerAngles;
            rot.Set(25.0f, 0.0f, NewRotation);
            transform.rotation = Quaternion.Euler(rot);

            transform.localScale = transform.localScale + Vector3.one * 0.6f * Time.deltaTime;

            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        transform.rotation = Quaternion.Euler(new Vector3(25.0f, 0.0f, 0.0f));
        transform.localScale = Vector3.one;
    }


    public IEnumerator DeathFlash()
    {
        float timer = 0.0f;
        float flash = 0.0f;

        while (timer < 2.0f)
        {
            timer += Time.deltaTime;
            flash += Time.deltaTime;

            if (timer > 1.5f)
            {
                isAngel = true;
			}
            if (flash < 0.15f)
            {
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
            else if (flash < 0.3f)
            {
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.6f);
            }
            else
            {
                flash = 0.0f;
			}

            yield return new WaitForFixedUpdate();
        }
    }
}
