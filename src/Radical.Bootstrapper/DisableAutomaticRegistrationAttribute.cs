using System;

namespace Radical.Bootstrapper
{
    /// <summary>
    /// Instructs the automatic registration process to ignore
    /// a type marked with this attribue.
    /// </summary>
    [AttributeUsage( AttributeTargets.Class )]
    public class DisableAutomaticRegistrationAttribute : Attribute
    {
    }
}
