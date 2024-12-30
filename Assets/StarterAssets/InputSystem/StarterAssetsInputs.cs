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
		public bool fKey;
		public bool bKey;
		public bool cKey;
		public bool qKey;
		public bool eKey;

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
			fire = false;
		}
		public void Cursorlock()
		{
			cursorLocked = true;
			cursorInputForLook = true;
			SetCursorState(true);
			aim = false;
			fire = false;
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
		public void OnFKey(InputValue value)
		{
			FKeyInput(value.isPressed);
		}
		public void OnBKey(InputValue value)
		{
			BKeyInput(value.isPressed);
		}
		public void OnCKey(InputValue value)
		{
			CKeyInput(value.isPressed);
		}
		public void OnQKey(InputValue value)
		{
			QKeyInput(value.isPressed);
		}
		public void OnEKey(InputValue value)
		{
			EKeyInput(value.isPressed);
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
		public void FKeyInput(bool newFKeyState)
		{
			fKey = newFKeyState;
		}

		public void BKeyInput(bool newBKeyState)
		{
			bKey = newBKeyState;
		}
		public void CKeyInput(bool newCKeyState)
		{
			cKey = newCKeyState;
		}
		public void QKeyInput(bool newQKeyState)
		{
			qKey = newQKeyState;
		}
		public void EKeyInput(bool newEKeyState)
		{
			eKey = newEKeyState;
		}
		public void AimInput(bool newAimState)
		{
			if (!坐下.isSitting)
				aim = !aim;
			fire = false;
		}
		public void FireInput(bool newFireState)
		{
			if (aim)
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