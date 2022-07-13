using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayDayHolster : MonoBehaviour
{
    public GameObject prefab;
    public GameObject[] coins;
    public PlayerMovement playerMovement;
    public Vector3[] directions;

    public float lifeCounter;
    public float lifeMax;
    public float loopCounter;
    public float loopMax;

    public int numCoins;

    void Start()
    {
        coins = new GameObject[numCoins];
        for (int i = 0; i < numCoins; i++)
        {
            coins[i] = Instantiate(prefab, transform.position + Vector3.up * 10.0f, Quaternion.identity);
            coins[i].GetComponent<PlayerAttacks>().playerMovement = playerMovement;
		}

        directions = new Vector3[4];
        directions[0] = Vector3.forward;
        directions[1] = Vector3.right;
        directions[2] = Vector3.back;
        directions[3] = Vector3.left;

        StartCoroutine(FireProjectiles());
    }

    
    void Update()
    {
        float NewRotation = (120.0f * Time.deltaTime) + gameObject.transform.rotation.eulerAngles.y;
        NewRotation = NewRotation % 360.0f;

        var rot = gameObject.transform.rotation.eulerAngles;
        rot.Set(0.0f, NewRotation, 0.0f);
        gameObject.transform.rotation = Quaternion.Euler(rot);

        lifeCounter += Time.deltaTime;
        loopCounter += Time.deltaTime;
    }

    public IEnumerator FireProjectiles()
    {
        while (lifeCounter < lifeMax)
        {
            for (int i = 0; i < numCoins / 2; i++)
            {
                coins[i].transform.position = transform.position;
                coins[i].GetComponent<PlayerAttacks>().direction = transform.TransformDirection(directions[i % 4]);
                coins[i].GetComponent<PlayerAttacks>().PlayCastSound();

                coins[i+4].transform.position = transform.position;
                coins[i+4].GetComponent<PlayerAttacks>().direction = transform.TransformDirection(directions[(i+2) % 4]);

                yield return new WaitForSeconds(0.1f);
            }
		}

        for (int i = 0; i < numCoins; i++)
        {
            Destroy(coins[i].gameObject, 0.3f);
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(0.4f);
    }
}
