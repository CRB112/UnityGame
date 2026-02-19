using UnityEngine;
using System.Collections;

public class Stick : MonoBehaviour
{
    private Rigidbody2D rb;

    public Quaternion startRot;
    public Quaternion endRot;
    public float flickSpeed = .1f;

    Coroutine flickEnum_;

    void Start()
    {
        startRot = transform.rotation;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

    }

    public void flick(bool b)
    {
        if (flickEnum_ != null)
        {
            StopCoroutine(flickEnum_);
        }

        flickEnum_ = StartCoroutine(flickEnum(b ? endRot : startRot));
    }
    IEnumerator flickEnum(Quaternion end)
    {
        Quaternion tempStart = transform.rotation;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / flickSpeed;
            rb.MoveRotation(Quaternion.Slerp(tempStart, end, t));
            yield return null;
        }

        transform.rotation = end;
    }
}
