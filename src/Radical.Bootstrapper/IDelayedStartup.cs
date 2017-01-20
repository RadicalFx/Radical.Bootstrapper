using System;

namespace Radical.Bootstrapper
{
    public interface IDelayedStartup<TContainer>
	{
		TContainer Container { get; }
		Func<TContainer> Startup { get; }
	}
}
