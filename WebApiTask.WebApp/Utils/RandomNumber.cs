using System;
using System.Threading;

namespace WebApiTask.WebApp
{
	public static class RandomNumber
	{
		private static int seed = Environment.TickCount;

		private static ThreadLocal<Random> randomWrapper = new ThreadLocal<Random>(() =>
			new Random(Interlocked.Increment(ref seed))
		);

		public static Random GetThreadRandom()
		{
			return randomWrapper.Value;
		}
	}

}
