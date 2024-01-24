using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private GameObject _trailPrefab;
    [SerializeField] private Transform _trailOffset;

    private GameManager _gameManager;

    private void Awake()
    {
        _camera = Camera.main;
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        DestroyWhenOffScreen();
        LeaveTrail();
    }

    public bool isOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        return screenPosition.x < 0 || screenPosition.x > _camera.pixelWidth || screenPosition.y < 0 || screenPosition.y > _camera.pixelHeight;
    }

    private void DestroyWhenOffScreen()
    {
        if(isOffScreen())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            _gameManager.EnemyDestroyed();
        }
    }

    private void LeaveTrail()
    {
        GameObject trail = Instantiate(_trailPrefab, _trailOffset.position, transform.rotation);
    }
}
