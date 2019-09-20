using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteractable : Interactable
{
    protected Collider2D _collider;

    protected bool _isInRange = false;

    protected void Start()
    {
        _collider = GetComponent<Collider2D>();
        Enable(false);
    }

    protected override bool Interact()
    {
        if (!base.Interact())
            return false;

        if (!_isInRange)
        {
            ErrorMessage.Instance.AnnounceError("Please get closer");
            return false;
        }

        base.Interact();
        return true;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            _isInRange = true;
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            _isInRange = false;
    }

    protected virtual void Enable(bool enable) => _collider.enabled = enable;
}
