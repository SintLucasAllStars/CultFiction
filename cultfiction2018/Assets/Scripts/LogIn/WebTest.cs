using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class WebTest : MonoBehaviour 
{
    IEnumerator Start()
    {
        WWW request = new WWW("http://localhost/sqlconnect/webtest.php");
        yield return request;
        string[] webResults =request.text.Split('\t');
        foreach (var s in webResults)
        {
            Debug.Log(s);
        }

        int webNumber =int.Parse(webResults[1]);
        webNumber *= 2;
        Debug.Log(webNumber);

    }
}
