using System;
using UnityEngine;

namespace SmapleChicken
{
	// Token: 0x020004BB RID: 1211
	public class ChickAnimatorScript : MonoBehaviour
	{
		// Token: 0x06001ADD RID: 6877 RVA: 0x000A5EFC File Offset: 0x000A40FC
		private void Start()
		{
			this.animator = base.GetComponent<Animator>();
			this.chicken = GameObject.Find("Stage/chicken_lowpoly");
			this.chicken_animator = this.chicken.GetComponent<Animator>();
			this.ctrl = base.GetComponent<CharacterController>();
			this.chicken_ctrl = this.chicken.GetComponent<CharacterController>();
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x000A5F54 File Offset: 0x000A4154
		private void Update()
		{
			this.DOWN_AND_RECOVER();
			this.GRAVITY();
			if (!this.chicken_animator.GetCurrentAnimatorStateInfo(0).IsTag("Stop"))
			{
				this.SET_BOOL();
				if (!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Stop"))
				{
					if (!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Action") && !this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Damage"))
					{
						this.MOVE();
						this.JUMP();
						this.KEY_DOWN2();
					}
					this.KEY_DOWN();
				}
			}
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x000A5FF8 File Offset: 0x000A41F8
		private void GRAVITY()
		{
			if (this.CheckGrounded())
			{
				this.animator.SetBool("to_landing", true);
				if (this.moveDirection.y < -0.5f)
				{
					this.moveDirection.y = -0.5f;
				}
			}
			else if (!this.CheckGrounded())
			{
				this.animator.SetBool("to_landing", false);
			}
			if (Input.GetKey(KeyCode.W) && !this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Stop"))
			{
				if (base.transform.position.y < 0.5f)
				{
					this.moveDirection.y = 0.5f;
				}
				else
				{
					this.moveDirection.y = 0f;
				}
			}
			else
			{
				this.moveDirection.y = this.moveDirection.y - this.gravity * Time.deltaTime;
			}
			this.ctrl.Move(this.moveDirection * Time.deltaTime);
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x000A60F4 File Offset: 0x000A42F4
		private bool CheckGrounded()
		{
			if (this.ctrl.isGrounded)
			{
				return true;
			}
			Ray ray = new Ray(base.transform.position + Vector3.up * 0.1f, Vector3.down);
			float maxDistance = 0.2f;
			return Physics.Raycast(ray, maxDistance);
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x000A6148 File Offset: 0x000A4348
		private void MOVE()
		{
			float num = Vector3.Distance(base.transform.position, this.chicken.transform.position);
			this.animator.SetFloat("speed", num * 2f);
			Quaternion localRotation = Quaternion.LookRotation(this.chicken.transform.position - base.transform.position, Vector3.up);
			base.transform.localRotation = localRotation;
			if (num >= 0.5f)
			{
				Vector3 b = this.chicken.transform.rotation * new Vector3(0f, 0f, -0.5f);
				float num2 = 2f;
				base.transform.position = Vector3.Lerp(base.transform.position, this.chicken.transform.position + b, num2 * Time.deltaTime);
			}
			if (num > 0.51f)
			{
				this.animator.SetBool("to_move", true);
				return;
			}
			this.animator.SetBool("to_move", false);
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x000A6260 File Offset: 0x000A4460
		private void JUMP()
		{
			if (!this.animator.IsInTransition(0) && !this.animator.GetCurrentAnimatorStateInfo(1).IsName("wing_flapping") && Input.GetKeyDown(KeyCode.S))
			{
				this.animator.SetTrigger("jump");
				this.moveDirection.y = 3f;
			}
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x000A62BF File Offset: 0x000A44BF
		private void KEY_DOWN()
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				this.animator.SetTrigger("damage");
			}
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x000A62DA File Offset: 0x000A44DA
		private void KEY_DOWN2()
		{
			if (Input.GetKeyDown(KeyCode.D))
			{
				this.animator.SetTrigger("eat");
			}
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x000A62F8 File Offset: 0x000A44F8
		private void SET_BOOL()
		{
			if (this.chicken_animator.GetBool("to_crouch"))
			{
				this.animator.SetBool("to_crouch", true);
			}
			else if (!this.chicken_animator.GetBool("to_crouch"))
			{
				this.animator.SetBool("to_crouch", false);
			}
			if (this.chicken_animator.GetCurrentAnimatorStateInfo(0).IsName("honk"))
			{
				this.animator.SetBool("peep", true);
			}
			else if (!this.chicken_animator.GetCurrentAnimatorStateInfo(0).IsName("honk"))
			{
				this.animator.SetBool("peep", false);
			}
			if (this.chicken_animator.GetCurrentAnimatorStateInfo(0).IsName("peck") || this.chicken_animator.GetCurrentAnimatorStateInfo(0).IsName("peck_flapping"))
			{
				this.animator.SetBool("peck", true);
			}
			else if (!this.chicken_animator.GetCurrentAnimatorStateInfo(0).IsName("peck") && !this.chicken_animator.GetCurrentAnimatorStateInfo(0).IsName("peck_flapping"))
			{
				this.animator.SetBool("peck", false);
			}
			if (this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Damage"))
			{
				this.animator.SetBool("during_damage", true);
				return;
			}
			if (!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Damage"))
			{
				this.animator.SetBool("during_damage", false);
			}
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x000A6490 File Offset: 0x000A4690
		private void DOWN_AND_RECOVER()
		{
			if (!this.to_stop)
			{
				if (this.chicken_animator.GetCurrentAnimatorStateInfo(0).IsTag("Stop"))
				{
					this.animator.CrossFade("down", 0.1f, 0, 0f);
					this.animator.CrossFade("wing_down", 0.1f, 1, 0f);
					this.to_stop = true;
					return;
				}
			}
			else if (this.to_stop && !this.chicken_animator.GetCurrentAnimatorStateInfo(0).IsTag("Stop"))
			{
				this.animator.SetTrigger("jump");
				this.moveDirection.y = 3f;
				this.to_stop = false;
			}
		}

		// Token: 0x04001A42 RID: 6722
		private Animator animator;

		// Token: 0x04001A43 RID: 6723
		private CharacterController ctrl;

		// Token: 0x04001A44 RID: 6724
		private GameObject chicken;

		// Token: 0x04001A45 RID: 6725
		private Animator chicken_animator;

		// Token: 0x04001A46 RID: 6726
		private CharacterController chicken_ctrl;

		// Token: 0x04001A47 RID: 6727
		private Vector3 moveDirection = Vector3.zero;

		// Token: 0x04001A48 RID: 6728
		private float gravity = 5f;

		// Token: 0x04001A49 RID: 6729
		private bool to_stop;
	}
}
