using UnityEngine;

public class Ball : DynamicObject
{
    private StatManager statManager;
    public float scoreMulti;

    private float maxSpeed = 22f;
    private Rigidbody2D rb;
    void Start()
    {
        rarity = RarityLevel.Common;
        statManager = FindFirstObjectByType<StatManager>();
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Scoreable scoreableObj = collision.gameObject.GetComponent<Scoreable>();
        if (scoreableObj != null)
        {
            scoreableObj.activateObj(this);
        }
    }
}
