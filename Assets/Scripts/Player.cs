using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int _stoneCount;
    private int _coconutCount;
    private float _mousePressStart;
    private float _currentSecond;
    private int _gameSecondsLeft;

    public Slider powerSlider;
    public GameObject stonePrefab;
    public Text timerText;
    public Text stoneText;
    public Text coconutText;
    
    void Start()
    {
        _stoneCount = 0;
        _coconutCount = 0;
        _currentSecond = 0;
        _gameSecondsLeft = 60;
    }

    void Update()
    {
        HandleStoneTrowing();
        HandleTimer();
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Stone"))
        {
            Destroy(coll.gameObject);
            _stoneCount++;
            stoneText.text = $"{_stoneCount} stone(s)";
        }
        if (coll.gameObject.CompareTag("Coconut"))
        {
            Destroy(coll.gameObject);
            _coconutCount++;
            coconutText.text = $"{_coconutCount} coconut(s)";
        }
    }

    private void HandleStoneTrowing()
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

    private void HandleTimer()
    {
        _currentSecond += Time.deltaTime;

        if (_currentSecond < 1)
        {
            return;
        }
        
        _currentSecond -= 1;
        _gameSecondsLeft--;

        if (_gameSecondsLeft == 0)
        {
            GameManager gameManager = GameManager.GetGameManager();
            gameManager.EndGame(_coconutCount);
            SceneManager.LoadScene("Start");
            return;
        }

        timerText.text = $"{_gameSecondsLeft} second(s)";

        switch (_gameSecondsLeft)
        {
            case 20:
                timerText.color = Color.yellow;
                break;
            case 10:
                timerText.color = Color.red;
                break;
        }
    }

    private void SchootStone(float power)
    {
        if (power > 1)
        {
            power = 1;
        }

        // Update stone counter
        _stoneCount--;
        stoneText.text = $"{_stoneCount} stone(s)";
        
        Camera camera = Camera.main;
        Vector3 pos = camera.transform.position;
        pos.y += 3;
        GameObject stone = Instantiate(stonePrefab, pos, Quaternion.identity);
        Rigidbody rigidbody = stone.GetComponent<Rigidbody>();
        Vector3 velocity = camera.transform.forward * 1000;
        Debug.Log($"X: {velocity.x}, Y: {velocity.y}, Z: {velocity.z}");
        rigidbody.AddForce(velocity*power);
    }
    
}