using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000312 RID: 786
public class UI_DevTool : UGUICtrl
{
	// Token: 0x0600122F RID: 4655 RVA: 0x0006CDB0 File Offset: 0x0006AFB0
	public UI_DevTool()
	{
		this.selfView = new UI_DevTool_View();
		base.OnCreate(this.selfView, "UI/Prefabs/UI_DevTool", base.GetType());
		this.SetData();
		Debug.Log("xxxx");
		this.alwaysUpdate = true;
	}

	// Token: 0x06001230 RID: 4656 RVA: 0x0006CE07 File Offset: 0x0006B007
	protected override void OpenPanel(object data)
	{
		if (data != null)
		{
			base.CloseSelfPanel();
		}
	}

	// Token: 0x06001231 RID: 4657 RVA: 0x0006CE12 File Offset: 0x0006B012
	protected override void ClosePanel()
	{
		if (this.curTreeNode != null)
		{
			this.BackChildNode();
			this.ClosePanel();
		}
	}

	// Token: 0x06001232 RID: 4658 RVA: 0x0006CE28 File Offset: 0x0006B028
	private void BackChildNode()
	{
		this.curTreeNode.CloseChildNode();
		if (this.curTreeNode.parentNode != null)
		{
			this.curTreeNode = this.curTreeNode.parentNode;
			this.curTreeList = this.curTreeNode.treeNodes;
			return;
		}
		this.curTreeNode = null;
		this.curTreeList = this.buttonTreeData.nodeList;
	}

	// Token: 0x06001233 RID: 4659 RVA: 0x0006CE88 File Offset: 0x0006B088
	private void SetData()
	{
		Assembly assembly = base.GetType().Assembly;
		List<Type> list = new List<Type>();
		foreach (Type type in assembly.GetTypes())
		{
			if (type.GetCustomAttribute<DevPriority>() != null)
			{
				list.Add(type);
			}
		}
		list.Sort(new Comparison<Type>(this.ComparaList));
		for (int j = 0; j < list.Count; j++)
		{
			MethodInfo[] methods = list[j].GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			for (int i = 0; i < methods.Length; i++)
			{
				MethodInfo methodOne = methods[i];
				DevConsole customAttribute = methodOne.GetCustomAttribute<DevConsole>();
				if (customAttribute != null)
				{
					this.buttonTreeData.AddNode(customAttribute.name, delegate
					{
						methodOne.Invoke(null, null);
						this.CloseSelfPanel();
					});
				}
			}
		}
		this.curTreeList = this.buttonTreeData.nodeList;
		PoolView component = this.CreateContent(this.selfView.trans_point.position).GetComponent<PoolView>();
		this.CreateUI(component, this.buttonTreeData.nodeList);
	}

	// Token: 0x06001234 RID: 4660 RVA: 0x0006CFA1 File Offset: 0x0006B1A1
	private int ComparaList(Type t1, Type t2)
	{
		return t1.GetCustomAttribute<DevPriority>().priority.CompareTo(t2.GetCustomAttribute<DevPriority>().priority);
	}

	// Token: 0x06001235 RID: 4661 RVA: 0x0006CFC0 File Offset: 0x0006B1C0
	public override void Update()
	{
		if (Input.anyKeyDown)
		{
			foreach (object obj in Enum.GetValues(typeof(KeyCode)))
			{
				KeyCode key = (KeyCode)obj;
				if (Input.GetKeyDown(key))
				{
					string text = key.ToString();
					if (text.Equals("Comma") || text.Equals("KeypadPeriod"))
					{
						if (this.isOpen)
						{
							base.CloseSelfPanel();
							return;
						}
						Game.UI.OpenUI<UI_DevTool>(null);
						return;
					}
					else
					{
						if (text.Equals("Backspace") || text.Equals("KeypadMinus"))
						{
							this.BackUI();
							return;
						}
						text = text.Replace("Alpha", "");
						text = text.Replace("Keypad", "");
						text = new Regex("^[0-9]$").Match(text).ToString();
						if (!string.IsNullOrEmpty(text))
						{
							int num = int.Parse(text);
							if (num < this.curTreeList.Count)
							{
								this.curTreeList[num].button.onClick.Invoke();
							}
						}
					}
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.F6))
		{
			EntityStatic.Get<UGUIManager>().canvasRectTransform.gameObject.SetActive(!EntityStatic.Get<UGUIManager>().canvasRectTransform.gameObject.activeSelf);
		}
	}

	// Token: 0x06001236 RID: 4662 RVA: 0x0006D160 File Offset: 0x0006B360
	private void BackUI()
	{
		if (this.curTreeNode != null)
		{
			this.BackChildNode();
			return;
		}
		base.CloseSelfPanel();
	}

	// Token: 0x06001237 RID: 4663 RVA: 0x0006D177 File Offset: 0x0006B377
	private PoolView CreateContent(Vector3 pos)
	{
		GameObject gameObject = this.selfView.pool_frame.AddView();
		gameObject.transform.position = pos;
		return gameObject.GetComponent<PoolView>();
	}

	// Token: 0x06001238 RID: 4664 RVA: 0x0006D19C File Offset: 0x0006B39C
	private void CreateUI(PoolView pool, List<TreeNode> nodeList)
	{
		for (int i = 0; i < nodeList.Count; i++)
		{
			TreeNode node = nodeList[i];
			GameObject go = pool.AddView();
			go.transform.GetComponentInChildren<Text>().text = i.ToString() + node.name;
			node.button = go.transform.GetComponent<Button>();
			go.transform.GetComponent<PointerEnterButton>().action = delegate()
			{
				this.OpenNode(node, nodeList, go);
			};
			node.button.onClick.AddListener(delegate()
			{
				if (this.OpenNode(node, nodeList, go))
				{
					node.action();
				}
			});
		}
	}

	// Token: 0x06001239 RID: 4665 RVA: 0x0006D294 File Offset: 0x0006B494
	private bool OpenNode(TreeNode node, List<TreeNode> nodeList, GameObject go)
	{
		this.curTreeNode = node;
		foreach (TreeNode treeNode in nodeList)
		{
			if (node != treeNode)
			{
				treeNode.CloseChildNode();
			}
		}
		if (node.treeNodes.Count > 0)
		{
			if (node.ChildNodeIsShow())
			{
				node.CloseChildNode();
				this.curTreeList = nodeList;
			}
			else
			{
				PoolView component = this.CreateContent(go.transform.position).GetComponent<PoolView>();
				RectTransform component2 = component.GetComponent<RectTransform>();
				if (component2 != null)
				{
					component2.anchoredPosition = new Vector2(component2.anchoredPosition.x + 205f, Mathf.Max(component2.anchoredPosition.y, (float)(-1080 + node.treeNodes.Count * 50)));
				}
				node.poolView = component;
				this.curTreeList = node.treeNodes;
				this.CreateUI(component, node.treeNodes);
			}
			return false;
		}
		return true;
	}

	// Token: 0x04001079 RID: 4217
	public UI_DevTool_View selfView;

	// Token: 0x0400107A RID: 4218
	private ButtonTreeData buttonTreeData = new ButtonTreeData();

	// Token: 0x0400107B RID: 4219
	private List<TreeNode> curTreeList;

	// Token: 0x0400107C RID: 4220
	private TreeNode curTreeNode;

	// Token: 0x0400107D RID: 4221
	private new bool isOpen;
}
