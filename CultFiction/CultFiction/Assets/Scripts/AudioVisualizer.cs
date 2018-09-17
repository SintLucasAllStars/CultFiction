using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizer : MonoBehaviour {

    private const int sampleSize = 1024;
    public float rmsValue, dbValue;
    private float referenceValue = 0.1f;

    public float maxVisualScale = 25.0f;
    public float visualModifier = 5.0f;
    public float smoothSpeed = 10.0f;
    public float keepPercentage = 0.5f;

    AudioSource source;
    private float[] samples;
    private float[] spectrum;
    private float sampleRate;

    public bool isHit;
    float pitchShift = 0.005f;
    public Transform[] spawnPoints;
    public Transform[] visuals;
    public GameObject prefab;
    float[] visualScale;
    private int visualAmount = 10;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        samples = new float[sampleSize];
        spectrum = new float[sampleSize];
        sampleRate = AudioSettings.outputSampleRate;
        SpawnLine();
        source.pitch = 1f;
	}
	void SpawnLine()
    {
        visualScale = new float[visualAmount];
        visuals = new Transform[visualAmount];

        for (int i = 0; i < visualAmount; i++)
        {
            GameObject go = Instantiate(prefab,spawnPoints[i].position, Quaternion.identity);
            visuals[i] = go.transform;
            visuals[i].position = spawnPoints[i].position;
        }
    }
	// Update is called once per frame
	void Update () {
        if (!isHit)
        {
            SoundAnalyzer();
            VisualUpdate();
        }
        else
        {
            if(source.pitch > 0f)
            source.pitch -= pitchShift;
        }

        
	}

    void VisualUpdate()
    {
        int visualIndex = 0;
        int spectrumIndex = 0;
        int averageSize = (int)((sampleSize * keepPercentage) / visualAmount);

        while (visualIndex < visualAmount)
        {
            int j = 0;
            float sum = 0;

            while(j < averageSize)
            {
                sum += spectrum[spectrumIndex];
                spectrumIndex++;
                j++;
            }

            float scaleY = sum / averageSize * (visualModifier * 10f);
            visualScale[visualIndex] -= Time.deltaTime * -smoothSpeed;


            visualScale[visualIndex] = Mathf.Clamp(visualScale[visualIndex], 0f, scaleY);
            visualScale[visualIndex] = Mathf.Clamp(visualScale[visualIndex], 0f, maxVisualScale);

            visuals[visualIndex].localScale = Vector3.one + Vector3.up * visualScale[visualIndex];
            visualIndex++;
        }

    }

    void SoundAnalyzer()
    {
        source.GetOutputData(samples, 0);

        int i = 0;
        float sum = 0;

        for (i = 0; i < sampleSize; i++)
        {
            sum += samples[i] * samples[i];
        }

        rmsValue = Mathf.Sqrt(sum / sampleSize);

        dbValue = 20 * Mathf.Log10(rmsValue / referenceValue);
        if (dbValue < -160) dbValue = -160;

        source.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

    }
}
