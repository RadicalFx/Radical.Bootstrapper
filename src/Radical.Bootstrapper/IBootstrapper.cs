using System;

namespace Radical.Bootstrapper
{
    public interface IBootstrapper
    {
		String ProbeDirectory { get; }
		String AssemblyFilter { get; }

        BootstrapConventions Conventions { get; }
    }

	public interface IBootstrapper<TContainer> : IBootstrapper
	{
		TContainer Boot();
		IDelayedStartup<TContainer> DelayedBoot();
	}
}
