using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaredownAnimation : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public List<Sprite> sprites;

    void Start()
    {
        playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
        Vector3 pos = new Vector3(transform.position.x + (playerMovement.facingRight ? -0.5f : 0.5f), transform.position.y + 0.5f, transform.position.z);
        if (gameObject.GetComponent<SpriteRenderer>())
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = playerMovement.facingRight ? sprites[1] : sprites[0];
            gameObject.transform.position = pos;
        }
        
    }

    void Update()
    {
        
    }
}
