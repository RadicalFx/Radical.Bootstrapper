namespace Radical.Bootstrapper
{
    public interface IServiceProviderWrapper
	{
		TContainer Unwrap<TContainer>();
	}
}
