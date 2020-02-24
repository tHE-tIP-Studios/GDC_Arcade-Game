using UnityEngine;

namespace GDC_Arcade_Game.Assets.Scripts.Traps
{
    public class FlameWall : GeneralTrap
    {
        [SerializeField] private Animator animator = default;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        protected override void Start()
        {
            
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected override void Update()
        {
            
        }

        /// <summary>
        /// Sent when another object enters a trigger collider attached to this
        /// object (2D physics only).
        /// </summary>
        /// <param name="other">The other Collider2D involved in this collision.</param>
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            
        }
    }
}