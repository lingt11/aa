using System;
using System.Collections.Generic;
using UnityEngine.UI;

// Token: 0x0200002F RID: 47
public class TreeNode
{
	// Token: 0x060000B7 RID: 183 RVA: 0x000058D0 File Offset: 0x00003AD0
	public TreeNode(string name)
	{
		this.name = name;
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x000058EA File Offset: 0x00003AEA
	public bool ChildNodeIsShow()
	{
		return this.poolView != null && this.poolView.viewList.Count > 0;
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00005910 File Offset: 0x00003B10
	public void CloseChildNode()
	{
		foreach (TreeNode treeNode in this.treeNodes)
		{
			treeNode.CloseChildNode();
		}
		PoolView poolView = this.poolView;
		if (poolView == null)
		{
			return;
		}
		poolView.RemoveAllView();
	}

	// Token: 0x040000E6 RID: 230
	public string name;

	// Token: 0x040000E7 RID: 231
	public Action action;

	// Token: 0x040000E8 RID: 232
	public Button button;

	// Token: 0x040000E9 RID: 233
	public PoolView poolView;

	// Token: 0x040000EA RID: 234
	public TreeNode parentNode;

	// Token: 0x040000EB RID: 235
	public List<TreeNode> treeNodes = new List<TreeNode>();
}
