using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubAndBobAnimation : MonoBehaviour
{
    public List<Sprite> sprites;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
	}
}
