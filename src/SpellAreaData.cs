using System;
using UnityEngine;

// Token: 0x0200028A RID: 650
public class SpellAreaData : MonoBehaviour
{
	// Token: 0x06000C26 RID: 3110 RVA: 0x00044CA4 File Offset: 0x00042EA4
	private void Awake()
	{
		GameHelperClient.CanSpellArea = new Vector2(this.canSpellArea.localScale.x / 2f, this.canSpellArea.localScale.z / 2f);
		GameHelperClient.NoSpellArea = new Vector2(this.noSpellArea.localScale.x / 2f, this.noSpellArea.localScale.z / 2f);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000CEE RID: 3310
	public Transform canSpellArea;

	// Token: 0x04000CEF RID: 3311
	public Transform noSpellArea;
}
