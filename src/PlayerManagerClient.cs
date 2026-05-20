using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200014D RID: 333
public class PlayerManagerClient : IUpdate
{
	// Token: 0x06000669 RID: 1641 RVA: 0x00026DF3 File Offset: 0x00024FF3
	public void AddPlayer(RoleBase roleBase)
	{
		if (!this.clientPlayerList.Contains(roleBase))
		{
			this.clientPlayerList.Add(roleBase);
		}
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x00026E0F File Offset: 0x0002500F
	public void AddRealPlayer(PlayerBase roleBase)
	{
		this.clientPlayerDic.TryAdd(roleBase.netId, roleBase);
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x00026E24 File Offset: 0x00025024
	public void RemoveRealPlayer(RoleBase roleBase)
	{
		if (roleBase.roleType == RoleType.Player)
		{
			UI_Msg ui = Game.UI.GetUI<UI_Msg>();
			if (ui != null)
			{
				ui.ShowMsg(PathDefine.Concat(string.Format(ColorDefine.NormalColor, roleBase.roleName), string.Format(ColorDefine.RedForColor, Game.Language.Get("已断开连接", ""))), true);
			}
			this.clientPlayerDic.Remove(roleBase.authorityId);
		}
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x00026E94 File Offset: 0x00025094
	public void RemovePlayer(RoleBase roleBase)
	{
		this.RemoveRealPlayer(roleBase);
		roleBase.OnRemove();
		this.clientPlayerList.Remove(roleBase);
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x00026EB0 File Offset: 0x000250B0
	public void Update()
	{
		if (GameHelperClient.isGameOver || GameHelperClient.localPlayer == null)
		{
			return;
		}
		if (this.rookieGuideManager != null)
		{
			this.rookieGuideManager.Update();
		}
		Vector3 position = GameHelperClient.localPlayer.MyTransform.position;
		for (int i = this.clientPlayerList.Count - 1; i > -1; i--)
		{
			RoleBase roleBase = this.clientPlayerList[i];
			if (roleBase == null)
			{
				this.clientPlayerList.RemoveAt(i);
			}
			else
			{
				roleBase.UpdateEvent();
				if (!roleBase.hasAuthority && roleBase.roleType != RoleType.Player)
				{
					if (Util.GetV2Distance(position, roleBase.MyTransform.position) > 22f)
					{
						roleBase.HideMode();
					}
					else
					{
						roleBase.ShowMode();
					}
				}
			}
		}
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x00026F70 File Offset: 0x00025170
	public void OnGameOver(bool isWin)
	{
		for (int i = this.clientPlayerList.Count - 1; i > -1; i--)
		{
			RoleBase roleBase = this.clientPlayerList[i];
			if (roleBase == null)
			{
				this.clientPlayerList.RemoveAt(i);
			}
			else if (roleBase.hasAuthority)
			{
				roleBase.UpdateRoleState(isWin ? RoleState.Idle : RoleState.Dead);
			}
		}
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x00026FD0 File Offset: 0x000251D0
	public void InitRookieGuideManager()
	{
		if (Game.Save.Check("RookieGuideMask"))
		{
			int num = Game.Save.LoadInt("RookieGuideMask");
			using (IEnumerator enumerator = Enum.GetValues(typeof(RookieGuideManager.RookieGuideMask)).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					RookieGuideManager.RookieGuideMask rookieGuideMask = (RookieGuideManager.RookieGuideMask)obj;
					if (rookieGuideMask != RookieGuideManager.RookieGuideMask.None && (num & (int)rookieGuideMask) == 0)
					{
						this.rookieGuideManager = new RookieGuideManager();
						this.rookieGuideManager.InitRookieGuideManager(num);
						break;
					}
				}
				return;
			}
		}
		this.rookieGuideManager = new RookieGuideManager();
		this.rookieGuideManager.InitRookieGuideManager(0);
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x00027084 File Offset: 0x00025284
	public void StartRookieGuide(RookieGuideManager.RookieGuideMask rookieGuideMask)
	{
		RookieGuideManager rookieGuideManager = this.rookieGuideManager;
		if (rookieGuideManager == null)
		{
			return;
		}
		rookieGuideManager.StartGuide(rookieGuideMask);
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x00027098 File Offset: 0x00025298
	public List<RoleBase> GetRangeEnemy(float distance, Vector3 pos)
	{
		List<RoleBase> list = new List<RoleBase>();
		foreach (RoleBase roleBase in this.clientPlayerList)
		{
			if (roleBase != null && roleBase.MyTransform != null)
			{
				float num = Vector3.Distance(roleBase.MyTransform.position, pos);
				RoleModeBase roleModeBase = roleBase.RoleModeBase;
				float? num2 = distance + ((roleModeBase != null) ? new float?(roleModeBase.addRange) : null);
				if (num <= num2.GetValueOrDefault() & num2 != null)
				{
					list.Add(roleBase);
				}
			}
		}
		return list;
	}

	// Token: 0x0400093D RID: 2365
	public List<RoleBase> clientPlayerList = new List<RoleBase>();

	// Token: 0x0400093E RID: 2366
	public RookieGuideManager rookieGuideManager;

	// Token: 0x0400093F RID: 2367
	public Dictionary<uint, PlayerBase> clientPlayerDic = new Dictionary<uint, PlayerBase>();
}
