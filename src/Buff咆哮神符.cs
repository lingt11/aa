using System;

// Token: 0x02000176 RID: 374
public class Buff咆哮神符 : RoleBuff
{
	// Token: 0x06000743 RID: 1859 RVA: 0x0002BC40 File Offset: 0x00029E40
	public override void OnInit()
	{
		this.info = ExcelManager.allExcelData["amulet"].DIC("1400").DIC("info");
		this.icon = "Amulet/" + ExcelManager.allExcelData["amulet"].DIC("1400").DIC("icon");
		PlayerBase playerBase = this.roleBase as PlayerBase;
		if (playerBase == null)
		{
			return;
		}
		playerBase.UpdateAttackPercent(1f);
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x0002BCC3 File Offset: 0x00029EC3
	public override void OnExit()
	{
		PlayerBase playerBase = this.roleBase as PlayerBase;
		if (playerBase == null)
		{
			return;
		}
		playerBase.UpdateAttackPercent(-1f);
	}
}
