using System;
using System.ComponentModel.Composition;

namespace Radical.Bootstrapper
{
    [InheritedExport]
	public interface IConfigurator
	{
		void Configure( IServiceProvider container );
	}
}
