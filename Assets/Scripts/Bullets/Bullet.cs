using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    public BulletPool bulletPool;
    private int _damage;
    private MeshRenderer _meshR;
    private TrailRenderer _trailR;
    private Vector3 _direction;

    private void Awake()
    {
        _meshR = gameObject.GetComponent<MeshRenderer>();
        _trailR = gameObject.GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * _direction);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<HealthComponent>())
        {
            other.gameObject.GetComponent<HealthComponent>().TakeDamage(_damage);
        }

        bulletPool.DestroyBullet(this);
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    public void SetColor(Color bulletColor)
    {
        _meshR.materials[0].color = bulletColor;
        _trailR.materials[0].color = bulletColor;
    }

    public void SetDirection(Vector3 dir)
    {
        _direction = dir;
    }
}