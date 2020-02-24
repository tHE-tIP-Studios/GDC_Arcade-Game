using UnityEngine;

namespace GDC_Arcade_Game.Assets.Scripts.Traps
{
    public class FlameWall : GeneralTrap
    {
        [SerializeField] private Animator animator = default;
        [SerializeField] private GameObject particle = default;
        [SerializeField] private BoxCollider2D flameWall = default;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        protected override void Start()
        {
            _wakeUpTimer = new WaitForSeconds(_wakeUpTime);
            _activeTimer = new WaitForSeconds(_activeTime);
            _cooldownTimer = new WaitForSeconds(_cooldownTime);
            animator.enabled = false;
            particle.SetActive(false);
            StartCoroutine(WarmUpTrap(animator, particle, flameWall));
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected override void Update()
        {
            
        }
    }
}