using UnityEngine;
using UnityEngine.InputSystem;

namespace MaskedMischiefNamespace
{
    public class PlayerPlaceTrapState : PlayerMovementState
    {
        public PlayerPlaceTrapState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Entered PlayerPlaceTrapState");
            PlaceTrap();
            stateMachine.ChangeState(stateMachine.IdlingState);
        }

        private void PlaceTrap()
        {
            GameObject trapPrefab = stateMachine.player.trapPrefab;
            if (trapPrefab != null)
            {
                Vector3 placePosition = stateMachine.player.transform.position + stateMachine.player.transform.forward * 1f;
                GameObject.Instantiate(trapPrefab, placePosition, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Trap prefab is not assigned in PlayerRunner.");
            }
        }

        protected override void AddCallbacks()
        {
            // No additional callbacks needed for this state
        }

        protected override void RemoveCallbacks()
        {
            // No additional callbacks to remove
        }
    }
}
