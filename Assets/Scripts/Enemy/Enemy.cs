using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private Vector2 _seeAreaSize;
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private LayerMask _targetLayer;

    private Rigidbody2D _rigidbody;
    private Transform _target;
    private int _wayPointsIndex;
    private float _maxSqrDistance = 0.03f;
    private float _waitTime = 2.0f;
    private float _endWaitTime;
    private bool _isWaiting;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _target = _wayPoints[_wayPointsIndex].transform;
    }

    private void FixedUpdate()
    {
        if (TrySeeTarget(out Transform target))
        {
            Move(target);
            return;
        }

        if (_isWaiting == false)
            Move(_target);

        if (IsTargetReached() && _isWaiting == false)
        {
            _isWaiting = true;
            _endWaitTime = Time.time + _waitTime;
        }

        if (_isWaiting == true && _endWaitTime <= Time.time)
        {
            ChangedTarget();
            _isWaiting = false;
        }
    }

    private bool TrySeeTarget(out Transform target)
    {
        target = null;

        Collider2D hit = Physics2D.OverlapBox(GetLookAreaOrigin(), _seeAreaSize, 0, _targetLayer);

        if (hit != null)
        {

            Vector2 direction = (hit.transform.position - transform.position).normalized;
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, direction, _seeAreaSize.y, ~(1 << gameObject.layer));

            if (hit2D.collider != null)
            {
                if (hit2D.collider == hit)
                {
                    Debug.DrawLine(transform.position, hit2D.point, Color.red);
                    target = hit2D.transform;
                    return true;
                }
                else
                {
                    Debug.DrawLine(transform.position, hit2D.point, Color.white);
                }
            }
        }

        return false;
    }

    private void Move(Transform target)
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, target.position, _speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPosition);
    }

    private bool IsTargetReached()
    {
        float sqrDistance = (transform.position - _target.position).sqrMagnitude;

        return sqrDistance < _maxSqrDistance;
    }

    private void ChangedTarget()
    {
        _wayPointsIndex = ++_wayPointsIndex % _wayPoints.Length;
        _target = _wayPoints[_wayPointsIndex].transform;
    }

    private Vector2 GetLookAreaOrigin()
    {
        float halfAreaCoeffiecent = 2.0f;
        float originY = transform.position.y - _seeAreaSize.y / halfAreaCoeffiecent;
        return new Vector2(transform.position.x, originY);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GetLookAreaOrigin(), _seeAreaSize);
    }
}
