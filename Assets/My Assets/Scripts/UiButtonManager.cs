using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButtonManager : MonoBehaviour
{
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Game Managers and debug").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnitSelectButton(int unitID)
    {
        
    }
}
