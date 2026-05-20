using System;
using System.Collections.Generic;

// Token: 0x02000085 RID: 133
public class ReferenceCollectorDataComparer : IComparer<ReferenceCollectorData>
{
	// Token: 0x06000303 RID: 771 RVA: 0x00014D8D File Offset: 0x00012F8D
	public int Compare(ReferenceCollectorData x, ReferenceCollectorData y)
	{
		return string.Compare(x.key, y.key, StringComparison.Ordinal);
	}
}
