using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Spike : MonoBehaviour
{

    public GameObject spike;
    public GameObject spike2;
    public GameObject spike3;
    public GameObject spike4;
    public GameObject spike5;
    public GameObject spike6;
    public GameObject spike7;
    public GameObject spike9;
    public GameObject spike10;
    public GameObject spike11;
    public GameObject spike12;
    public GameObject spike13;
    public GameObject spike14;
    public GameObject spike15;
    public GameObject spike16;
    public GameObject spike17;
    public GameObject spike18;
    public GameObject spike19;
    public GameObject spike20;
    public GameObject spike21;
    public GameObject spike22;
    public GameObject invidible_block1;
    public GameObject invidible_block2;
    public GameObject invidible_block3;
    public GameObject invidible_block4;
    public GameObject Saw1;
    public GameObject Saw2;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    

   public  void OnTriggerEnter2D(Collider2D col){

       if (col.gameObject.tag == "Text2")
	{
        text2.SetActive(true);
    }
     if (col.gameObject.tag == "Text1")
	{
        text1.SetActive(true);
    }
     if (col.gameObject.tag == "Text3")
	{
        text3.SetActive(true);
    }
    if (col.gameObject.tag == "Text4")
	{
        text4.SetActive(true);
    }
        if (col.gameObject.tag == "Spike1")
	{
        Debug.Log("Spike1");
        text1.SetActive(true);
        spike.SetActive(true);
        
    }
         if (col.gameObject.tag == "Spike2")
	{
        Debug.Log("Spike2");
        spike2.SetActive(true);
        
    }
     if (col.gameObject.tag == "Spike3")
	{
        Debug.Log("Spike3");
        spike3.SetActive(true);
        
    }
    if (col.gameObject.tag == "Spike4")
	{
        Debug.Log("Spike4");
        spike4.SetActive(true);
        
    }
    if (col.gameObject.tag == "Spike5")
	{
        Debug.Log("Spike5");
        spike5.SetActive(true);
       
    }
    if (col.gameObject.tag == "Spike6")
	{
        Debug.Log("Spike6");
        spike6.SetActive(true);
       
    }
     if (col.gameObject.tag == "Spike7")
	{
        Debug.Log("Spike6");
        spike7.SetActive(true);
        
    }
    if (col.gameObject.tag == "Spike9")
	{
        Debug.Log("Spike9");
        spike9.SetActive(true);
        
    }
    if (col.gameObject.tag == "Spike10")
	{
        Debug.Log("Spike10");
        spike10.SetActive(true);

    }
    if (col.gameObject.tag == "Spike11")
	{
        Debug.Log("Spike11");
        spike11.SetActive(true);
    }
    if (col.gameObject.tag == "Spike12")
	{
        Debug.Log("Spike12");
        spike12.SetActive(true);
    }
    if (col.gameObject.tag == "Spike13")
	{
        Debug.Log("Spike13");
        spike13.SetActive(true);
    }
    if (col.gameObject.tag == "Spike14")
	{
        Debug.Log("Spike14");
        spike14.SetActive(true);
    }
    if (col.gameObject.tag == "Spike15")
	{
        Debug.Log("Spike15");
        spike15.SetActive(true);
    }
    if (col.gameObject.tag == "Spike16")
	{
        Debug.Log("Spike16");
        spike16.SetActive(true);
    }
     if (col.gameObject.tag == "Spike17")
	{
        Debug.Log("Spike17");
        spike17.SetActive(true);
    }
     if (col.gameObject.tag == "Spike18")
	{
        Debug.Log("Spike18");
        spike18.SetActive(true);
    }
     if (col.gameObject.tag == "Spike19")
	{
        Debug.Log("Spike19");
        spike19.SetActive(true);
    }
    if (col.gameObject.tag == "Spike20")
	{
        Debug.Log("Spike20");
        spike20.SetActive(true);
    }
    if (col.gameObject.tag == "Spike21")
	{
        Debug.Log("Spike21");
        spike21.SetActive(true);
    }
    if (col.gameObject.tag == "Spike22")
	{
        Debug.Log("Spike22");
        spike22.SetActive(true);
    }
    if (col.gameObject.tag == "Invisible_Block1")
	{
        Debug.Log("invis");
        invidible_block1.SetActive(true);
    }
    if (col.gameObject.tag == "Invisible_Block2")
	{
        Debug.Log("invis");
        invidible_block2.SetActive(true);
    }
     if (col.gameObject.tag == "Invisible_Block3")
	{
        Debug.Log("invis");
        invidible_block3.SetActive(true);
    }
    if (col.gameObject.tag == "Invisible_Block4")
	{
        Debug.Log("invis");
        invidible_block4.SetActive(true);
    }
        if (col.gameObject.tag == "Saw1")
	{
        Debug.Log("saw1");
        Saw1.SetActive(true);
    }
     if (col.gameObject.tag == "Saw2")
	{
        Debug.Log("saw2");
        Saw2.SetActive(true);
    }
    if (col.gameObject.tag == "Finish")
	{
        Debug.Log("Win!");
        SceneManager.LoadScene("WinScene");
        }
    }
}

    
    
 
 

