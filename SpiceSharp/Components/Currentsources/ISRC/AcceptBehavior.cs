﻿using SpiceSharp.Behaviors;
using SpiceSharp.Simulations;

namespace SpiceSharp.Components.CurrentSourceBehaviors
{
    /// <summary>
    /// Accept behavior for a <see cref="CurrentSource"/>
    /// </summary>
    public class AcceptBehavior : BaseAcceptBehavior
    {
        /// <summary>
        /// Necessary behaviors
        /// </summary>
        private CommonBehaviors.IndependentSourceParameters _bp;

        /// <summary>
        /// Creates a new instance of the <see cref="AcceptBehavior"/> class.
        /// </summary>
        /// <param name="name">Name</param>
        public AcceptBehavior(string name) : base(name) { }

        /// <summary>
        /// Setup the behavior
        /// </summary>
        /// <param name="simulation">Simulation</param>
        /// <param name="provider">Data provider</param>
        public override void Setup(Simulation simulation, SetupDataProvider provider)
        {
            base.Setup(simulation, provider);

            provider.ThrowIfNull(nameof(provider));

            // Get parameters
            _bp = provider.GetParameterSet<CommonBehaviors.IndependentSourceParameters>();
        }

        /// <summary>
        /// Called when a new timepoint is being tested.
        /// </summary>
        /// <param name="simulation">The time-based simulation.</param>
        public override void Probe(TimeSimulation simulation)
        {
            _bp.Waveform?.Probe(simulation);
        }

        /// <summary>
        /// Accept the current timepoint
        /// </summary>
        /// <param name="simulation">Time-based simulation</param>
        public override void Accept(TimeSimulation simulation)
        {
            _bp.Waveform?.Accept(simulation);
        }
    }
}
