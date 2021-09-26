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
    private string previousDirection = "Right";

    public bool gameStarted = true;
    
    
    // Start is called before the first frame update
    IEnumerator Start()
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
            previousPlatform = i > 3? GenerateAnimatedPlatform(platformChild.position, previousDirection):GeneratePlatform(platformChild.position);
            if(i > 3) yield return new WaitForSeconds(3/4);
        }
    }

    GameObject GeneratePlatform(Vector3 position)
    {
        return Instantiate(platform, position, Quaternion.identity);
    }

    GameObject GenerateAnimatedPlatform(Vector3 position, string side)
    {
        //Start animation
        GameObject ap = GeneratePlatform(position);
        Animator apa = ap.GetComponent<Animator>();
        apa.SetFloat(side, 1);
        apa.Play($"PlatformAnimation{side}");
        previousDirection = (previousDirection == "Right") ? "Left" : "Right";
        return ap;
    }
}
