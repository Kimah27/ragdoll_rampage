using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YunaController : MonoBehaviour
{
    public Rigidbody rb;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer boxRenderer;
    public SpriteRenderer helpRenderer;
    public HelpController helpController;
    public AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        boxRenderer = transform.Find("Box").GetComponent<SpriteRenderer>();
        helpRenderer = transform.Find("Help").GetComponent<SpriteRenderer>();
        helpController = transform.Find("Help").GetComponent<HelpController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Spin()
    {
        float counter = 0.0f;
        float max = 3.0f;

        while (counter < max)
        {
            float NewRotation = (540.0f * Time.deltaTime) + boxRenderer.transform.rotation.eulerAngles.z;
            NewRotation = NewRotation % 360.0f;

            var rot = boxRenderer.transform.rotation.eulerAngles;
            rot.Set(25.0f, 0.0f, NewRotation);
            boxRenderer.transform.rotation = Quaternion.Euler(rot);
            counter += Time.deltaTime;

            yield return new WaitForFixedUpdate();
		}

        boxRenderer.transform.rotation = Quaternion.Euler(new Vector3(25.0f, 0.0f, 0.0f));
        transform.position = new Vector3(264.0f, 1.0f, 5.5f);
        rb.velocity = Vector3.zero;
    }

    public IEnumerator End()
    {
        float counter = 0.0f;
        float max = 5.0f;

        while (counter < max)
        {
            float NewRotation = (540.0f * Time.deltaTime) + boxRenderer.transform.rotation.eulerAngles.z;
            NewRotation = NewRotation % 360.0f;

            var rot = boxRenderer.transform.rotation.eulerAngles;
            rot.Set(25.0f, 0.0f, NewRotation);
            boxRenderer.transform.rotation = Quaternion.Euler(rot);
            counter += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator Rescued()
    {
        float counter = 0.0f;

        yield return new WaitForSeconds(2.0f);

        StartCoroutine(End());
        helpController.Saved();
        while (counter < 5.0f)
        {
            boxRenderer.transform.position = boxRenderer.transform.position + new Vector3(-1.0f, 1.0f, 0.0f) * 5.0f * Time.deltaTime;
            counter += Time.deltaTime;
            yield return new WaitForFixedUpdate();
		}
	}
}
