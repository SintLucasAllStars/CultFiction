using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CheckpointManager : MonoBehaviour
{
    public List<GameObject> checkPoints;
    public int playerNum = 0;
    private int checkpointCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        checkPoints = new List<GameObject>();
        checkpointCount = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject g = transform.GetChild(i).gameObject;
            checkPoints.Add(g);
            if(playerNum == 0)
            {
                g.layer = 10;
            }
            if (playerNum == 1)
            {
                g.layer = 11;
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextCheckPoint (GameObject checkpoint)
    {
        Destroy(checkpoint);
        checkpointCount++;
        if (checkpointCount < checkPoints.Count)
        {
            checkPoints[checkpointCount].SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        
    }
}
