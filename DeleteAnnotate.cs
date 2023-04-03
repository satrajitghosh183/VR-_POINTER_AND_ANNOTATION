using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAnnotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        int c=0;
        foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if(gameObj.name == "New Game Object")
            {
                c=c+1;
                if(c>2)
                {
                    Destroy(gameObj);
                    c=c-1;
                }    
            }
        }
    }
}
