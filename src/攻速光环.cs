using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001DC RID: 476
public class 攻速光环 : PasssiveSkill
{
	// Token: 0x060008A5 RID: 2213 RVA: 0x00030EBF File Offset: 0x0002F0BF
	public override void Enter()
	{
		this.rate = this.skillValues[0] * 0.01f;
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x00030ED8 File Offset: 0x0002F0D8
	public override void Update()
	{
		using (List<RoleBase>.Enumerator enumerator = this.roleBase.GetFriendRoles().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (Vector3.Distance(enumerator.Current.transform.position, this.roleBase.transform.position) <= base.Distance)
				{
					if (!this.roleList.Contains(this.roleBase))
					{
						this.roleList.Add(this.roleBase);
						if (this.roleBase.roleType != RoleType.King)
						{
							GameHelperClient.localPlayer.CmdUpdateOtherAttackSpeed(this.rate, this.roleBase.netId);
						}
					}
				}
				else if (this.roleList.Contains(this.roleBase))
				{
					this.roleList.Remove(this.roleBase);
					if (this.roleBase.roleType != RoleType.King)
					{
						GameHelperClient.localPlayer.CmdUpdateOtherAttackSpeed(-this.rate, this.roleBase.netId);
					}
				}
			}
		}
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x00030FF8 File Offset: 0x0002F1F8
	public override void Exit()
	{
		foreach (RoleBase roleBase in this.roleList)
		{
			GameHelperClient.localPlayer.CmdUpdateOtherAttackSpeed(-this.rate, this.roleBase.netId);
		}
		this.roleList.Clear();
	}

	// Token: 0x04000B94 RID: 2964
	private List<RoleBase> roleList = new List<RoleBase>();

	// Token: 0x04000B95 RID: 2965
	private float rate;
}
