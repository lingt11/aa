using System;

// Token: 0x02000184 RID: 388
public class Buff速度神符 : RoleBuff
{
	// Token: 0x06000769 RID: 1897 RVA: 0x0002C2A8 File Offset: 0x0002A4A8
	public override void OnInit()
	{
		this.info = ExcelManager.allExcelData["amulet"].DIC("1402").DIC("info");
		this.icon = "Amulet/" + ExcelManager.allExcelData["amulet"].DIC("1402").DIC("icon");
		this.roleBase.AddMoveSpeed(3f);
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x0002C321 File Offset: 0x0002A521
	public override void OnExit()
	{
		this.roleBase.AddMoveSpeed(-3f);
	}
}
