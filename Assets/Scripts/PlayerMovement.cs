using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    LevelManagement lm;
    PlayerControls controls;
    Vector2 move;

    public enum Cat
    {
        ANGUS,
        BAILEY,
        MIA,
        TITAN
    }

    public enum Player
    {
        P1,
        P2
	}

    public Cat cat;
    public Player p;

    [Header("GameObjects")]
    public GameObject currentAttack;
    public GameObject groundAttack1;
    public GameObject groundAttack2;
    public GameObject groundAttack3;
    public GameObject airAttack1;
    public GameObject airAttack2;
    public GameObject airAttack3;
    public GameObject bubbleAttack;
    public GameObject howlSpecial;
    public GameObject whirlwindSpecial;
    public GameObject alphaClawSpecial;
    public GameObject groundPoundSpecial;
    public GameObject staredownSuper;
    public GameObject payDaySuper;
    public GameObject nyanSlashSuper;
    public GameObject megaSlamSuper;
    public GameObject superSparkle;
    public GameObject kanjiEffect;
    public GameObject floatingText;

    [Header("Components")]
    public LevelManagement levelManagement;
    public HUDController HUDController;
    public Rigidbody rb;
    public BoxCollider bc;
    public Camera camera;
    public Transform shadow;
    public PlayerAnimation playerAnimation;
    public AudioSource audioSource;
    public AudioClip footstepSound;
    public AudioClip dashSound;
    public AudioClip jumpSound;
    public AudioClip expandSound;
    public AudioClip dodgeSound;
    public AudioClip superChargedSound;
    public AudioClip healSound;
    public Color color;
    public RaycastHit hitGround;

    [Header("Movement")]
    public Vector3 playerDirection;
    public Vector3 jumpDirection;
    public float distanceToGround;
    public float xVel;
    public float yVel;
    public float zVel;
    public float jumpHeight;
    public float floatSpeed;
    public float airAttackHeight;

    [Header("Stats")]
    public string catName;
    public float health;
    public float maxHealth;
    public float specialMeter;
    public float superMeter;
    public float movementSpeed;
    public float dodgeSpeed;
    public float dodgeDuration;
    public float dodgeRecovery;
    public float bubbleAttackDuration;
    public float knockbackForce;
    public float knockupForce;
    public float damageModifier;

    [Header("Attack & Animation Properties")]
    public float timeSinceLastBasicAttack;
    public float passiveSpecialChargeRate;
    public float activeSpecialChargeRate;
    public float activeSuperChargeRate;
    public float howlCastDuration;
    public float whirlwindDuration;
    public float alphaClawVelocity;
    public float alphaClawStartup;
    public float alphaClawDashDuration;
    public float alphaClawRecovery;
    public float groundPoundVelocity;
    public float groundPoundRecovery;
    public float staredownStartup;
    public float staredownRecovery;
    public float staredownGrowthRate;
    public float payDayStartup;
    public float payDayDuration;
    public float nyanSlashStartup;
    public float nyanSlashDuration;
    public float nyanSlashRecovery;
    public float megaSlamStartup;
    public float megaSlamVelocity;
    public float megaSlamGrowthRate;
    public float megaSlamShadowRate;
    public float megaSlamRecovery;

    public int combo;
    public int layerMask;

    [Header("Timers")]
    public float footstepCounter;
    public float footstepLoopTime;
    public float bubbleCounter;
    public float bubbleLoopTime;
    public float bubbleSpeed;
    public float bubbleAttackCounter;
    public float bubbleAttackCooldown;


    [Header("Booleans")]
    public bool characterSelected;
    public bool facingRight;
    public bool bubbleApex;
    public bool isMoving;
    public bool isJumping;
    public bool isRunning;
    public bool isDodging;
    public bool isAttacking;
    public bool isSpecialing;
    public bool isSupering;
    public bool isFlinching;
    public bool isFrozen;
    public bool isIntagible;
    public bool isInvulnerable;
    public bool isAlive;
    public bool isBubbled;
    public bool recentlyDamaged;
    public bool airAttackUsed;
    public bool canMove;
    public bool canJump;
    public bool canSpecial;
    public bool canSuper;
    public bool canRevive;
    public bool groundPounding;
    public bool groundPoundApex;
    public bool staredowning;
    public bool megaSlaming;
    public bool megaSlamApex;

    public float buttonTime;
    public bool reviveTime;

    //void OnEnable()
    //{
    //    controls.Gameplay.Enable();
    //}

    //void OnDisable()
    //{
    //    controls.Gameplay.Disable();
    //}

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        lm = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        if (!lm.p1Spawned)
        {
            this.p = Player.P1;
            lm.player1 = this.transform;
        }
        //if (lm.ActivePlayers == 2)
        if (lm.p1Spawned)
        {
            this.p = Player.P2;
            lm.player2 = this.transform;
            FindObjectOfType<Camera>().GetComponent<CameraMovement>().P2 = gameObject;
        }


        //controls = new PlayerControls();

        //controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        //controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
        //controls.Gameplay.Jump.performed += ctx => Jump();
        //controls.Gameplay.SprintStart.performed += ctx => StartSprint();
        //controls.Gameplay.SprintEnd.performed += ctx => StopSprint();
        //controls.Gameplay.Defend.performed += ctx => DefensiveAbility();
        //controls.Gameplay.BasicAttack.performed += ctx => BasicAttack();
        //controls.Gameplay.SpecialAttack.performed += ctx => SpecialAttack();
        //controls.Gameplay.SuperAttack.performed += ctx => SuperAttack();
        //controls.Gameplay.Quit.performed += ctx => Quit();
    }

    void Start()
    {
        SetCharacter();
        SetChargeRate();
        levelManagement = FindObjectOfType<LevelManagement>();
        HUDController = FindObjectOfType<HUDController>();
        HUDController.RefreshHUD();
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
        camera = FindObjectOfType<Camera>();
        shadow = transform.Find("Shadow");
        playerAnimation = transform.Find("Sprite").GetComponent<PlayerAnimation>();
        characterSelected = false;
        facingRight = true;
        isMoving = false;
        isJumping = false;
        isRunning = false;
        isDodging = false;
        isAttacking = false;
        isSpecialing = false;
        isSupering = false;
        isFlinching = false;
        isIntagible = false;
        isInvulnerable = false;
        isAlive = true;
        canMove = true;
        canJump = true;
        canSpecial = true;
        canSuper = false;
        canRevive = false;
        groundPounding = false;
        groundPoundApex = false;
        megaSlamApex = false;
        megaSlaming = false;
        maxHealth = 100.0f;
        health = maxHealth;
        knockbackForce = 240.0f;
        knockupForce = 150.0f;
        distanceToGround = 1.0f;
        footstepCounter = 0.0f;
        footstepLoopTime = 0.6f;
        specialMeter = 100.0f;
        superMeter = 0.0f;
        jumpHeight = 7.0f;
        floatSpeed = 2.5f;
        airAttackHeight = 2.4f;
        movementSpeed = 5.0f;
        dodgeSpeed = 10.0f;
        dodgeDuration = 0.5f;
        dodgeRecovery = 0.3f;
        bubbleAttackDuration = 0.5f;
        damageModifier = 1.0f;
        howlCastDuration = 0.8f;
        whirlwindDuration = 3.0f;
        alphaClawVelocity = 30.0f;
        alphaClawStartup = 0.4f;
        alphaClawDashDuration = 0.35f;
        alphaClawRecovery = 0.8f;
        groundPoundVelocity = 6.0f;
        groundPoundRecovery = 0.8f;
        staredownStartup = 0.4f;
        staredownRecovery = 1.0f;
        staredownGrowthRate = 3.0f;
        payDayStartup = 0.5f;
        payDayDuration = 6.0f;
        nyanSlashStartup = 1.0f;
        nyanSlashDuration = 1.8f;
        nyanSlashRecovery = 1.0f;
        megaSlamStartup = 1.0f;
        megaSlamVelocity = 1.5f;
        megaSlamGrowthRate = 4.0f;
        megaSlamShadowRate = 0.05f;
        megaSlamRecovery = 1.6f;
        layerMask = 1 << 13;
        bubbleCounter = -0.15f;
        bubbleLoopTime = 1.0f;
        bubbleSpeed = 0.2f;
        bubbleAttackCounter = 0.0f;
        bubbleAttackCooldown = 0.3f;
        //layerMask = ~layerMask;
    }

    void Update()
    {
        if (!characterSelected)
        {
            characterSelected = true;
            rb.useGravity = true;
		}
        if (!camera)
        {
            camera = FindObjectOfType<Camera>();
        }
        CheckInput();
        CheckGrounded();
        CheckBubbled();
        PassiveCharge();
        FootStepLoop();
        distanceToGround = RaycastDown();

        if (reviveTime)
        {
            buttonTime += Time.deltaTime;
            if (buttonTime > 1.0f)
            {
                Revive();
            }
        }
    }

	void FixedUpdate()
	{
        if (camera)
        {
            MovePlayer();
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.transform.CompareTag("BubbleProjectile"))
        {
            isBubbled = true;
            Destroy(other.gameObject);
        }

        if (other.transform.CompareTag("Roar"))
        {
            isBubbled = false;
            if (!isSpecialing && !isSupering)
            {
                rb.useGravity = true;
            }
        }

        if ((other.transform.CompareTag("EnemyAttack") || other.transform.CompareTag("EnemyProjectile")) && isAlive && !recentlyDamaged && !isIntagible)
        {
            EnemyAttacks attack = other.GetComponent<EnemyAttacks>();
            attack.PlayHitSound();

            if (other.transform.CompareTag("EnemyProjectile"))
            {
                if (other.gameObject.transform.Find("Sprite"))
                {
                    other.gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled = false;
                }
                if (other.gameObject.transform.Find("Shadow"))
                {
                    other.gameObject.transform.Find("Shadow").GetComponent<SpriteRenderer>().enabled = false;
                }           
                if (other.gameObject.GetComponent<BoxCollider>())
                {
                    other.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
                if (other.gameObject.GetComponent<SphereCollider>())
                {
                    other.gameObject.GetComponent<SphereCollider>().enabled = false;
                }

                Destroy(other.gameObject, 1.4f);
            }

            if (!isInvulnerable)
            {
                if (attack.knockback)
                {
                    if (other.transform.tag == "EnemyAttack")
                    {
                        Vector3 source = other.transform.parent.position;
                        source.x = source.x - transform.position.x;
                        source.y = 0.0f;
                        source.z = source.z - transform.position.z;

                        rb.AddForce(-source.normalized * knockbackForce * attack.attackLevel);
                    }
                    else
                    {
                        Vector3 source = other.transform.position;
                        source.x = source.x - transform.position.x;
                        source.y = 0.0f;
                        source.z = source.z - transform.position.z;

                        rb.AddForce(-source.normalized * knockbackForce * attack.attackLevel);
                    }
                }
                if (attack.knockup)
                {
                    rb.AddForce(Vector3.up * knockupForce * attack.attackLevel);
                }
                if (isFrozen)
                {
                    isFrozen = false;
				}

                float damageTaken = attack.damage;

                health -= damageTaken;

                if (floatingText && damageTaken > 0.0f)
                {
                    CharacterFlyText(damageTaken, Color.red);
                }

                if (health <= 0.0f)
                {
                    health = 0.0f;
                    isAlive = false;
                    StartCoroutine(playerAnimation.DeathFlash());
                    StartCoroutine(Death());
                }
                else
                {
                    if (!attack.freeze)
                    {
                        StartCoroutine(playerAnimation.HitFlash());
                        StartCoroutine(TakeHit(attack.flinch, true));
                    }
                    else
                    {
                        isFrozen = true;
                        StartCoroutine(Freeze());
                        StartCoroutine(playerAnimation.Frozen());
                        StartCoroutine(TakeHit(attack.flinch, false));
                    }
                    
                }
            }
        }
	}

	public float RaycastDown()
    {
        float distance = 10.0f;

        if (Physics.Raycast(transform.position, -Vector3.up, out hitGround, 10.0f, layerMask))
        {
            distance = hitGround.distance;
            Debug.DrawLine(transform.position, hitGround.point, Color.cyan);
        }

        return distance;
	}

	public void SetCharacter()
    {

	}

    public void SetChargeRate()
    {
        if (cat == Cat.ANGUS)
        {
            catName = "Angus";
            passiveSpecialChargeRate = 20.0f;
            activeSpecialChargeRate = 5.0f;
            activeSuperChargeRate = 1.0f;
        }
        else if (cat == Cat.BAILEY)
        {
            catName = "Bailey";
            passiveSpecialChargeRate = 10.0f;
            activeSpecialChargeRate = 5.0f;
            activeSuperChargeRate = 1.0f;
        }
        else if (cat == Cat.MIA)
        {
            catName = "Mia";
            passiveSpecialChargeRate = 10.0f;
            activeSpecialChargeRate = 5.0f;
            activeSuperChargeRate = 1.0f;
        }
        else if (cat == Cat.TITAN)
        {
            catName = "Titan";
            passiveSpecialChargeRate = 10.0f;
            activeSpecialChargeRate = 5.0f;
            activeSuperChargeRate = 1.0f;
        }
        activeSuperChargeRate = 4.0f;
    }

	public void CheckInput()
    {
        playerDirection = new Vector3(move.x, 0.0f, move.y).normalized;
        timeSinceLastBasicAttack += Time.deltaTime;

        if (cat == Cat.MIA && isJumping)
        {
            canSpecial = false;
        }

        if (!isJumping)
        {
            jumpDirection = playerDirection;
        }

        if ((move.x != 0 || move.y != 0 || isJumping) && !isFrozen)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (move.x > 0.0f && !isAttacking && !isFrozen && canMove)
        {
            facingRight = true;
        }

        if (move.x < 0.0f && !isAttacking && !isFrozen && canMove)
        {
            facingRight = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        HUDController.RefreshHUD();
        if (context.performed)
        {
            if (canJump && !isJumping && !isAttacking && !isFrozen)
            {
                rb.velocity = (Vector3.up * jumpHeight);
                audioSource.PlayOneShot(jumpSound, 0.7f);
                isJumping = true;
                canJump = false;
                //Debug.Log("jump");
            }
        }
    }

    public void StartSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isRunning = true;
            //Debug.Log("sprinting");
        }
    }

    public void StopSprint(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            isRunning = false;
            //Debug.Log("stop sprinting");
        }
    }

    public void DefensiveAbility(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isAlive)
            {
                Defend();
            }
        }
    }

    public void BasicAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isAlive)
            {
                Attack();
            }
            if (!isAlive)
            {
                reviveTime = true;
            }
        }
        if (context.canceled)
        {
            buttonTime = 0;
            reviveTime = false;
        }
    }

    public void SpecialAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isAlive)
            {
                Special();
            }
            if (!isAlive)
            {
                reviveTime = true;
            }
        }
        if (context.canceled)
        {
            buttonTime = 0;
            reviveTime = false;
        }
    }

    public void SuperAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isAlive)
            {
                Super();
            }
            if (!isAlive)
            {
                reviveTime = true;
            }
        }
        if (context.canceled)
        {
            buttonTime = 0;
            reviveTime = false;
        }
    }

    public void Confirm(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (levelManagement.hud.pressStart)
            {
                FadeManager.Instance.FadeOut(2.5f, Color.white, ReturnToMenu);
            }

            if (!isAlive && levelManagement.livesRemaining > 0)
            {
                Debug.Log("revive");
                Revive();
            }
        }
    }

    public void ReturnToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CleanUp");
    }

    public void Leave(InputAction.CallbackContext context)
    {
        if (this.p == Player.P2)
        {
            levelManagement.activePlayers = 1;
            levelManagement.p2Spawned = false;
            levelManagement.player2 = null;
            Destroy(this.gameObject);
        }
    }

    public void Quit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }

    public void CheckGrounded()
    {
        if (!isBubbled)
        {
            if (distanceToGround <= 1.0f && rb.velocity.y <= 0.0f)
            {
                if (isJumping)
                {
                    audioSource.PlayOneShot(footstepSound, 1.4f);
                }
                isJumping = false;
                airAttackUsed = false;
                canJump = true;
                movementSpeed = isRunning ? 8.0f : 5.0f;
            }
        }
        else
        {
            isJumping = false;
            airAttackUsed = false;
            canJump = false;
            movementSpeed = 5.0f;
        }
	}

    public void CheckBubbled()
    {
        if (isBubbled)
        {
            if (gameObject.transform.position.y < 3.5f && !bubbleApex)
            {
                rb.useGravity = false;
                transform.position = transform.position + Vector3.up * floatSpeed * Time.deltaTime;
            }
            else
            {
                bubbleApex = true;

                if (bubbleCounter < bubbleLoopTime / 2.0f)
                {
                    transform.localPosition = transform.localPosition + Vector3.up * bubbleSpeed * Time.deltaTime;
                }
                else if (bubbleCounter < bubbleLoopTime)
                {
                    transform.localPosition = transform.localPosition + Vector3.down * bubbleSpeed * Time.deltaTime;
                }
                else
                {
                    bubbleCounter = 0.0f;
                }

                transform.position = transform.position + Vector3.right * levelManagement.driftSpeed * Time.deltaTime;
                bubbleCounter += Time.deltaTime;
            }

            bubbleAttackCounter += Time.deltaTime;
		}
        else
        {
            bubbleApex = false;
        }
	}

    public void FootStepLoop()
    {
        if (isMoving && !isAttacking && !isFlinching && !isJumping && !isFrozen && canMove)
        {
            footstepCounter += Time.deltaTime;
            footstepLoopTime = isRunning ? 0.3f : 0.4f;
        }
        else
            footstepCounter = 0.2f;
        
        if (footstepCounter >= footstepLoopTime)
        {
            footstepCounter = 0.0f;
            if (!isBubbled)
            {
                audioSource.PlayOneShot(footstepSound, 1.0f);
            }
        }

	}

    public void PassiveCharge()
    {
        if (specialMeter < 100.0f && !isSpecialing)
        {
            specialMeter += passiveSpecialChargeRate * Time.deltaTime;
		}
        if (specialMeter > 100.0f)
        {
            specialMeter = 100.0f;
		}
        if (specialMeter == 100.0f && !canSpecial && !isJumping)
        {
            canSpecial = true;
        }
	}

    public void ActiveCharge()
    {
        if (specialMeter < 100.0f)
        {
            specialMeter += activeSpecialChargeRate;
        }
        if (specialMeter > 100.0f)
        {
            specialMeter = 100.0f;
        }
        if (superMeter < 100.0f)
        {
            superMeter += activeSuperChargeRate;
        }
        if (superMeter >= 100.0f && !canSuper)
        {
            superMeter = 100.0f;
            canSuper = true;
            audioSource.PlayOneShot(superChargedSound, 0.8f);

        }
    }

    public void MovePlayer()
    {
        if (canMove && !isJumping && !isFrozen && !isAttacking)
        {
            Vector3 newPos = transform.position + playerDirection * movementSpeed * Time.deltaTime;
            transform.position = new Vector3(Mathf.Clamp(newPos.x, camera.transform.position.x - 9.0f, camera.transform.position.x + 9.0f), newPos.y, newPos.z);
        }
        else if (isJumping)
        {
            Vector3 newPos = transform.position + jumpDirection * movementSpeed * Time.deltaTime;
            transform.position = new Vector3(Mathf.Clamp(newPos.x, camera.transform.position.x - 9.0f, camera.transform.position.x + 9.0f), newPos.y, newPos.z);
        }

        if (transform.position.y > 1.1f && rb.velocity.y < 0.1f && !isSupering && !isBubbled)
        {
            rb.AddForce(Physics.gravity * 2f, ForceMode.Acceleration);
        }
    }

    public void Attack()
    {
        if (isAlive && !isDodging && !isFrozen && combo < 3 && timeSinceLastBasicAttack > 0.15f)
        {
            timeSinceLastBasicAttack = 0.0f;
            canJump = false;

            if (isBubbled && bubbleAttackCounter > bubbleAttackCooldown)
            {
                Vector3 attackDirection = Vector3.zero;
                if (playerDirection != Vector3.zero)
                {
                    attackDirection = playerDirection;
                }
                else
                {
                    attackDirection = facingRight ? Vector3.right : Vector3.left;
                }

                StartCoroutine(BubbleAttack(attackDirection));
                StartCoroutine(playerAnimation.BubbleAttacking());
                bubbleAttackCounter = 0.0f;
            }

            if (!isBubbled && !isJumping && !isFlinching)
            {
                isAttacking = true;
                if (combo == 2)
                {
                    StartCoroutine(GroundCombo3(0.5f));
                    StartCoroutine(AttackShift(8.0f, 0.1f));
                    combo = 3;
                }
                if (combo == 1)
                {
                    StartCoroutine(GroundCombo2(0.4f));
                    StartCoroutine(AttackShift(8.0f, 0.06f));
                    combo = 2;
                }
                if (combo == 0)
                {
                    StartCoroutine(GroundCombo1(0.4f));
                    StartCoroutine(AttackShift(8.0f, 0.06f));
                    combo = 1;
                }
            }

            if (!isBubbled && isJumping && !isFlinching)
            {
                isAttacking = true;
                if (combo == 2)
                {
                    rb.velocity = (Vector3.up * airAttackHeight);
                    StartCoroutine(AirCombo3(0.5f));
                    combo = 3;
                }
                if (combo == 1)
                {
                    rb.velocity = (Vector3.up * airAttackHeight);
                    StartCoroutine(AirCombo2(0.4f));
                    combo = 2;
                }
                if (combo == 0)
                {
                    airAttackUsed = true;
                    jumpDirection /= 2.0f;
                    rb.velocity = (Vector3.up * airAttackHeight);
                    StartCoroutine(AirCombo1(0.4f));
                    combo = 1;
                }
            }
        }
	}

    public void Defend()
    {
        if (isAlive && !isAttacking && !isJumping && !isFlinching && !isFrozen && !isSpecialing && !isSupering && !isDodging && !isBubbled)
        {
            Vector3 dodgeDirection = Vector3.zero;
            if (playerDirection != Vector3.zero)
            {
                dodgeDirection = playerDirection;
            }
            else
            {
                dodgeDirection = facingRight ? Vector3.right : Vector3.left;
            }
            StartCoroutine(Dodge(dodgeDirection));
            StartCoroutine(playerAnimation.Dodging());
        }
	}

    public IEnumerator Dodge(Vector3 direction)
    {
        isDodging = true;
        canMove = false;
        canJump = false;
        isIntagible = true;
        gameObject.layer = 8;
        audioSource.PlayOneShot(dodgeSound, 0.7f);
        float timer = 0.0f;

        while (timer < dodgeDuration)
        {
            timer += Time.deltaTime;
            transform.position = transform.position + direction * dodgeSpeed * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        isIntagible = false;
        gameObject.layer = 6;
        yield return new WaitForSeconds(dodgeRecovery);
        isDodging = false;
        canMove = true;
        canJump = true;
    }

    public void Heal(float value)
    {
        if (health > maxHealth - value)
        {
            health = maxHealth;
	    }
        else
        {
            health += value;
        }
        
        if (floatingText)
        {
            CharacterFlyText(value, Color.green);
        }
        audioSource.PlayOneShot(healSound, 0.7f);
    }

    public void Revive()
    {
        if (levelManagement && levelManagement.livesRemaining > 0 && !isAlive && canRevive)
        {
            Heal(maxHealth);
            StartCoroutine(playerAnimation.HitFlash());
            StartCoroutine(ReviveIntangibility());
            playerAnimation.isAngel = false;
            isAlive = true;
            canRevive = false;
            levelManagement.livesRemaining--;
            HUDController.RefreshLives();
        }
	}

    public IEnumerator ReviveIntangibility()
    {
        isIntagible = true;

        yield return new WaitForSeconds(1.5f);

        isIntagible = false;
	}

    IEnumerator AttackShift(float shift, float time)
    {
        Vector3 direction = Vector3.zero;

        if (playerDirection == Vector3.zero)
        {
            if (!facingRight)
            {
                direction = new Vector3(-1.0f, 0.0f, 0.0f);
            }
            else if (facingRight)
            {
                direction = new Vector3(1.0f, 0.0f, 0.0f);
            }
        }
        else
        {
            direction = playerDirection;
        }

        rb.velocity = direction * shift;
        yield return new WaitForSeconds(time);
        rb.velocity = Vector3.zero;
    }

    IEnumerator GroundCombo1(float time)
    {
        currentAttack = (GameObject)Instantiate(groundAttack1);
        if (!facingRight)
        {
            currentAttack.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
        }
        currentAttack.transform.parent = gameObject.transform;
        currentAttack.tag = "PlayerAttack";
        currentAttack.GetComponent<PlayerAttacks>().direction = facingRight ? new Vector3(1.0f, -0.3f, 0.0f) : new Vector3(-1.0f, -0.3f, 0.0f);
        currentAttack.GetComponent<PlayerAttacks>().speed = 1.2f;
        currentAttack.transform.position = facingRight ? gameObject.transform.position + new Vector3(1.2f, -0.3f, 0.0f) : gameObject.transform.position + new Vector3(-1.2f, -0.3f, 0.0f);
        currentAttack.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.6f);
        Destroy(currentAttack, time);
        yield return new WaitForSeconds(0.2f);
        if (combo == 1)
        {
            currentAttack.GetComponent<BoxCollider>().enabled = false;
        }
        yield return new WaitForSeconds(time - 0.2f);

        

        if (combo == 1)
        {
            combo = 0;
            isAttacking = false;
            canJump = true;
        }
    }

    IEnumerator GroundCombo2(float time)
    {
        if (currentAttack)
        {
            currentAttack.GetComponent<SpriteRenderer>().enabled = false;
            currentAttack.GetComponent<BoxCollider>().enabled = false;
        }

        if (move.x > 0.0f)
        {
            facingRight = true;
        }
        if (move.x < 0.0f)
        {
            facingRight = false;
        }
        currentAttack = (GameObject)Instantiate(groundAttack2);
        if (!facingRight)
        {
            currentAttack.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
        }
        currentAttack.transform.parent = gameObject.transform;
        currentAttack.tag = "PlayerAttack";
        currentAttack.GetComponent<PlayerAttacks>().direction = facingRight ? new Vector3(1.0f, 0.3f, 0.0f) : new Vector3(-1.0f, 0.3f, 0.0f);
        currentAttack.GetComponent<PlayerAttacks>().speed = 1.2f;
        currentAttack.transform.position = facingRight ? gameObject.transform.position + new Vector3(1.2f, -0.3f, 0.0f) : gameObject.transform.position + new Vector3(-1.2f, -0.3f, 0.0f);
        currentAttack.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.6f);
        Destroy(currentAttack, time);
        yield return new WaitForSeconds(0.2f);
        if (combo == 2)
        {
            currentAttack.GetComponent<BoxCollider>().enabled = false;
        }
        yield return new WaitForSeconds(time - 0.2f);

        if (combo == 2)
        {
            combo = 0;
            isAttacking = false;
            canJump = true;
        }
    }

    IEnumerator GroundCombo3(float time)
    {
        if (currentAttack)
        {
            currentAttack.GetComponent<SpriteRenderer>().enabled = false;
            currentAttack.GetComponent<BoxCollider>().enabled = false;
        }

        if (move.x > 0.0f)
        {
            facingRight = true;
        }
        if (move.x < 0.0f)
        {
            facingRight = false;
        }
        currentAttack = (GameObject)Instantiate(groundAttack3);
        currentAttack.transform.parent = gameObject.transform;
        currentAttack.tag = "PlayerAttack";
        currentAttack.GetComponent<PlayerAttacks>().direction = Vector3.down;
        currentAttack.GetComponent<PlayerAttacks>().speed = 0.4f;
        currentAttack.transform.position = facingRight ? gameObject.transform.position + new Vector3(1.8f, -0.3f, 0.0f) : gameObject.transform.position + new Vector3(-1.8f, -0.3f, 0.0f);
        currentAttack.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.6f);
        Destroy(currentAttack, time);
        yield return new WaitForSeconds(0.2f);
        currentAttack.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(time - 0.2f);

        combo = 0;
        isAttacking = false;
        canJump = true;
    }

    IEnumerator AirCombo1(float time)
    {
        //rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        var att = (GameObject)Instantiate(airAttack1);
        if (!facingRight)
        {
            att.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
        }
        att.GetComponent<PlayerAttacks>().playerMovement = this;
        att.tag = "PlayerProjectile";
        att.GetComponent<PlayerAttacks>().direction = facingRight ? new Vector3(1.0f, -0.3f, 0.0f) : new Vector3(-1.0f, -0.3f, 0.0f);
        att.GetComponent<PlayerAttacks>().speed = 1.2f;
        att.transform.position = facingRight ? gameObject.transform.position + new Vector3(1.4f, -0.2f, 0.0f) : gameObject.transform.position + new Vector3(-1.4f, -0.2f, 0.0f);
        att.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.6f);
        yield return new WaitForSeconds(time / 2.0f);
        att.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.0f);
        att.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(time / 2.0f);
        
        if (combo == 1)
        {
            combo = 0;
            isAttacking = false;
            airAttackUsed = false;
            //rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        yield return new WaitForSeconds(time / 2.0f);
        Destroy(att);
    }

    IEnumerator AirCombo2(float time)
    {
        //rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        
        var att2 = (GameObject)Instantiate(airAttack2);
        if (!facingRight)
        {
            att2.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
        }
        att2.GetComponent<PlayerAttacks>().playerMovement = this;
        att2.tag = "PlayerProjectile";
        att2.GetComponent<PlayerAttacks>().direction = facingRight ? new Vector3(1.0f, 0.3f, 0.0f) : new Vector3(-1.0f, 0.3f, 0.0f);
        att2.GetComponent<PlayerAttacks>().speed = 1.2f;
        att2.transform.position = facingRight ? gameObject.transform.position + new Vector3(1.4f, -0.2f, 0.0f) : gameObject.transform.position + new Vector3(-1.4f, -0.2f, 0.0f);
        att2.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.6f);
        yield return new WaitForSeconds(time / 2.0f);
        att2.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.0f);
        att2.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(time / 2.0f);     

        if (combo == 2)
        {
            combo = 0;
            isAttacking = false;
            airAttackUsed = false;
            //rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        yield return new WaitForSeconds(time / 2.0f);
        Destroy(att2);
    }

    IEnumerator AirCombo3(float time)
    {
        //rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;

        var att3 = (GameObject)Instantiate(airAttack3);
        att3.GetComponent<PlayerAttacks>().playerMovement = this;
        att3.tag = "PlayerProjectile";
        att3.GetComponent<PlayerAttacks>().direction = Vector3.down;
        att3.GetComponent<PlayerAttacks>().speed = 0.4f;
        att3.transform.position = facingRight ? gameObject.transform.position + new Vector3(1.8f, -0.2f, 0.0f) : gameObject.transform.position + new Vector3(-1.8f, -0.2f, 0.0f);
        att3.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.6f);
        yield return new WaitForSeconds(0.2f);
        att3.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.0f);
        att3.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(time - 0.2f);

        
        combo = 0;
        isAttacking = false;
        //rb.constraints = RigidbodyConstraints.FreezeRotation;

        yield return new WaitForSeconds(time - 0.2f);
        Destroy(att3);
    }

    public IEnumerator BubbleAttack(Vector3 direction)
    {
        canMove = false;

        gameObject.layer = 8;
        float timer = 0.0f;
        var ba = Instantiate(bubbleAttack, transform);
        //ba.GetComponent<PlayerAttacks>().playerMovement = this;
        ba.transform.position = transform.position;
        Vector3 newPos = transform.position + direction * dodgeSpeed * Time.deltaTime;

        while (timer < dodgeDuration)
        {
            timer += Time.deltaTime;
            newPos = transform.position + direction * dodgeSpeed * Time.deltaTime;
            transform.position = new Vector3(Mathf.Clamp(newPos.x, camera.transform.position.x - 9.0f, camera.transform.position.x + 9.0f), newPos.y, Mathf.Clamp(newPos.z, -5.0f, 5.0f));
            yield return new WaitForFixedUpdate();
        }

        canMove = true;
        gameObject.layer = 6;
    }

    public void Special()
    {
        if (isAlive && !isFlinching && !isFrozen && !isSupering && !isDodging && !isAttacking && !isBubbled && canSpecial)
        {
            canSpecial = false;
            specialMeter = 0.0f;
            isSpecialing = true;
            combo = 4;

            if (cat == Cat.ANGUS)
            {
                canMove = false;
                canJump = false;
                StartCoroutine(HowlSpecial(howlCastDuration));
			}
            if (cat == Cat.BAILEY)
            {
                StartCoroutine(WhirlwindSpecial(whirlwindDuration));
            }
            if (cat == Cat.MIA)
            {
                canMove = false;
                canJump = false;
                isInvulnerable = true;
                StartCoroutine(AlphaClawSpecial(alphaClawStartup, alphaClawDashDuration, alphaClawRecovery));
            }
            if (cat == Cat.TITAN)
            {
                canMove = false;
                canJump = false;
                StartCoroutine(GroundPoundSpecial(groundPoundVelocity, groundPoundRecovery));
            }
		}
	}

    IEnumerator HowlSpecial(float time)
    {
        Vector3 direction = Vector3.zero;
        if (playerDirection.z > 0)
        {
            direction = facingRight ? new Vector3(1.0f, 0.0f, 0.6f) : new Vector3(-1.0f, 0.0f, 0.6f);
        }
        else if (playerDirection.z < 0)
        {
            direction = facingRight ? new Vector3(1.0f, 0.0f, -0.6f) : new Vector3(-1.0f, 0.0f, -0.6f);
        }
        else
        {
            direction = facingRight ? new Vector3(1.0f, 0.0f, 0.0f) : new Vector3(-1.0f, 0.0f, 0.0f);
        }
        
        var howl = Instantiate(howlSpecial, transform.position + direction, Quaternion.identity);
        howl.GetComponent<HowlAnimation>().trajectory = direction;
        howl.GetComponent<PlayerAttacks>().playerMovement = this;
        yield return new WaitForSeconds(time);
        isSpecialing = false;
        canMove = true;
        canJump = true;
        combo = 0;
    }

    IEnumerator WhirlwindSpecial(float time)
    {
        float counter = 0.0f;
        float reHit = 0.0f;
        float reHitLoop = 0.4f;
        currentAttack = (GameObject)Instantiate(whirlwindSpecial);
        currentAttack.transform.parent = gameObject.transform;
        currentAttack.tag = "PlayerAttack";
        currentAttack.transform.position = gameObject.transform.position + Vector3.down * 0.6f;

        while (counter < time && !isFrozen)
        {
            if (!currentAttack.GetComponent<CapsuleCollider>().enabled)
            {
                currentAttack.GetComponent<CapsuleCollider>().enabled = true;

            }
            if (currentAttack.GetComponent<CapsuleCollider>().enabled && reHit >= reHitLoop)
            {
                currentAttack.GetComponent<CapsuleCollider>().enabled = false;
                reHit = 0.0f;
            }

            reHit += Time.deltaTime;
            counter += Time.deltaTime;
            yield return new WaitForFixedUpdate();
		}
        
        currentAttack.GetComponent<CapsuleCollider>().enabled = false;
        isSpecialing = false;
        yield return new WaitForSeconds(0.3f);
        Destroy(currentAttack);
        combo = 0;
    }

    IEnumerator AlphaClawSpecial(float startup, float dashDuration, float recovery)
    {
        Vector3 direction = Vector3.zero;

        
        if (playerDirection.z > 0)
        {
            direction = facingRight ? new Vector3(1.0f, 0.0f, 0.6f) : new Vector3(-1.0f, 0.0f, 0.6f);
        }
        else if (playerDirection.z < 0)
        {
            direction = facingRight ? new Vector3(1.0f, 0.0f, -0.6f) : new Vector3(-1.0f, 0.0f, -0.6f);
        }
        else
        {
            direction = facingRight ? new Vector3(1.0f, 0.0f, 0.0f) : new Vector3(-1.0f, 0.0f, 0.0f);
        }

        yield return new WaitForSeconds(startup);
        rb.useGravity = false;
        gameObject.layer = 8;
        rb.velocity = direction * alphaClawVelocity;
        audioSource.PlayOneShot(dashSound, 0.8f);
        yield return new WaitForSeconds(dashDuration);
        rb.useGravity = true;
        gameObject.layer = 6;
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(recovery / 2);
        var ac = (GameObject)Instantiate(alphaClawSpecial);
        if (facingRight)
        {
            if (direction.z > 0)
            {
                ac.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 150.0f, 0.0f));
            }
            else if (direction.z < 0)
            {
                ac.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 210.0f, 0.0f));
            }
            else
            {
                ac.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
            }
        }
        else
        {
            if (direction.z > 0)
            {
                ac.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 30.0f, 0.0f));
            }
            if (direction.z < 0)
            {
                ac.transform.rotation = Quaternion.Euler(new Vector3(0.0f, -30.0f, 0.0f));
            }
        }
        ac.GetComponent<PlayerAttacks>().playerMovement = this;
        ac.tag = "PlayerProjectile";
        ac.transform.position = gameObject.transform.position - new Vector3(0.0f, 0.3f, 0.0f) - direction * 5.0f;

        yield return new WaitForSeconds(recovery / 2);
        
        canMove = true;
        canJump = true;
        
        isSpecialing = false;
        isInvulnerable = false;
        yield return new WaitForSeconds(recovery / 1.5f);
        Destroy(ac);
        combo = 0;
    }

    IEnumerator GroundPoundSpecial(float velocity, float recovery)
    {
        Vector3 direction = Vector3.zero;

        if (playerDirection == Vector3.zero)
        {
            if (!facingRight)
            {
                direction = new Vector3(-1.0f, 1.8f, 0.0f);
            }
            else if (facingRight)
            {
                direction = new Vector3(1.0f, 1.8f, 0.0f);
            }
        }
        else
        {
            direction = new Vector3(playerDirection.x, 1.8f, playerDirection.z);
        }

        //rb.useGravity = false;
        rb.velocity = direction * groundPoundVelocity;
        groundPounding = true;

        yield return new WaitForFixedUpdate();

        while (rb.velocity.y > 0.0f)
        {
            yield return new WaitForFixedUpdate();
        }

        groundPoundApex = true;
        yield return new WaitForFixedUpdate();

        //rb.useGravity = true;
        yield return new WaitForFixedUpdate();
        gameObject.layer = 8;
        while (rb.velocity.y != 0.0f)
        {
            yield return new WaitForFixedUpdate();
        }

        if (!isFrozen)
        {
            var gp = (GameObject)Instantiate(groundPoundSpecial);
            gp.transform.parent = gameObject.transform;
            gp.tag = "PlayerAttack";
            gp.transform.position = gameObject.transform.position;
            camera.GetComponent<CameraMovement>().shake = true;
            Destroy(gp, recovery + 0.4f);
        }
        

        yield return new WaitForSeconds(recovery);
        gameObject.layer = 6;
        groundPounding = false;
        yield return new WaitForFixedUpdate();

        rb.velocity = Vector3.zero;
        canMove = true;
        canJump = true;
        combo = 0;
        isSpecialing = false;

        yield return new WaitForSeconds(0.4f);
        
        camera.GetComponent<CameraMovement>().shake = false;
    }

    public void Super()
    {
        if (isAlive && canSuper && !isSupering && !isSpecialing && !isFrozen && !isFlinching && !isDodging && !isBubbled)
        {
            canSuper = false;
            superMeter = 0.0f;
            isSupering = true;
            combo = 4;

            var spark = Instantiate(superSparkle, transform.position + new Vector3(0.0f, 0.3f, 0.0f), Quaternion.identity, transform);

            if (cat == Cat.ANGUS)
            {
                canMove = false;
                canJump = false;
                isInvulnerable = true;
                StartCoroutine(StaredownSuper(nyanSlashStartup, nyanSlashRecovery));
            }
            if (cat == Cat.BAILEY)
            {
                canMove = false;
                canJump = false;
                isInvulnerable = true;
                StartCoroutine(PayDaySuper(payDayStartup, payDayDuration));
            }
            if (cat == Cat.MIA)
            {
                canMove = false;
                canJump = false;
                isInvulnerable = true;
                StartCoroutine(NyanSlashSuper(nyanSlashStartup, nyanSlashDuration, nyanSlashRecovery));
            }
            if (cat == Cat.TITAN)
            {
                canMove = false;
                canJump = false;
                isInvulnerable = true;
                StartCoroutine(MegaSlamSuper(megaSlamStartup, megaSlamVelocity, megaSlamRecovery));
            }
        }
    }

    IEnumerator StaredownSuper(float startup, float recovery)
    {
        float counter = 0.0f;
        yield return new WaitForSeconds(0.4f);

        while (gameObject.transform.localScale.y < 4.0f)
        {
            transform.localScale = transform.localScale + Vector3.one * staredownGrowthRate * Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        staredowning = true;

        currentAttack = (GameObject)Instantiate(staredownSuper);
        currentAttack.GetComponent<BoxCollider>().enabled = false;
        currentAttack.transform.parent = gameObject.transform;
        currentAttack.tag = "PlayerAttack";
        currentAttack.transform.position = new Vector3(gameObject.transform.position.x + (facingRight ? 1.0f : -1.0f), 0.0f, gameObject.transform.position.z);

        while (currentAttack.transform.localScale.y < 2.5f)
        {
            currentAttack.transform.localScale = currentAttack.transform.localScale + Vector3.one * staredownGrowthRate * Time.deltaTime;
            currentAttack.transform.position = currentAttack.transform.position + Vector3.up * 3.0f * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        staredowning = false;
        currentAttack.GetComponent<PlayerAttacks>().PlayCastSound();
        yield return new WaitForSeconds(0.5f);

        if (!facingRight)
        {
            while (counter < 0.5f)
            {
                counter += Time.deltaTime;
                float NewRotation = (300.0f * Time.deltaTime) + currentAttack.transform.rotation.eulerAngles.z;
                NewRotation = NewRotation % 360.0f;

                var rot = gameObject.transform.rotation.eulerAngles;
                rot.Set(0.0f, 0.0f, NewRotation);
                currentAttack.transform.rotation = Quaternion.Euler(rot);
                yield return new WaitForFixedUpdate();
            }
            currentAttack.GetComponent<BoxCollider>().enabled = true;
            currentAttack.GetComponent<PlayerAttacks>().PlayExtraSound();
            camera.GetComponent<CameraMovement>().shake = true;
            counter = 0.0f;
            while (counter < 0.3f)
            {
                counter += Time.deltaTime;
                float NewRotation = (-300.0f * Time.deltaTime) + currentAttack.transform.rotation.eulerAngles.z;
                NewRotation = NewRotation % 360.0f;

                var rot = gameObject.transform.rotation.eulerAngles;
                rot.Set(0.0f, 0.0f, NewRotation);
                currentAttack.transform.rotation = Quaternion.Euler(rot);
                yield return new WaitForFixedUpdate();
            }
            currentAttack.GetComponent<BoxCollider>().enabled = false;
            camera.GetComponent<CameraMovement>().shake = false;
            counter = 0.0f;
            while (counter < 0.3f)
            {
                counter += Time.deltaTime;
                float NewRotation = (300.0f * Time.deltaTime) + currentAttack.transform.rotation.eulerAngles.z;
                NewRotation = NewRotation % 360.0f;

                var rot = gameObject.transform.rotation.eulerAngles;
                rot.Set(0.0f, 0.0f, NewRotation);
                currentAttack.transform.rotation = Quaternion.Euler(rot);
                yield return new WaitForFixedUpdate();
            }
            currentAttack.GetComponent<BoxCollider>().enabled = true;
            currentAttack.GetComponent<PlayerAttacks>().PlayExtraSound();
            camera.GetComponent<CameraMovement>().shake = true;
            counter = 0.0f;
            while (counter < 0.3f)
            {
                counter += Time.deltaTime;
                float NewRotation = (-300.0f * Time.deltaTime) + currentAttack.transform.rotation.eulerAngles.z;
                NewRotation = NewRotation % 360.0f;

                var rot = gameObject.transform.rotation.eulerAngles;
                rot.Set(0.0f, 0.0f, NewRotation);
                currentAttack.transform.rotation = Quaternion.Euler(rot);
                yield return new WaitForFixedUpdate();
            }
            currentAttack.GetComponent<BoxCollider>().enabled = false;
            camera.GetComponent<CameraMovement>().shake = false;
            counter = 0.0f;
            while (counter < 0.3f)
            {
                counter += Time.deltaTime;
                float NewRotation = (300.0f * Time.deltaTime) + currentAttack.transform.rotation.eulerAngles.z;
                NewRotation = NewRotation % 360.0f;

                var rot = gameObject.transform.rotation.eulerAngles;
                rot.Set(0.0f, 0.0f, NewRotation);
                currentAttack.transform.rotation = Quaternion.Euler(rot);
                yield return new WaitForFixedUpdate();
            }
            currentAttack.GetComponent<BoxCollider>().enabled = true;
            currentAttack.GetComponent<PlayerAttacks>().PlayExtraSound();
            camera.GetComponent<CameraMovement>().shake = true;
        }
        else
        {
            while (counter < 0.5f)
            {
                counter += Time.deltaTime;
                float NewRotation = (-300.0f * Time.deltaTime) + currentAttack.transform.rotation.eulerAngles.z;
                NewRotation = NewRotation % 360.0f;

                var rot = gameObject.transform.rotation.eulerAngles;
                rot.Set(0.0f, 0.0f, NewRotation);
                currentAttack.transform.rotation = Quaternion.Euler(rot);
                yield return new WaitForFixedUpdate();
            }
            currentAttack.GetComponent<BoxCollider>().enabled = true;
            currentAttack.GetComponent<PlayerAttacks>().PlayExtraSound();
            camera.GetComponent<CameraMovement>().shake = true;
            counter = 0.0f;
            while (counter < 0.3f)
            {
                counter += Time.deltaTime;
                float NewRotation = (300.0f * Time.deltaTime) + currentAttack.transform.rotation.eulerAngles.z;
                NewRotation = NewRotation % 360.0f;

                var rot = gameObject.transform.rotation.eulerAngles;
                rot.Set(0.0f, 0.0f, NewRotation);
                currentAttack.transform.rotation = Quaternion.Euler(rot);
                yield return new WaitForFixedUpdate();
            }
            currentAttack.GetComponent<BoxCollider>().enabled = false;
            camera.GetComponent<CameraMovement>().shake = false;
            counter = 0.0f;
            while (counter < 0.3f)
            {
                counter += Time.deltaTime;
                float NewRotation = (-300.0f * Time.deltaTime) + currentAttack.transform.rotation.eulerAngles.z;
                NewRotation = NewRotation % 360.0f;

                var rot = gameObject.transform.rotation.eulerAngles;
                rot.Set(0.0f, 0.0f, NewRotation);
                currentAttack.transform.rotation = Quaternion.Euler(rot);
                yield return new WaitForFixedUpdate();
            }
            currentAttack.GetComponent<BoxCollider>().enabled = true;
            currentAttack.GetComponent<PlayerAttacks>().PlayExtraSound();
            camera.GetComponent<CameraMovement>().shake = true;
            counter = 0.0f;
            while (counter < 0.3f)
            {
                counter += Time.deltaTime;
                float NewRotation = (300.0f * Time.deltaTime) + currentAttack.transform.rotation.eulerAngles.z;
                NewRotation = NewRotation % 360.0f;

                var rot = gameObject.transform.rotation.eulerAngles;
                rot.Set(0.0f, 0.0f, NewRotation);
                currentAttack.transform.rotation = Quaternion.Euler(rot);
                yield return new WaitForFixedUpdate();
            }
            currentAttack.GetComponent<BoxCollider>().enabled = false;
            camera.GetComponent<CameraMovement>().shake = false;
            counter = 0.0f;
            while (counter < 0.3f)
            {
                counter += Time.deltaTime;
                float NewRotation = (-300.0f * Time.deltaTime) + currentAttack.transform.rotation.eulerAngles.z;
                NewRotation = NewRotation % 360.0f;

                var rot = gameObject.transform.rotation.eulerAngles;
                rot.Set(0.0f, 0.0f, NewRotation);
                currentAttack.transform.rotation = Quaternion.Euler(rot);
                yield return new WaitForFixedUpdate();
            }
            currentAttack.GetComponent<BoxCollider>().enabled = true;
            currentAttack.GetComponent<PlayerAttacks>().PlayExtraSound();
            camera.GetComponent<CameraMovement>().shake = true;
        }

		yield return new WaitForSeconds(recovery);
        camera.GetComponent<CameraMovement>().shake = false;
        Destroy(currentAttack);
        transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
        canMove = true;
        canJump = true;
        isInvulnerable = false;
        combo = 0;
        isSupering = false;
    }

    IEnumerator PayDaySuper(float startup, float duration)
    {
        yield return new WaitForSeconds(0.8f);
        playerAnimation.isPayDaying = true;

        yield return new WaitForSeconds(startup);
        canMove = true;
        canJump = true;
        StartCoroutine(playerAnimation.PayDay());
        var super = Instantiate(payDaySuper, transform.position, Quaternion.identity, transform);
        super.GetComponent<PayDayHolster>().playerMovement = this;

        yield return new WaitForSeconds(duration);
        Destroy(super);
        isInvulnerable = false;
        combo = 0;
        isSupering = false;
        playerAnimation.isPayDaying = false;
    }

    IEnumerator NyanSlashSuper(float startup, float duration, float recovery)
    {
        yield return new WaitForSeconds(0.8f);
        yield return new WaitForSeconds(startup);

        var ns = Instantiate(nyanSlashSuper, new Vector3(camera.transform.position.x, 3.2f, 0.0f), Quaternion.identity);
        ns.GetComponent<BoxCollider>().enabled = false;
        ns.GetComponent<PlayerAttacks>().playerMovement = this;

        yield return new WaitForSeconds(duration);

        ns.GetComponent<BoxCollider>().enabled = true;
        var kanji = Instantiate(kanjiEffect, transform.position + Vector3.down * 0.3f, Quaternion.identity, transform);

        yield return new WaitForSeconds(recovery);
        Destroy(kanji);
        canMove = true;
        canJump = true;
        isInvulnerable = false;
        combo = 0;
        isSupering = false;

    }

    IEnumerator MegaSlamSuper(float startup, float velocity, float recovery)
    {
        Vector3 destination = new Vector3(camera.transform.position.x, 13.0f, -1.5f);
        Vector3 direction = destination - gameObject.transform.position;
        float growTime = 0.0f;

        yield return new WaitForSeconds(0.8f);

        rb.useGravity = false;

        while (gameObject.transform.position.y < 13.0f)
        {
            transform.position = transform.position + direction * velocity * Time.deltaTime;


            yield return new WaitForFixedUpdate();
        }

        megaSlamApex = true;
        audioSource.pitch = 0.7f;
        audioSource.PlayOneShot(expandSound, 1.5f);
        while (growTime < startup)
        {
            transform.localScale = transform.localScale + Vector3.one * megaSlamGrowthRate * Time.deltaTime;
            shadow.localScale = shadow.localScale + Vector3.right * megaSlamShadowRate * Time.deltaTime;

            growTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        megaSlaming = true;
        rb.useGravity = true;
        gameObject.layer = 8;
        yield return new WaitForFixedUpdate();
        while (rb.velocity.y != 0.0f)
        {
            yield return new WaitForFixedUpdate();
        }

        audioSource.pitch = 1.0f;
        megaSlaming = false;
        var ms = (GameObject)Instantiate(megaSlamSuper);
        ms.transform.parent = gameObject.transform;
        ms.tag = "PlayerAttack";
        ms.transform.position = new Vector3(gameObject.transform.position.x, 1.0f, gameObject.transform.position.z);
        camera.GetComponent<CameraMovement>().shake = true;

        yield return new WaitForSeconds(recovery);

        gameObject.layer = 6;
        Destroy(ms);
        megaSlaming = false;

        shadow.localScale = new Vector3(0.03f, 0.03f, 1.0f);
        transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
        camera.GetComponent<CameraMovement>().shake = false;
        canMove = true;
        canJump = true;
        combo = 0;
        isSupering = false;
        isInvulnerable = false;
        megaSlamApex = false;
    }

    public IEnumerator TakeHit(bool flinch, bool damaged)
    {
        recentlyDamaged = damaged;
        isFlinching = flinch;
        yield return new WaitForSeconds(0.5f);
        isFlinching = false;
        yield return new WaitForSeconds(1.0f);
        recentlyDamaged = false;
    }

    public IEnumerator TakeFallDamage()
    {
        rb.useGravity = false;
        if (isAlive)
        {
            health -= 20.0f;
        }
        
        if (floatingText)
        {
            CharacterFlyText(20.0f, Color.red);
        }
        if (health <= 0.0f)
        {
            health = 0.0f;
            isAlive = false;
            StartCoroutine(playerAnimation.DeathFlash());
            StartCoroutine(Death());
        }
        else
        {
            StartCoroutine(playerAnimation.HitFlash());
        }

        transform.position = new Vector3(transform.position.x - 5.0f, 1.2f, 0.0f);
        rb.useGravity = true;

        recentlyDamaged = true;
        isFlinching = true;
        yield return new WaitForSeconds(0.5f);
        isFlinching = false;
        yield return new WaitForSeconds(1.0f);
        recentlyDamaged = false;
    }

    public IEnumerator Freeze()
    {
        float timer = 0.0f;

        while (timer < 5.0f)
        {
            timer += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        isFrozen = false;
    }

    public IEnumerator EnterPortal(Vector3 location)
    {
        float counter = 0.0f;
        float counterMax = 1.5f;

        StartCoroutine(playerAnimation.EnterPortalAnimation());
        rb.useGravity = false;
        canMove = false;
        combo = 4;
        Vector3 direction = (location - transform.position).normalized;
        while (counter < counterMax)
        {
            direction = location - transform.position;
            transform.position = transform.position + direction * Vector3.Distance(location, transform.position) * Time.deltaTime;

            counter += Time.deltaTime;
            yield return new WaitForFixedUpdate();
		}

        rb.useGravity = true;
        canMove = true;
        combo = 0;
	}


    public void CharacterFlyText(float damage, Color color)
    {
        var go = Instantiate(floatingText, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = damage.ToString();
        go.GetComponent<TextMesh>().color = color;
    }

    IEnumerator Death()
    {
        if (lm.livesRemaining == 0)
        {
            StartCoroutine(lm.AllLivesLost());
		}
        yield return new WaitForSeconds(3.0f);
        canRevive = true;
	}
}
