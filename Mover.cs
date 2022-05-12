using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    // If you set the Gravity Scale on the Rigidbody2D to 0, this function will continuously 
    // move the object to the right, while also handling collisions correctly
    void Awake() => GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
}
