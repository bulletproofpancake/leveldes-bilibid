using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretScript : MonoBehaviour
{
    RaycastHit2D _hitInfo;
    bool _playerOnSight;
    [Header("RANGE")]
    [SerializeField] float _distance;
    [SerializeField] float _rotationSpeed;
    [SerializeField] LineRenderer _lineOfSight;
    [SerializeField] Gradient _redColor;
    [SerializeField] Gradient _greenColor;

    float _lineWidthActive = 0.001f;
    float _lineWidthDefault = 0.05f;

    [Header("SHOOTING")]
    [SerializeField] Transform _bulletSpawnPosition;
    [SerializeField] float _startTimeBtwShots;
    [SerializeField] GameObject _bullet;
    float _timeBtwShots;
    bool _isRotating = true;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        _timeBtwShots = _startTimeBtwShots;
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
            transform.Rotate(Vector3.forward * -_rotationSpeed * Time.deltaTime);
        }
        _hitInfo = Physics2D.Raycast(transform.position, transform.up, _distance);

        if (_hitInfo.collider != null)
        {
            if (_hitInfo.collider.gameObject.CompareTag("Player"))
            {
                _isRotating = false;
                _lineOfSight.SetPosition(1, _hitInfo.point);
                _lineOfSight.colorGradient = _redColor;
                gameplayScript._playerCaught = true;
                //turretShoot();
            }
            else
            {
                _isRotating = true;
                _lineOfSight.SetPosition(1, _hitInfo.point);
                _lineOfSight.colorGradient = _greenColor;
                _timeBtwShots = _startTimeBtwShots;
                _lineOfSight.startWidth = _lineWidthDefault;
                _lineOfSight.endWidth = _lineWidthDefault;
            }
        }
        else
        {
            _isRotating = true;
            _lineOfSight.SetPosition(1, transform.position + transform.up * _distance);
            _lineOfSight.colorGradient = _greenColor;
            _timeBtwShots = _startTimeBtwShots;
            _lineOfSight.startWidth = _lineWidthDefault;
            _lineOfSight.endWidth = _lineWidthDefault;
        }
        _lineOfSight.SetPosition(0, transform.position);
    }

    void turretShoot()
    {
        if (_timeBtwShots <= 0)
        {

            //Instantiate(_bullet, _bulletSpawnPosition.transform.position, Quaternion.identity);
            _timeBtwShots = _startTimeBtwShots;
            _lineOfSight.startWidth = _lineWidthDefault;
            _lineOfSight.endWidth = _lineWidthDefault;
            gameplayScript._playerCaught = true;
        }
        else
        {
            _timeBtwShots -= Time.deltaTime;
            _lineOfSight.startWidth += _lineWidthActive;
            _lineOfSight.endWidth += _lineWidthActive;
        }

    }
}
