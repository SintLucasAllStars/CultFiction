using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using System.Collections;

[Serializable]
public class ChestState {
	public Vector3 angle;
	public AudioClip sound;
	public float speed;
}

public class SkullChest : MonoBehaviour
{
	public List<ChestState> targetAngles;
	public Transform top;
	private AudioSource audioSource;
	[Tooltip("Allows you to limit the angle to the camera under which a mousedown is accepted. -1 is from everywhere, 0 is from 90 degrees to the side and upward, 0-1 is limited to a cone to the front.")]
	public float acceptLeeway = 0;

    public GameObject player;
    public RayCaster RC;

    bool waitsec = true;

    private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void OnMouseDown()
	{
		float dot = Vector3.Dot(transform.right.normalized, (Camera.main.transform.position - transform.position).normalized);
		if (dot < acceptLeeway) return;

		if (targetAngles != null && targetAngles.Count > 0)
		{
			targetAngles.Add(targetAngles[0]);
			targetAngles.RemoveAt(0);

			AudioClip clip = targetAngles[0].sound;
			if (clip != null)
			{
				audioSource.pitch = Random.Range(0.8f, 1.1f);
				audioSource.volume = Random.Range(0.8f, 1.0f);
				audioSource.PlayOneShot(clip);
			}
		}

        RC.level++;

        if(RC.level == 2)
        {
            StartCoroutine(ChestTP());
        }
        if (RC.level > 3)
        {
            StartCoroutine(ChestTPEnd());
        }
    }

	// Update is called once per frame
	void Update()
    {
		ChestState state = targetAngles[0];
		top.transform.localRotation = Quaternion.Slerp(top.transform.localRotation, Quaternion.Euler(state.angle), state.speed * 0.5f * Time.deltaTime);
    }

    IEnumerator ChestTP()
    {
        if(waitsec == true)
        {
            waitsec = false;
            yield return new WaitForSeconds(2);
            player.transform.position = new Vector3(487.6015f, 20, 468.8332f);
            player.transform.rotation = new Quaternion(0, 1.6f, 0, 0);
            waitsec = true;
        }
    }

    IEnumerator ChestTPEnd()
    {
        if (waitsec == true)
        {
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("End");
            waitsec = false;
        }
    }
}
