using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaskedMischiefNamespace
{
    public class PlayerRunningState : PlayerGroundedState
    {

        public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            stateMachine.player.animator.SetTrigger("isSprinting");
        }

        public override void Exit()
        {
            base.Exit();
            stateMachine.player.animator.ResetTrigger("isSprinting");
        }

        /*
        public override void PhysicsUpdate()
        {
            float speed;
            speed = stateMachine.player.walkSpeed;

            Vector3 cameraDir = stateMachine.player.mainCamera.transform.rotation.eulerAngles;
			Vector3 moveDir = Quaternion.Euler(0, cameraDir.y, 0) * new Vector3(staticMovement.x, 0, staticMovement.y);
			//stateMachine.player.transform.Translate(moveDir * stateMachine.player.walkSpeed);
            
            if (stateMachine.player.isSprinting && !stateMachine.player.is_cooldown)
            {
                if(current_sprint_time >= sprint_cooldown)
                {
                    stateMachine.player.is_cooldown = true;
                    current_sprint_time = 0f;
                    //stateMachine.player.animator.ResetTrigger("isSprinting");
                    //stateMachine.player.animator.SetTrigger("isWalking");
                }
                speed = stateMachine.player.runSpeed;
                current_sprint_time += Time.deltaTime;
            }
            else
            {
                if (cooldown_timer >= sprint_cooldown)
                {
                    stateMachine.player.is_cooldown = false;
                    cooldown_timer = 0f;
                    //stateMachine.player.animator.ResetTrigger("isWalking");
                    //stateMachine.player.animator.SetTrigger("isSprinting");
                }
                speed = stateMachine.player.walkSpeed;
                cooldown_timer += Time.deltaTime;
            }
            
			if (!moveDir.Equals(new Vector3(0, 0, 0)))
			{
				Quaternion moveAngle = Quaternion.LookRotation(moveDir);
				stateMachine.player.transform.rotation = Quaternion.Slerp(stateMachine.player.transform.rotation, moveAngle, 0.2f);
				if (!stateMachine.player.WillCollide(stateMachine.player.transform.forward, speed, new[] { "Collidable" }))
					stateMachine.player.GetComponent<CharacterController>().Move(stateMachine.player.transform.forward * speed);

			}
			stateMachine.player.GetComponent<CharacterController>().Move(Vector3.down * -stateMachine.player.yVelocity);
            
		}
        */

        public override void PhysicsUpdate()
        {
            if (stateMachine.player.isSprinting && !stateMachine.player.is_cooldown)
            {
                if (current_sprint_time >= sprint_cooldown)
                {
                    stateMachine.player.is_cooldown = true;
                    current_sprint_time = 0f;
                    stateMachine.player.animator.ResetTrigger("isSprinting");
                    stateMachine.player.animator.SetTrigger("isWalking");
                }
                speed = stateMachine.player.runSpeed;
                current_sprint_time += Time.deltaTime;
            }
            else
            {
                if (cooldown_timer >= sprint_cooldown)
                {
                    stateMachine.player.is_cooldown = false;
                    cooldown_timer = 0f;
                    stateMachine.player.animator.ResetTrigger("isWalking");
                    stateMachine.player.animator.SetTrigger("isSprinting");
                }
                speed = stateMachine.player.walkSpeed;
                cooldown_timer += Time.deltaTime;
            }
            base.PhysicsUpdate();
        }

    }
}
