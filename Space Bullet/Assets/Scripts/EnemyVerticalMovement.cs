using UnityEngine;

public class EnemyVerticalMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D r2d;
    private GameManager _gameManager;

    private void Awake()
    {
        r2d = GetComponent<Rigidbody2D>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        r2d.velocity = new Vector2(r2d.velocity.x, -_moveSpeed);

        if(transform.position.y <= -5.6f)
        {
            _gameManager.LevelFailed();
        }
    }
}
