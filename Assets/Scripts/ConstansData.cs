using UnityEngine;

public static class ConstansData
{
    public static class AnimatorParameters
    {
        public static readonly int MoveX = Animator.StringToHash(nameof(MoveX));
        public static readonly int MoveY = Animator.StringToHash(nameof(MoveY));
        public static readonly int LastMoveX = Animator.StringToHash(nameof(LastMoveX));
        public static readonly int LastMoveY = Animator.StringToHash(nameof(LastMoveY));
        public static readonly int IsWalking = Animator.StringToHash(nameof(IsWalking));
        public static readonly int IsOn = Animator.StringToHash(nameof(IsOn));
        public static readonly int IsOff = Animator.StringToHash(nameof(IsOff));
    }

    public static class InputData
    {
        public const string Horizontal_Axis = "Horizontal";
        public const string Vertical_Axis = "Vertical";
    }
}
