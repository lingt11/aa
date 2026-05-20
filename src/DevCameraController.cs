using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class DevCameraController : MonoBehaviour
{
	// Token: 0x060000BA RID: 186 RVA: 0x00005970 File Offset: 0x00003B70
	private void Start()
	{
		Vector3 eulerAngles = base.transform.eulerAngles;
		this.yaw = eulerAngles.y;
		this.pitch = eulerAngles.x;
	}

	// Token: 0x060000BB RID: 187 RVA: 0x000059A4 File Offset: 0x00003BA4
	private void Update()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			base.transform.position += Vector3.up * this.moveSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.LeftControl))
		{
			base.transform.position += Vector3.down * this.moveSpeed * Time.deltaTime;
		}
		this.HandleRotation();
		this.HandleMovement();
		this.HandleZoom();
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00005A38 File Offset: 0x00003C38
	private void HandleRotation()
	{
		if (Input.GetMouseButton(1))
		{
			this.yaw += Input.GetAxis("Mouse X") * this.rotateSpeed;
			this.pitch -= Input.GetAxis("Mouse Y") * this.rotateSpeed;
			this.pitch = Mathf.Clamp(this.pitch, this.pitchMin, this.pitchMax);
			base.transform.rotation = Quaternion.Euler(this.pitch, this.yaw, 0f);
		}
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00005AC8 File Offset: 0x00003CC8
	private void HandleMovement()
	{
		float d = this.moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? this.fastMultiplier : 1f);
		Vector3 a = base.transform.forward * Input.GetAxis("Vertical") + base.transform.right * Input.GetAxis("Horizontal");
		base.transform.position += a * d * Time.deltaTime;
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00005B58 File Offset: 0x00003D58
	private void HandleZoom()
	{
		float axis = Input.GetAxis("Mouse ScrollWheel");
		if (axis != 0f)
		{
			this.distance -= axis * this.zoomSpeed;
			this.distance = Mathf.Clamp(this.distance, this.minDistance, this.maxDistance);
		}
		base.transform.position = base.transform.position;
	}

	// Token: 0x040000EC RID: 236
	[Header("旋转设置")]
	public float rotateSpeed = 2f;

	// Token: 0x040000ED RID: 237
	public float pitchMin = -80f;

	// Token: 0x040000EE RID: 238
	public float pitchMax = 80f;

	// Token: 0x040000EF RID: 239
	[Header("平移设置")]
	public float moveSpeed = 10f;

	// Token: 0x040000F0 RID: 240
	public float fastMultiplier = 2f;

	// Token: 0x040000F1 RID: 241
	[Header("缩放设置")]
	public float zoomSpeed = 10f;

	// Token: 0x040000F2 RID: 242
	public float minDistance = 2f;

	// Token: 0x040000F3 RID: 243
	public float maxDistance = 40f;

	// Token: 0x040000F4 RID: 244
	private float yaw;

	// Token: 0x040000F5 RID: 245
	private float pitch;

	// Token: 0x040000F6 RID: 246
	private float distance = 10f;
}
