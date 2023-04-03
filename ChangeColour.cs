using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    public Renderer render;
    public GameObject target;
    [SerializeField] public Color mat;
    [SerializeField] public Color mat1;
    [SerializeField] public Color mat2;
    // Start is called before the first frame update
    void Start()
    {
        render = target.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangetoRed()
    {
        render.material.color = mat;
    }
    public void Changetoblue()
    {
        render.material.color = mat1;
    }
    public void Changetogreen()
    {
        render.material.color = mat2;
    }
}
