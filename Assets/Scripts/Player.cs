using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed;
    public Rigidbody2D rb;
    private Vector3 movement;
    private Vector3 velocity;
    void Start()
    {

    }

    void Update()
    {
        movement = Vector2.zero;
        movement.x=Input.GetAxis("Horizontal");
        movement.y=Input.GetAxis("Vertical");
    }
    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position,transform.position+movement*Time.deltaTime*playerSpeed,ref velocity,0f);
    }
}
