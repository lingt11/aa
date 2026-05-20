using System;
using UnityEngine;

// Token: 0x020002A1 RID: 673
public class PlayerRollMode : MeleePlayerMode
{
	// Token: 0x06000EC4 RID: 3780 RVA: 0x00054740 File Offset: 0x00052940
	public override void OnStartSkill2()
	{
		this.playerBase.PlayAni(AnimDefine.Skill2, 1.5f, 0.1f);
		this.playerBase.timer = 0f;
		if (this.playerBase.hasAuthority)
		{
			this.myAnim.applyRootMotion = true;
			this.playerBase.roleBuffManager.AddOneBuff<Buff无敌>("Buff无敌", 0.8666666f);
		}
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x00046C81 File Offset: 0x00044E81
	public override void OnExitSkill2()
	{
		base.OnExitSkill2();
		if (this.playerBase.hasAuthority)
		{
			this.myAnim.applyRootMotion = false;
		}
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x000547AC File Offset: 0x000529AC
	private void OnAnimatorMove()
	{
		if (this.myAnim.applyRootMotion)
		{
			this.playerBase.CharacterController.Move(this.myAnim.deltaPosition * 0.8f + Time.deltaTime * Vector3.down);
		}
	}

	// Token: 0x06000EC7 RID: 3783 RVA: 0x00054800 File Offset: 0x00052A00
	public override void UpdateSkill2()
	{
		if (this.playerBase.hasAuthority)
		{
			float deltaTime = Time.deltaTime;
			this.playerBase.timer += deltaTime;
			if (this.playerBase.timer > 0.73333335f)
			{
				this.playerBase.UpdateRoleState(RoleState.Idle);
			}
		}
	}

	// Token: 0x04000DC0 RID: 3520
	private const float AniSpeed = 1.5f;

	// Token: 0x04000DC1 RID: 3521
	private const float RootMotionLevel = 0.8f;
}
