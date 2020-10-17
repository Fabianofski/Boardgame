using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharPickerCamMovement : MonoBehaviour
{

    public CharacterContainer charContainer;
    public float speed;
    public float offset;

    float minX = 0;
    float minY = 0;
    float maxX = 0;
    float maxY = 0;

    void Update()
    {
        CalcMinMaxPos();
        transform.position = Vector3.Lerp(transform.position, CalcCamPos(), speed * Time.deltaTime);
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, CalcOrthoSize(), speed * Time.deltaTime);
    }

    Vector3 CalcCamPos()
    {
        Vector3 pos = Vector3.zero;
        pos = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, -10);

        return pos;
    }

    void CalcMinMaxPos()
    {
        minX = 5;
        minY = 5;
        maxX = -5;
        maxY = 0;

        foreach (GameObject go in charContainer.CameraScroll)
        {
            if (!go) return;

            float posx = go.transform.position.x;
            float posy = go.transform.position.y;

            if (posy < minY)
                minY = posy;
            if (posy > maxY)
                maxY = posy;

            if (posx < minX)
                minX = posx;
            if (posx > maxX)
                maxX = posx;
        }
    }

    float CalcOrthoSize()
    {
        float dif = (maxX - minX) / 2;
        float ratio = 16f / 9f;

        return Mathf.Abs(dif / ratio) + offset;
    }
}
