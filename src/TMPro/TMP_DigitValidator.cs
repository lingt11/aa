using System;

namespace TMPro
{
	// Token: 0x0200041C RID: 1052
	[Serializable]
	public class TMP_DigitValidator : TMP_InputValidator
	{
		// Token: 0x060017DE RID: 6110 RVA: 0x00094C5A File Offset: 0x00092E5A
		public override char Validate(ref string text, ref int pos, char ch)
		{
			if (ch >= '0' && ch <= '9')
			{
				text += ch.ToString();
				pos++;
				return ch;
			}
			return '\0';
		}
	}
}
