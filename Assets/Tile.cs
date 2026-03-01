using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool taken = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setTaken(bool b)
    {
        taken = b;
    }
    public void setColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }
}
