using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour {

    [SerializeField] private float _fadeTime;

    public void StartFade(){
        StartCoroutine(Fade());
    }
    private IEnumerator Fade()
    {

        yield return new WaitForSecondsRealtime(2);

        MeshRenderer _meshRender = GetComponent<MeshRenderer>();
        Material _material = _meshRender.material;
        Color targetColor = new Color(_material.color.r, _material.color.g, _material.color.b, 0);

        float start = Time.time;
        float elapsed = 0;

        while (elapsed < _fadeTime)
        {
            elapsed = Time.time - start;
            float normalisedTime = Mathf.Clamp(elapsed / _fadeTime, 0, 5);
            _material.color = Color.Lerp(_material.color, targetColor, normalisedTime);
            yield return null;
        }

        Destroy(this.gameObject);

    }


}
