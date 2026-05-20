using System;

// Token: 0x020000DE RID: 222
public class EquipSkillBase
{
	// Token: 0x0600049F RID: 1183 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void Init()
	{
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void OnUpdate()
	{
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void Clear()
	{
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x0001C3D4 File Offset: 0x0001A5D4
	public virtual void AddEquipNum()
	{
		this.equipNum++;
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x0001C3E4 File Offset: 0x0001A5E4
	public virtual void RemoveEquipNum()
	{
		this.equipNum--;
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x0001C3F4 File Offset: 0x0001A5F4
	public virtual void OnUpdateStrengLevel(int updateLevel)
	{
		this.strengLevel += updateLevel;
	}

	// Token: 0x0400041C RID: 1052
	public EquipBase equipBase;

	// Token: 0x0400041D RID: 1053
	public EquipSkillType equipSkillType;

	// Token: 0x0400041E RID: 1054
	public PlayerBase playerBase;

	// Token: 0x0400041F RID: 1055
	public int equipNum = 1;

	// Token: 0x04000420 RID: 1056
	public string equipIndex;

	// Token: 0x04000421 RID: 1057
	protected int strengLevel;

	// Token: 0x04000422 RID: 1058
	public float[] skillValueAry;

	// Token: 0x04000423 RID: 1059
	public float[] skillValueUpAry;
}
