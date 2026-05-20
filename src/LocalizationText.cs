using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000042 RID: 66
public class LocalizationText : MonoBehaviour
{
	// Token: 0x060000FE RID: 254 RVA: 0x00006C40 File Offset: 0x00004E40
	private void Start()
	{
		if (this.myText == null)
		{
			this.myText = base.GetComponent<Text>();
		}
		if (string.IsNullOrEmpty(this.id))
		{
			if (!string.IsNullOrEmpty(this.stringKey))
			{
				this.id = this.stringKey;
			}
			else
			{
				this.id = this.myText.text;
			}
		}
		this.SetText();
	}

	// Token: 0x060000FF RID: 255 RVA: 0x00006CA6 File Offset: 0x00004EA6
	private void OnEnable()
	{
		MySystemEvent.Instance.RegisterMessage(21, new Action<Body>(this.ChangeLanguage));
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00006CC0 File Offset: 0x00004EC0
	private void OnDisable()
	{
		if (Game.UI == null)
		{
			return;
		}
		MySystemEvent.Instance.UnregisterMessage(21, new Action<Body>(this.ChangeLanguage));
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00006CE2 File Offset: 0x00004EE2
	public void ChangeLanguage(Body body)
	{
		this.SetText();
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00006CEC File Offset: 0x00004EEC
	private void SetText()
	{
		if (this.id == null)
		{
			return;
		}
		string text = Game.Language.Get(this.id, "");
		if (text == null)
		{
			Debug.LogError("错误");
		}
		if (!string.IsNullOrEmpty(this.extraStr))
		{
			this.myText.text = PathDefine.Concat(text, this.extraStr);
			return;
		}
		this.myText.text = text;
	}

	// Token: 0x04000129 RID: 297
	[SerializeField]
	private string stringKey;

	// Token: 0x0400012A RID: 298
	[SerializeField]
	private string extraStr;

	// Token: 0x0400012B RID: 299
	private Text myText;

	// Token: 0x0400012C RID: 300
	private string id;
}
