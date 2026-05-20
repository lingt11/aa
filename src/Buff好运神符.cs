using System;

// Token: 0x0200017A RID: 378
public class Buff好运神符 : RoleBuff
{
	// Token: 0x06000750 RID: 1872 RVA: 0x0002BF00 File Offset: 0x0002A100
	public override void OnInit()
	{
		this.info = ExcelManager.allExcelData["amulet"].DIC("1405").DIC("info");
		this.icon = "Amulet/" + ExcelManager.allExcelData["amulet"].DIC("1405").DIC("icon");
		((PlayerBase)this.roleBase).CmdUpdateLucky(100);
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x0002BF7B File Offset: 0x0002A17B
	public override void OnExit()
	{
		((PlayerBase)this.roleBase).CmdUpdateLucky(-100);
	}
}
