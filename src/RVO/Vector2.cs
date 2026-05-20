using System;
using System.Globalization;

namespace RVO
{
	// Token: 0x02000492 RID: 1170
	public struct Vector2
	{
		// Token: 0x06001A2F RID: 6703 RVA: 0x000A1D58 File Offset: 0x0009FF58
		public Vector2(float x, float y)
		{
			this.x_ = x;
			this.y_ = y;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x000A1D68 File Offset: 0x0009FF68
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"(",
				this.x_.ToString(new CultureInfo("").NumberFormat),
				",",
				this.y_.ToString(new CultureInfo("").NumberFormat),
				")"
			});
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x000A1DD2 File Offset: 0x0009FFD2
		public float x()
		{
			return this.x_;
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x000A1DDA File Offset: 0x0009FFDA
		public float y()
		{
			return this.y_;
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x000A1DE2 File Offset: 0x0009FFE2
		public static float operator *(Vector2 vector1, Vector2 vector2)
		{
			return vector1.x_ * vector2.x_ + vector1.y_ * vector2.y_;
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x000A1DFF File Offset: 0x0009FFFF
		public static Vector2 operator *(float scalar, Vector2 vector)
		{
			return vector * scalar;
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x000A1E08 File Offset: 0x000A0008
		public static Vector2 operator *(Vector2 vector, float scalar)
		{
			return new Vector2(vector.x_ * scalar, vector.y_ * scalar);
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x000A1E1F File Offset: 0x000A001F
		public static Vector2 operator /(Vector2 vector, float scalar)
		{
			return new Vector2(vector.x_ / scalar, vector.y_ / scalar);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x000A1E36 File Offset: 0x000A0036
		public static Vector2 operator +(Vector2 vector1, Vector2 vector2)
		{
			return new Vector2(vector1.x_ + vector2.x_, vector1.y_ + vector2.y_);
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x000A1E57 File Offset: 0x000A0057
		public static Vector2 operator -(Vector2 vector1, Vector2 vector2)
		{
			return new Vector2(vector1.x_ - vector2.x_, vector1.y_ - vector2.y_);
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x000A1E78 File Offset: 0x000A0078
		public static Vector2 operator -(Vector2 vector)
		{
			return new Vector2(-vector.x_, -vector.y_);
		}

		// Token: 0x0400196B RID: 6507
		internal float x_;

		// Token: 0x0400196C RID: 6508
		internal float y_;
	}
}
