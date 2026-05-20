using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000327 RID: 807
public class UI_GuideHeroItem : MonoBehaviour
{
	// Token: 0x0600128B RID: 4747 RVA: 0x0006E733 File Offset: 0x0006C933
	private void Awake()
	{
		this.button.AddButtonEvent(new UnityAction(this.OnBtnClick));
	}

	// Token: 0x0600128C RID: 4748 RVA: 0x0006E74C File Offset: 0x0006C94C
	public void SetHero(HeroType heroTypeValue)
	{
		this.heroType = heroTypeValue;
		this.skillNameText.text = Util.GetHeroName(this.heroType);
		this.skillIcon.sprite = Util.GetHeroIcon(this.heroType);
	}

	// Token: 0x0600128D RID: 4749 RVA: 0x0006E781 File Offset: 0x0006C981
	private void OnBtnClick()
	{
		EventSystem.current.SetSelectedGameObject(null);
		Game.UI.GetUI<UI_GuideHero>().ShowDec(this.skillNameText.text, UI_SelectHero.GetHeroInfo(this.heroType), this.skillIcon.sprite);
	}

	// Token: 0x040010C8 RID: 4296
	public Button button;

	// Token: 0x040010C9 RID: 4297
	public Image skillIcon;

	// Token: 0x040010CA RID: 4298
	public Text skillNameText;

	// Token: 0x040010CB RID: 4299
	private HeroType heroType;
}
