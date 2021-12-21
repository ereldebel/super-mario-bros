using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class M_WalkByKeyPress : MonoBehaviour
{
    [SerializeField] private KeyCode rightKey = KeyCode.RightArrow;
    [SerializeField] private KeyCode leftKey = KeyCode.LeftArrow;
    [SerializeField] private float speed = 10;
    
    private Rigidbody2D _rigidbody2D;
    private Vector2 _currentMovement = Vector2.zero;
    private Vector2 _right = Vector2.right;
    private Vector2 _left = Vector2.left;

    private void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _right *= speed;
        _left *= speed;
    }

    private void Update()
    {
        _currentMovement = Vector2.zero;
        if (Input.GetKey(leftKey))
            _currentMovement += _left;
        if (Input.GetKey(rightKey))
            _currentMovement += _right;
    }

    private void FixedUpdate()
    {
        _rigidbody2D.AddForce(_currentMovement);
    }
}
