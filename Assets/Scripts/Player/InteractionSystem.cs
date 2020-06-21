using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private int _distance;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private AudioSource _as;
    [SerializeField] private AudioClip _pickUpSound;
    [Inject] private FeedbackSystem _feedbackSystem;
    [Inject] private Inventory _inventory;
    private Transform _camTransform;
    private RaycastHit hit;
    private Dictionary<string, Action> _actionMap = new Dictionary<string, Action>();

    private int layerMask = 1 << 10;

    private void Update()
    {
        CheckRayCollision();
    }

    private void Start()
    {
        layerMask = ~layerMask;

        _actionMap.Add("Key", () => _inventory.AddItem(hit.collider.GetComponent<PickupableItemTag>()));
        _actionMap.Add("Health",() => GetComponent<HealthComponent>().Heal(hit.collider.gameObject.GetComponent<HealthTag>().healAmount));
        _actionMap.Add("Ammo", () => _inventory.AddAmmo(hit.collider.gameObject.GetComponent<AmmoTag>().ammoAmount));
    }


    private void CheckRayCollision()
    {
        _camTransform = _mainCamera.transform;
        if (!Physics.Raycast(_camTransform.position, _camTransform.TransformDirection(Vector3.forward), out hit,
            _distance, layerMask))
        {
            _feedbackSystem.HideFeedback();
            return;
        }

        if (IsSeeingPickupable())
        {
            _feedbackSystem.ShowFeedback("Press F to interact");
        }
        else
        {
            _feedbackSystem.HideFeedback();
        }


        if (!Input.GetKeyDown(KeyCode.F)) return;

        PickupItem("Key");
        PickupItem("Health");
        PickupItem("Ammo");
    }

    private void PickupItem(string name)
    {
        if (!IsTag(name)) return;
        _actionMap[name].Invoke();
        Destroy(hit.collider.gameObject);
        _feedbackSystem.HideFeedback();
    }

    private bool IsSeeingPickupable()
    {
        return IsTag("Key") || IsTag("Health") || IsTag("Ammo");
    }

    private bool IsTag(string name)
    {
        return hit.collider.CompareTag(name);
    }
}