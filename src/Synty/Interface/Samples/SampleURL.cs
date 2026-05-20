using System;
using UnityEngine;

namespace Synty.Interface.Samples
{
	// Token: 0x02000481 RID: 1153
	public class SampleURL : MonoBehaviour
	{
		// Token: 0x060019B4 RID: 6580 RVA: 0x0009CD86 File Offset: 0x0009AF86
		public void OpenURL(string url)
		{
			Application.OpenURL(url);
		}
	}
}
