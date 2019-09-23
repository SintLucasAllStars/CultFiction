using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutting : MonoBehaviour
{
    public int tester;
    
    public GameObject CuttebleObject;
    List<GameObject> CuttebleObjects = new List<GameObject> ();
    Mesh cuttebleObject;
    Mesh mesh;

    public Transform sword;

    Vector3[] vertices;
    int[] triangles;

    public float attackWith;
    public float attackrange;
    public Transform castOrigin;
    int verticiesAdded = 0;

    public List<int> debugList = new List<int>(); 

    void Start()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {
        if (!coll.gameObject.CompareTag("ignoreCut"))
        {
            CuttebleObjects.Add(coll.gameObject);
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (!coll.gameObject.CompareTag("ignoreCut"))
        {
            CuttebleObjects.Remove(coll.gameObject);
        }
    }





    void Update()
    {
        Debug.DrawRay(castOrigin.position, castOrigin.TransformDirection(Vector3.forward)*attackrange);
        if (Input.GetMouseButtonDown(0))
        {
            for (int n = 0; n < CuttebleObjects.Count; n++)
            {
                CuttebleObject = CuttebleObjects[n];
                

                if (CuttebleObject != null && CuttebleObject.GetComponent<Rigidbody>() != null)
                {
                    cuttebleObject = CuttebleObject.GetComponent<MeshFilter>().mesh;

                    Plane cuttingPlane = new Plane(sword.TransformDirection(Vector3.up), sword.position);


                    List<Vector3> topVerticies = new List<Vector3>();
                    List<Vector3> bottomVerticies = new List<Vector3>();

                    List<int> topVertIndex = new List<int>();
                    List<int> bottomVertIndex = new List<int>();

                    List<int> toptriangles = new List<int>();
                    List<int> bottomtriangles = new List<int>();

                    for (int i = 0; i < cuttebleObject.vertices.Length; i++)
                    {
                        if (cuttingPlane.GetSide(CuttebleObject.transform.TransformPoint(cuttebleObject.vertices[i])))
                        {
                            topVerticies.Add(cuttebleObject.vertices[i]);
                            topVertIndex.Add(i);

                        }
                        else
                        {
                            bottomVerticies.Add(cuttebleObject.vertices[i]);
                            bottomVertIndex.Add(i);
                        }
                    }

                    List<Vector3> virtecieToAdd = new List<Vector3>();
                    List<Vector3> BvirtecieToAdd = new List<Vector3>();





                    for (int i = 0; i < cuttebleObject.triangles.Length; i += 3)
                    {
                        int stillValid = 0;
                        bool[] vallid = {false,false,false};
                        for (int j = 0; j < 3; j++)
                        {
                            if (!topVertIndex.Contains(cuttebleObject.triangles[i + j]))
                            {
                                stillValid ++;
                                vallid[j] = true;
                            }
                        }
                        if (stillValid == 0)
                        {
                            toptriangles.Add(AddToTriangleList(cuttebleObject.triangles[i], bottomVertIndex));
                            toptriangles.Add(AddToTriangleList(cuttebleObject.triangles[i + 1], bottomVertIndex));
                            toptriangles.Add(AddToTriangleList(cuttebleObject.triangles[i + 2], bottomVertIndex));
                        }

                        if (stillValid == 1)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                if (vallid[j])
                                {
                                    Vector3 directionA = new Vector3();
                                    Vector3 directionB = new Vector3();
                                    int A = 0;
                                    int B = 0;
                                    float hit;
                                    if (j == 0)
                                    {
                                        directionA = cuttebleObject.vertices[cuttebleObject.triangles[i + j]] - cuttebleObject.vertices[cuttebleObject.triangles[i + 1]];
                                        directionB = cuttebleObject.vertices[cuttebleObject.triangles[i + j]] - cuttebleObject.vertices[cuttebleObject.triangles[i + 2]];
                                        A = 1;
                                        B = 2;

                                    }
                                    else if (j == 1)
                                    {
                                        directionA = cuttebleObject.vertices[cuttebleObject.triangles[i + j]] - cuttebleObject.vertices[cuttebleObject.triangles[i]];
                                        directionB = cuttebleObject.vertices[cuttebleObject.triangles[i + j]] - cuttebleObject.vertices[cuttebleObject.triangles[i + 2]];

                                        A = 0;
                                        B = 2;                                        
                                    }
                                    else if (j == 2)
                                    {
                                        directionA = cuttebleObject.vertices[cuttebleObject.triangles[i + j]] - cuttebleObject.vertices[cuttebleObject.triangles[i]];
                                        directionB = cuttebleObject.vertices[cuttebleObject.triangles[i + j]] - cuttebleObject.vertices[cuttebleObject.triangles[i + 1]];

                                        A = 0;
                                        B = 1;
                                    }

                                    Ray ra = new Ray(CuttebleObject.transform.TransformPoint(cuttebleObject.vertices[cuttebleObject.triangles[i + A]]), directionA);
                                    Ray rb = new Ray(CuttebleObject.transform.TransformPoint(cuttebleObject.vertices[cuttebleObject.triangles[i + B]]), directionB);


                                    if (cuttingPlane.Raycast(ra, out hit))
                                    {
                                        virtecieToAdd.Add(CuttebleObject.transform.InverseTransformPoint(ra.GetPoint(hit)));

                                        Debug.Log("raycast1");


                                    }
                                    if (cuttingPlane.Raycast(rb, out hit))
                                    {
                                        virtecieToAdd.Add(CuttebleObject.transform.InverseTransformPoint(rb.GetPoint(hit)));
                                        Debug.Log("raycast2");
                                    }

                                    
                                    
                                        toptriangles.Add(AddToTriangleList(cuttebleObject.triangles[i + A], bottomVertIndex));
                                        toptriangles.Add(AddToTriangleList(cuttebleObject.triangles[i + B], bottomVertIndex));
                                        toptriangles.Add(topVerticies.Count + virtecieToAdd.Count - 1);
                                        Debug.Log("function");
                                    
                                   
                                    
                                      //toptriangles.Add(AddToTriangleList(cuttebleObject.triangles[i + A], bottomVertIndex));
                                      //toptriangles.Add(topVerticies.Count + virtecieToAdd.Count - 2);
                                      //toptriangles.Add(topVerticies.Count + virtecieToAdd.Count - 1);




                                }
                            }


                        }

                        if (stillValid == 2)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                if (!vallid[j])
                                {
                                    Vector3 directionA = new Vector3();
                                    Vector3 directionB = new Vector3();
                                    float hit;
                                    if (j == 0)
                                    {
                                        directionA = cuttebleObject.vertices[cuttebleObject.triangles[i + 1]] - cuttebleObject.vertices[cuttebleObject.triangles[i + j]];
                                        directionB = cuttebleObject.vertices[cuttebleObject.triangles[i + 2]] - cuttebleObject.vertices[cuttebleObject.triangles[i + j]];

                                    } else if (j == 1)
                                    {
                                        directionA = cuttebleObject.vertices[cuttebleObject.triangles[i]] - cuttebleObject.vertices[cuttebleObject.triangles[i + j]];
                                        directionB = cuttebleObject.vertices[cuttebleObject.triangles[i + 2]] - cuttebleObject.vertices[cuttebleObject.triangles[i + j]];

                                    }
                                    else if (j == 2)
                                    {
                                        directionA = cuttebleObject.vertices[cuttebleObject.triangles[i]] - cuttebleObject.vertices[cuttebleObject.triangles[i+j]];
                                        directionB = cuttebleObject.vertices[cuttebleObject.triangles[i + 1]] - cuttebleObject.vertices[cuttebleObject.triangles[i+j]];

                                    }


                                    Ray ra = new Ray(CuttebleObject.transform.TransformPoint(cuttebleObject.vertices[cuttebleObject.triangles[i + j]]),directionA);
                                    Ray rb = new Ray(CuttebleObject.transform.TransformPoint(cuttebleObject.vertices[cuttebleObject.triangles[i + j]]), directionB);
                                    

                                    if (cuttingPlane.Raycast(ra, out hit))
                                    {
                                        virtecieToAdd.Add(CuttebleObject.transform.InverseTransformPoint(ra.GetPoint(hit)));
                                        toptriangles.Add(topVerticies.Count + virtecieToAdd.Count );
                                        
                                        
                                    }
                                    if (cuttingPlane.Raycast(rb, out hit))
                                    {
                                        virtecieToAdd.Add(CuttebleObject.transform.InverseTransformPoint(rb.GetPoint(hit)));
                                        toptriangles.Add(topVerticies.Count + virtecieToAdd.Count - 2);
                                        
                                        
                                    }
                                    toptriangles.Add(AddToTriangleList(cuttebleObject.triangles[i + j], bottomVertIndex));                                                                     
                                    
                                    
                                   
                                    

                                }
                            }
                        }

                    }

                    for (int i = 0; i < cuttebleObject.triangles.Length; i += 3)
                    {
                        int stillValid = 0;
                        bool[] vallid = { false, false, false };
                        for (int j = 0; j < 3; j++)
                        {
                            if (!bottomVertIndex.Contains(cuttebleObject.triangles[i + j]))
                            {
                                stillValid ++;
                                vallid[j] = true;
                            }
                        }
                        if (stillValid == 0)
                        {
                            bottomtriangles.Add(AddToTriangleList(cuttebleObject.triangles[i], topVertIndex));
                            bottomtriangles.Add(AddToTriangleList(cuttebleObject.triangles[i + 1], topVertIndex));
                            bottomtriangles.Add(AddToTriangleList(cuttebleObject.triangles[i + 2], topVertIndex));
                        }


                        if (stillValid == 1)
                        {

                        }

                        if (stillValid == 2)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                if (!vallid[j])
                                {
                                    Vector3 directionA = new Vector3();
                                    Vector3 directionB = new Vector3();
                                    float hit;
                                    if (j == 0)
                                    {
                                        directionA = cuttebleObject.vertices[cuttebleObject.triangles[i + 1]] - cuttebleObject.vertices[cuttebleObject.triangles[i + j]];
                                        directionB = cuttebleObject.vertices[cuttebleObject.triangles[i + 2]] - cuttebleObject.vertices[cuttebleObject.triangles[i + j]];

                                    }
                                    else if (j == 1)
                                    {
                                        directionA = cuttebleObject.vertices[cuttebleObject.triangles[i]] - cuttebleObject.vertices[cuttebleObject.triangles[i + j]];
                                        directionB = cuttebleObject.vertices[cuttebleObject.triangles[i + 2]] - cuttebleObject.vertices[cuttebleObject.triangles[i + j]];

                                    }
                                    else if (j == 2)
                                    {
                                        directionA = cuttebleObject.vertices[cuttebleObject.triangles[i]] - cuttebleObject.vertices[cuttebleObject.triangles[i + j]];
                                        directionB = cuttebleObject.vertices[cuttebleObject.triangles[i + 1]] - cuttebleObject.vertices[cuttebleObject.triangles[i + j]];

                                    }


                                    Ray ra = new Ray(CuttebleObject.transform.TransformPoint(cuttebleObject.vertices[cuttebleObject.triangles[i + j]]), directionA);
                                    Ray rb = new Ray(CuttebleObject.transform.TransformPoint(cuttebleObject.vertices[cuttebleObject.triangles[i + j]]), directionB);


                                    if (cuttingPlane.Raycast(ra, out hit))
                                    {
                                        BvirtecieToAdd.Add(CuttebleObject.transform.InverseTransformPoint(ra.GetPoint(hit)));
                                        bottomtriangles.Add(bottomVerticies.Count + BvirtecieToAdd.Count);
                                        
                                        

                                    }
                                    if (cuttingPlane.Raycast(rb, out hit))
                                    {
                                        BvirtecieToAdd.Add(CuttebleObject.transform.InverseTransformPoint(rb.GetPoint(hit)));
                                        bottomtriangles.Add(bottomVerticies.Count + BvirtecieToAdd.Count - 2);
                                        
                                        
                                    }
                                    bottomtriangles.Add(AddToTriangleList(cuttebleObject.triangles[i + j], topVertIndex));
                                    




                                }
                            }
                        }

                    }
                    
                    topVerticies.AddRange(virtecieToAdd);
                    bottomVerticies.AddRange(BvirtecieToAdd);
                    //debugList.AddRange(bottomtriangles);


                    GenerateMesh(topVerticies, toptriangles);
                    GenerateMesh(bottomVerticies, bottomtriangles);
                    
                    Destroy(CuttebleObject);
                    //debugList.AddRange(toptriangles);

                }
            }
        }
    }




    int AddToTriangleList(int objectToAdd, List <int> bottomIndexes)
    {
        int solution = 0;
        for (int i = 0; i < bottomIndexes.Count; i++)
        {
            if (objectToAdd >= bottomIndexes[i])
            {
                solution++;
            } 
        }

        solution = objectToAdd - solution;

        return solution;
    }

    void GenerateMesh(List <Vector3> vertexes, List <int> _triangels )
    {
        GameObject slice = new GameObject("slice");
        slice.transform.position = CuttebleObject.transform.position;
        slice.transform.rotation = CuttebleObject.transform.rotation;
        slice.transform.localScale = CuttebleObject.transform.localScale;
           
        Mesh m;
        MeshRenderer r;
        m = slice.AddComponent<MeshFilter>().mesh;
        r = slice.AddComponent<MeshRenderer>();
        r.material = CuttebleObject.GetComponent<MeshRenderer>().material;

        m.Clear();
        m.vertices = vertexes.ToArray();
        m.triangles = _triangels.ToArray();
        m.RecalculateNormals();
      

        MeshCollider c = slice.AddComponent<MeshCollider>();
        c.convex = true;
        slice.AddComponent<Rigidbody>();

    }
}
