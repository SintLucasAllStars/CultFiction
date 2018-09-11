
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//[CustomEditor(typeof(Tool))]
public class SpawnTool : EditorWindow
{
    RaycastHit hit;
    GameObject ter;

    Object terrain;
    public Object[] objectsToSpawn;
    Object parent;
    GameObject par;
    float objectCount;
    float minVal = -0.5f;
    float minLimit = -1;
    float maxVal = 0.5f;
    float maxLimit = 2;

    float minValRot = 90;
    float minLimitRot = 0;
    float maxValRot = 180;
    float maxLimitRot = 360;
    bool scaleEnabled;
    bool rotationEnabled;

    float startY = 50;
    float rayLength = 50;
	float maxSurfaceAngle =45;
    int treeCounter;
    Vector3 size;
    Vector3 randomPos;
    Vector2 scrollPos;
	Vector3 offset;
    bool check;
    string tagStr = "";
	List<GameObject> spawnedTrees = new List<GameObject>();
    SerializedProperty objectsToSpawnProperty;
    [MenuItem("Window/Spawn Tool")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SpawnTool));
    }
    void OnGUI()
    {
        GUILayout.BeginVertical();
        scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);

        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        terrain = EditorGUILayout.ObjectField("Terrain", terrain, typeof(Object), true);
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("objectsToSpawn");

        EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
        so.ApplyModifiedProperties();
        parent = EditorGUILayout.ObjectField("Parent", parent, typeof(Object), true);
		objectCount = EditorGUILayout.Slider("Objects to spawn", objectCount, 1, 1000);
		offset = EditorGUILayout.Vector3Field("Offset", offset);
        startY = EditorGUILayout.FloatField("Highest Point (Y)", startY);
		rayLength = EditorGUILayout.FloatField("Ray length", rayLength);
		maxSurfaceAngle = EditorGUILayout.FloatField("Max surface angle", maxSurfaceAngle);
        tagStr = EditorGUILayout.TagField("Objects to avoid:", tagStr);
        ter = terrain as GameObject;
        scaleEnabled = EditorGUILayout.BeginToggleGroup("RandomScale", scaleEnabled);
        EditorGUILayout.LabelField("Min Val Scale:", minVal.ToString());
        EditorGUILayout.LabelField("Max Val Scale:", maxVal.ToString());
        EditorGUILayout.MinMaxSlider(ref minVal, ref maxVal, minLimit, maxLimit);

        EditorGUILayout.EndToggleGroup();
        rotationEnabled = EditorGUILayout.BeginToggleGroup("RandomRotation", rotationEnabled);
        EditorGUILayout.LabelField("Min Val Rotation:", minValRot.ToString());
        EditorGUILayout.LabelField("Max Val Rotation:", maxValRot.ToString());
        EditorGUILayout.MinMaxSlider(ref minValRot, ref maxValRot, minLimitRot, maxLimitRot);

        EditorGUILayout.EndToggleGroup();
        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        if (GUILayout.Button("Spawn Objects!"))
        {
            spawnedTrees.Clear();
            treeCounter = 0;
            check = false;
            EditorApplication.update += EditorUpdate;
        }
     //   if (GUILayout.Button("Reset Counter"))
    //    {
     //       Debug.Log(treeCounter);
    //        treeCounter = 0;
     //   }
        if (GUILayout.Button("Undo"))
        {
            foreach (GameObject g in spawnedTrees)
            {
                DestroyImmediate(g);
            }
        }
    }

    void EditorUpdate()
    {
        if(EditorUtility.DisplayCancelableProgressBar("Spawning Objects... ", + treeCounter + "/" + objectCount, ((float)treeCounter / (float)objectCount))){
            EditorUtility.ClearProgressBar();
            EditorApplication.update -= EditorUpdate;
        }
        if (objectCount > treeCounter)
        {
            MeshRenderer mesh = ter.GetComponent<MeshRenderer>();
            float xPos = ter.transform.position.x;
            float zPos = ter.transform.position.z; size = new Vector3(mesh.bounds.size.x - 10, mesh.bounds.size.y, mesh.bounds.size.z - 10);
            randomPos = new Vector3(Random.Range(xPos - (size.x / 2), xPos + (size.x / 2)), startY, Random.Range(zPos - (size.z / 2), zPos + (size.z / 2)));
            GameObject spawnTreeP = PrefabUtility.InstantiatePrefab(objectsToSpawn[Random.Range(0,objectsToSpawn.Length)]) as GameObject;
            spawnedTrees.Add(spawnTreeP);
			if (par != null)
			{
				spawnTreeP.transform.parent = par.transform;
			}
            treeCounter++;
            spawnTreeP.transform.position = randomPos;
            spawnTreeP.transform.rotation = ter.transform.rotation;
            Ray disToGround = new Ray(spawnTreeP.transform.position, Vector3.down);
            float w = Random.Range(minVal, maxVal);
            if (scaleEnabled)
            {
                spawnTreeP.transform.localScale += new Vector3(w, w, w);

            }
            if (Physics.Raycast(disToGround, out hit, rayLength))
            {//last number is the distance
				if (hit.transform.CompareTag("TreesTag") ||(tagStr !=null && hit.transform.CompareTag(tagStr)))
				{
					DestroyImmediate(spawnTreeP);
					Debug.Log("Destroyed object");
					treeCounter--;
				}
				else
				{
					if (Vector3.Angle(hit.normal, Vector3.down) - 90 > maxSurfaceAngle)
					{                  
						spawnTreeP.transform.position = hit.point;

						if (rotationEnabled)
						{
							spawnTreeP.transform.rotation = Quaternion.AngleAxis(Random.Range(minValRot, maxValRot), Vector3.up);

						}
						if (parent != null)
						{
							if (!check)
							{

								par = parent as GameObject;
								check = true;
							}
							else
							{
								check = false;
							}
						}

					}else{
						DestroyImmediate(spawnTreeP);
                        treeCounter--;
					}
				}
            }
            else
            {
				DestroyImmediate(spawnTreeP);
                treeCounter--;

            }


        }
        else
        {
            EditorUtility.ClearProgressBar();
            Debug.Log("done!");
            treeCounter = 0;
            check = false;
            EditorApplication.update -= EditorUpdate;
        }

    }
   
}




