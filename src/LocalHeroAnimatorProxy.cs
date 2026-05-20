using System;
using UnityEngine;

// Token: 0x020003C5 RID: 965
[DisallowMultipleComponent]
public class LocalHeroAnimatorProxy : MonoBehaviour
{
	// Token: 0x06001626 RID: 5670 RVA: 0x00089820 File Offset: 0x00087A20
	public void Bind(RoleModeBase roleModeBaseValue)
	{
		this.roleModeBase = roleModeBaseValue;
	}

	// Token: 0x06001627 RID: 5671 RVA: 0x00089829 File Offset: 0x00087A29
	private void OnAnimatorMove()
	{
		if (this.roleModeBase == null)
		{
			return;
		}
		this.roleModeBase.gameObject.SendMessage("OnAnimatorMove", SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x040014C7 RID: 5319
	private RoleModeBase roleModeBase;
}
