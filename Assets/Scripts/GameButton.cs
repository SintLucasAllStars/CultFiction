using UnityEngine;

public class GameButton : MonoBehaviour
{
    public virtual void Press() => SoundEffectManager.Instance.PlaySound(SoundEffectName.Button);
}
