using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpController : MonoBehaviour
{
    public List<Sprite> helpAnim;
    public float time;

    public bool saved;
    public bool soundPlayed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time < 1.0f)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            if (saved && !soundPlayed)
            {
                soundPlayed = true;
                transform.parent.GetComponent<YunaController>().audioSource.Play();
			}
		}
        else if (time < 2.0f)
        {
            soundPlayed = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }

        time += Time.deltaTime;

        if (time >= 2.0f)
        {
            time = 0.0f;
		}
    }

    public void Saved()
    {
        saved = true;
        GetComponent<SpriteRenderer>().sprite = helpAnim[1];
	}
}
