using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
	public enum Phase
	{
		INTERMISSION,
		PHASE1,
		PHASE2,
		PHASE3
	};

	[Header("GameObjects")]
	public GameObject iceSpell;
	public GameObject swordAttack;
	public GameObject fireball;
	public GameObject teleportStart;
	public GameObject teleportEnd;
	public GameObject sparkle;
	public GameObject roar;
	public GameObject bubPrefab;
	public GameObject bobPrefab;
	public GameObject hitSparkParticleEffect;
	public GameObject floatingText;
	public GameObject warning;
	public GameObject warningSword;
	public GameObject warningShield;
	public GameObject warningFeral;
	public GameObject afterImage;
	public GameObject phaseShift;
	public GameObject shieldEffect;
	private GameObject hitSpark;

	[Header("Components")]
	public LevelManagement levelManagement;
	public SubspaceScript subspaceScript;
	public Rigidbody rb;
	public BoxCollider bc;
	public EnemyAnimation enemyAnimation;
	public ShadowAnimation shadowAnimation;
	public Camera camera;
	public FireballHolsterController fireballHolster;
	public LightSwordArrayController lightSwordArray;
	public AudioSource audioSource;
	public AudioClip dashSound;
	public AudioClip jumpSound;
	public AudioClip teleportSound;
	public AudioClip morphSound;
	public AudioClip superSound;
	public AudioClip castSound;
	public AudioClip deathSound;
	public AudioClip shieldHitSound;
	public BossHealthBar floatingHealthBar;
	public Transform targetedPlayer;
	public Transform yuna;
	public RaycastHit hitGround;

	[Header("Vectors")]
	public Vector3 startPos;
	public Vector3 moveDirection;
	public Vector3 approachPosition;

	[Header("Attributes")]
	public Phase phase;
	public float health;
	public float maxHealth;
	public float kingSpeed;
	public float bossSpeed;
	public float bossAcceleration;
	public float cutsceneSpeed;
	public float driftSpeed;
	public float jumpHeight;
	public float floatCounter;
	public float floatLoopTime;
	public float floatSpeed;
	public float distanceToGround;
	public float verticalVeloctiy;
	public float distanceToPlayer1;
	public float distanceToPlayer2;
	public float distanceToTarget;

	[Header("Timers")]
	public float cooldownCounter;
	public float cooldownMax;
	public float afterImageCounter;
	public float afterImageLoop;

	[Header("Booleans")]
	public bool isMoving;
	public bool isApproaching;
	public bool isJumping;
	public bool isAttacking;
	public bool isCasting;
	public bool isHit;
	public bool isWarning;
	public bool isFloating;
	public bool isDriftingRight;
	public bool isEngaged;
	public bool isTeleporting;
	public bool isIntangible;
	public bool isInvulnerable;
	public bool isAlive;
	public bool isCutscene;
	public bool isMorphing;
	public bool inRoutine;
	public bool inSubRoutine;
	public bool onCooldown;
	public bool facingRight;
	public bool endingPhase2;
	public bool configuredPhase3;

	void Start()
    {
		levelManagement = FindObjectOfType<LevelManagement>();
		subspaceScript = FindObjectOfType<SubspaceScript>();
		rb = GetComponent<Rigidbody>();
		bc = GetComponent<BoxCollider>();
		enemyAnimation = transform.Find("Sprite").GetComponent<EnemyAnimation>();
		shadowAnimation = transform.Find("Shadow").GetComponent<ShadowAnimation>();
		audioSource = GetComponent<AudioSource>();
		camera = FindObjectOfType<Camera>();
		fireballHolster = transform.Find("FireballHolster").GetComponent<FireballHolsterController>();
		lightSwordArray = GameObject.Find("LightSwordArray").GetComponent<LightSwordArrayController>();
		floatingHealthBar = transform.Find("BossHealthBar").GetComponent<BossHealthBar>();
		startPos = transform.position;
		jumpHeight = 7.0f;
		verticalVeloctiy = 5.0f;
		kingSpeed = 7.5f;
		bossSpeed = 5.0f;
		bossAcceleration = 4.0f;
		cutsceneSpeed = 4.5f;
		floatSpeed = 0.2f;
		floatCounter = -0.15f;
		floatLoopTime = 1.0f;
		driftSpeed = 0.0f;
		cooldownMax = 2.5f;
		afterImageLoop = 0.5f;
		isHit = false;
		isWarning = false;
		isAlive = true;
		onCooldown = true;
		phase = Phase.INTERMISSION;
	}

    // Update is called once per frame
    void Update()
    {
		if (!yuna)
		{
			yuna = GameObject.Find("Yuna").transform;
		}
		if (!lightSwordArray && GameObject.Find("LightSwordArray"))
		{
			lightSwordArray = GameObject.Find("LightSwordArray").GetComponent<LightSwordArrayController>();
		}

		CheckFacing();
		CheckGrounded();
		CheckHealth();
		CheckFloating();
		CheckDriftingRight();
		CheckEndingPhase2();
		CheckStartPhase3();
		CheckAfterImage();
		distanceToGround = RaycastDown();
		if ((levelManagement.player1 && levelManagement.player1.GetComponent<PlayerMovement>().isAlive) || (levelManagement.player2 && levelManagement.player2.GetComponent<PlayerMovement>().isAlive))
		{
			if (isAlive && isEngaged)
			{
				if (targetedPlayer)
				{
					distanceToTarget = Vector3.Distance(transform.position, targetedPlayer.position);
				}

				Fight();
			}
		}
	}

	public void CheckFacing()
	{
		if (!isApproaching && !isCasting && !isCutscene)
		{
			if (moveDirection.x > 0.0f)
			{
				facingRight = true;
			}

			if (moveDirection.x < 0.0f)
			{
				facingRight = false;
			}
		}
	}

	public void CheckHealth()
	{
		if (phase == Phase.PHASE1 && health < maxHealth * 0.7f)
		{
			health = maxHealth * 0.7f;
			phase = Phase.INTERMISSION;
			isInvulnerable = true;
		}
		if (phase == Phase.PHASE2 && health < maxHealth * 0.35f)
		{
			health = maxHealth * 0.35f;
			isInvulnerable = true;
		}
	}

	public void CheckFloating()
	{
		if (isFloating)
		{
			if (floatCounter < floatLoopTime / 2.0f)
			{
				transform.localPosition = transform.localPosition + Vector3.up * floatSpeed * Time.deltaTime;
			}
			else if (floatCounter < floatLoopTime)
			{
				transform.localPosition = transform.localPosition + Vector3.down * floatSpeed * Time.deltaTime;
			}
			else
			{
				floatCounter = 0.0f;
			}

			floatCounter += Time.deltaTime;
		}
	}

	public void CheckDriftingRight()
	{
		if (isDriftingRight)
		{
			transform.position = transform.position + Vector3.right * driftSpeed * Time.deltaTime;
		}
	}

	public void CheckEndingPhase2()
	{
		if (transform.position.x >= 253.8f && !endingPhase2)
		{
			endingPhase2 = true;
			StartCoroutine(EndPhase2());
		}
	}

	public void CheckStartPhase3()
	{
		if (phase == Phase.PHASE3 && camera.GetComponent<CameraMovement>().lockOn && !configuredPhase3)
		{
			configuredPhase3 = true;
			lightSwordArray.transform.position = new Vector3(camera.GetComponent<CameraMovement>().lockOn.transform.position.x, 0.0f, 0.0f);
			levelManagement.spawnTarget = new Vector3(camera.GetComponent<CameraMovement>().lockOn.transform.position.x, 0.0f, 0.0f);
			enemyAnimation.animationIndex = 4;
		}
	}

	public void CheckAfterImage()
	{
		if (phase == Phase.PHASE3 && isTeleporting)
		{
			if (afterImageCounter >= afterImageLoop)
			{
				var img = Instantiate(afterImage, transform.position, Quaternion.identity);
				img.transform.localScale = transform.localScale;
				afterImageCounter = 0.0f;
			}

			afterImageCounter += Time.deltaTime;
		}
		else
		{
			afterImageCounter = 0.0f;
		}
	}

	public float RaycastDown()
	{
		float distance = 10.0f;

		if (Physics.Raycast(transform.position, -Vector3.up, out hitGround))
		{
			distance = hitGround.distance;
			Debug.DrawLine(transform.position, hitGround.point, Color.cyan);
		}

		return distance;
	}

	public void Jump()
	{
		if (isAlive && !isAttacking && !isJumping)
		{
			isJumping = true;
			rb.velocity = (Vector3.up * jumpHeight);
			//audioSource.PlayOneShot(jumpSound, 0.3f);
		}
	}

	public void CheckGrounded()
	{
		if (distanceToGround <= 1.0f && rb.velocity.y <= 0.0f)
		{
			isJumping = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("Player") && isAlive)
		{
			if (!isEngaged)
			{
				if (subspaceScript)
				{
					subspaceScript.Phase1BGM();
				}
				phase = Phase.PHASE1;
				isEngaged = true;
				
				targetedPlayer = other.transform;
				var cameraTarget = Instantiate(levelManagement.cameraLockTarget, transform.position, Quaternion.identity);
				camera.GetComponent<CameraMovement>().lockedOn = true;
				camera.GetComponent<CameraMovement>().lockOn = cameraTarget.transform;
			}
		}
		if ((other.transform.CompareTag("PlayerAttack") || other.transform.CompareTag("PlayerProjectile")) && isAlive && !isIntangible)
		{
			PlayerAttacks attack = other.GetComponent<PlayerAttacks>();
			hitSpark = (GameObject)Instantiate(hitSparkParticleEffect);
			hitSpark.tag = "Effect";
			hitSpark.transform.position = gameObject.transform.position;

			isHit = true;

			if (!attack.playerMovement.isSupering)
			{
				attack.playerMovement.ActiveCharge();
			}

			if (isInvulnerable)
			{
				audioSource.PlayOneShot(shieldHitSound, 1.0f);
			}
			if (!isInvulnerable)
			{
				attack.PlayHitSound();
				float damageTaken = attack.damage * attack.playerMovement.damageModifier;

				health -= damageTaken;

				if (health <= 0.0f)
				{
					health = 0.0f;
					isAlive = false;
					StartCoroutine(enemyAnimation.DeathFlash());
					StartCoroutine(shadowAnimation.DeathFlash());
					StartCoroutine(Death());
				}

				if (floatingHealthBar)
				{
					floatingHealthBar.UpdateHealthBar();
				}

				if (floatingText)
				{
					DamageFlyText(damageTaken, attack.playerMovement.color);
				}
			}
		}
	}

	public void Fight()
	{
		if (onCooldown)
		{
			cooldownCounter += Time.deltaTime;
		}

		// Phase 1
		if (phase == Phase.PHASE1)
		{
			if (cooldownCounter >= cooldownMax)
			{
				onCooldown = false;
				cooldownCounter = 0.0f;

				StartCoroutine(Phase1Routine());
			}
		}
		// Phase 2
		if (phase == Phase.PHASE2)
		{
			if (cooldownCounter >= cooldownMax)
			{
				onCooldown = false;
				cooldownCounter = 0.0f;

				StartCoroutine(Phase2Routine());
			}
		}
		// Phase 3
		if (phase == Phase.PHASE3)
		{
			if (cooldownCounter >= cooldownMax && isAlive)
			{
				onCooldown = false;
				cooldownCounter = 0.0f;

				StartCoroutine(Phase3Routine());
			}
		}
		// Intermission
		if (phase == Phase.INTERMISSION)
		{
			isIntangible = true;

			if (!isCutscene && !inRoutine && !endingPhase2)
			{
				isCutscene = true;
				StartCoroutine(StartIntermission1());
			}
			if (!isCutscene && !inRoutine && endingPhase2)
			{
				isCutscene = true;
				StartCoroutine(StartIntermission2());
			}
		}
	}

	public IEnumerator Phase1Routine()
	{
		inRoutine = true;
		gameObject.layer = 16;
		// Teleport to random arena location
		Vector3 teleportDestination = SetTeleportDestination();

		isTeleporting = true;
		isIntangible = true;
		var tele1 = Instantiate(teleportStart, transform.position, Quaternion.identity, transform);
		StartCoroutine(enemyAnimation.TeleportFadeOut());
		audioSource.PlayOneShot(teleportSound, 0.5f);

		yield return new WaitForSeconds(1.0f);

		transform.position = teleportDestination;
		var tele2 = Instantiate(teleportEnd, transform.position, Quaternion.identity, transform);
		StartCoroutine(enemyAnimation.TeleportFadeIn());
		audioSource.PlayOneShot(teleportSound, 0.5f);
		facingRight = transform.position.x > camera.transform.position.x ? false : true;

		yield return new WaitForSeconds(0.8f);

		isTeleporting = false;
		isIntangible = false;

		// Cast ice spell at alive players
		if (levelManagement.player1 && levelManagement.player1.GetComponent<PlayerMovement>().isAlive)
		{
			isCasting = true;			
			StartCoroutine(CastIceSpell(levelManagement.player1));
			yield return new WaitForSeconds(1.0f);
		}
		if (levelManagement.player2 && levelManagement.player2.GetComponent<PlayerMovement>().isAlive)
		{
			isCasting = true;			
			StartCoroutine(CastIceSpell(levelManagement.player2));
			yield return new WaitForSeconds(1.0f);
		}

		yield return new WaitForSeconds(1.0f);

		// Cast sword attack at alive players
		if (levelManagement.player1 && levelManagement.player1.GetComponent<PlayerMovement>().isAlive)
		{
			isApproaching = true;
			StartCoroutine(CastSwordAttack(levelManagement.player1));
			while (isApproaching)
			{
				yield return new WaitForFixedUpdate();
			}
		}
		if (levelManagement.player2 && levelManagement.player2.GetComponent<PlayerMovement>().isAlive)
		{
			isApproaching = true;
			StartCoroutine(CastSwordAttack(levelManagement.player2));
			while (isApproaching)
			{
				yield return new WaitForFixedUpdate();
			}
		}

		inRoutine = false;
		onCooldown = true;
	}

	public IEnumerator Phase2Routine()
	{
		inRoutine = true;
		isCasting = true;
		var spark = Instantiate(sparkle, transform.position + new Vector3(0.0f, 0.3f, 0.0f), Quaternion.identity, transform);
		audioSource.PlayOneShot(castSound, 1.2f);

		yield return new WaitForSeconds(1.5f);

		fireballHolster.isFiring = true;
		StartCoroutine(fireballHolster.FireProjectiles());

		while (fireballHolster.isFiring)
		{
			yield return new WaitForFixedUpdate();
		}

		isCasting = false;
		Destroy(spark);

		yield return new WaitForSeconds(0.5f);

		inRoutine = false;
		onCooldown = true;
	}

	public IEnumerator EndPhase2()
	{
		while (driftSpeed > 0.0f)
		{
			driftSpeed -= Time.deltaTime;
			levelManagement.driftSpeed = driftSpeed;
			yield return new WaitForFixedUpdate();
		}

		driftSpeed = 0.0f;
		isDriftingRight = false;
		levelManagement.driftSpeed = driftSpeed;
		rb.velocity = Vector3.zero;

		phase = Phase.INTERMISSION;
	}

	public IEnumerator Phase3Routine()
	{
		float counter = 0.0f;
		float currentSpeed = 0.0f;
		int pattern1 = UnityEngine.Random.Range(0, 2);
		int pattern2 = UnityEngine.Random.Range(0, 2);
		int cycle = 0;
		
		// Teleport to random side
		Vector3 destination1 = UnityEngine.Random.Range(0, 2) == 0 ? new Vector3(camera.transform.position.x - 6.0f, 1.0f, 0.0f) : new Vector3(camera.transform.position.x + 6.0f, 1.0f, 0.0f);
		Vector3 destination2 = Vector3.zero;
		Vector3 direction = (destination1 - transform.position).normalized;
		isTeleporting = true;
		var ps1 = Instantiate(phaseShift, transform.position, Quaternion.identity, transform);

		while (Vector3.Distance(transform.position, destination1) > 0.1f)
		{
			direction = (destination1 - transform.position).normalized;
			transform.position = transform.position + direction * Mathf.Clamp(currentSpeed, 0.0f, bossSpeed) * Time.deltaTime;
			currentSpeed += Time.deltaTime * bossAcceleration;

			yield return new WaitForFixedUpdate();
		}

		isTeleporting = false;
		Destroy(ps1, 0.2f);
		facingRight = transform.position.x < camera.transform.position.x ? true : false;
		inSubRoutine = true;

		// Begin first attack of pattern 1
		if (pattern1 == 0)
		{
			StartCoroutine(SubroutineLightSwords());
		}
		if (pattern1 == 1)
		{
			StartCoroutine(SubroutineSpawnFerals());
		}

		while (inSubRoutine)
		{
			yield return new WaitForFixedUpdate();
		}

		// Teleport to center and throw fireballs
		isTeleporting = true;

		int roll = UnityEngine.Random.Range(0, 2);
		destination1 = roll == 0 ? new Vector3(camera.transform.position.x , 1.0f, 3.0f) : new Vector3(camera.transform.position.x, 1.0f, -3.0f);
		destination2 = roll == 0 ? new Vector3(camera.transform.position.x , 1.0f, -3.0f) : new Vector3(camera.transform.position.x, 1.0f, 3.0f);
		direction = (destination1 - transform.position).normalized;
		isTeleporting = true;
		var ps2 = Instantiate(phaseShift, transform.position, Quaternion.identity, transform);

		while (Vector3.Distance(transform.position, destination1) > 0.1f)
		{
			direction = (destination1 - transform.position).normalized;
			transform.position = transform.position + direction * Mathf.Clamp(currentSpeed, 0.0f, bossSpeed) * Time.deltaTime;
			currentSpeed += Time.deltaTime * bossAcceleration;
			counter += Time.deltaTime;

			yield return new WaitForFixedUpdate();
		}

		while (Vector3.Distance(transform.position, destination2) > 0.1f)
		{
			direction = (destination2 - transform.position).normalized;
			transform.position = transform.position + direction * Mathf.Clamp(currentSpeed, 0.0f, bossSpeed) * Time.deltaTime;
			currentSpeed += Time.deltaTime * bossAcceleration;
			counter += Time.deltaTime;

			if (counter > 0.3f)
			{
				counter = 0.0f;
				if (levelManagement.player1 && cycle % 2 == 0)
				{
					var fb = Instantiate(fireball, transform.position, Quaternion.identity);
					fb.GetComponent<EnemyAttacks>().direction = (levelManagement.player1.position - transform.position).normalized;
					fb.GetComponent<EnemyAttacks>().speed = 5.0f;
				}
				if (levelManagement.player2 && cycle % 2 == 1)
				{
					var fb = Instantiate(fireball, transform.position, Quaternion.identity);
					fb.GetComponent<EnemyAttacks>().direction = (levelManagement.player2.position - transform.position).normalized;
					fb.GetComponent<EnemyAttacks>().speed = 5.0f;
				}

				cycle++;
			}

			yield return new WaitForFixedUpdate();
		}

		isTeleporting = false;
		currentSpeed = 0.0f;
		Destroy(ps2, 0.2f);
		facingRight = transform.position.x < camera.transform.position.x ? true : false;
		inSubRoutine = true;

		// Begin first attack of pattern 2
		if (pattern2 == 0)
		{
			StartCoroutine(SubroutineMeleeRush());
		}
		if (pattern2 == 1)
		{
			StartCoroutine(SubroutineCastShield());
		}

		while (inSubRoutine)
		{
			yield return new WaitForFixedUpdate();
		}

		// Teleport to random side
		destination1 = UnityEngine.Random.Range(0, 2) == 0 ? new Vector3(camera.transform.position.x - 6.0f, 1.0f, 0.0f) : new Vector3(camera.transform.position.x + 6.0f, 1.0f, 0.0f);
		direction = (destination1 - transform.position).normalized;
		isTeleporting = true;
		var ps3 = Instantiate(phaseShift, transform.position, Quaternion.identity, transform);

		while (Vector3.Distance(transform.position, destination1) > 0.1f)
		{
			direction = (destination1 - transform.position).normalized;
			transform.position = transform.position + direction * Mathf.Clamp(currentSpeed, 0.0f, bossSpeed) * Time.deltaTime;
			currentSpeed += Time.deltaTime * bossAcceleration;

			yield return new WaitForFixedUpdate();
		}

		isTeleporting = false;
		Destroy(ps3, 0.2f);
		facingRight = transform.position.x < camera.transform.position.x ? true : false;
		inSubRoutine = true;

		// Begin second attack of pattern 1
		if (pattern1 == 0)
		{
			StartCoroutine(SubroutineSpawnFerals());
		}
		if (pattern1 == 1)
		{
			StartCoroutine(SubroutineLightSwords());
		}

		while (inSubRoutine)
		{
			yield return new WaitForFixedUpdate();
		}

		// Teleport to center
		destination1 = new Vector3(camera.transform.position.x, 1.0f, 0.0f);
		direction = (destination1 - transform.position).normalized;
		isTeleporting = true;
		var ps4 = Instantiate(phaseShift, transform.position, Quaternion.identity, transform);

		while (Vector3.Distance(transform.position, destination1) > 0.1f)
		{
			direction = (destination1 - transform.position).normalized;
			transform.position = transform.position + direction * Mathf.Clamp(currentSpeed, 0.0f, bossSpeed) * Time.deltaTime;
			currentSpeed += Time.deltaTime * bossAcceleration;
			counter += Time.deltaTime;

			yield return new WaitForFixedUpdate();
		}

		isTeleporting = false;
		currentSpeed = 0.0f;
		Destroy(ps4, 0.2f);
		facingRight = transform.position.x < camera.transform.position.x ? true : false;
		inSubRoutine = true;

		// Begin second attack of pattern 2
		if (pattern2 == 0)
		{
			StartCoroutine(SubroutineCastShield());
		}
		if (pattern2 == 1)
		{
			StartCoroutine(SubroutineMeleeRush());
		}

		while (inSubRoutine)
		{
			yield return new WaitForFixedUpdate();
		}

		isTeleporting = false;
		inRoutine = false;
		onCooldown = true;
	}

	public IEnumerator CastIceSpell(Transform targetPlayer)
	{
		facingRight = transform.position.x > targetPlayer.position.x ? false : true;

		var spell = Instantiate(iceSpell, targetPlayer.position + Vector3.up * 5.0f, Quaternion.identity);
		spell.GetComponent<IceSpellController>().target = targetPlayer;

		yield return new WaitForSeconds(0.8f);

		isCasting = false;
	}

	public IEnumerator CastSwordAttack(Transform targetPlayer)
	{
		float counter = 0.0f;
		float max = 1.2f;
		Vector3 destination = transform.position.x > targetPlayer.position.x ? targetPlayer.position + Vector3.right * 3.5f : targetPlayer.position + Vector3.left * 3.5f;
		float distance = Vector3.Distance(transform.position, destination);
		facingRight = transform.position.x > targetPlayer.position.x ? false : true;

		isMoving = true;

		while (distance > 0.1f && counter < max)
		{
			destination = transform.position.x > targetPlayer.position.x ? targetPlayer.position + Vector3.right * 3.5f : targetPlayer.position + Vector3.left * 3.5f;
			moveDirection = (destination - transform.position).normalized;
			distance = Vector3.Distance(transform.position, destination);
			transform.position = transform.position + moveDirection * kingSpeed * Time.deltaTime;
			counter += Time.deltaTime;

			yield return new WaitForFixedUpdate();
		}

		isMoving = false;
		facingRight = transform.position.x > targetPlayer.position.x ? false : true;

		// Attack warning
		isWarning = true;
		var warn = Instantiate(warning, transform.position + Vector3.up, Quaternion.identity, transform);

		float timer = 0.0f;
		while (timer < 0.6f && isAlive)
		{
			timer += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}

		isWarning = false;
		isAttacking = true;
		Destroy(warn);

		var attack = Instantiate(swordAttack, facingRight ? transform.position + Vector3.up * 1.5f + Vector3.right * 2.0f : transform.position + Vector3.up * 1.5f + Vector3.left * 2.0f, Quaternion.identity, transform);
		if (!facingRight)
		{
			attack.GetComponent<EnemyAttacks>().direction = Vector3.left;
			attack.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
		}
		else
		{
			attack.GetComponent<EnemyAttacks>().direction = Vector3.right;
		}

		StartCoroutine(AttackShift(8.0f, 0.2f, facingRight ? Vector3.right : Vector3.left));

		yield return new WaitForSeconds(0.8f);

		attack.GetComponent<BoxCollider>().enabled = false;
		attack.GetComponent<SpriteRenderer>().enabled = false;
		Destroy(attack, 1.0f);
		isApproaching = false;
		isAttacking = false;
	}

	public void CastFireball(Vector3 position)
	{
		var fb = Instantiate(fireball, transform.position, Quaternion.identity);
		fb.GetComponent<EnemyAttacks>().direction = (position - transform.position).normalized;
		fb.GetComponent<EnemyAttacks>().speed = 5.0f;
	}

	public IEnumerator SubroutineLightSwords()
	{
		// Attack warning
		isWarning = true;
		var warn = Instantiate(warningSword, transform.position + Vector3.up * 1.2f, Quaternion.identity, transform);

		float timer = 0.0f;
		while (timer < 0.6f && isAlive)
		{
			timer += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}

		isWarning = false;
		isAttacking = true;
		Destroy(warn);

		lightSwordArray.transform.rotation = facingRight ? Quaternion.Euler(0.0f, 0.0f, 0.0f) : Quaternion.Euler(0.0f, 180.0f, 0.0f);
		StartCoroutine(lightSwordArray.FireProjectiles());

		yield return new WaitForSeconds(2.5f);

		isAttacking = false;
		inSubRoutine = false;
	}

	public IEnumerator SubroutineSpawnFerals()
	{
		// Attack warning
		isWarning = true;
		var warn = Instantiate(warningFeral, transform.position + Vector3.up * 1.2f, Quaternion.identity, transform);

		float timer = 0.0f;
		while (timer < 0.6f && isAlive)
		{
			timer += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}

		isWarning = false;
		isAttacking = true;
		Destroy(warn);

		var r = Instantiate(roar, transform.position, Quaternion.identity);
		StartCoroutine(levelManagement.SpawnFerals());

		yield return new WaitForSeconds(2.5f);

		isAttacking = false;
		inSubRoutine = false;
	}

	public IEnumerator SubroutineMeleeRush()
	{
		int attacks = 0;
		int roll = 0;
		if (levelManagement.activePlayers <= 1)
		{
			attacks = 3;
			for (int i = 0; i < attacks; i++)
			{
				isApproaching = true;
				StartCoroutine(CastSwordAttack(levelManagement.player1));

				while (isApproaching)
				{
					yield return new WaitForFixedUpdate();
				}

				yield return new WaitForSeconds(1.0f);
			}
		}
		else if (levelManagement.activePlayers == 2)
		{
			attacks = 5;
			for (int i = 0; i < attacks; i++)
			{
				roll = UnityEngine.Random.Range(0, 2);
				isApproaching = true;
				StartCoroutine(CastSwordAttack(roll == 0 ? levelManagement.player1 : levelManagement.player2));

				while (isApproaching)
				{
					yield return new WaitForFixedUpdate();
				}

				yield return new WaitForSeconds(1.0f);
			}
		}

		inSubRoutine = false;
	}

	public IEnumerator SubroutineCastShield()
	{
		// Attack warning
		isWarning = true;
		var warn = Instantiate(warningShield, transform.position + Vector3.up * 1.2f, Quaternion.identity, transform);

		float timer = 0.0f;
		while (timer < 0.6f && isAlive)
		{
			timer += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}

		isWarning = false;
		isCasting = true;
		Destroy(warn);
		StartCoroutine(CastShield());
		
		yield return new WaitForSeconds(1.5f);

		isCasting = false;

		yield return new WaitForSeconds(1.0f);

		if (levelManagement.player1 && levelManagement.player1.GetComponent<PlayerMovement>().isAlive)
		{
			isCasting = true;
			StartCoroutine(CastIceSpell(levelManagement.player1));
			yield return new WaitForSeconds(1.0f);
		}
		if (levelManagement.player2 && levelManagement.player2.GetComponent<PlayerMovement>().isAlive)
		{
			isCasting = true;
			StartCoroutine(CastIceSpell(levelManagement.player2));
			yield return new WaitForSeconds(1.0f);
		}

		yield return new WaitForSeconds(1.0f);

		inSubRoutine = false;
	}

	public IEnumerator AttackShift(float shift, float time, Vector3 direction)
	{
		rb.velocity = direction * shift;
		yield return new WaitForSeconds(time);
		rb.velocity = Vector3.zero;
	}

	public IEnumerator CastShield()
	{
		var shield = Instantiate(shieldEffect, transform.position, Quaternion.identity, transform);
		isInvulnerable = true;

		yield return new WaitForSeconds(5.0f);

		Destroy(shield);
		isInvulnerable = false;
	}

	public Vector3 SetTeleportDestination()
	{
		int roll = Random.Range(0, 6);
		Vector3 toReturn = Vector3.zero;

		switch (roll)
		{
			case 0:
				toReturn = new Vector3(camera.transform.position.x - 6.0f, 1.0f, -3.0f);
				break;
			case 1:
				toReturn = new Vector3(camera.transform.position.x - 6.0f, 1.0f, 0.0f);
				break;
			case 2:
				toReturn = new Vector3(camera.transform.position.x - 6.0f, 1.0f, 3.0f);
				break;
			case 3:
				toReturn = new Vector3(camera.transform.position.x + 6.0f, 1.0f, -3.0f);
				break;
			case 4:
				toReturn = new Vector3(camera.transform.position.x + 6.0f, 1.0f, 0.0f);
				break;
			case 5:
				toReturn = new Vector3(camera.transform.position.x + 6.0f, 1.0f, 3.0f);
				break;
		}

		return toReturn;
	}

	public IEnumerator Death()
	{
		AudioManager.Instance.SlowFade();
		levelManagement.runGameOver = true;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		yield return new WaitForSeconds(2.2f);

		for (int i = 0; i < enemies.Length; i++)
		{
			Destroy(enemies[i]);
			yield return new WaitForFixedUpdate();
		}
		Destroy(gameObject);
	}

	public IEnumerator StartIntermission1()
	{
		bool apex = false;
		float ascendSpeed = 1.0f;
		float counter = 0.0f;
		float counterMax = 0.0f;

		StartCoroutine(AudioManager.Instance.SlowFade());
		gameObject.layer = 15;
		Vector3 destination = yuna.position + Vector3.left + Vector3.back * 2.0f;
		Vector3 direction = (destination - transform.position).normalized;
		facingRight = transform.position.x > destination.x ? false : true;
		isMoving = true;

		while (Vector3.Distance(destination, transform.position) > 0.1f)
		{
			direction = (destination - transform.position).normalized;
			transform.position = transform.position + direction * cutsceneSpeed * Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}

		isMoving = false;
		facingRight = true;

		yield return new WaitForSeconds(0.4f);

		isCasting = true;
		audioSource.PlayOneShot(teleportSound, 0.5f);
		
		yuna.GetComponent<Rigidbody>().velocity = new Vector3(5.0f, 5.0f, 0.0f);
		StartCoroutine(yuna.GetComponent<YunaController>().Spin());

		yield return new WaitForSeconds(0.5f);

		isCasting = false;
		rb.useGravity = false;

		yield return new WaitForSeconds(0.5f);

		isTeleporting = true;
		var spark = Instantiate(sparkle, transform.position + new Vector3(0.0f, 0.3f, 0.0f), Quaternion.identity, transform);
		audioSource.PlayOneShot(superSound, 0.5f);

		yield return new WaitForSeconds(0.4f);

		while (!apex)
		{
			if (gameObject.transform.position.y < 3.5f && !apex)
			{
				transform.position = transform.position + Vector3.up * ascendSpeed * Time.deltaTime;
			}
			else
			{
				apex = true;
			}
			yield return new WaitForFixedUpdate();
		}

		yield return new WaitForSeconds(0.2f);

		if (subspaceScript)
		{
			subspaceScript.Phase2BGM();
		}

		destination = new Vector3(camera.transform.position.x, 3.6f, 1.0f);
		direction = (destination - transform.position).normalized;

		while (Vector3.Distance(destination, transform.position) > 0.01f)
		{
			transform.position = transform.position + direction * ascendSpeed * Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}

		transform.position = destination;
		isFloating = true;
		camera.GetComponent<CameraMovement>().lockOn = transform;
		camera.GetComponent<CameraMovement>().inPosition = false;
		isTeleporting = false;
		Destroy(spark);

		yield return new WaitForSeconds(0.6f);

		isDriftingRight = true;
		counterMax = 2.0f;

		while (counter < counterMax)
		{
			driftSpeed += Time.deltaTime;
			levelManagement.driftSpeed = driftSpeed;
			counter += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}

		if (levelManagement.player1 || targetedPlayer)
		{
			var bub = Instantiate(bubPrefab, new Vector3(transform.position.x + 2.0f, 10.0f, -3.0f), Quaternion.identity);
			bub.GetComponent<BubAndBobController>().target = levelManagement.player1 ? levelManagement.player1 : targetedPlayer;
			yield return new WaitForSeconds(0.4f);
		}
		if (levelManagement.player2)
		{
			var bob = Instantiate(bobPrefab, new Vector3(transform.position.x + 4.0f, 10.0f, 3.0f), Quaternion.identity);
			bob.GetComponent<BubAndBobController>().target = levelManagement.player2;
			yield return new WaitForSeconds(0.4f);
		}

		counter = 0.0f;
		while (counter < counterMax)
		{
			driftSpeed += Time.deltaTime;
			levelManagement.driftSpeed = driftSpeed;
			counter += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}

		driftSpeed = 4.0f;
		levelManagement.driftSpeed = driftSpeed;
		cooldownCounter = 0.0f;
		phase = Phase.PHASE2;
		isCutscene = false;
		isIntangible = false;
		isInvulnerable = false;
	}

	public IEnumerator StartIntermission2()
	{
		float descendSpeed = 1.0f;
		float growSpeed = 0.2f;
		float counter = 0.0f;
		float counterMax = 0.0f;

		fireballHolster.DestroyAll();
		isCasting = true;
		

		StartCoroutine(AudioManager.Instance.SlowFade());	

		while (transform.position.y > 1.2f)
		{
			transform.position = transform.position + Vector3.down * descendSpeed * Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}

		isFloating = false;
		isCasting = false;
		rb.useGravity = true;
		var cameraTarget = Instantiate(levelManagement.cameraLockTarget, transform.position, Quaternion.identity);
		camera.GetComponent<CameraMovement>().lockedOn = true;
		camera.GetComponent<CameraMovement>().lockOn = cameraTarget.transform;

		yield return new WaitForSeconds(0.4f);

		gameObject.layer = 15;
		Vector3 destination = transform.position + new Vector3(3.0f, 0.0f, -1.0f);
		Vector3 direction = (destination - transform.position).normalized;
		facingRight = transform.position.x > destination.x ? false : true;
		isMoving = true;

		while (Vector3.Distance(destination, transform.position) > 0.1f)
		{
			direction = (destination - transform.position).normalized;
			transform.position = transform.position + direction * cutsceneSpeed * Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}

		yield return new WaitForSeconds(0.5f);

		gameObject.layer = 16;
		isMoving = false;
		isMorphing = true;
		audioSource.PlayOneShot(morphSound, 0.6f);
		StartCoroutine(enemyAnimation.MorphAnimation());

		while (isMorphing)
		{
			transform.localScale = transform.localScale + Vector3.one * growSpeed * Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}

		cooldownCounter = 0.0f;
		onCooldown = false;
		isCasting = false;
		phase = Phase.PHASE3;
		var r = Instantiate(roar, transform.position, Quaternion.identity, transform);
		bc.center = new Vector3(0.0f, 0.11f, 0.0f);

		yield return new WaitForSeconds(2.1f);

		if (subspaceScript)
		{
			subspaceScript.Phase3BGM();
		}

		yield return new WaitForSeconds(1.0f);

		onCooldown = true;
		cooldownCounter = 0.0f;
		isCutscene = false;
		isIntangible = false;
		isInvulnerable = false;
	}




	public void DamageFlyText(float damage, Color color)
	{
		var go = Instantiate(floatingText, transform.position, Quaternion.identity, transform);
		go.GetComponent<TextMesh>().text = damage.ToString();
		go.GetComponent<TextMesh>().color = color;
	}
}
