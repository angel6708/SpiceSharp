﻿using SpiceSharp.Behaviors;
using SpiceSharp.Simulations;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SpiceSharp.Entities
{
    public static partial class DI
    {
        /// <summary>
        /// Class for creating binding contexts in a cached way.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        protected static class Context<TContext>
            where TContext : IBindingContext
        {
            private static readonly object _lock = new object();
            private static ContextFactory _contextFactory;

            /// <summary>
            /// Creates a new binding context.
            /// </summary>
            /// <param name="simulation">The simulation.</param>
            /// <param name="entity">The entity.</param>
            /// <param name="container">The behavior container.</param>
            /// <param name="linkParameters">if set to <c>true</c>, parameters are linked and not cloned.</param>
            /// <returns>The new context.</returns>
            public static TContext Get(ISimulation simulation, IEntity entity, IBehaviorContainer container, bool linkParameters)
            {
                if (_contextFactory == null)
                {
                    lock(_lock)
                    {
                        if (_contextFactory == null)
                            CreateFactory();
                    }
                }
                return _contextFactory(simulation, entity, container, linkParameters);
            }

            /// <summary>
            /// A delegate that describes the possible inputs for a context.
            /// </summary>
            /// <param name="simulation">The simulation.</param>
            /// <param name="entity">The entity.</param>
            /// <param name="container">The container with the behaviors.</param>
            /// <param name="linkParameters">if set to <c>true</c>, the parameters will be linked.</param>
            /// <returns>The context.</returns>
            public delegate TContext ContextFactory(ISimulation simulation, IEntity entity, IBehaviorContainer container, bool linkParameters);

            /// <summary>
            /// Creates the factory for creating binding contexts.
            /// </summary>
            /// <exception cref="ArgumentException">Thrown if the constructor cannot be resolved.</exception>
            private static void CreateFactory()
            {
                // Create the constructor for the binding context
                var ctors = typeof(TContext).GetTypeInfo().GetConstructors();
                ConstructorInfo ctor;
                if (ctors == null)
                    ctor = null;
                else if (ctors.Length > 0)
                    ctor = ctors[0];
                else
                    throw new ArgumentException("Cannot resolve constructor for '{0}'.".FormatString(typeof(TContext).FullName));

                var pSimulation = Expression.Parameter(typeof(ISimulation), "simulation");
                var pEntity = Expression.Parameter(typeof(IEntity), "entity");
                var pContainer = Expression.Parameter(typeof(IBehaviorContainer), "behaviors");
                var pLinkParameters = Expression.Parameter(typeof(bool), "linkParameters");
                var parameters = ctor.GetParameters();
                var arguments = new Expression[parameters.Length];
                foreach (var p in parameters)
                {
                    var type = p.ParameterType;
                    if (type == typeof(ISimulation))
                        arguments[p.Position] = pSimulation;
                    else if (type == typeof(bool))
                        arguments[p.Position] = pLinkParameters;
                    else if (type == typeof(IBehaviorContainer))
                        arguments[p.Position] = pContainer;
                    else if (type == typeof(IEntity))
                        arguments[p.Position] = pEntity;

                    // Harder/slower checks
                    else if (typeof(IEntity).GetTypeInfo().IsAssignableFrom(type))
                        arguments[p.Position] = Expression.TypeAs(pEntity, type);
                    else if (typeof(ISimulation).GetTypeInfo().IsAssignableFrom(type))
                        arguments[p.Position] = Expression.TypeAs(pSimulation, type);
                    else if (typeof(IBehaviorContainer).GetTypeInfo().IsAssignableFrom(type))
                        arguments[p.Position] = Expression.TypeAs(pContainer, type);
                    else
                        throw new ArgumentException("Constructor of '{0}' contains a parameter that cannot be resolved.".FormatString(typeof(TContext).FullName));
                }
                _contextFactory = Expression.Lambda<ContextFactory>(Expression.New(ctor, arguments), pSimulation, pEntity, pContainer, pLinkParameters).Compile();
            }
        }
    }
}
