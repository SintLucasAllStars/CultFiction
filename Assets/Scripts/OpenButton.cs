using UnityEngine;
using UnityEngine.UI;

public class OpenButton : GameButton
{
    [SerializeField]
    private Sprite[] _buttonStatuses = null;

    private Image _image;

    private void Start() => _image = GetComponent<Image>();

    public override void Press()
    {
        base.Press();
        GameManager.Instance.ToggleOpen();
        _image.sprite = (GameManager.Instance.IsOpened) ? _buttonStatuses[1] : _buttonStatuses[0];
    }
}
