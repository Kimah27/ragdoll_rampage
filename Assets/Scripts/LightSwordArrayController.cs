using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwordArrayController : MonoBehaviour
{
    public GameObject[] verticalSwords;
    public GameObject[] horizontalSwords;

    public float counter;
    public float counterLoop;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public IEnumerator FireProjectiles()
    {
        for (int i = 0; i < verticalSwords.Length; i++)
        {
            verticalSwords[i].GetComponent<LightSwordController>().isActive = true;
            verticalSwords[i].GetComponent<EnemyAttacks>().PlayCastSound();

            yield return new WaitForSeconds(0.8f);
        }
        for (int i = 0; i < horizontalSwords.Length; i++)
        {
            horizontalSwords[i].GetComponent<LightSwordController>().isActive = true;
            horizontalSwords[i].GetComponent<EnemyAttacks>().PlayCastSound();

            yield return new WaitForSeconds(0.8f);
        }
    }
}
