using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MaskedMischiefNamespace
{
    public class PlayerIdlingState : PlayerGroundedState
    {
        public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            staticMovement = new Vector2(0, 0);
            stateMachine.player.animator.SetTrigger("isIdle");
            //AddCallbacks(); // Ensure callbacks are added when entering the state //No need, the base functions already do that
        }

        public override void Exit()
        {
            base.Exit();
            //RemoveCallbacks(); // Ensure callbacks are removed when exiting the state //No need, the base functions already do that
        }

        protected override void AddCallbacks()
        {
            base.AddCallbacks();
            //stateMachine.player.gameInput.on_place_trap_action += OnPlaceTrapAction;
        }

        protected override void RemoveCallbacks()
        {
            base.RemoveCallbacks();
            //stateMachine.player.gameInput.on_place_trap_action -= OnPlaceTrapAction;
        }

        // Define OnPlaceTrapAction to handle the PlaceTrap event
        private void OnPlaceTrapAction(object sender, System.EventArgs e)
        {
            Debug.Log("OnPlaceTrapAction called in PlayerIdlingState");
            stateMachine.ChangeState(stateMachine.PlaceTrapState);
        }
		protected override void OnPlaceTrapStart(InputAction.CallbackContext callbackContext)
		{
			base.OnPlaceTrapStart(callbackContext);
			Debug.Log("OnPlaceTrapAction called in PlayerIdlingState");
			stateMachine.ChangeState(stateMachine.PlaceTrapState);
		}
	}
}
