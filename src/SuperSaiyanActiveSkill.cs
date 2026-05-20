using System;

// Token: 0x020002F9 RID: 761
public class SuperSaiyanActiveSkill : BuffActiveSkill
{
	// Token: 0x06001198 RID: 4504 RVA: 0x00066DA8 File Offset: 0x00064FA8
	public override void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, string effectName, ActiveSkillData activeSkillData)
	{
		base.InitSkill(activeSkillType, attackRole, effectName, activeSkillData);
		this.skillTime = Util.GetRealSkillDuration(attackRole, activeSkillData.duration);
		base.InitEffect(effectName, 1.5f, 0.05f);
		if (this.attackRoleBase.HasAuthority)
		{
			this.addValue = activeSkillData.damageBase;
			PlayerBase playerBase = this.attackRoleBase as PlayerBase;
			if (playerBase != null)
			{
				this.addValue = (int)((float)activeSkillData.damageBase * (1f + playerBase.addHenshin));
			}
			this.attackRoleBase.AddSTA(this.addValue);
			this.attackRoleBase.AddSTR(this.addValue);
			this.attackRoleBase.AddAGI(this.addValue);
		}
	}

	// Token: 0x06001199 RID: 4505 RVA: 0x0005ECF0 File Offset: 0x0005CEF0
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		if (this.attackRoleBase != null && this.attackRoleBase.IsDead())
		{
			this.skillTime = -1f;
		}
	}

	// Token: 0x0600119A RID: 4506 RVA: 0x00066E5C File Offset: 0x0006505C
	public override void Clear(int clearData)
	{
		if (this.attackRoleBase != null && this.attackRoleBase.HasAuthority)
		{
			this.attackRoleBase.AddSTA(-this.addValue);
			this.attackRoleBase.AddSTR(-this.addValue);
			this.attackRoleBase.AddAGI(-this.addValue);
		}
		base.Clear(clearData);
	}

	// Token: 0x04000FBF RID: 4031
	private int addValue;
}
