using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SetPlayersSortingLayer : MonoBehaviour
{
    public GridLayout grid;
    public Tilemap tilemap;

    private float z;
    private float oldZ = 0;
    private bool ZChanged;

    void Awake()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridLayout>();
        tilemap = grid.GetComponentInChildren<Tilemap>();
    }
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3Int cell;
        TileBase tile;

        for (int i = 0; i < 10; i++)
        {
            pos.z = i;
            cell = grid.WorldToCell(pos);
            tile = tilemap.GetTile(cell);

            if (tile != null)
               z = i;
        }

        if(z != oldZ && !ZChanged)
        {
            float difference = z - oldZ;
            transform.position += new Vector3(0, 0.15f * difference, difference);
            ZChanged = true;
            StartCoroutine(SwitchOff());
        }

        oldZ = z;
    }

    IEnumerator SwitchOff()
    {
        yield return new WaitForSeconds(1f);
        ZChanged = false;
    }
}
