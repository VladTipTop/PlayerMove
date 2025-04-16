using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerMove : MonoBehaviour {
    private const float SPEED = 50.0f;
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _dashForce = 1000.0f;

    private Rigidbody2D _playerRigidbody;
    private PlayerAnimator _animator;
    private float _horizontalInput;
    private float _verticalInput;
    private Vector2 _dashMove;
    private bool _isDash;

    private void Start() {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<PlayerAnimator>();
    }

    private void Update() {
        _horizontalInput = Input.GetAxis(HORIZONTAL_AXIS);
        _verticalInput = Input.GetAxis(VERTICAL_AXIS);
        _dashMove = new Vector2(_horizontalInput, _verticalInput).normalized;

        if ((Input.GetKeyDown(KeyCode.Space)) && (_horizontalInput != 0 || _verticalInput != 0)) {
            _isDash = true;
        }
    }

    private void FixedUpdate() {
        _animator.SetMoveX(_horizontalInput);
        _animator.SetMoveY(_verticalInput);

        bool isWalking = _horizontalInput !=0 || _verticalInput != 0;
        _animator.SetIsWalking(isWalking);

        if (isWalking ) {
            _animator.SetLastMoveX(_horizontalInput);
            _animator.SetLastMoveY(_verticalInput);
        }

        _playerRigidbody.velocity = new Vector2(_horizontalInput, _verticalInput) * _speed * SPEED *
            Time.fixedDeltaTime;

        if (_isDash) {
            _playerRigidbody.AddForce(_dashMove * _dashForce);
            _isDash = false;
        }
    }

}
