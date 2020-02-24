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
    }
}