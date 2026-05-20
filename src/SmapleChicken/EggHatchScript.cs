using System;
using UnityEngine;

namespace SmapleChicken
{
	// Token: 0x020004BD RID: 1213
	public class EggHatchScript : MonoBehaviour
	{
		// Token: 0x06001AF6 RID: 6902 RVA: 0x000A703D File Offset: 0x000A523D
		private void Start()
		{
			this._Egg = Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/egg"), base.transform);
			this._Anim = this._Egg.GetComponent<Animator>();
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x000A706C File Offset: 0x000A526C
		private void Update()
		{
			this._Time = Time.time;
			if (this._Time >= 3f && this._Hatch_Lv == 0)
			{
				this._Hatch_Lv++;
				this._Anim.CrossFade("sway", 0.1f, 0, 0f);
				return;
			}
			if (this._Time >= 6f && this._Hatch_Lv == 1)
			{
				this._Hatch_Lv++;
				this._Anim.CrossFade("hop", 0.1f, 0, 0f);
				return;
			}
			if (this._Time >= 9f && this._Hatch_Lv == 2)
			{
				this._Hatch_Lv++;
				this._Anim.CrossFade("spring_up", 0.1f, 0, 0f);
				return;
			}
			if (this._Time >= 12f && this._Hatch_Lv == 3)
			{
				this._Hatch_Lv++;
				Object.Destroy(this._Egg);
				this._Egg = null;
				this._Egg = Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/egg_break"), base.transform);
				this._Anim = this._Egg.GetComponent<Animator>();
				this._Anim.CrossFade("break2", 0f, 0, 0f);
				return;
			}
			if (this._Time >= 15f && this._Hatch_Lv == 4)
			{
				this._Hatch_Lv++;
				Object.Destroy(this._Egg);
			}
		}

		// Token: 0x04001A56 RID: 6742
		private GameObject _Egg;

		// Token: 0x04001A57 RID: 6743
		private Animator _Anim;

		// Token: 0x04001A58 RID: 6744
		private float _Time;

		// Token: 0x04001A59 RID: 6745
		private int _Hatch_Lv;
	}
}
