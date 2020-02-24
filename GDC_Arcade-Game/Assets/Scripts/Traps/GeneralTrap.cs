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
        /// Variable that contains the time for a trap to activate
        /// </summary>
        [Tooltip("To be used if traps that require a start up time, like FlameWall")]
        [SerializeField] protected float _wakeUpTime = default;
        /// <summary>
        /// Variable that contains the time a trap is activate
        /// </summary>
        [Tooltip("To be used if traps that require a start up time, like FlameWall")]
        [SerializeField] protected float _activeTime = default;
        /// <summary>
        /// Variable that contains the time a trap is inactive before activating
        /// again
        /// </summary>
        [Tooltip("To be used if traps that require a start up time, like FlameWall")]
        [SerializeField] protected float _cooldownTime = default;
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
        /// Variable responsible to be used on a coroutine to check if trap
        /// is going to be activated
        /// </summary>
        protected WaitForSeconds _wakeUpTimer = default;
        /// <summary>
        /// Variable responsible to be used on a coroutine to check how long
        /// a trap is active
        /// </summary>
        protected WaitForSeconds _activeTimer = default;
        /// <summary>
        /// Variable responsible to be used on a coroutine to check how long
        /// a trap is inactive
        /// </summary>
        protected WaitForSeconds _cooldownTimer = default;

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
        protected void MoveTrap()
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
        /// 
        /// </summary>
        /// <returns></returns>
        protected IEnumerator WarmUpTrap(Animator animator, 
                                         GameObject gameObject,
                                         Collider2D collider)
        {
            animator.enabled = true;
            yield return _wakeUpTimer;
            animator.enabled = false;
            StartCoroutine(TrapActive(animator, gameObject, collider));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IEnumerator TrapActive(Animator animator,
                                         GameObject gameObject,
                                         Collider2D collider)
        {
            gameObject.SetActive(true);
            collider.enabled = true;
            yield return _activeTimer;
            gameObject.SetActive(false);
            collider.enabled = false;
            yield return _cooldownTimer;
            StartCoroutine(WarmUpTrap(animator, gameObject, collider));
        }

        /// <summary>
        /// Sent when another object enters a trigger collider attached to this
        /// object (2D physics only).
        /// </summary>
        /// <param name="other">The other Collider2D involved in this collision.
        /// </param>
        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _knockBackDir =
                    (other.transform.position - transform.position).normalized;

                Player.PlayerBehaviour pStats = other.
                    GetComponent<Player.PlayerBehaviour>();

                pStats.OnHit(new Vector2(
                    _knockBack.x * _knockBackDir.x, _knockBack.y));

                print("Im hitting a player");
                print(_knockBack);
                print(_knockBackDir);
            }
        }
    }
}