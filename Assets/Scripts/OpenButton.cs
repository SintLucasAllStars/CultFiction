using UnityEngine;
using UnityEngine.UI;

public class OpenButton : GameButton
{
    public override void Press()
    {
        base.Press();
        GameManager.Instance.Open();

        if (!GameManager.Instance.IsOpened)
            return;

        Destroy(this.gameObject);
    }
}
