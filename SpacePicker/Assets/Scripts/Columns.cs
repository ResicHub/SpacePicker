using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Columns : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform column in transform)
        {
            column.localPosition = column.localPosition / 4;
        }
    }
}
