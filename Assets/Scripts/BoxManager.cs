using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BoxManager : MonoBehaviour
{
    public GameObject Floor;
    public GameObject box;
    public GameObject panel;
    public Texture2D blankTexture;
    public List<GameObject> boxes;
    public List<Item> items;

    // Use this for initialization
    void Start()
    {
        Random.InitState(5);
        boxes = new List<GameObject>();
        GenerateBoxes();
        
        foreach (Item a in items)
        {
            if (a.image == null)
            {
                a.image = blankTexture;
            }
        }
    }

    public void GenerateBoxes()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 position = new Vector3(Random.Range(-6f, 6f), Random.Range(1, 2f), Random.Range(0f, 4f));
            GameObject o = Instantiate(box, position, Quaternion.identity, this.transform);
            BoxScript bs = o.GetComponent<BoxScript>();
            bs.item = items[Random.Range(0, items.Count - 1)];
            bs.hp = Random.Range(6, 10);

            boxes.Add(o);
        }
    }

    public void CheckBoxes()
    {
        if (boxes.Count == 0)
        {
            GenerateBoxes();
        }
    }
}

[System.Serializable]
public class Item
{
    public string name;
    public int value;
    public Texture2D image;
}