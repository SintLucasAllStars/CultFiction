using UnityEngine;

public class ObjectContainer : Interactable
{
    [SerializeField]
    private ObjectType _objectType = ObjectType.Table;
    public ObjectType ObjectType => _objectType;

    protected Animator _animator = null;

    [SerializeField]
    protected SpriteRenderer _location = null;

    protected GameObject _occupiedBy = null;

    public virtual bool IsUsed => _occupiedBy;
    public virtual bool IsEnabled => _animator.GetBool("isEnabled");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        Enable(false);

        ObjectManager.Instance.FindLocations();
    }

    public virtual void OnDrop(GameObject obj)
    {
        Enable(false);

        ObjectManager.Instance.ShowAllLocations(false);
        ObjectManager.Instance.SetMode(ObjectMode.Normal);
    }

    public void Enable(bool enabled) => _animator.SetBool("isEnabled", enabled);

    protected override bool Interact()
    {
        if (!base.Interact())
            return false;
        if (!ObjectManager.Instance.InObjectMode())
            return false;

        OnDrop(ObjectManager.Instance.Object);

        return true;
    }
}
