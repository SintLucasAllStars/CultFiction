using UnityEngine;

public class AnimController : MonoBehaviour
{
    private MovementController _movementController;
    private ShootingController _shootingController;
    private Animator animController;

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsShooting = Animator.StringToHash("IsShooting");
    private static readonly int IsAimning = Animator.StringToHash("IsAiming");

    private void Start()
    {
        _movementController = GetComponent<MovementController>();
        animController = GetComponent<Animator>();
        _shootingController = GetComponent<ShootingController>();
    }

    private void Update()
    {
        animController.SetFloat(Speed, _movementController.movementSpeed);
        animController.SetBool(IsShooting, _shootingController.isShooting);
        animController.SetBool(IsAimning, _shootingController.isAiming);
    }
}