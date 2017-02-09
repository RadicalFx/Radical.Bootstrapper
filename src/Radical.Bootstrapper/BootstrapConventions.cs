using System;
using System.Linq;
using System.Collections.Generic;
using Topics.Radical.ComponentModel;
using Topics.Radical.Linq;
using Topics.Radical.Reflection;
using Topics.Radical.ComponentModel.Validation;

namespace Radical.Bootstrapper
{
    /// <summary>
    /// 
    /// </summary>
    public class BootstrapConventions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BootstrapConventions" /> class.
        /// </summary>
        public BootstrapConventions()
        {
            this.IsConcreteType = t => !t.IsInterface && !t.IsAbstract && !t.IsGenericType && t.Namespace != null;

            this.IsService = t => this.IsConcreteType( t ) && Topics.Radical.StringExtensions.IsLike( t.Namespace, "*.Services" );

            this.SelectServiceContracts = type =>
            {
                var interfaces = type.GetInterfaces();
                var types = new HashSet<Type>( interfaces )
                {
                    type
                };

                var contracts = types.Where( t => t.IsAttributeDefined<ContractAttribute>() );
                if ( contracts.Any() )
                {
                    return contracts;
                }

                if ( interfaces.Any() )
                {
                    return interfaces;
                }

                return types;
            };

            this.IsComponent = t => this.IsConcreteType(t) && Topics.Radical.StringExtensions.IsLike(t.Namespace, "*.Components");

            this.SelectComponentContracts = type =>
            {
                var interfaces = type.GetInterfaces();
                var types = new HashSet<Type>(interfaces)
                {
                    type
                };

                var contracts = types.Where(t => t.IsAttributeDefined<ContractAttribute>());
                if (contracts.Any())
                {
                    return contracts;
                }

                if (interfaces.Any())
                {
                    return interfaces;
                }

                return types;
            };

            this.IsFactory = t => this.IsConcreteType( t ) && t.IsNested && t.Name.EndsWith( "Factory" );

            this.SelectFactoryContracts = type => new[] { type };

            this.IsValidator = t => this.IsConcreteType( t ) && t.Namespace.EndsWith( ".Validators" ) && t.Is( typeof( IValidator<> ) );

            this.SelectValidatorContracts = t =>
            {
                var contracts = t.GetInterfaces()
                            .Where( i => i.IsAttributeDefined<ContractAttribute>() )
                            .ToArray();
                return contracts;
            };

            this.IsExcluded = t =>
            {
                return t.IsAttributeDefined<DisableAutomaticRegistrationAttribute>();
            };

            //this.AssemblyFileScanPatterns = entryAssembly => 
            //{
            //    var name = entryAssembly.GetName().Name;

            //    var dllPattern = String.Format( "{0}*.dll", name );
            //    var radical = "Radical.*.dll";

            //    return new[] { dllPattern, radical };
            //};

            //this.IncludeAssemblyInContainerScan = assembly => true;

        }

        /// <summary>
        /// Gets or sets the type of the is concrete.
        /// </summary>
        /// <value>
        /// The type of the is concrete.
        /// </value>
        public Predicate<Type> IsConcreteType { get; set; }

        /// <summary>
        /// Gets or sets the is service.
        /// </summary>
        /// <value>
        /// The is service.
        /// </value>
        public Predicate<Type> IsService { get; set; }

        /// <summary>
        /// Gets or sets the select service contracts.
        /// </summary>
        /// <value>
        /// The select service contracts.
        /// </value>
        public Func<Type, IEnumerable<Type>> SelectServiceContracts { get; set; }

        public Predicate<Type> IsComponent { get; set; }

        public Func<Type, IEnumerable<Type>> SelectComponentContracts { get; set; }

        public Predicate<Type> IsFactory { get; set; }

        public Func<Type, IEnumerable<Type>> SelectFactoryContracts { get; set; }

        public Predicate<Type> IsValidator { get; set; }

        public Func<Type, IEnumerable<Type>> SelectValidatorContracts { get; set; }

        /// <summary>
        /// Gets or sets the is excluded.
        /// </summary>
        /// <value>
        /// The is excluded.
        /// </value>
        public Func<Type, Boolean> IsExcluded { get; set; }

        public Predicate<Type> IsComponent { get; private set; }

        public Func<Type, IEnumerable<Type>> SelectComponentContracts { get; private set; }

        ///// <summary>
        ///// Gets or sets the assembly file scan patterns.
        ///// </summary>
        ///// <value>
        ///// The assembly file scan patterns.
        ///// </value>
        //public Func<Assembly, IEnumerable<String>> AssemblyFileScanPatterns{get;set;}

        ///// <summary>
        ///// Gets or sets the include assembly in container scan.
        ///// </summary>
        ///// <value>
        ///// The include assembly in container scan.
        ///// </value>
        //public Predicate<Assembly> IncludeAssemblyInContainerScan { get; set; }

    }
}
