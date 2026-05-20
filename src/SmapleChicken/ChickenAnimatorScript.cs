using System;
using UnityEngine;
using UnityEngine.UI;

namespace SmapleChicken
{
	// Token: 0x020004BC RID: 1212
	public class ChickenAnimatorScript : MonoBehaviour
	{
		// Token: 0x06001AE8 RID: 6888 RVA: 0x000A656C File Offset: 0x000A476C
		private void Start()
		{
			this._Animator = base.GetComponent<Animator>();
			this._Ctrl = base.GetComponent<CharacterController>();
			this._HP_Num = Object.FindObjectOfType<HPSystem>();
			this._TextObj = GameObject.Find("AnimationState");
			this._ViewCamera = GameObject.Find("Camera");
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x000A65BC File Offset: 0x000A47BC
		private void Update()
		{
			this.STATE_TEXT();
			this.CAMERA();
			this._HP = this._HP_Num.HP_Public;
			if (this._HP <= 0)
			{
				if (!this._ToStop)
				{
					this._Animator.CrossFade("down", 0.1f, 0, 0f);
					this._Animator.CrossFade("wing_down", 0.1f, 1, 0f);
					this._ToStop = true;
				}
				else if (this._ToStop)
				{
					this.isRunning = false;
					this._Animator.SetBool("to_move", false);
					if (Input.GetKeyDown(KeyCode.E))
					{
						this._Animator.SetTrigger("jump");
						this._HP_Num.HP_Public = 10000;
						this._ToStop = false;
						this._MoveDirection.y = 3f;
					}
				}
			}
			this.GRAVITY();
			if (this._HP > 0 && !this._Animator.GetCurrentAnimatorStateInfo(0).IsTag("Stop"))
			{
				if (!this._Animator.GetCurrentAnimatorStateInfo(0).IsTag("Action") && !this._Animator.GetCurrentAnimatorStateInfo(0).IsTag("Damage"))
				{
					this.MOVE();
					this.JUMP();
					this.KEY_DOWN2();
				}
				this.KEY_DOWN();
			}
			this.KEY_UP();
			if (this._Animator.GetCurrentAnimatorStateInfo(0).IsTag("Damage"))
			{
				this._Animator.SetBool("during_damage", true);
				return;
			}
			if (!this._Animator.GetCurrentAnimatorStateInfo(0).IsTag("Damage"))
			{
				this._Animator.SetBool("during_damage", false);
			}
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x000A6770 File Offset: 0x000A4970
		private void CAMERA()
		{
			this._ViewCamera.transform.position = base.transform.position + new Vector3(0f, 1f, -3f);
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x000A67A8 File Offset: 0x000A49A8
		private void GRAVITY()
		{
			if (this.CheckGrounded())
			{
				this._Animator.SetBool("to_landing", true);
				if (this._MoveDirection.y < -0.5f)
				{
					this._MoveDirection.y = -0.5f;
				}
			}
			else if (!this.CheckGrounded())
			{
				this._Animator.SetBool("to_landing", false);
			}
			if (Input.GetKeyDown(KeyCode.W))
			{
				this._Animator.SetTrigger("to_flapping");
			}
			else if (this._Animator.GetBool("to_flapping") && this._Animator.GetCurrentAnimatorStateInfo(0).IsTag("Basis"))
			{
				this._Animator.ResetTrigger("to_flapping");
			}
			if (Input.GetKey(KeyCode.W) && !this._Animator.GetCurrentAnimatorStateInfo(0).IsTag("Stop"))
			{
				if (base.transform.position.y < 0.5f)
				{
					this._MoveDirection.y = 0.5f;
				}
				else
				{
					this._MoveDirection.y = -0.5f;
				}
			}
			else
			{
				this._MoveDirection.y = this._MoveDirection.y - this._Gravity * Time.deltaTime;
			}
			this._Ctrl.Move(this._MoveDirection * Time.deltaTime);
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x000A68FC File Offset: 0x000A4AFC
		private bool CheckGrounded()
		{
			if (this._Ctrl.isGrounded)
			{
				return true;
			}
			Ray ray = new Ray(base.transform.position + Vector3.up * 0.1f, Vector3.down);
			float maxDistance = 0.2f;
			return Physics.Raycast(ray, maxDistance);
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x000A6950 File Offset: 0x000A4B50
		private void MOVE()
		{
			if (this.isRunning)
			{
				this._Speed = this._Chicken_Speed * 2f;
			}
			else
			{
				this._Speed = this._Chicken_Speed;
			}
			this._Animator.SetFloat("speed", this._Speed);
			if (Input.GetKey(KeyCode.UpArrow))
			{
				this._Animator.SetBool("to_move", true);
				if (this._Animator.GetCurrentAnimatorStateInfo(0).IsName("move") || !this._Ctrl.isGrounded)
				{
					Vector3 velocity = base.transform.rotation * new Vector3(0f, 0f, this._Speed);
					this.MOVE_XZ(velocity);
					this.MOVE_RESET();
				}
			}
			if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
			{
				base.transform.Rotate(Vector3.up, 0.5f);
			}
			else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
			{
				base.transform.Rotate(Vector3.up, -0.5f);
			}
			if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
			{
				if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
				{
					this._Animator.SetBool("to_move", true);
					return;
				}
				if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
				{
					this._Animator.SetBool("to_move", true);
					return;
				}
				if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
				{
					this._Animator.SetBool("to_move", false);
				}
			}
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x000A6B07 File Offset: 0x000A4D07
		private void MOVE_XZ(Vector3 velocity)
		{
			this._MoveDirection = new Vector3(velocity.x, this._MoveDirection.y, velocity.z);
			this._Ctrl.Move(this._MoveDirection * Time.deltaTime);
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x000A6B47 File Offset: 0x000A4D47
		private void MOVE_RESET()
		{
			this._MoveDirection.x = 0f;
			this._MoveDirection.z = 0f;
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x000A6B6C File Offset: 0x000A4D6C
		private void JUMP()
		{
			if (!this._Animator.IsInTransition(0) && !this._Animator.GetCurrentAnimatorStateInfo(1).IsName("wing_flapping") && Input.GetKeyDown(KeyCode.S))
			{
				this._Animator.SetTrigger("jump");
				this._MoveDirection.y = 3f;
			}
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x000A6BCC File Offset: 0x000A4DCC
		private void KEY_DOWN()
		{
			if (Input.GetKeyDown(KeyCode.Z))
			{
				this.isRunning = true;
			}
			if (!this._Animator.GetCurrentAnimatorStateInfo(0).IsName("move") && Input.GetKey(KeyCode.C))
			{
				this._Animator.SetBool("to_crouch", true);
			}
			if (Input.GetKeyDown(KeyCode.Q))
			{
				this._Animator.SetTrigger("damage");
			}
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x000A6C38 File Offset: 0x000A4E38
		private void KEY_DOWN2()
		{
			if (Input.GetKeyDown(KeyCode.X))
			{
				this._Animator.SetTrigger("honk");
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				this._Animator.SetTrigger("eat");
			}
			if (Input.GetKeyDown(KeyCode.A) && !this._Animator.GetCurrentAnimatorStateInfo(0).IsTag("Jump"))
			{
				this._Animator.SetTrigger("peck");
			}
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x000A6CAC File Offset: 0x000A4EAC
		private void KEY_UP()
		{
			if (Input.GetKeyUp(KeyCode.Z))
			{
				this.isRunning = false;
				return;
			}
			if (Input.GetKeyUp(KeyCode.C))
			{
				this._Animator.SetBool("to_crouch", false);
				return;
			}
			if (Input.GetKeyUp(KeyCode.UpArrow))
			{
				this._Animator.SetBool("to_move", false);
				return;
			}
			if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)))
			{
				this._Animator.SetBool("to_move", false);
			}
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x000A6D44 File Offset: 0x000A4F44
		private void STATE_TEXT()
		{
			if (this._Animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
			{
				this._TextObj.GetComponent<Text>().text = "idle";
				return;
			}
			if (this._Animator.GetCurrentAnimatorStateInfo(0).IsName("move"))
			{
				if (this._Animator.GetFloat("speed") == 1f)
				{
					this._TextObj.GetComponent<Text>().text = "walk";
					return;
				}
				if (this._Animator.GetFloat("speed") > 1f)
				{
					this._TextObj.GetComponent<Text>().text = "run";
					return;
				}
			}
			else
			{
				if (this._Animator.GetCurrentAnimatorStateInfo(0).IsName("flapping"))
				{
					this._TextObj.GetComponent<Text>().text = "flapping";
					return;
				}
				if (this._Animator.GetCurrentAnimatorStateInfo(0).IsName("peck_flapping"))
				{
					this._TextObj.GetComponent<Text>().text = "peck_flapping";
					return;
				}
				if (this._Animator.GetCurrentAnimatorStateInfo(0).IsName("peck"))
				{
					this._TextObj.GetComponent<Text>().text = "peck";
					return;
				}
				if (this._Animator.GetCurrentAnimatorStateInfo(0).IsName("jump"))
				{
					this._TextObj.GetComponent<Text>().text = "jump";
					return;
				}
				if (this._Animator.GetCurrentAnimatorStateInfo(0).IsName("crouch"))
				{
					this._TextObj.GetComponent<Text>().text = "crouch";
					return;
				}
				if (this._Animator.GetCurrentAnimatorStateInfo(0).IsName("eat"))
				{
					this._TextObj.GetComponent<Text>().text = "eat";
					return;
				}
				if (this._Animator.GetCurrentAnimatorStateInfo(0).IsName("honk"))
				{
					this._TextObj.GetComponent<Text>().text = "honk";
					return;
				}
				if (this._Animator.GetCurrentAnimatorStateInfo(0).IsName("damage"))
				{
					this._TextObj.GetComponent<Text>().text = "damage";
					return;
				}
				if (this._Animator.GetCurrentAnimatorStateInfo(0).IsName("damage_flapping"))
				{
					this._TextObj.GetComponent<Text>().text = "damage_flapping";
					return;
				}
				if (this._Animator.GetCurrentAnimatorStateInfo(0).IsName("down"))
				{
					this._TextObj.GetComponent<Text>().text = "down";
					return;
				}
				if (this._Animator.GetCurrentAnimatorStateInfo(0).IsName("recovery"))
				{
					this._TextObj.GetComponent<Text>().text = "recovery";
				}
			}
		}

		// Token: 0x04001A4A RID: 6730
		private Animator _Animator;

		// Token: 0x04001A4B RID: 6731
		private float _Chicken_Speed = 1f;

		// Token: 0x04001A4C RID: 6732
		private float _Speed;

		// Token: 0x04001A4D RID: 6733
		private bool isRunning;

		// Token: 0x04001A4E RID: 6734
		private int _HP;

		// Token: 0x04001A4F RID: 6735
		private HPSystem _HP_Num;

		// Token: 0x04001A50 RID: 6736
		private GameObject _TextObj;

		// Token: 0x04001A51 RID: 6737
		private bool _ToStop;

		// Token: 0x04001A52 RID: 6738
		private CharacterController _Ctrl;

		// Token: 0x04001A53 RID: 6739
		private float _Gravity = 5f;

		// Token: 0x04001A54 RID: 6740
		private Vector3 _MoveDirection = Vector3.zero;

		// Token: 0x04001A55 RID: 6741
		private GameObject _ViewCamera;
	}
}
