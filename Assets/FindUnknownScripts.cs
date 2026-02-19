using UnityEngine;
using UnityEditor;

public class FindUnknownScripts : MonoBehaviour
{
    void Start()
    {
        Find();
    }

    [MenuItem("Tools/Find Unknown Scripts")]
    static void Find()
    {
        foreach (GameObject go in GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            var components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                    Debug.Log("Missing script on GameObject: " + go.name + " (Index " + i + ")", go);
            }
        }
    }
}
