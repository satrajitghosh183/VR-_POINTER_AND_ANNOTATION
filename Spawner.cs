using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject object1;
    public Transform reff;
    // Start is called before the first frame update

    void Start()
    {
          object1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
       
    }   
    public void Spawn()
    {
        
        Instantiate(object1, reff.position, reff.rotation);
    }    
        
    
}
