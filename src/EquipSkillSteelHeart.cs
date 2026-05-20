using System;
using UnityEngine;

// Token: 0x020000FC RID: 252
public class EquipSkillSteelHeart : EquipSkillBase
{
	// Token: 0x0600052C RID: 1324 RVA: 0x0001E7FE File Offset: 0x0001C9FE
	public override void Init()
	{
		base.Init();
		this.equipValue = Mathf.RoundToInt(this.skillValueAry[0]);
		this.equipUpValue = Mathf.RoundToInt(this.skillValueUpAry[0]);
		this.playerBase.UpdateReduce(this.equipValue);
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x0001E83D File Offset: 0x0001CA3D
	public override void AddEquipNum()
	{
		base.AddEquipNum();
		this.playerBase.UpdateReduce(this.equipValue);
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x0001E856 File Offset: 0x0001CA56
	public override void RemoveEquipNum()
	{
		base.RemoveEquipNum();
		this.playerBase.UpdateReduce(-this.equipValue);
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x0001E870 File Offset: 0x0001CA70
	public override void Clear()
	{
		base.Clear();
		if (this.equipNum > 0)
		{
			this.playerBase.UpdateReduce(-this.equipValue);
		}
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x0001E893 File Offset: 0x0001CA93
	public override void OnUpdateStrengLevel(int updateLevel)
	{
		base.OnUpdateStrengLevel(updateLevel);
		this.playerBase.UpdateReduce(updateLevel * this.equipUpValue);
	}

	// Token: 0x04000484 RID: 1156
	private int equipValue;

	// Token: 0x04000485 RID: 1157
	private int equipUpValue;
}
