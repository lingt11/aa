using System;

// Token: 0x0200017B RID: 379
public class Buff护甲神符 : RoleBuff
{
	// Token: 0x06000753 RID: 1875 RVA: 0x0002BF90 File Offset: 0x0002A190
	public override void OnInit()
	{
		this.info = ExcelManager.allExcelData["amulet"].DIC("1401").DIC("info");
		this.icon = "Amulet/" + ExcelManager.allExcelData["amulet"].DIC("1401").DIC("icon");
		this.roleBase.AddArmor(999);
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x0002C009 File Offset: 0x0002A209
	public override void OnExit()
	{
		this.roleBase.AddArmor(-999);
	}
}
