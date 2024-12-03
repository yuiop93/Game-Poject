using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;

#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool fire;
		public bool aim;
		public bool walk;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
public void CursorUnLock()
		{
			cursorLocked = false;
			cursorInputForLook = false;
			SetCursorState(false);
			aim = false;
		}
		public void Cursorlock()
		{
			cursorLocked = true;
			cursorInputForLook = true;
			SetCursorState(true);
			aim = false;
		}
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		public void OnAim(InputValue value)
		{
			AimInput(value.isPressed);
		}
		public void OnFire(InputValue value)
		{
			FireInput(value.isPressed);
		}
		public void OnWalk(InputValue value)
		{
			WalkInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}
		public void WalkInput(bool newWalkState)
		{
			walk = !walk;
		}
		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
			walk = false;
		}
		public void AimInput(bool newAimState)
		{
			if(!坐下.isSitting)
			aim = !aim;
			fire = false;
		}
		public void FireInput(bool newFireState)
		{
			if(aim)
			fire = newFireState;
		}
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

	}

}