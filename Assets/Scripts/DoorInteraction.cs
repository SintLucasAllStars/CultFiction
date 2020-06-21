using System.Collections;
using UnityEngine;
using Zenject;

public class DoorInteraction : InteractableObject
{
    public bool _locked;
    private bool _canInteract;
    private bool _isOpen;
    private Animator _animator;
    private Animation _animation;
    private string _animationName;
    private bool isMoving;

    [SerializeField] private Vector3 newV;
    [SerializeField] private Vector3 originalV;
    [SerializeField] private float speed;
    [SerializeField] private string _keyName;
    [Inject] private FeedbackSystem _feedbackSystem;
    [Inject] private Inventory _inventory;

    private void Start()
    {
        _isOpen = false;
    }

    private void Update()
    {
        CheckFeedback();
        CheckInteraction();
    }

    private void CheckFeedback()
    {
        if (!_canInteract) return;

        if (_isOpen)
        {
            _feedbackSystem.ShowFeedback("E to close");
        }
        else _feedbackSystem.ShowFeedback("E to open");
    }

    private void CheckInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E) && _canInteract && !isMoving)
        {
            if (_locked)
            {
                CheckIfHasKey();
                return;
            }

            if (_isOpen)
            {
                StartCoroutine(CloseDoor());
            }
            else StartCoroutine(OpenDoor());
        }
    }

    private void CheckIfHasKey()
    {
        if (_inventory.HasItem(_keyName))
        {
            StartCoroutine(OpenDoor());
            _locked = false;
            CheckInteraction();
        }
        else
        {
            _feedbackSystem.PlayWarning("Locked", 2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MovementController>())
        {
            _canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _canInteract = false;
        _feedbackSystem.HideFeedback();
    }

    private IEnumerator OpenDoor()
    {
        isMoving = true;
        while (gameObject.transform.position != newV)
        {
            Open();
            yield return null;
        }

        isMoving = false;
    }

    private IEnumerator CloseDoor()
    {
        isMoving = true;
        while (gameObject.transform.position != originalV)
        {
            Close();
            yield return null;
        }

        isMoving = false;
    }

    private void Open()
    {
        _isOpen = true;

        gameObject.transform.position =
            Vector3.MoveTowards(gameObject.transform.position,
                newV,
                speed * Time.deltaTime);
    }

    private void Close()
    {
        _isOpen = false;

        gameObject.transform.position =
            Vector3.MoveTowards(gameObject.transform.position,
                originalV,
                speed * Time.deltaTime);
    }
}