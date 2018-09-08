using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

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
                if (colliderTag == "EditorOnly")
                {
                    /* 
                    _targetEntity = raycastHit.collider.gameObject.GetComponent<Entity>();

                    _uiScript.ShowPlayerActions(_targetEntity);
                    _destination = RoundMousePosition(_targetEntity.transform.position);
                    */
                }
                else
                {
                    /*
                    _uiScript.ShowPlayerActions(null);
                    _destination = RoundMousePosition(raycastHit.point);
                     */
                }

            }
            else
            {
                if (colliderTag == "EditorOnly")
                {
                    /*/
                    Entity entity = raycastHit.collider.gameObject.GetComponent<Entity>();
                    gridPointerTransform.position = entity.transform.position;
                    _uiScript.ShowObjectViewer(true, entity.name, entity.HP, entity.MaxHP);
                    */
                }
                else
                {
                    _gridPointer.position = RoundMousePosition(raycastHit.point);
                    //_uiScript.ShowObjectViewer(false);
                }
            }

        }
        else
        {
            _gridPointer.position = _gridPointerResetPos;
        }
    }
    private Vector3 RoundMousePosition(Vector3 mousePos)
    {

        //Debug.Log("G");

        if (mousePos.x > -2.5f && mousePos.x < 3)
        {
            if (mousePos.x % 1 > 0 && mousePos.x % 1 < .5f)
                mousePos.x = Mathf.Round(mousePos.x) ;
            else
                mousePos.x = Mathf.Round(mousePos.x) ;
        }
        else
        {
            if (mousePos.x > 0)
                mousePos.x = 3f;
            else
                mousePos.x = -3f;
        }

        if (mousePos.z > 0f)
        {
            if (mousePos.z % 1 > 0 && mousePos.z % 1 < .5f)
                mousePos.z = Mathf.Round(mousePos.z);
            else
                mousePos.z = Mathf.Round(mousePos.z) ;
        }
        else{
            mousePos.z = Mathf.Round(mousePos.z);
        }

        Vector3 roundedPosition = new Vector3(mousePos.x,0, mousePos.z);
        return roundedPosition;
    }
}
