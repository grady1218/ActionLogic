using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpSpanScript : MonoBehaviour
{
    public bool isInPortal = false;
    [SerializeField] float SpanTime = .1f;
    float count = 0;
    void Update()
    {
        if (isInPortal) count = count < SpanTime ? count += Time.deltaTime : 0;
        if (count == 0) isInPortal = false;
    }
}
