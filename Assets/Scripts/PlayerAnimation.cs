using UnityEngine;


public sealed class PlayerAnimation : MonoBehaviour
{
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int FireDisable = Animator.StringToHash("FireDisable");
    private static readonly int FireEnable = Animator.StringToHash("FireEnable");
    public Animator Animator { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void SetMove(Vector3 movement)
    {
        Animator.SetFloat(Vertical, movement.z);
        Animator.SetFloat(Horizontal, movement.x);
    }

    public void OnFireEnable()
    {
        Animator.SetTrigger(FireEnable);
    }

    public void OnFireDisable()
    {
        Animator.SetTrigger(FireDisable);
    }


}
