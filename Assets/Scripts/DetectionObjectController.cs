using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionObjectController : MonoBehaviour
{
    public LevelManagement lm;

    public GameObject cameraLockObject;

    public Vector3 target;

    public bool activated;
    public bool tutorial;

    void Start()
    {
        lm = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        target = transform.position + Vector3.right * 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!lm)
        {
            lm = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        }
    }

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player") && !activated)
        {
            activated = true;

            var tar = Instantiate(cameraLockObject, target, Quaternion.identity);

            if (lm)
            {
                lm.spawnTarget = target;
                lm.cam.GetComponent<CameraMovement>().lockOn = tar.transform;
                lm.cam.GetComponent<CameraMovement>().lockedOn = true;
                lm.inEncounter = true;
                StartCoroutine(tutorial ? lm.SpawnTutorialEncounter() : lm.SpawnEncounter());
            }
		}
	}
}
