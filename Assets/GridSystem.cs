using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GridSystem : MonoBehaviour
{
    public globalControls controls;
    public StatManager statMan;
    public SystemSerializer ss;

    //Grid System
    public Collider2D mesh;
    public GameObject tile;
    public GameObject tileParent;
    public List<GameObject> tiles = new List<GameObject>();

    //Building stuff
    [SerializeField] private GameObject selectedPos;
    private List<GameObject> selectedTiles = new List<GameObject>();

    //Currently selected to build
    public GameObject selectedBuildPrefab;
    public GameObject selectedBuild;
    public Collider2D selectedBuildCollider;

    //Selected existed object
    private bool move = false;
    public Vector3 selectedBuildOldPos;

    public GameObject selectedRemove;
    private Vector3 mousePos;
    private bool validPos;

    void Start()
    {
        placeTiles();
        statMan = FindAnyObjectByType<StatManager>();
        controls.controls.Player.BuildLClick.performed += ctx => {
            if (selectedBuild != null && validPos) {
                buildObj();
            }
            else if (selectedBuild == null)
            {
                Scoreable obj = checkForObj(mousePos);
                if (obj != null)
                {
                    move = true;
                    selectBuild(obj);
                }
            }
        };
        controls.controls.Player.BuildRClick.performed += ctx =>
        {
            if (selectedBuild != null)
            {
                if (move)
                {
                    selectedBuild.transform.position = selectedBuildOldPos;
                }
                deselectBuild();
            }
            else
            {
                Scoreable obj = checkForObj(mousePos);
                if (obj != null)
                {
                    removeObject(obj);
                }
            }
        };
    }
    void OnEnable()
    {
        if (controls == null)
            controls = FindAnyObjectByType<globalControls>();
        if (ss == null)
            ss = FindAnyObjectByType<SystemSerializer>();
        controls.controls.Player.BuildLClick.Enable();
        controls.controls.Player.BuildRClick.Enable();
    }
    void OnDisable()
    {
        controls.controls.Player.BuildLClick.Disable();
        controls.controls.Player.BuildRClick.Disable();
    }
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        GameObject tempSelect = worldToTile(mousePos + new Vector3(0, 0, 9));
        if (tempSelect != selectedPos)
        {
            if (tempSelect != null)
            {
                selectedPos = tempSelect;
                if (selectedBuild != null)
                {
                    selectedBuild.transform.position = objectPos(selectedPos, selectedBuild);
                    validPos = selectObjPosTiles(selectedPos);
                }
            }
        }
        if (tempSelect == null && selectedBuild != null)
        {
            selectedBuild.transform.position = mousePos;
        }

    }
    public void placeTiles()
    {
        Vector3 scale = tileParent.transform.lossyScale;
        Bounds tileBounds = tile.GetComponent<SpriteRenderer>().bounds;

        for (float i = mesh.bounds.min.x + tileBounds.size.x / 2 * scale.x; i <= mesh.bounds.max.x; i += tileBounds.size.x * scale.x)
        {
            for (float j = mesh.bounds.min.y + tileBounds.size.y / 2 * scale.y; j <= mesh.bounds.max.y; j += tileBounds.size.y * scale.y)
            {
                Vector3 point = new Vector3(i, j, -1);
                if (mesh.OverlapPoint(point))
                {
                    GameObject temp = Instantiate(tile, point, Quaternion.identity, tileParent.transform);
                    tiles.Add(temp);
                }
            }
        }
    }
    //Instantiating new obejct on cursor
    public void selectBuild(Scoreable p)
    {
        selectedBuildPrefab = ss.findObj(p).prefab;
        //Selecting existing obejct
        if (selectedBuild != null)
        {
            Destroy(selectedBuild);
        }

        selectedBuild = Instantiate(selectedBuildPrefab, FindAnyObjectByType<BoardMan>().boardObjects.transform);
        selectedBuildCollider = selectedBuild.GetComponent<Scoreable>().myCollider;

        if (move)
        {
            Scoreable obj = selectedBuild.GetComponent<Scoreable>();
            obj.colorMyTiles(Color.clear);
            foreach (Tile tile in obj.myTiles)
            {
                tile.setTaken(false);
            }
            selectedPos = p.myTiles[0].gameObject;
            selectedBuild.transform.position = objectPos(selectedPos, selectedBuild);
            validPos = selectObjPosTiles(selectedPos);
            Destroy(p.gameObject);
        }
    }
    public void deselectBuild()
    {
        Destroy(selectedBuild);
        move = false;
    }
    private void removeObject(Scoreable p)
    {
        foreach (Tile t in p.myTiles)
            t.setTaken(false);
        statMan.addItem(ss.findObj(p));
        Destroy(p.gameObject);
    }
    private Scoreable checkForObj(Vector3 pos)
    {
        Collider2D hit = Physics2D.OverlapPoint(pos, LayerMask.GetMask("Pin"));
        return hit == null ? null : hit.GetComponent<Scoreable>();
    }
    private GameObject worldToTile(Vector3 pos)
    {
        float minDist = 4f;
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
    public Vector3 objectPos(GameObject tile, GameObject obj)
    {
        BoxCollider2D col = obj.GetComponent<Scoreable>().myCollider;

        Vector2 tileSize = tile.GetComponent<SpriteRenderer>().bounds.size;
        Vector2 scaledSize = col.bounds.size;

        float snappedWidth = Mathf.Ceil(scaledSize.x / tileSize.x) * tileSize.x;
        float snappedHeight = Mathf.Ceil(scaledSize.y / tileSize.y) * tileSize.y;

        // Bottom-left of the ORIGINAL tile (anchor)
        float tileBLX = tile.transform.position.x - tileSize.x * 0.5f;
        float tileBLY = tile.transform.position.y - tileSize.y * 0.5f;

        // Center of the full snapped footprint expanding from that tile
        float finalX = tileBLX + snappedWidth * 0.5f;
        float finalY = tileBLY + snappedHeight * 0.5f;

        return new Vector3(finalX, finalY, -2);
    }
    public bool selectObjPosTiles(GameObject tile)
    {
        foreach (GameObject g in selectedTiles) {
            Tile t = g.GetComponent<Tile>();
            t.setColor(Color.clear);
        }
        selectedTiles.Clear();
        bool valid = true;
        BoxCollider2D col = selectedBuild.GetComponent<Scoreable>().myCollider;
        Vector2 tileSize = tile.GetComponent<SpriteRenderer>().bounds.size;
        Vector2 scaledSize = Vector2.Scale(col.size, selectedBuild.transform.lossyScale);

        int tilesX = Mathf.CeilToInt(scaledSize.x / tileSize.x);
        int tilesY = Mathf.CeilToInt(scaledSize.y / tileSize.y);

        float startX = tile.transform.position.x - tileSize.x * 0.5f;
        float startY = tile.transform.position.y - tileSize.y * 0.5f;

        for (int x = 0; x < tilesX; x++)
        {
            for (int y = 0; y < tilesY; y++)
            {
                float i = startX + x * tileSize.x + tileSize.x * 0.5f; // center of tile
                float j = startY + y * tileSize.y + tileSize.y * 0.5f; // center of tile

                Collider2D hit = Physics2D.OverlapPoint(new Vector2(i, j), LayerMask.GetMask("Tile"));
                if (hit != null && !selectedTiles.Contains(hit.gameObject))
                {
                    selectedTiles.Add(hit.gameObject);
                    if (hit.gameObject.GetComponent<Tile>().taken)
                        valid = false;
                }
                else
                {
                    valid = false;
                }
            }
        }
        foreach (GameObject g in selectedTiles)
        {
            Tile t = g.GetComponent<Tile>();
            t.setColor(valid ? Color.blue : Color.red);
        }
        return valid;
    }
    private void buildObj()
    {
        foreach (GameObject tile in selectedTiles)
        {
            tile.GetComponent<Tile>().setTaken(true);
            tile.GetComponent<SpriteRenderer>().color = Color.clear;
            selectedBuild.GetComponent<Scoreable>().myTiles.Add(tile.GetComponent<Tile>());
        }
        if (!move)
            statMan.removeItem(selectedBuildPrefab.GetComponent<DynamicObject>());
        move = false;
        selectedBuildPrefab = null;
        selectedBuildCollider = null;
        selectedBuild = null;
    }
}
