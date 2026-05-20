using System;
using UnityEngine;

// Token: 0x02000087 RID: 135
[CreateAssetMenu(menuName = "ScriptableObject/SOBrotatoWeaponConfig")]
public class SOBrotatoWeaponConfig : ScriptableObject
{
	// Token: 0x0600030A RID: 778 RVA: 0x00014E9C File Offset: 0x0001309C
	public BrotatoWeaponData GetBrotatoWeaponData(BrotatoWeaponType brotatoWeaponType)
	{
		foreach (BrotatoWeaponData brotatoWeaponData in Util.GetSOBrotatoWeaponConfig().brotatoWeaponData)
		{
			if (brotatoWeaponData.brotatoWeaponType == brotatoWeaponType)
			{
				return brotatoWeaponData;
			}
		}
		return default(BrotatoWeaponData);
	}

	// Token: 0x0400028F RID: 655
	public BrotatoWeaponData[] brotatoWeaponData;
}
