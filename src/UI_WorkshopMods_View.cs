using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003C4 RID: 964
public class UI_WorkshopMods_View : UGUIView
{
	// Token: 0x06001623 RID: 5667 RVA: 0x00089708 File Offset: 0x00087908
	public override void Init(Transform trans)
	{
		this.btn_back = UI_WorkshopMods_View.Get<Button>(trans, "panel/toolbar/btn_back");
		this.btn_refresh = UI_WorkshopMods_View.Get<Button>(trans, "panel/toolbar/btn_refresh");
		this.btn_openWorkshop = UI_WorkshopMods_View.Get<Button>(trans, "panel/toolbar/btn_openWorkshop");
		this.btn_openHeroWorkshop = UI_WorkshopMods_View.Get<Button>(trans, "panel/toolbar/btn_openHeroWorkshop");
		this.text_title = UI_WorkshopMods_View.Get<Text>(trans, "panel/header/text_title");
		this.text_status = UI_WorkshopMods_View.Get<Text>(trans, "panel/header/text_status");
		this.text_filterHero = UI_WorkshopMods_View.Get<Text>(trans, "panel/header/text_filterHero");
		this.text_empty = UI_WorkshopMods_View.Get<Text>(trans, "panel/content/text_empty");
		this.pool_items = UI_WorkshopMods_View.Get<PoolView>(trans, "panel/content/scroll_view/viewport/pool_items");
	}

	// Token: 0x06001624 RID: 5668 RVA: 0x000897B0 File Offset: 0x000879B0
	private static T Get<T>(Transform root, string path) where T : Component
	{
		Transform transform = root.Find(path);
		if (transform == null)
		{
			Debug.LogError("UI_WorkshopMods_View missing path: " + path);
			return default(T);
		}
		T component = transform.GetComponent<T>();
		if (component == null)
		{
			Debug.LogError("UI_WorkshopMods_View missing component " + typeof(T).Name + " on path: " + path);
		}
		return component;
	}

	// Token: 0x040014BE RID: 5310
	public Button btn_back;

	// Token: 0x040014BF RID: 5311
	public Button btn_refresh;

	// Token: 0x040014C0 RID: 5312
	public Button btn_openWorkshop;

	// Token: 0x040014C1 RID: 5313
	public Button btn_openHeroWorkshop;

	// Token: 0x040014C2 RID: 5314
	public Text text_title;

	// Token: 0x040014C3 RID: 5315
	public Text text_status;

	// Token: 0x040014C4 RID: 5316
	public Text text_filterHero;

	// Token: 0x040014C5 RID: 5317
	public Text text_empty;

	// Token: 0x040014C6 RID: 5318
	public PoolView pool_items;
}
