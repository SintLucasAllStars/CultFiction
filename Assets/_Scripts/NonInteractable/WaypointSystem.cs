using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class WaypointSystem : MonoBehaviour {


    public List<Transform> waypoints = new List<Transform> ();

    int index = 0;

    public bool disableInGame;

    void Update () {
	


			
        if (!Application.isPlaying) {
            Transform[] tem = GetComponentsInChildren<Transform> ();

            if (tem.Length > 0) {
                waypoints.Clear ();

                index = 0;

                foreach (Transform t in tem) {
                    if (t != transform) {
					

                        t.name = "Waypoint " + index.ToString ();

                        waypoints.Add (t);

                        index++;
                    }
                }

            }
        }
    }


    void OnDrawGizmos()
    {

        if (waypoints.Count > 0) {

            Gizmos.color = Color.green;

            foreach (Transform t in waypoints)
                Gizmos.DrawSphere (t.position, 1f);

            Gizmos.color = Color.red;

            for (int a = 0; a < waypoints.Count - 1; a++)
                Gizmos.DrawLine (waypoints [a].position, waypoints [a + 1].position);
        }
    }
}