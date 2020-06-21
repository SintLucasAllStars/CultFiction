using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TurretBehavior : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _detectionDistance;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private Transform _firePoint;
    [Inject] private BulletPool _bulletPool;
    private GameObject _rotator;
    private Transform _target;
    private float rotateDelayTime;
    private float lastTimeShot;

    private Quaternion _lookRotation;

    [SerializeField] private Slider healthBar;
    [SerializeField] private HealthComponent health;
    private MeshRenderer[] _meshes;
    public ParticleSystem psGunShot;
    public ParticleSystem explosion;

    private void Start()
    {
        _meshes = GetComponentsInChildren<MeshRenderer>();
        _rotator = GetComponentInChildren<RotatorTag>().gameObject;
        _target = FindObjectOfType<PlayerTag>().transform;
        healthBar.maxValue = health.maxHealth;
    }

    private void Update()
    {
        CheckTurretHealth();
        UpdateHealthBarRotation();

        if (DetectPlayer())
        {
            RotateToPlayer();
            StartShooting();
        }
        else
        {
            SearchForPlayer();
        }
    }

    private void UpdateHealthBarRotation()
    {
        Vector3 viewDirection = _target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(viewDirection);
        healthBar.transform.rotation = rotation;
    }

    private void CheckTurretHealth()
    {
        healthBar.value = health.currentHealth;

        if (health.currentHealth <= 0)
        {
            explosion.Play();
            GetComponentInChildren<AudioSource>().Play();
            foreach (MeshRenderer mesh in _meshes)
            {
                mesh.enabled = false;
            }

            healthBar.gameObject.SetActive(false);
            this.enabled = false;
        }
    }

    private void StartShooting()
    {
        if (lastTimeShot > fireRate)
        {
            psGunShot.Play();
            Bullet b = _bulletPool.GetBullet(Color.red);
            b.SetDamage(_damage);
            b.transform.position = _firePoint.position;
            b.SetDirection(_rotator.transform.forward);
            lastTimeShot = 0;
        }
        else
        {
            lastTimeShot += Time.deltaTime;
        }
    }

    private void SearchForPlayer()
    {
        rotateDelayTime += Time.deltaTime;
        if (rotateDelayTime < 5)
        {
            RotateTurret(_rotator.transform.position + Vector3.right, _rotationSpeed);
        }
        else if (rotateDelayTime > 5 && rotateDelayTime < 10)
        {
            RotateTurret(_rotator.transform.position + Vector3.left, _rotationSpeed);
        }
        else
        {
            rotateDelayTime = 0;
        }
    }

    private void RotateToPlayer()
    {
        var firePoint = _firePoint.position;
        var targetPosition = _target.position + (Vector3.up * 2f);
        float distance = Vector3.Distance(firePoint, targetPosition);
        if (distance < _detectionDistance)
        {
            RotateTurret(targetPosition, _rotationSpeed * 2);
        }
    }

    private bool DetectPlayer()
    {
        RaycastHit hit;
        var targetPosition = _target.position;
        var firePoint = _firePoint.position;
        if (!Physics.Raycast(firePoint, targetPosition - firePoint + Vector3.up, out hit, _detectionDistance)) return false;
        if (hit.collider.GetComponent<PlayerTag>())
        {
            return true;
        }

        return false;
    }

    private void RotateTurret(Vector3 target, float speed)
    {
        Vector3 dir = (target - transform.position).normalized;
        Quaternion newRot = Quaternion.LookRotation(dir, _rotator.transform.up);

        _rotator.transform.rotation = Quaternion.Slerp(_rotator.transform.rotation, newRot,
            speed * Time.deltaTime);

        _rotator.transform.localEulerAngles = new Vector3(_rotator.transform.localEulerAngles.x,
            _rotator.transform.localEulerAngles.y, 0f);
    }
}