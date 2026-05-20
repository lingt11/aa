using System;
using UnityEngine;
using UnityEngine.UI;

namespace SmapleChicken
{
	// Token: 0x020004BE RID: 1214
	public class HPSystem : MonoBehaviour
	{
		// Token: 0x06001AF9 RID: 6905 RVA: 0x000A71F0 File Offset: 0x000A53F0
		private void Start()
		{
			this.image = GameObject.Find("HpGauge");
			this.textObj = GameObject.Find("HpText");
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x000A7214 File Offset: 0x000A5414
		private void Update()
		{
			if (-1 <= this.hp_num)
			{
				this.hp_num--;
			}
			this.HPDown((float)this.hp_num, this.maxHP);
			if (0 <= this.hp_num)
			{
				Text component = this.textObj.GetComponent<Text>();
				int num = this.hp_num;
				component.text = num.ToString();
			}
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x000A7272 File Offset: 0x000A5472
		private void HPDown(float current, int max)
		{
			this.image.GetComponent<Image>().fillAmount = current / (float)max;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06001AFC RID: 6908 RVA: 0x000A7288 File Offset: 0x000A5488
		// (set) Token: 0x06001AFD RID: 6909 RVA: 0x000A7290 File Offset: 0x000A5490
		public int HP_Public
		{
			get
			{
				return this.hp_num;
			}
			set
			{
				this.hp_num = value;
			}
		}

		// Token: 0x04001A5A RID: 6746
		private int maxHP = 10000;

		// Token: 0x04001A5B RID: 6747
		private GameObject image;

		// Token: 0x04001A5C RID: 6748
		private GameObject textObj;

		// Token: 0x04001A5D RID: 6749
		private Text text;

		// Token: 0x04001A5E RID: 6750
		private int hp_num = 10000;
	}
}
