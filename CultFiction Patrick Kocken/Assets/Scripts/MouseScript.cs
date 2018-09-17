using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour {

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _gridPointer;

    private Vector3 _gridPointerResetPos;


   private void Start()
   {
        _gridPointerResetPos = _gridPointer.position;
    }
    private void Update()
    {

        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(raycast, out raycastHit, Mathf.Infinity))
        {
            string colliderTag = raycastHit.collider.tag;
            if (Input.GetMouseButtonDown(0))
            {
                if (colliderTag == "Floor" && _playerController.PlayerIsNotMoving)
                    _playerController.StartWalking(RoundMousePosition(raycastHit.point));
            }
            else
                    _gridPointer.position = RoundMousePosition(raycastHit.point);
        }
        else
            _gridPointer.position = _gridPointerResetPos;
    }
    private Vector3 RoundMousePosition(Vector3 mousePos)
    {
        if (mousePos.x > -2.5f && mousePos.x < 3)
        {
            if (mousePos.x % 1 > -1.5f && mousePos.x % 1 < .5f)
                mousePos.x = Mathf.Round(mousePos.x) + .5f;
            else
                mousePos.x = Mathf.Round(mousePos.x) - .5f;
        }
        else
        {
            if (mousePos.x > 0)
                mousePos.x = 2.5f;
            else
                mousePos.x = -2.5f;
        }

        if (mousePos.z > 0f)
        {
            if (mousePos.z % 1 > -1.5f && mousePos.z % 1 < .5f)
                mousePos.z = Mathf.Round(mousePos.z);
            else
                mousePos.z = Mathf.Round(mousePos.z) ;
        }
        else{
            mousePos.z = Mathf.Round(mousePos.z);
        }

        Vector3 roundedPosition = new Vector3(mousePos.x ,0, mousePos.z);
        return roundedPosition;
    }
}
