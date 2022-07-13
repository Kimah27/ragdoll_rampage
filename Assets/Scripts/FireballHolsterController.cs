using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballHolsterController : MonoBehaviour
{
    public GameObject prefab;
    public GameObject[] fireballs;
    public EnemyMovement enemyMovement;
    public Vector3[] directions;

    public float loopCounter;
    public float loopMax;

    public int numFireballs;

    public bool isFiring;

    void Start()
    {
        fireballs = new GameObject[numFireballs];
        for (int i = 0; i < numFireballs; i++)
        {
            fireballs[i] = Instantiate(prefab, transform.position + Vector3.up * 10.0f + Vector3.forward * 10.0f, Quaternion.identity);
            fireballs[i].GetComponent<EnemyAttacks>().enemyMovement = enemyMovement;
        }

        directions = new Vector3[4];
        directions[0] = Vector3.forward;
        directions[1] = Vector3.right;
        directions[2] = Vector3.back;
        directions[3] = Vector3.left;

        isFiring = false;
    }


    void Update()
    {
        float NewRotation = (120.0f * Time.deltaTime) + gameObject.transform.rotation.eulerAngles.y;
        NewRotation = NewRotation % 360.0f;

        var rot = gameObject.transform.rotation.eulerAngles;
        rot.Set(0.0f, NewRotation, 0.0f);
        gameObject.transform.rotation = Quaternion.Euler(rot);

        if (isFiring)
        {
            loopCounter += Time.deltaTime;
		}
    }

    public IEnumerator FireProjectiles()
    {
        int pattern = 0;

        while (loopCounter < loopMax)
        {
            pattern = Random.Range(0, 3);

            if (pattern == 0)
            {
                for (int i = 0; i < numFireballs / 2; i++)
                {
                    if (!fireballs[i])
                    {
                        fireballs[i] = Instantiate(prefab, transform.position + Vector3.up * 10.0f + Vector3.forward * 10.0f, Quaternion.identity);
                        fireballs[i].GetComponent<EnemyAttacks>().enemyMovement = enemyMovement;
                    }

                    fireballs[i].transform.position = transform.position;
                    fireballs[i].GetComponent<EnemyAttacks>().direction = transform.TransformDirection(directions[i % 4]);
                    fireballs[i].GetComponent<EnemyAttacks>().PlayCastSound();

                    if (!fireballs[i + 4])
                    {
                        fireballs[i + 4] = Instantiate(prefab, transform.position + Vector3.up * 10.0f + Vector3.forward * 10.0f, Quaternion.identity);
                        fireballs[i + 4].GetComponent<EnemyAttacks>().enemyMovement = enemyMovement;
                    }
                    fireballs[i + 4].transform.position = transform.position;
                    fireballs[i + 4].GetComponent<EnemyAttacks>().direction = transform.TransformDirection(directions[(i + 2) % 4]);

                    yield return new WaitForSeconds(0.3f);
                }
            }
            if (pattern == 1)
            {
                for (int i = 0; i < numFireballs; i++)
                {
                    if (!fireballs[i])
                    {
                        fireballs[i] = Instantiate(prefab, transform.position + Vector3.up * 10.0f + Vector3.forward * 10.0f, Quaternion.identity);
                        fireballs[i].GetComponent<EnemyAttacks>().enemyMovement = enemyMovement;
                    }

                    fireballs[i].transform.position = transform.position;
                    fireballs[i].GetComponent<EnemyAttacks>().direction = transform.TransformDirection(directions[i % 4]);
                    fireballs[i].GetComponent<EnemyAttacks>().PlayCastSound();

                    yield return new WaitForSeconds(0.15f);
                }
            }
            if (pattern == 2)
            {
                for (int i = 0; i < numFireballs; i++)
                {
                    if (!fireballs[i])
                    {
                        fireballs[i] = Instantiate(prefab, transform.position + Vector3.up * 10.0f + Vector3.forward * 10.0f, Quaternion.identity);
                        fireballs[i].GetComponent<EnemyAttacks>().enemyMovement = enemyMovement;
                    }

                    fireballs[i].transform.position = transform.position;
                    fireballs[i].GetComponent<EnemyAttacks>().direction = transform.TransformDirection(directions[0]);
                    fireballs[i].GetComponent<EnemyAttacks>().PlayCastSound();

                    yield return new WaitForSeconds(0.15f);
                }
            }

            loopCounter += Time.deltaTime;
        }

        isFiring = false;
        loopCounter = 0.0f;
    }

    public void DestroyAll()
    {
        for (int i = 0; i < numFireballs; i++)
        {
            Destroy(fireballs[i].gameObject, 0.3f);
        }
        Destroy(gameObject, 0.4f);
    }
}
