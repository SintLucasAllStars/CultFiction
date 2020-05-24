using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float camZOffset = 10;

    // Update is called once per frame
    void Update()
    {
        Vector3 newLocation = new Vector3(player.transform.position.x, transform.position.y, 0 );


        transform.position = Vector3.Lerp(transform.position, newLocation, Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z - camZOffset);
    }
}
