using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int _stoneCount;
    private int _coconutCount;
    private float _mousePressStart;

    public Slider powerSlider;
    public GameObject stonePrefab;
    void Start()
    {
        _stoneCount = 0;
        _coconutCount = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _stoneCount > 0)
        {
            _mousePressStart = Time.time;
        }
        if (Input.GetMouseButtonUp(0) && _mousePressStart > 0)
        {
            float pressTime = Time.time - _mousePressStart;
            _mousePressStart = 0;
            powerSlider.value = 0;
            SchootStone(pressTime);
        }
        if (_mousePressStart > 0)
        {
            float pressTime = Time.time - _mousePressStart;
            powerSlider.value = pressTime;
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Stone"))
        {
            Destroy(coll.gameObject);
            _stoneCount++;
        }
        if (coll.gameObject.CompareTag("Coconut"))
        {
            Destroy(coll.gameObject);
            _coconutCount++;
        }
    }

    private void SchootStone(float power)
    {
        if (power > 1)
        {
            power = 1;
        }

        _stoneCount--;
        Vector3 pos = Camera.main.transform.position;
        pos.y += 3;
        GameObject stone = Instantiate(stonePrefab, pos, Quaternion.identity);
        Rigidbody rigidbody = stone.GetComponent<Rigidbody>();
        //rigidbody.AddForce(0, 5000*power, 0);
        Vector3 velocity = Camera.main.transform.forward * 1000;
        Debug.Log($"X: {velocity.x}, Y: {velocity.y}, Z: {velocity.z}");
        rigidbody.AddForce(velocity*power);
    }
    
}