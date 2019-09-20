using UnityEngine;

[ExecuteInEditMode]
public class RatioForcer : MonoBehaviour
{
    void Start()
    {
        Camera.main.aspect = 9f / 16f;      // Forces camera aspect ratio to phone portrait
    }
}
