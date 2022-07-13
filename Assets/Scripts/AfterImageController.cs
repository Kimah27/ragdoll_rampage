using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageController : MonoBehaviour
{
    public Sprite sprite;

    public float lifeMax;
    public float fadeRate;
    public float alpha;


    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
        Destroy(gameObject, lifeMax);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, alpha);
        alpha -= fadeRate * Time.deltaTime;
    }
}
