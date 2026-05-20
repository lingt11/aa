using System;

// Token: 0x020002D0 RID: 720
public class ChickenDanceActiveSkill : AnimationOverrideActiveSkill
{
	// Token: 0x060010D4 RID: 4308 RVA: 0x0005EC8C File Offset: 0x0005CE8C
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, float rangeValue, ActiveSkillData activeSkillDataValue)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = activeSkillDataValue;
		this.skillTime = Util.GetRealSkillDuration(attackRole, this.activeSkillData.duration);
		this.attackRoleBase = attackRole;
		base.LoadAnimatorController("Bundles/Animator/ChickenDance");
		this.attackRoleBase.UpdateAnimSpeed(1f);
	}

	// Token: 0x060010D5 RID: 4309 RVA: 0x0005ECE1 File Offset: 0x0005CEE1
	protected override void UpdateLocalSkill(float time)
	{
		this.attackRoleBase == null;
	}

	// Token: 0x060010D6 RID: 4310 RVA: 0x0005ECF0 File Offset: 0x0005CEF0
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		if (this.attackRoleBase != null && this.attackRoleBase.IsDead())
		{
			this.skillTime = -1f;
		}
	}

	// Token: 0x060010D7 RID: 4311 RVA: 0x0005ED1F File Offset: 0x0005CF1F
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
	}
}
