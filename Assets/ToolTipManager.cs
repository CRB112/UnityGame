using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

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

    public void show(DynamicObject dO, Transform t, string desc)
    {
        if (openTT != null)
            Destroy(openTT);
        openTT = Instantiate(box, t.transform.position  + new Vector3(0, 100, 0), quaternion.identity, t.transform.parent?.GetComponentInParent<Canvas>().transform);
        openTT.GetComponent<Image>().color = dO.getColor();
        openTT.GetComponentInChildren<TextMeshProUGUI>().text = desc;
    }
    public void hide()
    {
        Destroy(openTT);
        openTT = null;
    }
}
