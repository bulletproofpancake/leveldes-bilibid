using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    [SerializeField] float _movementSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _distance;
    [SerializeField] LineRenderer _lineOfSight;
    [SerializeField] Gradient _redColor;
    [SerializeField] Gradient _greenColor;

    Transform _target;
    bool _isRotating = true;
    RaycastHit2D _hitInfo;
    Vector3 _startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        _startingPosition = transform.position;
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {
        lookForPlayer();
    }

    void lookForPlayer()
    {
        if (_isRotating)
        {
            transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
        }

        //angle = Vector3.Angle(new Vector3(0, 0, transform.position.z - 135), new Vector3(0, 0, transform.position.z + 135));
        _hitInfo = Physics2D.Raycast(transform.position, transform.up, _distance);

        if (_hitInfo.collider != null)
        {
            if (_hitInfo.collider.gameObject.CompareTag("Player"))
            {
                _lineOfSight.SetPosition(1, _hitInfo.point);
                _lineOfSight.colorGradient = _redColor;
                catchPlayer();
            }
            else
            {
                _lineOfSight.SetPosition(1, _hitInfo.point);
                _lineOfSight.colorGradient = _greenColor;
                lostPlayer();
            }
        }
        else
        {
            _lineOfSight.SetPosition(1, transform.position + transform.up * _distance);
            _lineOfSight.colorGradient = _greenColor;
            lostPlayer();
        }

        _lineOfSight.SetPosition(0, transform.position);
    }

    void catchPlayer()
    {
        _isRotating = false;
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _movementSpeed * Time.deltaTime);
    }

    void lostPlayer()
    {
        FindObjectOfType<audioManagerScript>().playSound("GuardAlert");
        _isRotating = true;
        transform.position = Vector2.MoveTowards(transform.position, _startingPosition, _movementSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.CompareTag("Player"))
            gameplayScript._playerCaught = true;
        else gameplayScript._playerCaught = false;
    }

}
