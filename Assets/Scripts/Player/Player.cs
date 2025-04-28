using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(CollisionHandler))]
[RequireComponent(typeof(PlayerAnimator), typeof(PlayerMove))]
public class Player : MonoBehaviour
{
    private PlayerAnimator _animator;
    private PlayerMove _move;
    private InputReader _inputReader;
    private CollisionHandler _collisionHandler;

    private IInteractable _interactable;

    private void Awake()
    {
        _animator = GetComponent<PlayerAnimator>();
        _move = GetComponent<PlayerMove>();
        _inputReader = GetComponent<InputReader>();
        _collisionHandler = GetComponent<CollisionHandler>();
    }

    private void OnEnable()
    {
        _collisionHandler.FinishReached += OnFinishReached;
    }

    private void OnDisable()
    {
        _collisionHandler.FinishReached -= OnFinishReached;
    }

    private void FixedUpdate()
    {
        _animator.SetMoveX(_inputReader.SideWays);
        _animator.SetMoveY(_inputReader.ForwardWays);

        bool isWalking = _inputReader.SideWays != 0 || _inputReader.ForwardWays != 0;
        _animator.SetIsWalking(isWalking);

        if (isWalking)
        {
            _animator.SetLastMoveX(_inputReader.SideWays);
            _animator.SetLastMoveY(_inputReader.ForwardWays);
        }

        _move.Move();

        _move.Dash();

        if (_inputReader.GetIsInteract() && _interactable != null)
        {
            _interactable.Interact();
        }
    }

    private void OnFinishReached(IInteractable interactable)
    {
        _interactable = interactable;
    }

}
