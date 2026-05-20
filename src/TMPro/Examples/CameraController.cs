using System;
using UnityEngine;

namespace TMPro.Examples
{
	// Token: 0x0200042C RID: 1068
	public class CameraController : MonoBehaviour
	{
		// Token: 0x06001812 RID: 6162 RVA: 0x00095EAC File Offset: 0x000940AC
		private void Awake()
		{
			if (QualitySettings.vSyncCount > 0)
			{
				Application.targetFrameRate = 60;
			}
			else
			{
				Application.targetFrameRate = -1;
			}
			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
			{
				Input.simulateMouseWithTouches = false;
			}
			this.cameraTransform = base.transform;
			this.previousSmoothing = this.MovementSmoothing;
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00095EFF File Offset: 0x000940FF
		private void Start()
		{
			if (this.CameraTarget == null)
			{
				this.dummyTarget = new GameObject("Camera Target").transform;
				this.CameraTarget = this.dummyTarget;
			}
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00095F30 File Offset: 0x00094130
		private void LateUpdate()
		{
			this.GetPlayerInput();
			if (this.CameraTarget != null)
			{
				if (this.CameraMode == CameraController.CameraModes.Isometric)
				{
					this.desiredPosition = this.CameraTarget.position + Quaternion.Euler(this.ElevationAngle, this.OrbitalAngle, 0f) * new Vector3(0f, 0f, -this.FollowDistance);
				}
				else if (this.CameraMode == CameraController.CameraModes.Follow)
				{
					this.desiredPosition = this.CameraTarget.position + this.CameraTarget.TransformDirection(Quaternion.Euler(this.ElevationAngle, this.OrbitalAngle, 0f) * new Vector3(0f, 0f, -this.FollowDistance));
				}
				if (this.MovementSmoothing)
				{
					this.cameraTransform.position = Vector3.SmoothDamp(this.cameraTransform.position, this.desiredPosition, ref this.currentVelocity, this.MovementSmoothingValue * Time.fixedDeltaTime);
				}
				else
				{
					this.cameraTransform.position = this.desiredPosition;
				}
				if (this.RotationSmoothing)
				{
					this.cameraTransform.rotation = Quaternion.Lerp(this.cameraTransform.rotation, Quaternion.LookRotation(this.CameraTarget.position - this.cameraTransform.position), this.RotationSmoothingValue * Time.deltaTime);
					return;
				}
				this.cameraTransform.LookAt(this.CameraTarget);
			}
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x000960B0 File Offset: 0x000942B0
		private void GetPlayerInput()
		{
			this.moveVector = Vector3.zero;
			this.mouseWheel = Input.GetAxis("Mouse ScrollWheel");
			float num = (float)Input.touchCount;
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || num > 0f)
			{
				this.mouseWheel *= 10f;
				if (Input.GetKeyDown(KeyCode.I))
				{
					this.CameraMode = CameraController.CameraModes.Isometric;
				}
				if (Input.GetKeyDown(KeyCode.F))
				{
					this.CameraMode = CameraController.CameraModes.Follow;
				}
				if (Input.GetKeyDown(KeyCode.S))
				{
					this.MovementSmoothing = !this.MovementSmoothing;
				}
				if (Input.GetMouseButton(1))
				{
					this.mouseY = Input.GetAxis("Mouse Y");
					this.mouseX = Input.GetAxis("Mouse X");
					if (this.mouseY > 0.01f || this.mouseY < -0.01f)
					{
						this.ElevationAngle -= this.mouseY * this.MoveSensitivity;
						this.ElevationAngle = Mathf.Clamp(this.ElevationAngle, this.MinElevationAngle, this.MaxElevationAngle);
					}
					if (this.mouseX > 0.01f || this.mouseX < -0.01f)
					{
						this.OrbitalAngle += this.mouseX * this.MoveSensitivity;
						if (this.OrbitalAngle > 360f)
						{
							this.OrbitalAngle -= 360f;
						}
						if (this.OrbitalAngle < 0f)
						{
							this.OrbitalAngle += 360f;
						}
					}
				}
				if (num == 1f && Input.GetTouch(0).phase == TouchPhase.Moved)
				{
					Vector2 deltaPosition = Input.GetTouch(0).deltaPosition;
					if (deltaPosition.y > 0.01f || deltaPosition.y < -0.01f)
					{
						this.ElevationAngle -= deltaPosition.y * 0.1f;
						this.ElevationAngle = Mathf.Clamp(this.ElevationAngle, this.MinElevationAngle, this.MaxElevationAngle);
					}
					if (deltaPosition.x > 0.01f || deltaPosition.x < -0.01f)
					{
						this.OrbitalAngle += deltaPosition.x * 0.1f;
						if (this.OrbitalAngle > 360f)
						{
							this.OrbitalAngle -= 360f;
						}
						if (this.OrbitalAngle < 0f)
						{
							this.OrbitalAngle += 360f;
						}
					}
				}
				RaycastHit raycastHit;
				if (Input.GetMouseButton(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 300f, 23552))
				{
					if (raycastHit.transform == this.CameraTarget)
					{
						this.OrbitalAngle = 0f;
					}
					else
					{
						this.CameraTarget = raycastHit.transform;
						this.OrbitalAngle = 0f;
						this.MovementSmoothing = this.previousSmoothing;
					}
				}
				if (Input.GetMouseButton(2))
				{
					if (this.dummyTarget == null)
					{
						this.dummyTarget = new GameObject("Camera Target").transform;
						this.dummyTarget.position = this.CameraTarget.position;
						this.dummyTarget.rotation = this.CameraTarget.rotation;
						this.CameraTarget = this.dummyTarget;
						this.previousSmoothing = this.MovementSmoothing;
						this.MovementSmoothing = false;
					}
					else if (this.dummyTarget != this.CameraTarget)
					{
						this.dummyTarget.position = this.CameraTarget.position;
						this.dummyTarget.rotation = this.CameraTarget.rotation;
						this.CameraTarget = this.dummyTarget;
						this.previousSmoothing = this.MovementSmoothing;
						this.MovementSmoothing = false;
					}
					this.mouseY = Input.GetAxis("Mouse Y");
					this.mouseX = Input.GetAxis("Mouse X");
					this.moveVector = this.cameraTransform.TransformDirection(this.mouseX, this.mouseY, 0f);
					this.dummyTarget.Translate(-this.moveVector, Space.World);
				}
			}
			if (num == 2f)
			{
				Touch touch = Input.GetTouch(0);
				Touch touch2 = Input.GetTouch(1);
				Vector2 a = touch.position - touch.deltaPosition;
				Vector2 b = touch2.position - touch2.deltaPosition;
				float magnitude = (a - b).magnitude;
				float magnitude2 = (touch.position - touch2.position).magnitude;
				float num2 = magnitude - magnitude2;
				if (num2 > 0.01f || num2 < -0.01f)
				{
					this.FollowDistance += num2 * 0.25f;
					this.FollowDistance = Mathf.Clamp(this.FollowDistance, this.MinFollowDistance, this.MaxFollowDistance);
				}
			}
			if (this.mouseWheel < -0.01f || this.mouseWheel > 0.01f)
			{
				this.FollowDistance -= this.mouseWheel * 5f;
				this.FollowDistance = Mathf.Clamp(this.FollowDistance, this.MinFollowDistance, this.MaxFollowDistance);
			}
		}

		// Token: 0x04001744 RID: 5956
		private Transform cameraTransform;

		// Token: 0x04001745 RID: 5957
		private Transform dummyTarget;

		// Token: 0x04001746 RID: 5958
		public Transform CameraTarget;

		// Token: 0x04001747 RID: 5959
		public float FollowDistance = 30f;

		// Token: 0x04001748 RID: 5960
		public float MaxFollowDistance = 100f;

		// Token: 0x04001749 RID: 5961
		public float MinFollowDistance = 2f;

		// Token: 0x0400174A RID: 5962
		public float ElevationAngle = 30f;

		// Token: 0x0400174B RID: 5963
		public float MaxElevationAngle = 85f;

		// Token: 0x0400174C RID: 5964
		public float MinElevationAngle;

		// Token: 0x0400174D RID: 5965
		public float OrbitalAngle;

		// Token: 0x0400174E RID: 5966
		public CameraController.CameraModes CameraMode;

		// Token: 0x0400174F RID: 5967
		public bool MovementSmoothing = true;

		// Token: 0x04001750 RID: 5968
		public bool RotationSmoothing;

		// Token: 0x04001751 RID: 5969
		private bool previousSmoothing;

		// Token: 0x04001752 RID: 5970
		public float MovementSmoothingValue = 25f;

		// Token: 0x04001753 RID: 5971
		public float RotationSmoothingValue = 5f;

		// Token: 0x04001754 RID: 5972
		public float MoveSensitivity = 2f;

		// Token: 0x04001755 RID: 5973
		private Vector3 currentVelocity = Vector3.zero;

		// Token: 0x04001756 RID: 5974
		private Vector3 desiredPosition;

		// Token: 0x04001757 RID: 5975
		private float mouseX;

		// Token: 0x04001758 RID: 5976
		private float mouseY;

		// Token: 0x04001759 RID: 5977
		private Vector3 moveVector;

		// Token: 0x0400175A RID: 5978
		private float mouseWheel;

		// Token: 0x0400175B RID: 5979
		private const string event_SmoothingValue = "Slider - Smoothing Value";

		// Token: 0x0400175C RID: 5980
		private const string event_FollowDistance = "Slider - Camera Zoom";

		// Token: 0x0200042D RID: 1069
		public enum CameraModes
		{
			// Token: 0x0400175E RID: 5982
			Follow,
			// Token: 0x0400175F RID: 5983
			Isometric,
			// Token: 0x04001760 RID: 5984
			Free
		}
	}
}
