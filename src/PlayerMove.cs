using System;
using UnityEngine;

// Token: 0x020002A0 RID: 672
public class PlayerMove : MonoBehaviour
{
	// Token: 0x06000EC1 RID: 3777 RVA: 0x00054648 File Offset: 0x00052848
	private void Start()
	{
		this.controller = base.GetComponent<CharacterController>();
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x00054658 File Offset: 0x00052858
	private void Update()
	{
		if (this.controller.isGrounded && this.velocity.y < 0f)
		{
			this.velocity.y = -0.5f;
		}
		float axisRaw = Input.GetAxisRaw("Horizontal");
		float axisRaw2 = Input.GetAxisRaw("Vertical");
		Vector3 normalized = new Vector3(axisRaw, 0f, axisRaw2).normalized;
		Vector3 a = base.transform.TransformDirection(normalized) * this.moveSpeed;
		this.velocity.y = this.velocity.y + this.gravity * Time.deltaTime;
		Vector3 motion = a * Time.deltaTime;
		motion.y = this.velocity.y * Time.deltaTime;
		this.controller.Move(motion);
	}

	// Token: 0x04000DBC RID: 3516
	[Header("Movement Settings")]
	public float moveSpeed = 5f;

	// Token: 0x04000DBD RID: 3517
	public float gravity = -9.81f;

	// Token: 0x04000DBE RID: 3518
	private CharacterController controller;

	// Token: 0x04000DBF RID: 3519
	private Vector3 velocity;
}
