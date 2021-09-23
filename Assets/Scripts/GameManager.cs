using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject platform;
    
    [SerializeField]
    private int startPlatformLength;
    [SerializeField]
    private List<string> childrenNames;

    private GameObject previousPlatform;
    
    // Start is called before the first frame update
    void Start()
    {
        //Generate the first platform with a 2 grid offset;
        previousPlatform = GeneratePlatform(Vector3.zero - Vector3.forward * 3 * 2);
        
        //Create the next platforms
        for (int i = 0; i < startPlatformLength; i++)
        {
            //Create a temp variable to save a transform
            Transform platformChild = previousPlatform.transform;

            //Loop until you have the right child
            foreach (var childName in childrenNames.Append("Right"))
            {
                //Get the child transform
                platformChild = platformChild.transform.Find(childName);
            }

            //Generate next platform
            previousPlatform = GeneratePlatform(platformChild.position);
        }
    }

    GameObject GeneratePlatform(Vector3 position)
    {
        return Instantiate(platform, position, Quaternion.identity);
    }
}
