using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    [SerializeField] private Animator _animator;

    public void SetMoveX(float moveX) {
        _animator.SetFloat(ConstansData.AnimatorParameters.MoveX, moveX);
    }

    public void SetMoveY(float moveY) {
        _animator.SetFloat(ConstansData.AnimatorParameters.MoveY, moveY);
    }

    public void SetIsWalking (bool isWalking) {
        _animator.SetBool(ConstansData.AnimatorParameters.IsWalking, isWalking);
    }

    public void SetLastMoveX (float lastMoveX) {
        _animator.SetFloat(ConstansData.AnimatorParameters.LastMoveX, lastMoveX);
    }
    
    public void SetLastMoveY (float lastMoveY) {
        _animator.SetFloat(ConstansData.AnimatorParameters.LastMoveY, lastMoveY);
    }
}
