using System;
using System.Collections.Generic;

// Token: 0x0200002D RID: 45
public class ButtonTreeData
{
	// Token: 0x060000B2 RID: 178 RVA: 0x0000579C File Offset: 0x0000399C
	public void AddNode(string name, Action ac)
	{
		string[] array = name.Split('/', StringSplitOptions.None);
		List<TreeNode> treeNodes = this.nodeList;
		TreeNode treeNode = null;
		Action <>9__0;
		for (int i = 0; i < array.Length; i++)
		{
			TreeNode nodeByName = this.GetNodeByName(treeNodes, array[i]);
			if (treeNode != null)
			{
				nodeByName.parentNode = treeNode;
			}
			if (i < array.Length - 1)
			{
				treeNodes = nodeByName.treeNodes;
			}
			else
			{
				TreeNode treeNode2 = nodeByName;
				Delegate action = treeNode2.action;
				Action b;
				if ((b = <>9__0) == null)
				{
					b = (<>9__0 = delegate()
					{
						ac();
					});
				}
				treeNode2.action = (Action)Delegate.Combine(action, b);
			}
			treeNode = nodeByName;
		}
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x00005844 File Offset: 0x00003A44
	private TreeNode GetNodeByName(List<TreeNode> list, string name)
	{
		foreach (TreeNode treeNode in list)
		{
			if (treeNode.name.Equals(name))
			{
				return treeNode;
			}
		}
		TreeNode treeNode2 = new TreeNode(name);
		list.Add(treeNode2);
		return treeNode2;
	}

	// Token: 0x040000E3 RID: 227
	public List<TreeNode> nodeList = new List<TreeNode>();
}
