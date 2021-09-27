using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject platform;
    [SerializeField]
    private int startPlatformLength;
    [SerializeField]
    private List<string> childrenNames;

    private GameObject previousPlatform;
    private string previousAnimDirection = "Right";
    private bool previousDirection;

    private int amountLeft;
    private int amountRight;

    private Random rnd = new Random();

    public bool gameStarted = true;

    public static GameManager gameManager;

    private void Awake()
    {
        if (gameManager == null) gameManager = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        //Generate the first platform with a 2 grid offset;
        previousPlatform = GeneratePlatform(Vector3.zero - Vector3.forward * 3 * 2, false);
        
        //Create the next platforms
        for (int i = 0; i < startPlatformLength; i++)
        {
            Transform platformChild = extractPositionChild(previousPlatform, previousDirection);
            
            //Generate next platform
            previousPlatform = i > 3? GenerateAnimatedPlatform(platformChild.position, previousAnimDirection):GeneratePlatform(platformChild.position, false);
            if(i > 3) yield return new WaitForSeconds(3/4);
        }
    }

    public void CollisionCaller(GameObject originPlatform)
    {
        if (amountLeft == 0 && amountRight == 0)
        {
            previousDirection = !previousDirection;
            if (previousDirection) amountLeft = rnd.Next(1, 10);
            else amountRight = rnd.Next(1, 10);
        }
        
        Debug.Log(amountLeft + " " + amountRight);

        if (amountLeft > 0)
        {
            previousPlatform = GenerateAnimatedPlatform(extractPositionChild(previousPlatform, previousDirection).position, "Left");
            amountLeft--;
        }

        if (amountRight > 0)
        {
            previousPlatform = GenerateAnimatedPlatform(extractPositionChild(previousPlatform, previousDirection).position, "Right");
            amountRight--;
        }
    }

    Transform extractPositionChild(GameObject originPlatform, bool direction)
    {
        //Create a temp variable to save a transform
        Transform platformChild = originPlatform.transform;

        //Loop until you have the right child
        foreach (var childName in childrenNames.Append(direction? "Left":"Right"))
        {
            //Get the child transform
            platformChild = platformChild.transform.Find(childName);
        }

        return platformChild;
    }

    GameObject GeneratePlatform(Vector3 position, bool enableCollider)
    {
        GameObject gp = Instantiate(platform, position, Quaternion.identity);
        gp.transform.Find("Collider").gameObject.SetActive(enableCollider);
        return gp;
    }

    GameObject GenerateAnimatedPlatform(Vector3 position, string side)
    {
        //Start animation
        GameObject ap = GeneratePlatform(position, true);
        Animator apa = ap.GetComponent<Animator>();
        apa.SetFloat(side, 1);
        apa.Play($"PlatformAnimation{side}");
        previousAnimDirection = (previousAnimDirection == "Right") ? "Left" : "Right";
        return ap;
    }
}
