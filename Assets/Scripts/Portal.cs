using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public LevelManagement lm;

    public List<Sprite> portalAnim;

    public int animationMax;
    public int animationIndex;
    public int frameLoop;
    public int frameCount;

    public bool activated;

    void Start()
    {
        lm = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        animationMax = 7;
        animationIndex = 0;
        frameLoop = 7;
        frameCount = 0;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        AnimatePortal();
    }

    public void AnimatePortal()
    {
        if (frameCount >= frameLoop && animationIndex < animationMax)
        {
            animationIndex += 1;

            gameObject.GetComponent<SpriteRenderer>().sprite = portalAnim[animationIndex];
            frameCount = 0;
        }
        else if (frameCount >= frameLoop && animationIndex == animationMax)
        {
            animationIndex = 0;

            gameObject.GetComponent<SpriteRenderer>().sprite = portalAnim[animationIndex];
            frameCount = 0;
        }
        else
        {
            frameCount += 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !activated)
        {
            activated = true;
            if (lm)
            {
                if (lm.player1)
                {
                    StartCoroutine(lm.player1.GetComponent<PlayerMovement>().EnterPortal(transform.position + Vector3.up));
                }
                if (lm.player2)
                {
                    StartCoroutine(lm.player2.GetComponent<PlayerMovement>().EnterPortal(transform.position + Vector3.up));
                }
            }            

            if (SceneManager.GetActiveScene().name == "FrozenSuburbs")
            {
                FadeManager.Instance.FadeOut(1.5f, Color.black, Level2);
            }

            if (SceneManager.GetActiveScene().name == "ThePark")
            {
                FadeManager.Instance.FadeOut(1.5f, Color.black, Level3);
            }

            if (SceneManager.GetActiveScene().name == "TechnoBase")
            {
                FadeManager.Instance.FadeOut(1.5f, Color.black, Level4);
                StartCoroutine(AudioManager.Instance.FastFade());
            }
        }
    }

    public void Level2()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ThePark");
    }

    public void Level3()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TechnoBase");
    }

    public void Level4()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Subspace");
    }
}
