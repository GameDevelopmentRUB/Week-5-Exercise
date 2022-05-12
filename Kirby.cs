using UnityEngine;

public class Kirby : MonoBehaviour
{
    // This keeps track of the total Kirbys, so that we don't spawn to many
    static int kirbyCount = 0;

    [SerializeField] Transform spawnPosition;
    [SerializeField] Vector2 forceDirectionUp;
    [SerializeField] Vector2 forceDirectionRight;

    Rigidbody2D rb;
    SpriteRenderer sr;

    private void Awake()
    {
        kirbyCount++;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // FixedUpdate is called at fixed timesteps instead of frames (default: 0.02s = 50fps)
    private void FixedUpdate()
    {
        // For continuous input you can use FixedUpdate
        if (Input.GetKey(KeyCode.Space))
            rb.AddForce(forceDirectionUp);
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(forceDirectionRight);
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(-forceDirectionRight);
    }

    // Between FixedUpdate and Update is where the actual physics update happens

    void Update()
    {
        // There might be Update steps that don't have a FixedUpdate, which is why we put code like GetKeyDown 
        // in Update - if you press the button when there's no FixedUpdate, the call will be ignored
        if (Input.GetKeyDown(KeyCode.W))
            // This code overwrites the current velocity on the y-axis while keeping the x-velocity, which
            // is physically inaccurate, but feels right in a lot games (especially when you need a jump)
            rb.velocity = new Vector2(rb.velocity.x, forceDirectionUp.y);
    }

    // This will be called on every single physical (!) collision
    private void OnCollisionEnter2D(Collision2D collision)
    {  
        sr.color = Random.ColorHSV(0, 1, 0, 1, 1, 1);
    }

    // This will be called every time Kirby hits a collider that has 'Is Trigger' checked
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // A tag is used to differentiate between different objects. When 'Shredder' is hit, 
        // Kirby should be destroyed. Otherwise a copy of this Kirby should spawn
        if (collision.CompareTag("Shredder"))
        {
            Destroy(gameObject);
            kirbyCount--;
        }
        else
            Instantiate(gameObject, spawnPosition.position, Quaternion.identity);
    }
}
