using System;
using UnityEngine;

// Token: 0x02000414 RID: 1044
public class Readme : ScriptableObject
{
	// Token: 0x040016CD RID: 5837
	public Texture2D icon;

	// Token: 0x040016CE RID: 5838
	public string title;

	// Token: 0x040016CF RID: 5839
	public Readme.Section[] sections;

	// Token: 0x040016D0 RID: 5840
	public bool loadedLayout;

	// Token: 0x02000415 RID: 1045
	[Serializable]
	public class Section
	{
		// Token: 0x040016D1 RID: 5841
		public string heading;

		// Token: 0x040016D2 RID: 5842
		public string text;

		// Token: 0x040016D3 RID: 5843
		public string linkText;

		// Token: 0x040016D4 RID: 5844
		public string url;
	}
}
