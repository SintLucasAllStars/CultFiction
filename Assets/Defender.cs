using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]

public class Defender : MonoBehaviour
{


    public DefenderStats stats;//= new DefenderStats();
    private Heroin target;

    bool placed = false;
    bool placeble = true;
    [HideInInspector]
    public bool selected = false;

    Transform rangeCircle;
    Transform tower;


    // Use this for initialization
    void Start()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;

        tower = transform.GetChild(1);
        tower.GetComponent<Image>().sprite = UiManager.instance.defendersImg[stats.image];

        rangeCircle = transform.GetChild(0);

        transform.position = Input.mousePosition;
        StartCoroutine(Shoot());
    }



    // Update is called once per frame
    void Update()
    {
        rangeCircle.GetComponent<RectTransform>().sizeDelta = new Vector2(stats.range * 2, stats.range * 2);
        if (!placed)
        {
            Vector2 mousePos = Input.mousePosition;
            transform.position = mousePos;


            Color c = placeble ? Color.green : Color.red;
            c.a = .4f;
            rangeCircle.GetComponent<Image>().color = c;

            if (Input.GetKeyDown(KeyCode.Mouse0)&& placeble)
            {
                placed = true;
            }

            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Sell();
            }
        }
        else
        {
            rangeCircle.gameObject.SetActive(selected);

            target = null;
            for (int i = Gamecontroller.instance.heroin.Count-1; i > -1; i--)
            {
                Vector2 heroinPos = Gamecontroller.instance.heroin[i].gameObject.transform.localPosition;
                float distance = Vector2.Distance(heroinPos, transform.localPosition);
                
                if (distance < stats.range)
                {
                    target = Gamecontroller.instance.heroin[i];
                }
            }

            if (target)
            {
                tower.right = target.transform.position - transform.position;
            }
        }

    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (placed)
            {
                yield return new WaitForSeconds(1 / stats.fireRate);
                if (target)
                { 
                    target.Hit(stats.damage);
                }
            }
            yield return null;
        }
    }

    public void Select()
    {
        for (int i = 0; i < Gamecontroller.instance.placedDefenders.Count; i++)
        {
            if (Gamecontroller.instance.placedDefenders[i] != this)
            {
                Gamecontroller.instance.placedDefenders[i].selected = false;
            }
        }
        selected = !selected;
        UiManager.instance.UpdateUI();
    }

    public void Sell()
    {
        Gamecontroller.instance.points += stats.price;
        Gamecontroller.instance.placedDefenders.Remove(this);

        Destroy(gameObject);

    }




    private void OnTriggerEnter2D(Collider2D collision)
    { 
        placeble = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        placeble = true;
    }

}
