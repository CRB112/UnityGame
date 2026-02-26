using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GridSystem : MonoBehaviour
{
    //Grid System
    public Collider2D mesh;
    public GameObject tile;
    public GameObject tileParent;
    public List<GameObject> tiles = new List<GameObject>();

    //Building stuff
    private GameObject selectedTile;

    public GameObject selectedBuild;
    public Collider2D selectedBuildCollider;
    public GameObject selectedRemove;
    public GameObject selectedObj;
    private Vector3 mousePos;

    void Start()
    {
        Vector3 scale = tileParent.transform.lossyScale;
        Bounds tileBounds = tile.GetComponent<SpriteRenderer>().bounds;

        for (float i = mesh.bounds.min.x + tileBounds.size.x / 2 * scale.x; i <= mesh.bounds.max.x; i += tileBounds.size.x * scale.x)
        {
            for (float j = mesh.bounds.min.y + tileBounds.size.y / 2 * scale.y; j <= mesh.bounds.max.y; j += tileBounds.size.y * scale.y)
            {
                Vector3 point = new Vector3(i, j, 0);
                if (mesh.OverlapPoint(point))
                {
                    GameObject temp = Instantiate(tile, point, Quaternion.identity, tileParent.transform);
                    tiles.Add(temp);
                }
            }
        }
    }
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        GameObject tempSelect = worldToTile(mousePos);
        if (tempSelect != selectedTile)
        {
            if (selectedTile != null)
                selectedTile.GetComponent<SpriteRenderer>().color = Color.clear;
            selectedTile = tempSelect;
            if (selectedBuild != null)
            {
                selectedBuild.transform.position = selectedTile.transform.position - new Vector3(selectedBuildCollider.bounds.max.x, selectedBuildCollider.bounds.max.y, 0);
                
            }
            selectedTile.GetComponent<SpriteRenderer>().color = Color.blue;
        }

    }
    //Selecting active obj on grid
    public void selectBuild(GameObject g)
    {

    }
    //Instantiating new obejct on cursor
    public void selectBuild(Pin p)
    {
        selectedBuild = Instantiate(p.gameObject);
        selectedBuildCollider = selectedBuild.GetComponent<Pin>().myCollider;
    }
    private GameObject worldToTile(Vector3 pos)
    {
        float minDist = Mathf.Infinity;
        GameObject closest = null;
        foreach (GameObject g in tiles)
        {
            float tempDist = (pos - g.transform.position).sqrMagnitude;
            if (tempDist < minDist)
            {
                minDist = tempDist;
                closest = g;
            }
        }
        return closest;
    }
}
