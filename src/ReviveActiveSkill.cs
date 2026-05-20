using System;

// Token: 0x020002F3 RID: 755
public class ReviveActiveSkill : BuffActiveSkill
{
	// Token: 0x06001167 RID: 4455 RVA: 0x000653E3 File Offset: 0x000635E3
	public override void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, string effectName, ActiveSkillData activeSkillData)
	{
		base.InitSkill(activeSkillType, attackRole, effectName, activeSkillData);
		this.skillTime = 4.6f;
		this.addHpTime = this.skillTime;
		base.InitEffect(effectName, 1.25f, 1f);
	}

	// Token: 0x06001168 RID: 4456 RVA: 0x00065418 File Offset: 0x00063618
	protected override void UpdateLocalSkill(float time)
	{
		if (this.addHpTime > this.skillTime)
		{
			this.attackRoleBase.StartHealthHp((double)((float)this.attackRoleBase.maxHp * 0.05f), this.attackRoleBase);
			this.addHpTime -= 0.5f;
		}
	}

	// Token: 0x06001169 RID: 4457 RVA: 0x0005ECF0 File Offset: 0x0005CEF0
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		if (this.attackRoleBase != null && this.attackRoleBase.IsDead())
		{
			this.skillTime = -1f;
		}
	}

	// Token: 0x04000F88 RID: 3976
	private float addHpTime;
}
