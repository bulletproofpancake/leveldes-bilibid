using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 5.0f;

    Vector2 _movement;
    Rigidbody2D _rb;
    Vector2 _mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    private void FixedUpdate()
    {
        moveLogic();
    }

    void movePlayer()
    {
        _movement = new Vector2
        (
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
        _movement.Normalize();
        restrictMovement();
        controlReticle();
    }

    void moveLogic()
    {
        _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
        Vector2 lookDirection = _mousePosition - _rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = angle;

    }

    void controlReticle()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void restrictMovement()
    {
        Vector3 upperRightCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector3 lowerLeftCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));

        float playerWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float playerHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        float xVal = Mathf.Clamp(transform.position.x, lowerLeftCorner.x + playerWidth, upperRightCorner.x - playerWidth);
        float yVal = Mathf.Clamp(transform.position.y, lowerLeftCorner.y + playerHeight, upperRightCorner.y - playerHeight);

        transform.position = new Vector3(xVal, yVal, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Exit"))
        {

            FindObjectOfType<audioManagerScript>().playSound("Exit");
            gameplayScript._reachedExit = true;
        }
        else gameplayScript._reachedExit = false;
        if (other.gameObject.CompareTag("Keys"))
        {

            FindObjectOfType<audioManagerScript>().playSound("GetKey");
            gameplayScript._hasKey++;
            Destroy(other.gameObject);
        }
    }
}
