﻿using SpiceSharp.Attributes;
using SpiceSharp.Behaviors;
using SpiceSharp.Components.VoltageControlledCurrentSources;
using SpiceSharp.ParameterSets;
using SpiceSharp.Simulations;
using SpiceSharp.Validation;
using System;
using System.Linq;

namespace SpiceSharp.Components
{
    /// <summary>
    /// A voltage-controlled current source.
    /// </summary>
    /// <seealso cref="Component"/>
    /// <seealso cref="IParameterized{P}"/>
    /// <seealso cref="IRuleSubject"/>
    /// <seealso cref="Parameters"/>
    [Pin(0, "G+"), Pin(1, "G-"), Pin(2, "VC+"), Pin(3, "VC-"), Connected()]
    public class VoltageControlledCurrentSource : Component,
        IParameterized<Parameters>,
        IRuleSubject
    {
        /// <inheritdoc/>
        public Parameters Parameters { get; } = new Parameters();

        /// <summary>
        /// The pin count for a voltage-controlled current source.
        /// </summary>
        [ParameterName("pincount"), ParameterInfo("Number of pins")]
        public const int PinCount = 4;

        /// <summary>
        /// Initializes a new instance of the <see cref="VoltageControlledCurrentSource"/> class.
        /// </summary>
        /// <param name="name">The name of the voltage-controlled current source.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is <c>null</c>.</exception>
        public VoltageControlledCurrentSource(string name)
            : base(name, PinCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VoltageControlledCurrentSource"/> class.
        /// </summary>
        /// <param name="name">The name of the voltage-controlled current source.</param>
        /// <param name="pos">The positive node.</param>
        /// <param name="neg">The negative node.</param>
        /// <param name="controlPos">The positive controlling node.</param>
        /// <param name="controlNeg">The negative controlling node.</param>
        /// <param name="gain">The transconductance gain.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is <c>null</c>.</exception>
        public VoltageControlledCurrentSource(string name, string pos, string neg, string controlPos, string controlNeg, double gain)
            : this(name)
        {
            Parameters.Transconductance = gain;
            Connect(pos, neg, controlPos, controlNeg);
        }

        /// <inheritdoc/>
        public override void CreateBehaviors(ISimulation simulation)
        {
            var behaviors = new BehaviorContainer(Name);
            var context = new ComponentBindingContext(this, simulation, behaviors, LinkParameters);
            behaviors.Build(simulation, context)
                .AddIfNo<IFrequencyBehavior>(context => new FrequencyBehavior(Name, context))
                .AddIfNo<IBiasingBehavior>(context => new BiasingBehavior(Name, context));
            simulation.EntityBehaviors.Add(behaviors);
        }

        void IRuleSubject.Apply(IRules rules)
        {
            var p = rules.GetParameterSet<ComponentRuleParameters>();
            var nodes = Nodes.Select(name => p.Factory.GetSharedVariable(name)).ToArray();
            foreach (var rule in rules.GetRules<IConductiveRule>())
                rule.AddPath(this, ConductionTypes.None, nodes);
        }
    }
}
