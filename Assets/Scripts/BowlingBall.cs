using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    [SerializeField]
    private float _launchForce = 1.0f;

    [SerializeField]
    private float _launchDelay = 1.0f;

    [SerializeField]
    private float _maxOffset = 1f;

    [SerializeField]
    private List<Material> _materials = new List<Material>();

    private Rigidbody _rigidbody;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        ChooseRandomColor();

        StartCoroutine(WaitForLaunch());
    }

    private void ChooseRandomColor()
    {
        GetComponentInChildren<MeshRenderer>().material = _materials[Random.Range(0, _materials.Count)];
    }

    private void LaunchBall()
    {
        Vector3 dir = new Vector3(-_launchForce, 0, Random.Range(-_maxOffset, _maxOffset));
        _rigidbody.AddForce(dir, ForceMode.Impulse);
    }

    private IEnumerator WaitForLaunch()
    {
        yield return new WaitForSeconds(_launchDelay);
        LaunchBall();
    }
}
