using UnityEngine;

public class EnemyHorizontalMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Rigidbody2D r2d;

    private void Awake()
    {
        r2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if(transform.position.x <= -6f || transform.position.x >= 6f)
        {
            _moveSpeed = -_moveSpeed;
        }
        r2d.velocity = new Vector2(_moveSpeed, r2d.velocity.y);
    }
}
