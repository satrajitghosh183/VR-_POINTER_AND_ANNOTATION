using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{
    [SerializeField] public GameObject obj1;
    public void ToggleObject()
    {
        obj1.SetActive(!obj1.activeSelf);
    }
}
