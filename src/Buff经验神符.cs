using System;
using UnityEngine;

// Token: 0x02000182 RID: 386
public class Buff经验神符 : RoleBuff
{
	// Token: 0x06000766 RID: 1894 RVA: 0x0002C21C File Offset: 0x0002A41C
	public override void OnInit()
	{
		this.info = ExcelManager.allExcelData["amulet"].DIC("1406").DIC("info");
		this.icon = "Amulet/" + ExcelManager.allExcelData["amulet"].DIC("1406").DIC("icon");
		int exp = Random.Range(200, 1001);
		((PlayerBase)this.roleBase).GainExp(exp);
	}
}
