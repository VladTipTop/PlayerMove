using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputReader))]
public class PlayerMove : MonoBehaviour
{
    private const float Speed_coefficient = 50.0f;
    private const string Layer_River = "River";

    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _dashForce = 1000.0f;

    private Rigidbody2D _rigidbody;
    private InputReader _inputReader;
    private Vector2 _dashMove;
    private bool _isDash;
    private float _coolDownTime = 3.0f;
    private bool _canUse = true;

    private int _playerLayer;
    private int _riverLayer;
   // private bool _isInRiver;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputReader = GetComponent<InputReader>();

        _playerLayer = gameObject.layer;
        _riverLayer = LayerMask.NameToLayer(Layer_River);

        SetIgnoreRiverCollision(false);
    }

    private void SetIgnoreRiverCollision(bool ignore)
    {
        Physics2D.IgnoreLayerCollision(_playerLayer, _riverLayer, ignore);
    }

    public void Move()
    {
        _rigidbody.velocity = new Vector2(_inputReader.SideWays, _inputReader.ForwardWays) * _speed * Speed_coefficient;
    }

    public void Dash()
    {
        _dashMove = new Vector2(_inputReader.SideWays, _inputReader.ForwardWays).normalized;

        if (_canUse && _inputReader.GetIsDash() && (_inputReader.SideWays != 0 || _inputReader.ForwardWays != 0))
        {
            _isDash = true;

            SetIgnoreRiverCollision(true);
        }

        if (_isDash)
        {
            _rigidbody.AddForce(_dashMove * _dashForce);
            _isDash = false;
            StartCoroutine(UseAbility());
        }
    }
    private IEnumerator UseAbility()
    {
        _canUse = false;
        yield return new WaitForSeconds(_coolDownTime);
        _canUse = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _riverLayer)
        {
            //_isInRiver = true;

            if (!_isDash)
                SetIgnoreRiverCollision(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _riverLayer)
        {
           // _isInRiver = false;

            if (_isDash)
                SetIgnoreRiverCollision(true);
        }

        SetIgnoreRiverCollision(false);
    }
}
