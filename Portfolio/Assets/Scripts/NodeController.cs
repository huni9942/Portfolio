using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    public Color hoverColor;

    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = hoverColor;
    }
}
