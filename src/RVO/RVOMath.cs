using System;

namespace RVO
{
	// Token: 0x0200048F RID: 1167
	public struct RVOMath
	{
		// Token: 0x060019F0 RID: 6640 RVA: 0x000A111A File Offset: 0x0009F31A
		public static float abs(Vector2 vector)
		{
			return RVOMath.sqrt(RVOMath.absSq(vector));
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x000A1127 File Offset: 0x0009F327
		public static float absSq(Vector2 vector)
		{
			return vector * vector;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x000A1130 File Offset: 0x0009F330
		public static Vector2 normalize(Vector2 vector)
		{
			return vector / RVOMath.abs(vector);
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x000A113E File Offset: 0x0009F33E
		internal static float det(Vector2 vector1, Vector2 vector2)
		{
			return vector1.x_ * vector2.y_ - vector1.y_ * vector2.x_;
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x000A115C File Offset: 0x0009F35C
		internal static float distSqPointLineSegment(Vector2 vector1, Vector2 vector2, Vector2 vector3)
		{
			float num = (vector3 - vector1) * (vector2 - vector1) / RVOMath.absSq(vector2 - vector1);
			if (num < 0f)
			{
				return RVOMath.absSq(vector3 - vector1);
			}
			if (num > 1f)
			{
				return RVOMath.absSq(vector3 - vector2);
			}
			return RVOMath.absSq(vector3 - (vector1 + num * (vector2 - vector1)));
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000A11D2 File Offset: 0x0009F3D2
		internal static float fabs(float scalar)
		{
			return Math.Abs(scalar);
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x000A11DA File Offset: 0x0009F3DA
		internal static float leftOf(Vector2 a, Vector2 b, Vector2 c)
		{
			return RVOMath.det(a - c, b - a);
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x000A11EF File Offset: 0x0009F3EF
		internal static float sqr(float scalar)
		{
			return scalar * scalar;
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x000A11F4 File Offset: 0x0009F3F4
		internal static float sqrt(float scalar)
		{
			return (float)Math.Sqrt((double)scalar);
		}

		// Token: 0x04001959 RID: 6489
		internal const float RVO_EPSILON = 1E-05f;
	}
}
