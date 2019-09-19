using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public bool _isFinished;

    [SerializeField]
    private DancingManager _dancingManager;

    private void Update()
    {
        if (_isFinished)
        {
            _dancingManager.Finish();
            _isFinished = false;
        }
    }
}
