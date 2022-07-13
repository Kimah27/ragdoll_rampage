using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NyanSlashAnimation : MonoBehaviour
{
    public PlayerAttacks playerAttacks;
    public Camera camera;
    public Transform sprite1;
    public Transform sprite2;

    public float speed;

    void Start()
    {
        playerAttacks = gameObject.GetComponent<PlayerAttacks>();
        sprite1 = transform.Find("Sprite1");
        sprite2 = transform.Find("Sprite2");
        camera = FindObjectOfType<Camera>();
        speed = 60.0f;
        StartCoroutine(AnimateNyanSlash());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(camera.transform.position.x, transform.position.y, transform.position.z);
    }

    IEnumerator AnimateNyanSlash()
    {
        Vector3 direction1 = new Vector3(1.04f, -0.6f, 0.0f);
        Vector3 direction2 = new Vector3(-1.04f, -0.6f, 0.0f);
        float alpha = 1.0f;

        yield return new WaitForSeconds(0.1f);

        playerAttacks.PlayCastSound();
        while (sprite1.localPosition.x < 0.0f)
        {
            sprite1.position = sprite1.position + direction1 * speed * Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }
        playerAttacks.PlayCastSound();
        while (sprite2.localPosition.x > 0.0f)
        {
            sprite2.position = sprite2.position + direction2 * speed * Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }
        while (alpha > 0.0f)
        {
            alpha -= 1.2f * Time.deltaTime;
            Mathf.Clamp01(alpha);
            sprite1.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
            sprite2.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);

            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(1.0f);

        Destroy(gameObject);
    }
}
