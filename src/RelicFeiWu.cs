using System;

// Token: 0x02000215 RID: 533
public class RelicFeiWu : RelicBase
{
	// Token: 0x060009AF RID: 2479 RVA: 0x00033FDC File Offset: 0x000321DC
	public override void Enter()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.onEquipChange = (RoleBase.OnEquipChange)Delegate.Combine(playerBase.onEquipChange, new RoleBase.OnEquipChange(this.OnEquipChange));
		this.OnEquipChange();
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x0003400C File Offset: 0x0003220C
	private void OnEquipChange()
	{
		int count = this.playerBase.playerAttribute.equipList.Count;
		this.playerBase.addDamagePercent -= this.curAddDamage;
		this.curAddDamage = ((count == 0) ? base.GetValue(0, 2.5f) : 0f);
		this.playerBase.addDamagePercent += this.curAddDamage;
		if (this.curAddDamage > 0f)
		{
			if (this.roleBuff == null && this.playerBase.isLocalPlayer)
			{
				this.roleBuff = base.AddShowBuff(-1f);
				return;
			}
		}
		else if (this.roleBuff != null)
		{
			this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
			this.roleBuff = null;
		}
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x000340D4 File Offset: 0x000322D4
	public override void Exit()
	{
		if (this.roleBuff != null)
		{
			this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
			this.roleBuff = null;
		}
		PlayerBase playerBase = this.playerBase;
		playerBase.onEquipChange = (RoleBase.OnEquipChange)Delegate.Remove(playerBase.onEquipChange, new RoleBase.OnEquipChange(this.OnEquipChange));
		this.playerBase.addDamagePercent -= this.curAddDamage;
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x00034145 File Offset: 0x00032345
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.OnEquipChange();
		if (this.roleBuff != null)
		{
			this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
			this.roleBuff = base.AddShowBuff(-1f);
		}
	}

	// Token: 0x04000BC9 RID: 3017
	private RoleBuff roleBuff;

	// Token: 0x04000BCA RID: 3018
	private float curAddDamage;
}
