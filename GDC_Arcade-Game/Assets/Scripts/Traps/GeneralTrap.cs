using UnityEngine;
using System.Collections;

namespace GDC_Arcade_Game.Assets.Scripts.Traps
{
    public abstract class GeneralTrap : MonoBehaviour
    {
        /// <summary>
        /// Variable responsible by movement distance per frame 
        /// (in case trap moves)
        /// </summary>
        [SerializeField] protected Vector3 _moveDistant = default;
        /// <summary>
        /// Variable responsible for the movement time
        /// (in case trap moves)
        /// </summary>
        [SerializeField] protected float _moveTime = default;
        /// <summary>
        /// Knockback vector that a trap does to a player
        /// </summary>
        [SerializeField] protected Vector2 _knockBack = default;
        /// <summary>
        /// Knockback direction
        /// </summary>
        protected Vector2 _knockBackDir = default;
        /// <summary>
        /// Variable responsible for counting down the time until movement
        /// direction changes
        /// </summary>
        protected float _turnTimer = default;
        /// <summary>
        /// Bool to check if trap (in case it moves) 
        /// is going left on the screen
        /// </summary>
        protected bool _moveLeft = default;
        /// <summary>
        /// Bool to check if trap (in case it moves) 
        /// is going right on the screen
        /// </summary>
        protected bool _moveRight = default;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        protected abstract void Start();


        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected abstract void Update();

        /// <summary>
        /// Method to move the saw in a direction
        /// </summary>
        protected void MoveSaw()
        {
            if (_moveLeft)
            {
                transform.position -= _moveDistant * Time.deltaTime;
            }

            if (_moveRight)
            {
                transform.position += _moveDistant * Time.deltaTime;
            }
        }

        /// <summary>
        /// Method to change movement direction
        /// </summary>
        protected void ChangeDirection()
        {
            if (_turnTimer < 0)
            {
                _turnTimer = _moveTime;
                _moveLeft = !_moveLeft;
                _moveRight = !_moveRight;
            }
        }

        /// <summary>
        /// Sent when another object enters a trigger collider attached to this
        /// object (2D physics only).
        /// </summary>
        /// <param name="other">The other Collider2D involved in this collision.
        /// </param>
        protected abstract void OnTriggerEnter2D(Collider2D other);
    }
}