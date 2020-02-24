using System.Collections;
using UnityEngine;

namespace GDC_Arcade_Game.Assets.Scripts.Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Player's hit points
        /// </summary>
        [SerializeField] private byte _playerHp = default;
        /// <summary>
        /// The time (in seconds) a player is invulnerable after being hit
        /// </summary>
        [SerializeField] private float _invulTime = default;
        /// <summary>
        /// Timer to be be used in invulnerable coroutine, value is _invulTime
        /// value
        /// </summary>
        private WaitForSeconds _invulTimer = default;
        /// <summary>
        /// Player's avatar Rigidbody2D
        /// </summary>
        private Rigidbody2D _rb = default;
        /// <summary>
        /// Bool to check if the player is to be knocked back after being hit
        /// </summary>
        private bool _knockbacked = default;
        private bool _isInvulnerable = default;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _invulTimer = new WaitForSeconds(_invulTime);
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            if (_playerHp <= 0)
            {
                //Destroy(this.gameObject);
                print("I should be dead");
            }

            if (_knockbacked)
            {
                StartCoroutine(Invulnerable());
            }
        }

        /// <summary>
        /// Coroutine responsible for making the player invulnerable to traps
        /// for a certain time
        /// </summary>
        /// <returns> WaitForSeconds with invulnerability time</returns>
        private IEnumerator Invulnerable()
        {
            _isInvulnerable = true;
            print("Im invulnerable!");
            yield return _invulTimer;
            _isInvulnerable = false;
            _knockbacked = false;
        }

        /// <summary>
        /// Method responsible for the loss of HP on hit and knockback 
        /// to the avatar
        /// </summary>
        /// <param name="_knockBack"> Knockback vector to determine direction
        /// and force.
        /// </param>
        public void OnHit(Vector2 _knockBack)
        {
            if (!_isInvulnerable)
            {
                _playerHp -= 1;
                _rb.AddForce(_knockBack);
                _knockbacked = true;
            }
            else
            {
                return;
            }
        }
    }
}