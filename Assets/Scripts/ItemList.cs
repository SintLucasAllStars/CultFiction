using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public GameObject Floor;
    public GameObject box;
    public GameObject panel;
    public Texture2D blankTexture;
    public List<Item> items;

    // Use this for initialization
    void Start()
    {
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
            Vector3 position = new Vector3(Random.Range(-4f, 4f), Random.Range(0, 2f), Random.Range(-2f, 3f));
            GameObject o = Instantiate(box, position, Quaternion.identity, this.transform);
            BoxScript bs = o.GetComponent<BoxScript>();
            bs.item = items[Random.Range(0, items.Count - 1)];
            bs.hp = Random.Range(2,7);
            //Destroy(this.gameObject, 4f);
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
