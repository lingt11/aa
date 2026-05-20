using System;

// Token: 0x020001CE RID: 462
public class PasssiveSkill : SkillBase
{
	// Token: 0x1700004C RID: 76
	// (get) Token: 0x06000877 RID: 2167 RVA: 0x0003061D File Offset: 0x0002E81D
	public float Distance
	{
		get
		{
			return this.distance + this.roleBase.skillRange * this.distance;
		}
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void Enter()
	{
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void Exit()
	{
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x00030638 File Offset: 0x0002E838
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		base.OnLevelChanged(oldLevel, newLevel);
	}

	// Token: 0x04000B8B RID: 2955
	public new SkillAttribute skillAttribute;
}
