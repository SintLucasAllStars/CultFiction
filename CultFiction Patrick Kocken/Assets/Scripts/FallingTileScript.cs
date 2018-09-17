using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTileScript : MonoBehaviour {

    private IEnumerator ShrinkingTile()
    {
        Transform tileTransform = transform.GetChild(0).transform;
		BoxCollider tileDeathCollider = GetComponents<BoxCollider>()[1];

        float start = Time.time;
        float elapsed = 0;
		float fadeTime = 1.0f;

        new WaitForSeconds(2);

        while (elapsed < fadeTime)
        {
            elapsed = Time.time - start;

            float normalisedTime = Mathf.Clamp(elapsed / fadeTime, 0, fadeTime);
            tileTransform.localScale = Vector3.Lerp(tileTransform.localScale, Vector3.zero,normalisedTime);
            yield return null;
        }

        tileDeathCollider.enabled = true;
    }


	private void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.CompareTag("Player"))
        StartCoroutine(ShrinkingTile());
    }
}
