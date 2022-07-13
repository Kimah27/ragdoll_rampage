using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	public enum EnemyType
	{
		GRUNT,
		FLYER,
		FERAL,
		SHADY
	}

	public EnemyType enemyType;

	[Header("GameObjects")]
	public GameObject currentAttack;
	public GameObject gruntAttack;
	public GameObject fireballProjectile;
	public GameObject feralDive;
	public GameObject daggerAttack;
	public GameObject daggerProjectile;
	public GameObject hitSparkParticleEffect;
	public GameObject floatingText;
	public GameObject warning;
	public GameObject taco;
	private GameObject hitSpark;

	[Header("Components")]
	public LevelManagement levelManagement;
	public Rigidbody rb;
	public BoxCollider bc;
	public SphereCollider detectionRadius;
	public EnemyAnimation enemyAnimation;
	public ShadowAnimation shadowAnimation;
	public AudioSource audioSource;
	public AudioClip dashSound;
	public AudioClip jumpSound;
	public AudioClip deathSound;
	public FloatingHealthBar floatingHealthBar;
	public Transform targetedPlayer;
	public RaycastHit hitGround;

	[Header("Vectors")]
	public Vector3 startPos;
	public Vector3 moveDirection;
	public Vector3 approachPosition;
	public Vector3 knockbackTaken;

	[Header("Attributes")]
	public float health;
	public float maxHealth;
	public float wanderSpeed;
	public float engagedSpeed;
	public float approachSpeed;
	public float jumpHeight;
	public float backflipHeight;
	public float flySpeed;
	public float attackRange;
	public float distanceToGround;
	public float knockbackForce;
	public float knockupForce;
	public float knockdownForce;
	public float verticalVeloctiy;
	public float distanceToPlayer1;
	public float distanceToPlayer2;
	public float distanceToTarget;
	public int layerMask;

	[Header("Timers")]
	public float testDummyTimer;
	public float timeStunned;
	public float timeSinceLastMovement;
	public float timeSinceLastApproach;
	public float timeSinceLastAttack;
	public float timeSinceBeingHit;
	public float timeSinceKnockedDown;
	public float intervalBetweenMovements;
	public float intervalBetweenApproaches;
	public float intervalBetweenAttacks;

	[Header("Booleans")]
	public bool isMoving;
	public bool isApproaching;
	public bool isJumping;
	public bool isAttacking;
	public bool isThrowingDagger;
	public bool isHit;
	public bool isStunned;
	public bool isKnockedDown;
	public bool isEngaged;
	public bool isIntangible;
	public bool isWarning;
	public bool isTestDummy;
	public bool isAlive;
	public bool isCircling;
	public bool facingRight;
	public bool gameOver;

	void Start()
	{
		levelManagement = FindObjectOfType<LevelManagement>();
		rb = GetComponent<Rigidbody>();
		bc = GetComponent<BoxCollider>();
		enemyAnimation = transform.Find("Sprite").GetComponent<EnemyAnimation>();
		shadowAnimation = transform.Find("Shadow").GetComponent<ShadowAnimation>();
		floatingHealthBar = transform.Find("FloatingHealthBar").GetComponent<FloatingHealthBar>();
		detectionRadius = transform.Find("Detection").GetComponent<SphereCollider>();
		startPos = transform.position;
		wanderSpeed = 3.0f;
		SetEngagedSpeed();
		jumpHeight = 7.0f;
		backflipHeight = 14.0f;
		knockbackForce = 220.0f;
		knockupForce = 220.0f;
		knockdownForce = 100.0f;
		verticalVeloctiy = 5.0f;
		flySpeed = 1.5f;
		intervalBetweenMovements = 4.0f;
		timeSinceLastAttack = intervalBetweenAttacks / 2.0f;
		intervalBetweenApproaches = 5.0f;
		timeSinceLastApproach = 6.0f;
		layerMask = 1 << 13;
		isHit = false;
		isWarning = false;
		isAlive = true;
	}


	void Update()
	{
		CheckFacing();
		CheckGrounded();
		CheckGameOver();
		Fly();
		Timers();
		Stunned();
		//ResetTestPos();
		distanceToGround = RaycastDown();
		if (isEngaged && isAlive)
		{
			if (targetedPlayer)
			{
				distanceToTarget = Vector3.Distance(transform.position, targetedPlayer.position);
			}
			
			Fight();
		}
		else
		{
			Idle();
		}
	}

	public void SetEngagedSpeed()
	{
		if (enemyType == EnemyType.GRUNT || enemyType == EnemyType.FLYER)
		{
			engagedSpeed = 5.0f;
		}
		if (enemyType == EnemyType.SHADY)
		{
			engagedSpeed = 4.5f;
			approachSpeed = 6.5f;
		}
		if (enemyType == EnemyType.FERAL)
		{
			engagedSpeed = 5.5f;
			approachSpeed = 6.0f;
		}
	}

	public void CheckFacing()
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

	public void CheckGameOver()
	{
		if (levelManagement.killEnemies && !gameOver)
		{
			gameOver = true;
			isAlive = false;
			StartCoroutine(Death());
			StartCoroutine(enemyAnimation.DeathFlash());
			StartCoroutine(shadowAnimation.DeathFlash());
		}
	}

	public void Timers()
	{
		timeSinceLastMovement += Time.deltaTime;
		timeSinceBeingHit += Time.deltaTime;
		timeSinceKnockedDown += Time.deltaTime;
		if (isEngaged)
		{
			timeSinceLastApproach += Time.deltaTime;
			timeSinceLastAttack += Time.deltaTime;
		}
		
	}

	public float RaycastDown()
	{
		float distance = 10.0f;

		if (Physics.Raycast(transform.position, -Vector3.up, out hitGround, layerMask))
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

	public void Fly()
	{
		if (enemyType == EnemyType.FLYER && !isKnockedDown && gameObject.transform.position.y < 2.0f)
		{
			rb.velocity = Vector3.up * verticalVeloctiy;
		}
	}

	public void CheckGrounded()
	{
		if (distanceToGround <= 1.16f && rb.velocity.y <= 0.0f)
		{
			isJumping = false;
		}
	}

	public void ResetTestPos()
	{
		if (isTestDummy)
		{
			if (enemyType == EnemyType.GRUNT && transform.position.x != 48.0f)
			{
				testDummyTimer += Time.deltaTime;
				if (testDummyTimer >= 5.0f)
				{
					transform.position = new Vector3(48.0f, 1.0f, 0.0f);
				}
			}
			if (enemyType == EnemyType.FLYER && transform.position.x != 54.0f)
			{
				testDummyTimer += Time.deltaTime;
				if (testDummyTimer >= 5.0f)
				{
					transform.position = new Vector3(54.0f, 1.0f, 0.0f);
				}
			}
			if (enemyType == EnemyType.GRUNT && transform.position.x == 48.0f)
			{
				testDummyTimer = 0.0f;
			}
			if (enemyType == EnemyType.FLYER && transform.position.x == 54.0f)
			{
				testDummyTimer = 0.0f;
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("Player") && isAlive)
		{
			isEngaged = true;
			if (!targetedPlayer || !targetedPlayer.gameObject.GetComponent<PlayerMovement>().isAlive)
			{
				targetedPlayer = other.transform;
			}
		}
		else if ((other.transform.tag == "PlayerAttack" || other.transform.tag == "PlayerProjectile") && isAlive && !isIntangible)
		{
			PlayerAttacks attack = other.GetComponent<PlayerAttacks>();
			hitSpark = (GameObject)Instantiate(hitSparkParticleEffect);
			hitSpark.tag = "Effect";
			hitSpark.transform.position = gameObject.transform.position;

			isHit = true;
			isEngaged = true;
			targetedPlayer = attack.playerMovement.gameObject.transform;
			if (!attack.playerMovement.isSupering)
			{
				attack.playerMovement.ActiveCharge();
			}

			attack.PlayHitSound();

			if (attack.knockback)
			{
				if (other.transform.tag == "PlayerAttack")
				{
					Vector3 source = other.transform.parent.position;
					source.x = source.x - transform.position.x;
					source.y = 0.0f;
					source.z = source.z - transform.position.z;

					rb.AddForce(-source.normalized * knockbackForce * attack.attackLevel);
				}
				else
				{
					Vector3 source = attack.playerMovement.transform.position;
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
			if (attack.knockdown)
			{
				StartCoroutine(TakeKnockdown(3.0f));
				rb.AddForce(Vector3.down * knockdownForce * attack.attackLevel);
			}

			float damageTaken = attack.damage * attack.playerMovement.damageModifier;
			ApplyStun(attack.stun);

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

			testDummyTimer = 0.0f;
		}
	}

	IEnumerator TakeKnockdown(float duration)
	{
		isKnockedDown = true;
		yield return new WaitForSeconds(duration);
		isKnockedDown = false;
	}

	public void Stunned()
	{
		if (timeStunned > 0.0f)
		{
			isStunned = true;
			timeStunned -= Time.deltaTime;
		}
		else
		{
			isStunned = false;
		}
	}

	public void ApplyStun(float stun)
	{
		timeStunned += stun;
	}

	IEnumerator Death()
	{
		gameObject.layer = 15;
		if (levelManagement && levelManagement.inEncounter)
		{
			levelManagement.encounterEnemies -= 1;
			levelManagement.CheckEncounter();
		}
		yield return new WaitForSeconds(1.2f);
		DropTaco();
		Destroy(gameObject);
	}

	public void DropTaco()
	{
		int roll = Random.Range(0, 6);

		if (roll == 5)
		{
			var t = Instantiate(taco, new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 1.0f, 4.0f), transform.position.z), Quaternion.identity);
		}
	}

	public void DamageFlyText(float damage, Color color)
	{
		var go = Instantiate(floatingText, transform.position, Quaternion.identity, transform);
		go.GetComponent<TextMesh>().text = damage.ToString();
		go.GetComponent<TextMesh>().color = color;
	}

	public void Idle()
	{
		if (!isMoving)
		{
			enemyAnimation.animationIndex = 0;
		}
		if (timeSinceLastMovement >= intervalBetweenMovements)
		{
			StartCoroutine(Wander());
			timeSinceLastMovement = 0.0f + Random.Range(-1.0f, 1.0f);
		}
	}

	public void Fight()
	{
		if (enemyType == EnemyType.GRUNT)
		{
			EngageGrunt();
		}
		if (enemyType == EnemyType.FLYER)
		{
			EngageFlyer();
		}
		if (enemyType == EnemyType.SHADY)
		{
			EngageShady();
		}
		if (enemyType == EnemyType.FERAL)
		{
			EngageFeral();
		}
	}

	public IEnumerator Wander()
	{
		float timer = 0.0f;
		isMoving = true;
		if (!isStunned & !isEngaged)
		{
			if (transform.position.x > startPos.x && transform.position.z > startPos.z)
			{
				moveDirection = new Vector3(Random.Range(-1.0f, 0.0f), 0.0f, Random.Range(-1.0f, 0.0f)).normalized;
			}
			else if (transform.position.x > startPos.x && transform.position.z < startPos.z)
			{
				moveDirection = new Vector3(Random.Range(-1.0f, 0.0f), 0.0f, Random.Range(1.0f, 0.0f)).normalized;
			}
			else if (transform.position.x < startPos.x && transform.position.z > startPos.z)
			{
				moveDirection = new Vector3(Random.Range(1.0f, 0.0f), 0.0f, Random.Range(-1.0f, 0.0f)).normalized;
			}
			else if (transform.position.x < startPos.x && transform.position.z < startPos.z)
			{
				moveDirection = new Vector3(Random.Range(1.0f, 0.0f), 0.0f, Random.Range(1.0f, 0.0f)).normalized;
			}
			else
			{
				moveDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)).normalized;
			}

			while (timer <= 1.5f && !isEngaged)
			{
				timer += Time.deltaTime;
				transform.position = transform.position + moveDirection * wanderSpeed * Time.deltaTime;

				yield return new WaitForFixedUpdate();
			}
		}

		if (!isEngaged)
		{
			moveDirection = Vector3.zero;
		}

		isMoving = false;
	}

	public void EngageGrunt()
	{
		if (timeSinceLastMovement >= intervalBetweenMovements && !isApproaching && !isAttacking && !isWarning)
		{
			isMoving = true;
			moveDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)).normalized;
			timeSinceLastMovement = 0.0f;
			StartCoroutine(Reposition());
		}

		if (timeSinceLastApproach >= intervalBetweenApproaches && timeSinceLastAttack >= intervalBetweenAttacks && targetedPlayer.gameObject.GetComponent<PlayerMovement>().isAlive && !isAttacking && !isWarning)
		{
			isMoving = true;
			isApproaching = true;
			timeSinceLastApproach = 0.0f;
			StartCoroutine(Approach());
		}

		if (timeSinceLastAttack >= intervalBetweenAttacks)
		{
			if (targetedPlayer && targetedPlayer.gameObject.GetComponent<PlayerMovement>().isAlive && Vector3.Distance(targetedPlayer.position, transform.position) < attackRange)
			{
				timeSinceLastAttack = 0.0f;
				StartCoroutine(GruntAttack());
			}
		}
	}

	public void EngageFlyer()
	{
		if (timeSinceLastApproach >= intervalBetweenApproaches && targetedPlayer.gameObject.GetComponent<PlayerMovement>().isAlive && !isAttacking)
		{
			isMoving = true;
			moveDirection = SetFlightDestination() - transform.position;
			StartCoroutine(Approach());
		}

		if (timeSinceLastAttack >= intervalBetweenAttacks && !isMoving)
		{
			if (targetedPlayer && targetedPlayer.gameObject.GetComponent<PlayerMovement>().isAlive)
			{
				timeSinceLastAttack = 0.0f;
				StartCoroutine(ShootFireball());
			}
		}
	}

	float currentAngle = 0.0f;
	float circleRadius = 4.0f;

	public void EngageFeral()
	{
		if (targetedPlayer && targetedPlayer.gameObject.GetComponent<PlayerMovement>().isAlive)
		{
			if (!isCircling)
			{
				currentAngle = Random.Range(0, 2.0f * Mathf.PI);
				isCircling = true;
			}
			if (timeSinceLastAttack >= intervalBetweenAttacks && !isAttacking)
			{
				isMoving = false;
				currentAngle += Mathf.PI;
				timeSinceLastAttack = 0.0f;
				StartCoroutine(DiveAttack());
				StartCoroutine(enemyAnimation.FeralDive());
			}
			else if (!isAttacking && !isWarning)
			{
				isMoving = true;
				Vector3 centrePosition = targetedPlayer.position;

				float degreesMoved = engagedSpeed / (2.0f * Mathf.PI * circleRadius) * Time.deltaTime;
				degreesMoved = degreesMoved * 2.0f * Mathf.PI;
				currentAngle += degreesMoved;
				Vector3 newPosition = new Vector3(Mathf.Cos(currentAngle) * circleRadius, transform.position.y, Mathf.Sin(currentAngle) * circleRadius) + centrePosition;
				moveDirection = (newPosition - transform.position).normalized;

				transform.position = transform.position + moveDirection * engagedSpeed * Time.deltaTime;
			}
		}
	}

	public void EngageShady()
	{
		if (timeSinceLastMovement >= intervalBetweenMovements && !isApproaching && !isAttacking && !isWarning)
		{
			isMoving = true;

			moveDirection = distanceToTarget > 4.5f ? (SetShadyDestination() - transform.position).normalized : (transform.position - targetedPlayer.position).normalized;
			timeSinceLastMovement = 0.0f;
			StartCoroutine(Reposition());
		}

		if (timeSinceLastApproach >= intervalBetweenApproaches && targetedPlayer.gameObject.GetComponent<PlayerMovement>().isAlive && !isAttacking && !isMoving && !isJumping)
		{
			if (Random.Range(0, 3) == 2)
			{
				isMoving = true;
				isApproaching = true;
				StartCoroutine(Approach());
			}

			timeSinceLastApproach = 0.0f;
		}
	}

	public IEnumerator Reposition()
	{
		float timer = 0.0f;
		if (enemyType == EnemyType.GRUNT)
		{
			if (Random.Range(0, 2) == 1)
			{
				Jump();
			}
			while (timer <= 1.0f && isAlive && !isStunned && !isApproaching && !isKnockedDown)
			{
				timer += Time.deltaTime;
				transform.position = transform.position + moveDirection * engagedSpeed * Time.deltaTime;

				yield return new WaitForFixedUpdate();
			}
			isMoving = false;
		}
		if (enemyType == EnemyType.SHADY)
		{
			while (timer <= 0.8f && isAlive && !isStunned && !isApproaching && !isKnockedDown)
			{
				timer += Time.deltaTime;
				transform.position = transform.position + moveDirection * engagedSpeed * Time.deltaTime;

				yield return new WaitForFixedUpdate();
			}

			if (isAlive && !isStunned && !isKnockedDown)
			{
				StartCoroutine(ThrowDagger());
				StartCoroutine(enemyAnimation.Backflip());
			}
			else
			{
				isMoving = false;
			}
		}
	}

	public IEnumerator Approach()
	{
		timeSinceLastApproach = 0.0f;
		if (enemyType == EnemyType.GRUNT)
		{
			moveDirection = (targetedPlayer.position - transform.position).normalized;
			while (Vector3.Distance(targetedPlayer.position, transform.position) > 1.0f && isAlive && !isWarning && !isAttacking && !isStunned)
			{
				moveDirection = (targetedPlayer.position - transform.position).normalized;
				transform.position = transform.position + moveDirection * engagedSpeed * Time.deltaTime;
				yield return new WaitForFixedUpdate();
			}
		}

		else if (enemyType == EnemyType.FLYER)
		{
			Vector3 destination = SetFlightDestination();
			float distance = Vector3.Distance(destination, transform.position);
			moveDirection = (destination - transform.position).normalized;
			moveDirection = moveDirection + Vector3.up / 2;
			rb.velocity = moveDirection * flySpeed * distance;
			yield return new WaitForSeconds(0.2f);

			while (distanceToGround > 2.0f && !isKnockedDown && !isStunned)
			{
				
				yield return new WaitForFixedUpdate();
			}
			rb.velocity = Vector3.zero;
		}

		else if (enemyType == EnemyType.FERAL)
		{
			
		}

		else if (enemyType == EnemyType.SHADY)
		{
			gameObject.layer = 15;
			isIntangible = true;
			StartCoroutine(enemyAnimation.FadeOut());
			int roll = Random.Range(0, 2);
			Vector3 destination = roll == 1 ? targetedPlayer.position + Vector3.left : targetedPlayer.position + Vector3.right;
			float distance = Vector3.Distance(destination, transform.position);
			moveDirection = (destination - transform.position).normalized;

			while (distance > 0.1f && isAlive && !isWarning && !isAttacking && !isStunned)
			{
				destination = roll == 1 ? targetedPlayer.position + Vector3.left : targetedPlayer.position + Vector3.right;
				moveDirection = (destination - transform.position).normalized;
				distance = Vector3.Distance(destination, transform.position);
				transform.position = transform.position + moveDirection * approachSpeed * Time.deltaTime;
				yield return new WaitForFixedUpdate();
			}

			if (isAlive && !isWarning && !isAttacking && !isStunned)
			{
				StartCoroutine(DaggerAttack());
			}

			gameObject.layer = 9;
			isIntangible = false;
			StartCoroutine(enemyAnimation.FadeIn());
		}

		isMoving = false;
		isApproaching = false;
		yield return new WaitForSeconds(1.0f);
	}

	public Vector3 SetFlightDestination()
	{
		int roll = Random.Range(0, 6);
		Vector3 toReturn = Vector3.zero;

		switch(roll)
		{
			case 0:
				toReturn = new Vector3(targetedPlayer.position.x - 6.0f, 3.0f, targetedPlayer.position.z - 3.0f);
				break;
			case 1:
				toReturn = new Vector3(targetedPlayer.position.x - 6.0f, 3.0f, targetedPlayer.position.z);
				break;
			case 2:
				toReturn = new Vector3(targetedPlayer.position.x - 6.0f, 3.0f, targetedPlayer.position.z + 3.0f);
				break;
			case 3:
				toReturn = new Vector3(targetedPlayer.position.x + 6.0f, 3.0f, targetedPlayer.position.z - 3.0f);
				break;
			case 4:
				toReturn = new Vector3(targetedPlayer.position.x + 6.0f, 2.0f, targetedPlayer.position.z);
				break;
			case 5:
				toReturn = new Vector3(targetedPlayer.position.x + 6.0f, 2.0f, targetedPlayer.position.z + 2.0f);
				break;
		}

		return toReturn;
	}

	public Vector3 SetShadyDestination()
	{
		int roll = Random.Range(0, 4);
		Vector3 toReturn = Vector3.zero;

		switch (roll)
		{
			case 0:
				toReturn = new Vector3(targetedPlayer.position.x, 1.0f, targetedPlayer.position.z + 3.0f);
				break;
			case 1:
				if (transform.position.x > targetedPlayer.position.x)
				{
					toReturn = new Vector3(targetedPlayer.position.x + 3.0f, 1.0f, targetedPlayer.position.z);
				}
				else
				{
					toReturn = new Vector3(targetedPlayer.position.x - 3.0f, 1.0f, targetedPlayer.position.z);
				}
				break;
			case 2:
				toReturn = new Vector3(targetedPlayer.position.x, 1.0f, targetedPlayer.position.z - 3.0f);
				break;
			case 3:
				if (transform.position.x > targetedPlayer.position.x)
				{
					toReturn = new Vector3(targetedPlayer.position.x - 3.0f, 1.0f, targetedPlayer.position.z);
				}
				else
				{
					toReturn = new Vector3(targetedPlayer.position.x + 3.0f, 1.0f, targetedPlayer.position.z);
				}
				break;
		}

		return toReturn;
	}

	IEnumerator AttackShift(float shift, float time, Vector3 direction)
	{
		rb.velocity = direction * shift;
		yield return new WaitForSeconds(time);
		rb.velocity = Vector3.zero;
	}

	public IEnumerator GruntAttack()
	{
		isWarning = true;
		Vector3 direction = (targetedPlayer.position - transform.position).normalized;
		moveDirection = Vector3.zero;
		facingRight = direction.x > 0.0f ? true : false;

		var warn = Instantiate(warning, transform.position + Vector3.up, Quaternion.identity, transform);

		// Attack warning
		float timer = 0.0f;
		while (timer < 0.6f && isAlive && !isStunned)
		{
			timer += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}

		isWarning = false;
		isAttacking = true;
		Destroy(warn);
		timeSinceLastAttack = 0.0f;
		if (isAlive && !isStunned)
		{
			StartCoroutine(AttackShift(8.0f, 0.06f, direction));
			currentAttack = Instantiate(gruntAttack, transform.position + direction * 1.2f, Quaternion.identity, transform);
			currentAttack.GetComponent<EnemyAttacks>().enemyMovement = this;
			yield return new WaitForSeconds(0.2f);
			currentAttack.GetComponent<BoxCollider>().enabled = false;
		}
		
		yield return new WaitForSeconds(0.6f);
		Destroy(currentAttack);
		isAttacking = false;
	}

	public IEnumerator ShootFireball()
	{
		isAttacking = true;
		if (isAlive && !isStunned && !isKnockedDown)
		{
			if (levelManagement.activePlayers == 2)
			{
				var fb1 = Instantiate(fireballProjectile, transform.position + (facingRight ? Vector3.right * 0.3f : Vector3.left * 0.3f), Quaternion.identity);
				fb1.GetComponent<EnemyAttacks>().direction = (levelManagement.player1.position - transform.position).normalized;
				fb1.GetComponent<EnemyAttacks>().speed = 5.0f;
				fb1.GetComponent<EnemyAttacks>().enemyMovement = this;
				yield return new WaitForSeconds(0.5f);
				if (levelManagement.player2.GetComponent<PlayerMovement>().isAlive)
				{
					var fb2 = Instantiate(fireballProjectile, transform.position + (facingRight ? Vector3.right * 0.3f : Vector3.left * 0.3f), Quaternion.identity);
					fb2.GetComponent<EnemyAttacks>().direction = (levelManagement.player2.position - transform.position).normalized;
					fb2.GetComponent<EnemyAttacks>().speed = 5.0f;
					fb2.GetComponent<EnemyAttacks>().enemyMovement = this;
				}
				else
				{
					var fb2 = Instantiate(fireballProjectile, transform.position + (facingRight ? Vector3.right * 0.3f : Vector3.left * 0.3f), Quaternion.identity);
					fb2.GetComponent<EnemyAttacks>().direction = (levelManagement.player1.position - transform.position).normalized;
					fb2.GetComponent<EnemyAttacks>().speed = 5.0f;
					fb2.GetComponent<EnemyAttacks>().enemyMovement = this;
				}
			}
			else
			{
				var fb1 = Instantiate(fireballProjectile, transform.position + (facingRight ? Vector3.right * 0.3f : Vector3.left * 0.3f), Quaternion.identity);
				fb1.GetComponent<EnemyAttacks>().direction = (targetedPlayer.position - transform.position).normalized;
				fb1.GetComponent<EnemyAttacks>().speed = 5.0f;
				fb1.GetComponent<EnemyAttacks>().enemyMovement = this;
				yield return new WaitForSeconds(0.5f);
				var fb2 = Instantiate(fireballProjectile, transform.position + (facingRight ? Vector3.right * 0.3f : Vector3.left * 0.3f), Quaternion.identity);
				fb2.GetComponent<EnemyAttacks>().direction = (targetedPlayer.position - transform.position).normalized;
				fb2.GetComponent<EnemyAttacks>().speed = 5.0f;
				fb2.GetComponent<EnemyAttacks>().enemyMovement = this;
			}
		}
		

		yield return new WaitForSeconds(2.0f);
		isAttacking = false;
	}

	public IEnumerator DaggerAttack()
	{
		isWarning = true;
		Vector3 direction = (targetedPlayer.position - transform.position).normalized;
		moveDirection = Vector3.zero;
		facingRight = direction.x > 0.0f ? true : false;

		var warn = Instantiate(warning, transform.position + Vector3.up, Quaternion.identity, transform);

		// Attack warning
		float timer = 0.0f;
		while (timer < 0.6f && isAlive && !isStunned)
		{
			timer += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}

		isWarning = false;
		isAttacking = true;
		Destroy(warn);
		timeSinceLastAttack = 0.0f;
		if (isAlive && !isStunned)
		{
			currentAttack = Instantiate(daggerAttack, transform.position + (facingRight ? Vector3.right * 2.0f : Vector3.left * 2.0f) + Vector3.down * 0.5f, Quaternion.identity, transform);
			currentAttack.GetComponent<EnemyAttacks>().enemyMovement = this;
			if (facingRight)
			{
				currentAttack.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
			}
			yield return new WaitForSeconds(0.2f);
			currentAttack.GetComponent<BoxCollider>().enabled = false;
			currentAttack.GetComponent<SpriteRenderer>().enabled = false;
		}

		yield return new WaitForSeconds(0.6f);
		Destroy(currentAttack);
		isAttacking = false;
	}

	public IEnumerator ThrowDagger()
	{
		facingRight = transform.position.x < targetedPlayer.position.x ? false : true;

		if (isAlive && !isStunned)
		{
			isJumping = true;
			rb.velocity = (Vector3.up * backflipHeight) + (facingRight ? Vector3.left : Vector3.right) * 2.0f;
			//audioSource.PlayOneShot(jumpSound, 0.3f);
		}

		yield return new WaitForSeconds(0.6f);

		isThrowingDagger = true;
		if (isAlive && !isStunned && !isKnockedDown)
		{
			var fb1 = Instantiate(daggerProjectile, transform.position + (facingRight ? Vector3.right * 0.3f : Vector3.left * 0.3f), Quaternion.identity);
			fb1.GetComponent<EnemyAttacks>().direction = (targetedPlayer.position - transform.position).normalized;
			fb1.GetComponent<EnemyAttacks>().speed = 7.0f;
			fb1.GetComponent<EnemyAttacks>().enemyMovement = this;
		}

		yield return new WaitForSeconds(0.8f);
		isThrowingDagger = false;
		isMoving = false;
	}

	public IEnumerator DiveAttack()
	{
		float diveVelocity = 12.0f;
		isWarning = true;
		Vector3 direction = (targetedPlayer.position - transform.position).normalized + Vector3.up * 0.4f;
		moveDirection = Vector3.zero;
		facingRight = direction.x > 0.0f ? true : false;

		var warn = Instantiate(warning, transform.position + Vector3.up, Quaternion.identity, transform);

		// Attack warning
		float timer = 0.0f;
		while (timer < 0.6f && isAlive && !isStunned)
		{
			timer += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}
		isAttacking = true;
		isWarning = false;
		
		Destroy(warn);
		timeSinceLastAttack = 0.0f;
		if (isAlive && !isStunned)
		{
			gameObject.layer = 15;
			currentAttack = Instantiate(feralDive, transform.position, Quaternion.identity, transform);
			currentAttack.GetComponent<EnemyAttacks>().enemyMovement = this;
			rb.velocity = direction * diveVelocity;
			bc.center = Vector3.up * 0.2f;

			yield return new WaitForSeconds(0.6f);
			currentAttack.GetComponent<BoxCollider>().enabled = false;
			gameObject.layer = 9;
		}

		yield return new WaitForSeconds(0.6f);

		bc.center = Vector3.zero;
		Destroy(currentAttack);
		isAttacking = false;
	}
}
