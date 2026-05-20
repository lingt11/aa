using System;
using System.Collections.Generic;

// Token: 0x020002B8 RID: 696
public class RoleBuffManager
{
	// Token: 0x0600105E RID: 4190 RVA: 0x0005C316 File Offset: 0x0005A516
	public RoleBuffManager(RoleBase role)
	{
		this.roleBase = role;
	}

	// Token: 0x0600105F RID: 4191 RVA: 0x0005C330 File Offset: 0x0005A530
	public void Update()
	{
		for (int i = this.buffList.Count - 1; i >= 0; i--)
		{
			this.buffList[i].Update();
		}
	}

	// Token: 0x06001060 RID: 4192 RVA: 0x0005C368 File Offset: 0x0005A568
	public RoleBuff AddOneBuff(string buffName, float lifeTime, RoleBuff buff)
	{
		RoleBuff roleBuff = null;
		bool flag = false;
		for (int i = 0; i < this.buffList.Count; i++)
		{
			if (this.buffList[i].buffName.Equals(buffName))
			{
				flag = true;
				roleBuff = this.buffList[i];
			}
		}
		if (!flag)
		{
			roleBuff = buff;
			roleBuff.Init(this.roleBase, buffName);
			this.buffList.Add(roleBuff);
			if (this.roleBase.isLocalPlayer)
			{
				UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
				if (ui != null)
				{
					ui.RefreshRelic();
				}
			}
		}
		roleBuff.SetLifeTime(lifeTime);
		return roleBuff;
	}

	// Token: 0x06001061 RID: 4193 RVA: 0x0005C400 File Offset: 0x0005A600
	public RoleBuff AddOneBuff<T>(string buffName, float lifeTime) where T : RoleBuff, new()
	{
		bool flag = false;
		RoleBuff roleBuff = null;
		for (int i = 0; i < this.buffList.Count; i++)
		{
			if (this.buffList[i].buffName.Equals(buffName))
			{
				flag = true;
				roleBuff = this.buffList[i];
				roleBuff.SetLifeTime(lifeTime);
			}
		}
		if (!flag)
		{
			T t = Activator.CreateInstance<T>();
			t.SetLifeTime(lifeTime);
			t.Init(this.roleBase, buffName);
			this.buffList.Add(t);
			roleBuff = t;
			if (this.roleBase.isLocalPlayer)
			{
				UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
				if (ui != null)
				{
					ui.RefreshRelic();
				}
			}
		}
		roleBuff.OnInit();
		return roleBuff;
	}

	// Token: 0x06001062 RID: 4194 RVA: 0x0005C4BE File Offset: 0x0005A6BE
	public void RemoveBuff(RoleBuff roleBuff)
	{
		this.buffList.Remove(roleBuff);
		if (this.roleBase.isLocalPlayer)
		{
			UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
			if (ui == null)
			{
				return;
			}
			ui.RefreshRelic();
		}
	}

	// Token: 0x06001063 RID: 4195 RVA: 0x0005C4F0 File Offset: 0x0005A6F0
	public void RemoveBuff(string buffName)
	{
		for (int i = 0; i < this.buffList.Count; i++)
		{
			if (this.buffList[i].buffName.Equals(buffName))
			{
				this.buffList[i].Clear();
			}
		}
		if (this.roleBase.isLocalPlayer)
		{
			UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
			if (ui == null)
			{
				return;
			}
			ui.RefreshRelic();
		}
	}

	// Token: 0x04000E5A RID: 3674
	public List<RoleBuff> buffList = new List<RoleBuff>();

	// Token: 0x04000E5B RID: 3675
	public RoleBase roleBase;
}
