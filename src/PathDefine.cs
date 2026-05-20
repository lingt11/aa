using System;
using System.Text;

// Token: 0x02000116 RID: 278
public class PathDefine
{
	// Token: 0x0600058E RID: 1422 RVA: 0x00020716 File Offset: 0x0001E916
	public static string Concat(object a, object b)
	{
		PathDefine.stringBuilder.Length = 0;
		PathDefine.stringBuilder.Append(a);
		PathDefine.stringBuilder.Append(b);
		return PathDefine.stringBuilder.ToString();
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x00020745 File Offset: 0x0001E945
	public static string Concat(object a, object b, object c)
	{
		PathDefine.stringBuilder.Length = 0;
		PathDefine.stringBuilder.Append(a);
		PathDefine.stringBuilder.Append(b);
		PathDefine.stringBuilder.Append(c);
		return PathDefine.stringBuilder.ToString();
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x00020780 File Offset: 0x0001E980
	public static string Concat(object arg0, object arg1, object arg2, object arg3)
	{
		PathDefine.stringBuilder.Clear();
		PathDefine.stringBuilder.Append(arg0);
		PathDefine.stringBuilder.Append(arg1);
		PathDefine.stringBuilder.Append(arg2);
		PathDefine.stringBuilder.Append(arg3);
		return PathDefine.stringBuilder.ToString();
	}

	// Token: 0x040006CC RID: 1740
	public const string EnemyPath = "Prefabs/EnemyBase";

	// Token: 0x040006CD RID: 1741
	private static readonly StringBuilder stringBuilder = new StringBuilder();

	// Token: 0x040006CE RID: 1742
	public const string MaterialPath = "Bundles/Material/";
}
