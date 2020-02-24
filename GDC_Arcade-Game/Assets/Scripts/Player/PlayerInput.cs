using UnityEngine;

namespace GDC_Arcade_Game.Assets.Scripts.Player
{
    public class PlayerInput
    {
        private PlayerNumber controllingPlayer;
        private Vector2 inputVector;
        private bool jumpBool;

        public Vector2 MovementInput
        {
            get
            {
                switch (controllingPlayer)
                {
                    case PlayerNumber.PlayerOne:
                        inputVector.x = Input.GetAxis("Horizontal");
                        inputVector.y = Input.GetAxis("Vertical");
                        return inputVector;
                    case PlayerNumber.PlayerTwo:
                        inputVector.x = Input.GetAxis("Horizontal2");
                        inputVector.y = Input.GetAxis("Vertical2");
                        return inputVector;
                    default:
                        return inputVector;
                }
            }
        }

        public bool Jumping => Input.GetButtonDown
            (controllingPlayer == PlayerNumber.PlayerOne ? "Jump" : "Jump2");

        public PlayerInput(PlayerNumber number)
        {
            controllingPlayer = number;
        }
    }
}