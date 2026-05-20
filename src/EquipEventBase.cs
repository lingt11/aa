using System;

// Token: 0x020000D8 RID: 216
public class EquipEventBase
{
	// Token: 0x0600046D RID: 1133 RVA: 0x0001B59B File Offset: 0x0001979B
	public virtual void Init(EquipBase equipBaseValue)
	{
		this.equipBase = equipBaseValue;
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void OnUpdate()
	{
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void Clear()
	{
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x0001B5A4 File Offset: 0x000197A4
	public virtual void OnLevelUpSuccess()
	{
		this.strengLevel++;
	}

	// Token: 0x04000402 RID: 1026
	public EquipBase equipBase;

	// Token: 0x04000403 RID: 1027
	public PlayerBase playerBase;

	// Token: 0x04000404 RID: 1028
	public int strengLevel;

	// Token: 0x04000405 RID: 1029
	public float[] skillValueAry;

	// Token: 0x04000406 RID: 1030
	public float[] skillValueUpAry;
}
