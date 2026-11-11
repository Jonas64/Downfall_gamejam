using UnityEngine;

public class playerctrl : MonoBehaviour
{
    public float movspeed;
    float speedx, speedy;
    Rigidbody2D rb; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {

        speedx = Input.GetAxisRaw("Horizontal") * movspeed;
        speedy = Input.GetAxisRaw("Vertical") * movspeed;
        rb.linearVelocity = new Vector2(speedx, speedy);
    }
}
