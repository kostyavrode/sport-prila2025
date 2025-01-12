using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training : MonoBehaviour
{

    public Transform contentContainer;

    public void Init(string info)
    {

    }

    public void CloseButton()
    {
        Destroy(gameObject);
    }
}
