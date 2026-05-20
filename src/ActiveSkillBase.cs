using System;

// Token: 0x020002C4 RID: 708
public class ActiveSkillBase
{
	// Token: 0x060010A8 RID: 4264 RVA: 0x0005D88F File Offset: 0x0005BA8F
	public void UpdateEvent(float time)
	{
		this.UpdateSkill(time);
		if (this.attackRoleBase.HasAuthority)
		{
			this.UpdateLocalSkill(time);
		}
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x0005D8AC File Offset: 0x0005BAAC
	public virtual void Clear(int clearData)
	{
		this.attackRoleBase = null;
		this.trackRoleBase = null;
	}

	// Token: 0x060010AA RID: 4266 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected virtual void UpdateSkill(float time)
	{
	}

	// Token: 0x060010AB RID: 4267 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected virtual void UpdateLocalSkill(float time)
	{
	}

	// Token: 0x060010AC RID: 4268 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void StartSkillAciton()
	{
	}

	// Token: 0x060010AD RID: 4269 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void EndSkillAciton()
	{
	}

	// Token: 0x04000EA2 RID: 3746
	public uint skillId;

	// Token: 0x04000EA3 RID: 3747
	public RoleBase attackRoleBase;

	// Token: 0x04000EA4 RID: 3748
	public float skillTime;

	// Token: 0x04000EA5 RID: 3749
	protected RoleBase trackRoleBase;

	// Token: 0x04000EA6 RID: 3750
	public ActiveSkillEnum activeSkillEnum;

	// Token: 0x04000EA7 RID: 3751
	public ActiveSkillData activeSkillData;

	// Token: 0x04000EA8 RID: 3752
	public bool isPassSkill;
}
