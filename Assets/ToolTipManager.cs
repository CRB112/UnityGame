using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
    public GameObject box; //Prefab

    private GameObject openTT;
    void Start()
    {
        DontDestroyOnLoad(this);
    }


    void Update()
    {

    }

    public void show(Transform t, string desc)
    {
        if (openTT != null)
            Destroy(openTT);
        openTT = Instantiate(box, t.transform.position, quaternion.identity, t);
        openTT.GetComponentInChildren<TextMeshProUGUI>().text = desc;
    }
    public void hide()
    {
        Destroy(openTT);
        openTT = null;
    }
}
