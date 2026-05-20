using System;
using UnityEngine;

// Token: 0x020002C6 RID: 710
public class AnimationOverrideActiveSkill : ActiveSkillBase
{
	// Token: 0x060010B2 RID: 4274 RVA: 0x0005DB4C File Offset: 0x0005BD4C
	protected void LoadAnimatorController(string path)
	{
		this.myAnimatorController = this.attackRoleBase.RoleModeBase.myAnim.runtimeAnimatorController;
		AnimatorOverrideController animatorOverrideController = new AnimatorOverrideController(this.attackRoleBase.RoleModeBase.myAnim.runtimeAnimatorController);
		AnimationClip value = Resources.Load<AnimationClip>(path);
		animatorOverrideController[AnimDefine.IdleAnimationName] = value;
		this.attackRoleBase.UpdateAnimSpeed(1f);
		this.attackRoleBase.RoleModeBase.myAnim.runtimeAnimatorController = null;
		this.attackRoleBase.RoleModeBase.myAnim.runtimeAnimatorController = animatorOverrideController;
		this.attackRoleBase.StartOverrideAnim();
		this.attackRoleBase.overrideAnimSkillId = (int)this.skillId;
		Resources.UnloadUnusedAssets();
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x0005DC00 File Offset: 0x0005BE00
	public override void Clear(int clearData)
	{
		if (this.attackRoleBase != null)
		{
			this.attackRoleBase.RoleModeBase.myAnim.runtimeAnimatorController = this.myAnimatorController;
			this.attackRoleBase.ReplayAnim();
			this.attackRoleBase.UpdateRoleState(RoleState.Idle);
			this.attackRoleBase.overrideAnimSkillId = -1;
			this.myAnimatorController = null;
		}
		base.Clear(clearData);
	}

	// Token: 0x04000EAE RID: 3758
	private RuntimeAnimatorController myAnimatorController;
}
