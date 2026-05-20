using System;

// Token: 0x0200017F RID: 383
public class Buff狂暴神符 : RoleBuff
{
	// Token: 0x06000760 RID: 1888 RVA: 0x0002C174 File Offset: 0x0002A374
	public override void OnInit()
	{
		this.info = ExcelManager.allExcelData["amulet"].DIC("1404").DIC("info");
		this.icon = "Amulet/" + ExcelManager.allExcelData["amulet"].DIC("1404").DIC("icon");
		this.roleBase.AddCritical(0.8f);
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x0002C1ED File Offset: 0x0002A3ED
	public override void OnExit()
	{
		this.roleBase.AddCritical(-0.8f);
	}
}
