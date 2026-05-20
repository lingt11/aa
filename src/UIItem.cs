using System;
using UnityEngine;

// Token: 0x02000258 RID: 600
public class UIItem : MonoBehaviour
{
	// Token: 0x06000AC1 RID: 2753 RVA: 0x00037334 File Offset: 0x00035534
	private void Update()
	{
		if (this.showState == 2)
		{
			if (this.timeCD > 0f)
			{
				this.timeCD -= Time.deltaTime;
				return;
			}
			this.showState = 3;
			this.sell.gameObject.SetActive(false);
			this.discord.gameObject.SetActive(false);
		}
	}

	// Token: 0x04000BEF RID: 3055
	public GameObject sell;

	// Token: 0x04000BF0 RID: 3056
	public GameObject discord;

	// Token: 0x04000BF1 RID: 3057
	public int showState;

	// Token: 0x04000BF2 RID: 3058
	public float timeCD;
}
