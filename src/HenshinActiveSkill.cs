using System;

// Token: 0x020002E1 RID: 737
public class HenshinActiveSkill : ActiveSkillBase
{
	// Token: 0x06001113 RID: 4371 RVA: 0x00061D74 File Offset: 0x0005FF74
	public virtual void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, string modePath, ActiveSkillData activeSkillDataValue)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = activeSkillDataValue;
		this.attackRoleBase = attackRole;
		this.skillTime = Util.GetRealSkillDuration(attackRole, this.activeSkillData.duration);
		if (attackRole.roleType == RoleType.Player)
		{
			this.skillTime *= 1f + (attackRole as PlayerBase).addHenshinTime;
		}
		if (this.attackRoleBase.henShinSkillId > 0)
		{
			Game.SkillManager.ClearSkill((uint)this.attackRoleBase.henShinSkillId);
		}
		this.attackRoleBase.henShinSkillId = (int)this.skillId;
		if (this.attackRoleBase.isLocalPlayer)
		{
			this.roleBuff = GameHelperClient.AddShowBuff(Game.Language.Get(PathDefine.Concat("a_", this.activeSkillData.id), ""), SkillBase.GetActiveSkillTip(this.activeSkillEnum), "Skill/" + this.activeSkillData.icon, this.skillTime);
		}
		this.LoadHenshinMode(modePath);
	}

	// Token: 0x06001114 RID: 4372 RVA: 0x00061E74 File Offset: 0x00060074
	protected void LoadHenshinMode(string path)
	{
		this.oldRoleMode = this.attackRoleBase.RoleModeBase;
		this.attackRoleBase.OldModeBase = this.oldRoleMode;
		this.oldRoleMode.OnClearMode();
		this.oldRoleMode.gameObject.SetActive(false);
		RoleModeBase component = AssetManager.LoadPrefab(path, null, true).GetComponent<RoleModeBase>();
		this.attackRoleBase.InitRoleModeBase(component);
		this.attackRoleBase.ReplayAnim();
		Game.EffectManager.PlayEffect(EffectDefine.SummonEffect, 2f, this.attackRoleBase.MyTransform.position, 1.25f + component.addRange);
	}

	// Token: 0x06001115 RID: 4373 RVA: 0x00061F15 File Offset: 0x00060115
	protected override void UpdateLocalSkill(float time)
	{
		base.UpdateLocalSkill(time);
		if (this.attackRoleBase != null && this.attackRoleBase.IsDead())
		{
			GameHelperClient.localPlayer.CmdClearSkill(this.skillId);
		}
	}

	// Token: 0x06001116 RID: 4374 RVA: 0x00061F4C File Offset: 0x0006014C
	public override void Clear(int clearData)
	{
		if (this.attackRoleBase != null)
		{
			if (this.attackRoleBase.overrideAnimSkillId >= 0)
			{
				Game.SkillManager.ClearSkill((uint)this.attackRoleBase.overrideAnimSkillId);
			}
			this.attackRoleBase.RoleModeBase.OnClearMode();
			Game.EffectManager.PlayEffect(EffectDefine.SummonEffect, 2f, this.attackRoleBase.MyTransform.position, 1.25f + this.attackRoleBase.RoleModeBase.addRange);
			AssetManager.UnLoadPrefab(this.attackRoleBase.RoleModeBase.gameObject, false);
			this.oldRoleMode.gameObject.SetActive(true);
			this.attackRoleBase.InitRoleModeBase(this.oldRoleMode);
			this.attackRoleBase.ReplayAnim();
			this.attackRoleBase.henShinSkillId = -1;
			if (this.attackRoleBase.isLocalPlayer)
			{
				this.attackRoleBase.roleBuffManager.RemoveBuff(this.roleBuff);
			}
		}
		base.Clear(clearData);
	}

	// Token: 0x04000F1E RID: 3870
	private RoleModeBase oldRoleMode;

	// Token: 0x04000F1F RID: 3871
	private RoleBuff roleBuff;
}
