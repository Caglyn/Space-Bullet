using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private bool isRotatingLeft;
    [SerializeField] private float rotationTime = 3.5f;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _indicatorOffset;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private int _bulletNumber = 4;

    private UIManager _uiManager;
    private GameManager _gameManager;

    private void Awake()
    {
        isRotatingLeft = true;
        rotationTime = 2f;
        _uiManager = FindObjectOfType<UIManager>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Update()
    {
        if(_bulletNumber != 0)
        {
            FireBullet();
        }
        else
        {
            StartCoroutine(OnLastBullet());
        }
    }
    
    IEnumerator OnLastBullet(){
        yield return new WaitForSeconds(2f);
        _gameManager.LevelFailed();
    }

    private void FireBullet()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(_bulletPrefab, _indicatorOffset.position, transform.rotation);
            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
            rigidbody.velocity = _bulletSpeed * transform.up;
            _bulletNumber--;
            _uiManager.OnBulletFired(_bulletNumber);
        }
    }

    public int getBulletNumber(){
        return _bulletNumber;
    }

    private void Rotate()
    {
        if(isRotatingLeft == true)
        {
            transform.Rotate(0f, 0f, (rotationSpeed * Time.deltaTime));
            StartCoroutine(RotateLeftRoutine());
        }
        else
        {
            transform.Rotate(0f, 0f, (-rotationSpeed * Time.deltaTime));
            StartCoroutine(RotateLRightRoutine());
        }
    }

    IEnumerator RotateLeftRoutine()
    {
        yield return new WaitForSeconds(rotationTime);
        isRotatingLeft = false;
        rotationTime = 4f;
    }

    IEnumerator RotateLRightRoutine()
    {
        yield return new WaitForSeconds(rotationTime);
        isRotatingLeft = true;
    }

}
