using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject P1;
    public GameObject P2;

    public GameObject[] players;

    public Transform lockOn;

    public float defaultPosX;
    public float defaultPosY;
    public float defaultPosZ;
    public float shakeY;
    public float distanceBetweenP1P2;
    public float distanceBetweenP1C;
    public float distanceBetweenP2C;
    public float speed;

    public int frameShakeCount;
    public int frameShakeMod;

    public bool canMove;
    public bool shake;
    public bool shaken;
    public bool resetFlag;
    public bool lockedOn;
    public bool inPosition;

    void Start()
    {
        defaultPosX = 4.1f;
        defaultPosY = 10.0f;
        defaultPosZ = -14.0f;
        speed = 6.0f;
        shakeY = defaultPosY;
        canMove = true;
        shake = false;
        shaken = false;
        resetFlag = false;
        players = GameObject.FindGameObjectsWithTag("Player");
    }


    void Update()
    {
        
        if (!P1)
        {
            CheckForPlayers();
            if (players.Length > 0)
            {
                P1 = players[0];
            }
		}
        if (!P2 && players.Length > 1)
        {
            P2 = players[1];
        }
    }

	void LateUpdate()
	{
        MoveCamera();
        ShakeCamera();
	}

	public void MoveCamera()
    {
        if (P1 && P2 && !lockedOn && !inPosition)
        {
            distanceBetweenP1P2 = Mathf.Abs(P1.transform.position.x - P2.transform.position.x);
            //distanceBetweenP1C = Mathf.Abs(P1.transform.position.x - transform.position.x);
            //distanceBetweenP2C = Mathf.Abs(P2.transform.position.x - transform.position.x);
            if (distanceBetweenP1P2 > 16.90f)
            {
                canMove = false;
            }
            else
            {
                canMove = true;
            }

            if (canMove)
            {
                transform.position = new Vector3(Mathf.Clamp((P1.transform.position.x + P2.transform.position.x) / 2.0f, defaultPosX, 316.0f), shakeY, transform.position.z);
            }
        }
        else if (P1 && !lockedOn && !inPosition)
        {
            if (P1.transform.position.x > transform.position.x + 3.0f && canMove)
            {
                transform.position = new Vector3(P1.transform.position.x - 3.0f, shakeY, transform.position.z);
            }
            else if (P1.transform.position.x < transform.position.x - 3.0f && transform.position.x > 4.15f && canMove)
            {
                transform.position = new Vector3(P1.transform.position.x + 3.0f, shakeY, transform.position.z);
            }
        }
        else if (P2 && !lockedOn && !inPosition)
        {
            if (P2.transform.position.x > transform.position.x + 3.0f && canMove)
            {
                transform.position = new Vector3(P2.transform.position.x - 3.0f, transform.position.y, transform.position.z);
            }
            else if (P2.transform.position.x < transform.position.x - 3.0f && transform.position.x > 4.15f && canMove)
            {
                transform.position = new Vector3(P2.transform.position.x + 3.0f, transform.position.y, transform.position.z);
            }
        }
        else if (lockedOn && lockOn && !inPosition)
        {
            if (transform.position.x < lockOn.position.x - 0.08)
            {
                transform.position = transform.position + Vector3.right * speed * Time.deltaTime;
			}
            else if (transform.position.x > lockOn.position.x + 0.08)
            {
                transform.position = transform.position + Vector3.left * speed * Time.deltaTime;
            }
            else
            {
                inPosition = true;
			}
		}
        else if (lockedOn && lockOn && inPosition)
        {
            if (transform.position.x != lockOn.position.x)
            {
                transform.position = new Vector3(lockOn.position.x, transform.position.y, transform.position.z);
			}
            /*if (transform.position.x < lockOn.position.x - 0.08f)
			{
				transform.position = transform.position + Vector3.right * chase * Time.deltaTime;
			}
			else if (transform.position.x > lockOn.position.x + 0.08f)
			{
				transform.position = transform.position + Vector3.left * chase * Time.deltaTime;
			}*/
        }
        else if (!lockedOn && inPosition)
        {
            if (P1 && P2)
            {
                if (transform.position.x < (P1.transform.position.x + P2.transform.position.x) / 2.0f - 0.05f)
                {
                    transform.position = transform.position + Vector3.right * speed * Time.deltaTime;
                }
                else if (transform.position.x > (P1.transform.position.x + P2.transform.position.x) / 2.0f + 0.05f)
                {
                    transform.position = transform.position + Vector3.left * speed * Time.deltaTime;
                }
                else 
                {
                    inPosition = false;
				}
            }
            else if (P1)
            {
                if (transform.position.x < P1.transform.position.x - 0.05f)
                {
                    transform.position = transform.position + Vector3.right * speed * Time.deltaTime;
                }
                else if (transform.position.x > P1.transform.position.x + 0.05f)
                {
                    transform.position = transform.position + Vector3.left * speed * Time.deltaTime;
                }
                else
                {
                    inPosition = false;
                }
            }
            else if (P2)
            {
                if (transform.position.x < P2.transform.position.x - 0.05f)
                {
                    transform.position = transform.position + Vector3.right * speed * Time.deltaTime;
                }
                else if (transform.position.x > P2.transform.position.x + 0.05f)
                {
                    transform.position = transform.position + Vector3.left * speed * Time.deltaTime;
                }
                else
                {
                    inPosition = false;
                }
            }
        }
	}

    public void ShakeCamera()
    {
        if (shake)
        {
            resetFlag = true;
            frameShakeMod = frameShakeCount % 6;

            if (frameShakeMod < 3 && !shaken)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
                shaken = true;
            }
            else if (frameShakeMod >= 3 && !shaken)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
                shaken = true;
            }
            else if (frameShakeMod % 3 == 1)
            {
                shaken = false;
			}
            frameShakeCount++;
            shakeY = transform.position.y;
		}

        else if (resetFlag)
        {
            transform.position = new Vector3(transform.position.x, defaultPosY, defaultPosZ);
            resetFlag = false;
            shaken = false;
            frameShakeCount = 0;
            shakeY = defaultPosY;
        }


	}
    
    public void CheckForPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void AddPlayer2()
    {
        
	}

    public void RemovePlayer2()
    {
        P2 = null;
	}
}
