using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class 查找幽灵怪 : MonoBehaviour
{
	// Token: 0x0600004C RID: 76 RVA: 0x000030E1 File Offset: 0x000012E1
	private void Awake()
	{
		this.role = base.GetComponent<RoleBase>();
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00002D1D File Offset: 0x00000F1D
	private void Start()
	{
	}

	// Token: 0x0600004E RID: 78 RVA: 0x000030F0 File Offset: 0x000012F0
	private void Update()
	{
		Debug.LogError("RoleState:" + this.role.RoleState.ToString());
		Debug.LogError("Hp:" + this.role.hp.ToString());
		Debug.LogError("authorityId:" + this.role.authorityId.ToString());
		Debug.LogError("HasAuthority:" + this.role.HasAuthority.ToString());
		Debug.LogError("clientEnemyListIndex:" + Game.EnemyManagerClient.clientEnemyList.IndexOf(this.role).ToString());
	}

	// Token: 0x0400004E RID: 78
	private RoleBase role;
}
