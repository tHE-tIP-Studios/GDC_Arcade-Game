using UnityEngine;

namespace GDC_Arcade_Game.Assets.Scripts.Traps
{
    public class MovingSaw : GeneralTrap
    {
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        protected override void Start()
        {
            _moveLeft = true;
            _moveRight = false;
            _turnTimer = _moveTime;
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected override void Update()
        {
            _turnTimer -= Time.deltaTime;
            MoveSaw();
            ChangeDirection();
        }

        protected override void OnTriggerEnter2D(Collider2D other)
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