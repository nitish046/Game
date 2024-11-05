using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            AddCallbacks(); // Ensure callbacks are added when entering the state
        }

        public override void Exit()
        {
            base.Exit();
            RemoveCallbacks(); // Ensure callbacks are removed when exiting the state
        }

        protected override void AddCallbacks()
        {
            // Subscribe to GameInput's on_place_trap_action event
            stateMachine.player.gameInput.on_place_trap_action += OnPlaceTrapAction;
        }

        protected override void RemoveCallbacks()
        {
            // Unsubscribe to avoid memory leaks
            stateMachine.player.gameInput.on_place_trap_action -= OnPlaceTrapAction;
        }

        // Define OnPlaceTrapAction to handle the PlaceTrap event
        private void OnPlaceTrapAction(object sender, System.EventArgs e)
        {
            Debug.Log("OnPlaceTrapAction called in PlayerIdlingState");
            stateMachine.ChangeState(stateMachine.PlaceTrapState);
        }
    }
}
