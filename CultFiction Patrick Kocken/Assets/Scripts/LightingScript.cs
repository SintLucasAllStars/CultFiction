using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingScript : MonoBehaviour {

    [SerializeField] private Light _playerLight;
    [SerializeField] private Light _worldLight;

    [SerializeField] private float _fadeTime;

    public void StartChangingIntensity (float playerLightIntensityTarget,float worldLightIntensityTarget)
    {
        StartCoroutine(Fade(playerLightIntensityTarget,worldLightIntensityTarget));
    }

    //Fades the Lighting with a Coroutine
    private IEnumerator Fade(float playerLightIntensityTarget,float worldLightIntensityTarget)
    {
        yield return new WaitForSecondsRealtime(2);

        float start = Time.time;
        float elapsed = 0;

        while (elapsed < _fadeTime)
        {
            elapsed = Time.time - start;

            float normalisedTime = Mathf.Clamp(elapsed / _fadeTime, 0, 5);

            _playerLight.intensity = Mathf.Lerp(_playerLight.intensity, playerLightIntensityTarget, normalisedTime);
            _worldLight.intensity = Mathf.Lerp(_worldLight.intensity, worldLightIntensityTarget, normalisedTime);

            yield return null;
        }
    }
}
