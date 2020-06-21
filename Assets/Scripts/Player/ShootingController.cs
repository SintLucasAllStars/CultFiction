using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShootingController : MonoBehaviour
{
    public Slider reloadBar;
    public GameObject spawnPoint;
    public float spawnRate;
    public bool isShooting;
    public bool isAiming;
    [Inject] private BulletPool _bulletPool;
    [Inject] private FeedbackSystem _feedbackSystem;
    [Inject] private Inventory _inventory;
    [SerializeField] private int _damage;
    private bool _isShooting;
    private bool _isAiming;
    private float timer = 2;
    private Color _playerColor = new Color(0.91764f, 1, 1);

    private void Update()
    {
        isAiming = Input.GetMouseButton(1);
        timer += Time.deltaTime;

        if (Input.GetMouseButton(1) && Input.GetMouseButton(0))
        {
            UpdateShootingControls();
        }
        else
        {
            isShooting = false;
        }

        UpdateReloadBar();
    }

    private void UpdateReloadBar()
    {
        reloadBar.maxValue = spawnRate;
        reloadBar.value = timer;
    }

    private void UpdateShootingControls()
    {
        if (_inventory.ammo <= 0)
        {
            _feedbackSystem.PlayWarning("No Ammo", 2);
            return;
        }

        isShooting = true;
        if (timer <= spawnRate)
        {
            return;
        }

        Bullet bullet = _bulletPool.GetBullet(_playerColor);
        _inventory.RemoveAmmo();
        bullet.SetDamage(_damage);
        bullet.SetDirection(Camera.main.transform.forward);
        bullet.transform.position = spawnPoint.transform.position;
        timer = 0;
    }
}