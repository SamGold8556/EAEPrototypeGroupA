using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class BillboardSprite : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Camera.main != null)
        {
            transform.LookAt(Camera.main.transform.position, Vector3.up);
        }
    }

    public void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }
}
