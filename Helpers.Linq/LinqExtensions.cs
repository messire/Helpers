namespace Helpers.Linq;

public static class LinqExtensions
{
	public static T With<T>(this T self, Action<T> apply, Func<bool> when)
	{
		if (when())
		{
			apply.Invoke(self);
		}

		return self;
	}
	
	public static T With<T>(this T self, Action<T> apply, bool when)
	{
		if (when)
		{
			apply.Invoke(self);
		}

		return self;
	}
	
	public static T With<T>(this T self, Action<T> apply)
	{
		apply.Invoke(self);
		return self;
	}
}
