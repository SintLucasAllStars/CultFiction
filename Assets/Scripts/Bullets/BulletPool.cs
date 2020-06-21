using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int desiredAmount = 25;
    private GameObject _container;

    private Queue<Bullet> _unused = new Queue<Bullet>();

    private readonly List<Bullet> _used = new List<Bullet>();

    private void Awake()
    {
        InitializeBulletPool("bullets");
    }

    private void InitializeBulletPool(string containerName)
    {
        _container = new GameObject(containerName);
        for (int i = 0; i < desiredAmount; i++)
        {
            Bullet b = Instantiate(_bulletPrefab, _container.transform).GetComponent<Bullet>();
            b.bulletPool = this;
            _unused.Enqueue(b);
            b.gameObject.SetActive(false);
        }
    }

    public Bullet GetBullet(Color bulletColor)
    {
        if (_unused.Count > 0)
        {
            Bullet bullet = _unused.Dequeue();
            bullet.SetColor(bulletColor);
            bullet.gameObject.SetActive(true);
            _used.Add(bullet);
            return bullet;
        }
        else
        {
            GameObject go = Instantiate(_bulletPrefab);
            Bullet bullet = go.GetComponent<Bullet>();
            bullet.bulletPool = this;
            bullet.SetColor(bulletColor);
            _used.Add(bullet);
            go.SetActive(true);
            Debug.LogWarning("There is a new instances being created, you might want to up the desiredAmount value");
            return bullet;
        }
    }

    public void DestroyBullet(Bullet bullet)
    {
        _used.Remove(bullet);
        _unused.Enqueue(bullet);
        bullet.gameObject.SetActive(false);

        while (_unused.Count > desiredAmount)
        {
            _unused.Dequeue();
        }
    }
}