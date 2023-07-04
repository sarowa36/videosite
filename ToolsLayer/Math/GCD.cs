using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsLayer.Math
{
	public static class GCD
	{
		public static int Calculate(int a, int b)
		{
			int Remainder;
			while (b != 0)
			{
				Remainder = a % b;
				a = b;
				b = Remainder;
			}
			return a;
		}
	}
}
