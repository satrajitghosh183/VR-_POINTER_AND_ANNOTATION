using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVRDraw : MonoBehaviour
{
    [SerializeField] public GameObject obj1;
    [SerializeField] public GameObject obj2;
    public void ToggleObject()
    {
        obj1.SetActive(!obj1.activeSelf);
        obj2.SetActive(!obj2.activeSelf);
    }
}
