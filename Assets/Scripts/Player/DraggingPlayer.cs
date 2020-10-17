using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingPlayer : MonoBehaviour
{

    void OnMouseDrag()
    {
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousepos;
    }

    void OnMouseUp()
    {
        transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    }

}
