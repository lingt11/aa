using System;

// Token: 0x020001E9 RID: 489
public class 移速加成 : PasssiveSkill
{
	// Token: 0x060008D6 RID: 2262 RVA: 0x0003194C File Offset: 0x0002FB4C
	public override void Update()
	{
		float num = this.skillValues[0] * 0.01f;
		float num2 = (float)this.roleBase.hp * 1f / (float)this.roleBase.maxHp;
		if (num2 < num && !this.isAddSpeed)
		{
			this.isAddSpeed = true;
			this.roleBase.AddMoveSpeed(this.skillValues[1]);
			return;
		}
		if (this.isAddSpeed && num2 >= num)
		{
			this.isAddSpeed = false;
			this.roleBase.AddMoveSpeed(-this.skillValues[1]);
		}
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x000319D6 File Offset: 0x0002FBD6
	public override void Exit()
	{
		if (this.isAddSpeed)
		{
			this.isAddSpeed = false;
			this.roleBase.AddMoveSpeed(-this.skillValues[1]);
		}
	}

	// Token: 0x04000BA0 RID: 2976
	private bool isAddSpeed;
}
