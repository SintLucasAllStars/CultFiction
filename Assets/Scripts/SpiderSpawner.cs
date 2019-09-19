using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawner : MonoBehaviour
{
    [SerializeField]
    private float _minSpawnDelay = 1.0f;

    [SerializeField]
    private float _maxSpawnDelay = 5.0f;

    [SerializeField]
    private GameObject _spider;

    [SerializeField]
    private float _xSpawnMin = 0.0f;

    [SerializeField]
    private float _xSpawnMax = 1.0f;

    [SerializeField]
    private float _zSpawnMin = 0.0f;

    [SerializeField]
    private float _zSpawnMax = 1.0f;

    [SerializeField]
    private float _spawnHeight = 7.5f;

    [SerializeField]
    private float _spiderRepelSpeed = 10.0f;

    [SerializeField]
    private Material _webMaterial;

    public IEnumerator SpawnSpider()
    {
        Vector3 spawnPos = new Vector3(Random.Range(_xSpawnMin, _xSpawnMax), _spawnHeight, Random.Range(_zSpawnMin, _zSpawnMax));
        GameObject spider = Instantiate(_spider, spawnPos, Quaternion.identity);
        RepelSpawnedSpider(spider, spawnPos);
        yield return new WaitForSeconds(Random.Range(_minSpawnDelay, _maxSpawnDelay));

        if (GameManager.Instance.GameIsRunnning)
        {
            StartCoroutine(SpawnSpider());
        }
    }

    private void RepelSpawnedSpider(GameObject spider, Vector3 spawnPos)
    {
        LineRenderer rope = spider.AddComponent<LineRenderer>();

        rope.material = _webMaterial;
        rope.widthMultiplier = 0.025f;
        rope.SetPosition(0, spawnPos);
        rope.SetPosition(1, spider.transform.position);
        StartCoroutine(LerpSpiderMovement(spider, spawnPos, rope));
    }

    private IEnumerator LerpSpiderMovement(GameObject spider, Vector3 spawnPos, LineRenderer rope)
    {
        spider.transform.position = Vector3.Lerp(spider.transform.position, new Vector3(spawnPos.x, 0, spawnPos.z), _spiderRepelSpeed * Time.deltaTime);
        rope.SetPosition(1, spider.transform.position);
        yield return new WaitForEndOfFrame();

        if (spider != null)
        {
            if (spider.transform.position.y > 0.001f)
            {
                StartCoroutine(LerpSpiderMovement(spider, spawnPos, rope));
            }
            else
            {
                Destroy(rope);
            }
        }        
    }
}
