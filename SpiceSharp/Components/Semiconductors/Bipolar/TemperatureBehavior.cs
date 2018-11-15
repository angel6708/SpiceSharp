﻿using System;
using SpiceSharp.Behaviors;
using SpiceSharp.Simulations;
using SpiceSharp.Simulations.Behaviors;

namespace SpiceSharp.Components.BipolarBehaviors
{
    /// <summary>
    /// Temperature behavior for a <see cref="BipolarJunctionTransistor"/>
    /// </summary>
    public class TemperatureBehavior : ExportingBehavior, ITemperatureBehavior
    {
        /// <summary>
        /// Gets the base parameters.
        /// </summary>
        /// <value>
        /// The base parameters.
        /// </value>
        protected BaseParameters BaseParameters { get; private set; }

        /// <summary>
        /// Gets the model parameters.
        /// </summary>
        /// <value>
        /// The model parameters.
        /// </value>
        protected ModelBaseParameters ModelParameters { get; private set; }

        /// <summary>
        /// Gets the model temperature behavior.
        /// </summary>
        /// <value>
        /// The model temperature behavior.
        /// </value>
        protected ModelTemperatureBehavior ModelTemperature { get; private set; }

        /// <summary>
        /// Shared parameters
        /// </summary>
        public double TempSaturationCurrent { get; protected set; }
        public double TempBetaForward { get; protected set; }
        public double TempBetaReverse { get; protected set; }
        public double TempBeLeakageCurrent { get; protected set; }
        public double TempBcLeakageCurrent { get; protected set; }
        public double TempBeCap { get; protected set; }
        public double TempBePotential { get; protected set; }
        public double TempBcCap { get; protected set; }
        public double TempBcPotential { get; protected set; }
        public double TempDepletionCap { get; protected set; }
        public double TempFactor1 { get; protected set; }
        public double TempFactor4 { get; protected set; }
        public double TempFactor5 { get; protected set; }
        public double TempVCritical { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name</param>
        public TemperatureBehavior(string name) : base(name) { }

        /// <summary>
        /// Setup behavior
        /// </summary>
        /// <param name="simulation">Simulation</param>
        /// <param name="provider">Data provider</param>
        public override void Setup(Simulation simulation, SetupDataProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            // Get parameters
            BaseParameters = provider.GetParameterSet<BaseParameters>();
            ModelParameters = provider.GetParameterSet<ModelBaseParameters>("model");

            // Get behaviors
            ModelTemperature = provider.GetBehavior<ModelTemperatureBehavior>("model");
        }

        /// <summary>
        /// Do temperature-dependent calculations
        /// </summary>
        /// <param name="simulation">Base simulation</param>
        public void Temperature(BaseSimulation simulation)
        {
			if (simulation == null)
				throw new ArgumentNullException(nameof(simulation));

            if (!BaseParameters.Temperature.Given)
                BaseParameters.Temperature.RawValue = simulation.RealState.Temperature;
            var vt = BaseParameters.Temperature * Circuit.KOverQ;
            var fact2 = BaseParameters.Temperature / Circuit.ReferenceTemperature;
            var egfet = 1.16 - 7.02e-4 * BaseParameters.Temperature * BaseParameters.Temperature / (BaseParameters.Temperature + 1108);
            var arg = -egfet / (2 * Circuit.Boltzmann * BaseParameters.Temperature) + 1.1150877 / (Circuit.Boltzmann * (Circuit.ReferenceTemperature +
                                                                                                                Circuit.ReferenceTemperature));
            var pbfact = -2 * vt * (1.5 * Math.Log(fact2) + Circuit.Charge * arg);

            var ratlog = Math.Log(BaseParameters.Temperature / ModelParameters.NominalTemperature);
            var ratio1 = BaseParameters.Temperature / ModelParameters.NominalTemperature - 1;
            var factlog = ratio1 * ModelParameters.EnergyGap / vt + ModelParameters.TempExpIs * ratlog;
            var factor = Math.Exp(factlog);
            TempSaturationCurrent = ModelParameters.SatCur * factor;
            var bfactor = Math.Exp(ratlog * ModelParameters.BetaExponent);
            TempBetaForward = ModelParameters.BetaF * bfactor;
            TempBetaReverse = ModelParameters.BetaR * bfactor;
            TempBeLeakageCurrent = ModelParameters.LeakBeCurrent * Math.Exp(factlog / ModelParameters.LeakBeEmissionCoefficient) / bfactor;
            TempBcLeakageCurrent = ModelParameters.LeakBcCurrent * Math.Exp(factlog / ModelParameters.LeakBcEmissionCoefficient) / bfactor;

            var pbo = (ModelParameters.PotentialBe - pbfact) / ModelTemperature.Factor1;
            var gmaold = (ModelParameters.PotentialBe - pbo) / pbo;
            TempBeCap = ModelParameters.DepletionCapBe / (1 + ModelParameters.JunctionExpBe * (4e-4 * (ModelParameters.NominalTemperature - Circuit.ReferenceTemperature) - gmaold));
            TempBePotential = fact2 * pbo + pbfact;
            var gmanew = (TempBePotential - pbo) / pbo;
            TempBeCap *= 1 + ModelParameters.JunctionExpBe * (4e-4 * (BaseParameters.Temperature - Circuit.ReferenceTemperature) - gmanew);

            pbo = (ModelParameters.PotentialBc - pbfact) / ModelTemperature.Factor1;
            gmaold = (ModelParameters.PotentialBc - pbo) / pbo;
            TempBcCap = ModelParameters.DepletionCapBc / (1 + ModelParameters.JunctionExpBc * (4e-4 * (ModelParameters.NominalTemperature - Circuit.ReferenceTemperature) - gmaold));
            TempBcPotential = fact2 * pbo + pbfact;
            gmanew = (TempBcPotential - pbo) / pbo;
            TempBcCap *= 1 + ModelParameters.JunctionExpBc * (4e-4 * (BaseParameters.Temperature - Circuit.ReferenceTemperature) - gmanew);

            TempDepletionCap = ModelParameters.DepletionCapCoefficient * TempBePotential;
            TempFactor1 = TempBePotential * (1 - Math.Exp((1 - ModelParameters.JunctionExpBe) * ModelTemperature.Xfc)) / (1 - ModelParameters.JunctionExpBe);
            TempFactor4 = ModelParameters.DepletionCapCoefficient * TempBcPotential;
            TempFactor5 = TempBcPotential * (1 - Math.Exp((1 - ModelParameters.JunctionExpBc) * ModelTemperature.Xfc)) / (1 - ModelParameters.JunctionExpBc);
            TempVCritical = vt * Math.Log(vt / (Circuit.Root2 * ModelParameters.SatCur));
        }
    }
}
