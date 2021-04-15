using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public int health = 3;
    public Camera camera;

    private float moveLimiter = 0.7f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 mousePosition;

    [SerializeField] private Text healthText;
    [SerializeField] private Canvas gameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthText.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        if(movement.x != 0 && movement.y != 0)
        {
            movement *= moveLimiter;
        }

        rb.MovePosition(rb.position + (movement * moveSpeed * Time.fixedDeltaTime));

        Vector2 faceDir = mousePosition - rb.position;
        float angle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            --health;
            healthText.text = health.ToString();
        }    

        if(health <= 0)
        {
            gameOverCanvas.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
