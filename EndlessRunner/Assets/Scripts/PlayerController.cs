using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField]
    private GameObject _mesh;

    [SerializeField]
    private GameObject _mesh2;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _lerpSpeed;

    [SerializeField]
    private List<Vector3> _lanes;

    [SerializeField]
    private List<GameObject> _liveSprites;

    [SerializeField]
    private int _maxHealth;

    private int _health;
    private bool _canMove = true;

    private void Start()
    {
        _health = _maxHealth;
    }

    void Update()
    {
        _mesh.transform.LookAt(new Vector3(_mesh.transform.position.x + Input.GetAxis("Horizontal"), _mesh.transform.position.y, _mesh.transform.position.z + 1));

        if (_canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                Movement();
                return;
            }

            //transform.position = Vector3.LerpUnclamped(ClosestLane(), transform.position, Time.deltaTime * _lerpSpeed);
        }
    }

    private void Movement()
    {
        if ((Input.GetAxis("Horizontal") < 0 && transform.position.x > -2) || (Input.GetAxis("Horizontal") > 0 && transform.position.x < 2))
        {
            transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * _speed, 0, 0);
        }
    }

    private Vector3 ClosestLane()
    {
        Vector3 closestLane = Vector3.zero;
        float smallestDistance = 0;

        for (int i = 0; i < _lanes.Count; i++)
        {
            float currentDistance = Vector3.Distance(_lanes[i], transform.position);
            if (closestLane == Vector3.zero || smallestDistance > currentDistance)
            {
                closestLane = _lanes[i];
                smallestDistance = currentDistance;
            }
        }
        return closestLane;
    }

    private void TakeDamage(int damage)
    {
        _health -= damage;
        for (int i = 0; i < _maxHealth - _health; i++)
        {
            _liveSprites[i].SetActive(false);
        }

        if (_health <= 0)
        {
            Die();
            _canMove = false;
            return;
        }

        StartCoroutine(DamageIndicator());
    }

    private void Die()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(1);
        ScoreManagement.Instance.ToggleUI();
    }

    private IEnumerator DamageIndicator()
    {
        if (_mesh == true)
        {
            gameObject.GetComponent<Collider>().enabled = false;

            for (int i = 0; i < 60; i++)
            {
                gameObject.GetComponent<Collider>().enabled = false;
                _mesh2.SetActive(false);
                yield return new WaitForEndOfFrame();
                _mesh2.SetActive(true);
                yield return new WaitForEndOfFrame();
            }

            gameObject.GetComponent<Collider>().enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();

        if (obstacle != null)
        {
            Debug.Log("collide");
            TakeDamage(obstacle._damage);
        }
    }
}
