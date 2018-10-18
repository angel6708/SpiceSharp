﻿using System;
using System.Numerics;
using NUnit.Framework;
using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Simulations;

namespace SpiceSharpTest.Models
{
    [TestFixture]
    public class TransmissionLineTests : Framework
    {
        [Test]
        public void When_LosslessTransmissionLineTransient_Expect_Reference()
        {
            // Build the circuit
            var ckt = new Circuit(
                new VoltageSource("V1", "in", "0", new Pulse(1, 5, 2e-6, 1e-9, 1e-9, 5e-6, 10e-6)),
                new Resistor("Rsource", "in", "a", 100),
                new LosslessTransmissionLine("T1", "a", "0", "b", "0", 50.0, 1e-6),
                new Resistor("Rload", "b", "0", 25)
            );
            ckt.Entities["T1"].SetParameter("reltol", 0.5);

            // Build the simulation
            var tran = new Transient("tran", 1e-6, 20e-6);
            var exports = new Export<double>[]
            {
                new GenericExport<double>(tran, () => tran.Method.Time),
                new RealVoltageExport(tran, "a"),
                new RealVoltageExport(tran, "b")
            };
            var references = new[]
            {
                new[]
                {
                    0, 4E-09, 8E-09, 1.6E-08, 3.2E-08, 6.4E-08, 1.28E-07, 2.56E-07, 5.12E-07, 9.12E-07, 1.312E-06,
                    1.712E-06, 2E-06, 2.0001E-06, 2.0003E-06, 2.0007E-06, 2.001E-06, 2.00108E-06, 2.00124E-06,
                    2.00156E-06, 2.0022E-06, 2.00348E-06, 2.00604E-06, 2.01116E-06, 2.0214E-06, 2.04188E-06,
                    2.08284000000001E-06, 2.16476000000001E-06, 2.32860000000002E-06, 2.65628000000005E-06, 3E-06,
                    3.0001E-06, 3.0003E-06, 3.0007E-06, 3.001E-06, 3.00108E-06, 3.00124E-06, 3.00156E-06, 3.0022E-06,
                    3.00348E-06, 3.00604E-06, 3.01116E-06, 3.02139999999999E-06, 3.04187999999999E-06,
                    3.08283999999997E-06, 3.16475999999994E-06, 3.32859999999989E-06, 3.65627999999977E-06, 4E-06,
                    4.0001E-06, 4.0003E-06, 4.0007E-06, 4.001E-06, 4.00108E-06, 4.00124E-06, 4.00156E-06, 4.0022E-06,
                    4.00348E-06, 4.00604E-06, 4.01116E-06, 4.02139999999999E-06, 4.04187999999999E-06,
                    4.08283999999997E-06, 4.16475999999994E-06, 4.32859999999989E-06, 4.65627999999977E-06, 5E-06,
                    5.0001E-06, 5.0003E-06, 5.0007E-06, 5.001E-06, 5.00108E-06, 5.00124E-06, 5.00156E-06, 5.0022E-06,
                    5.00348E-06, 5.00604E-06, 5.01116E-06, 5.02139999999999E-06, 5.04187999999999E-06,
                    5.08283999999997E-06, 5.16475999999994E-06, 5.32859999999988E-06, 5.65627999999977E-06, 6E-06,
                    6.0001E-06, 6.0003E-06, 6.0007E-06, 6.001E-06, 6.00108E-06, 6.00124E-06, 6.00156E-06, 6.0022E-06,
                    6.00348E-06, 6.00604E-06, 6.01116E-06, 6.02139999999999E-06, 6.04187999999998E-06,
                    6.08283999999997E-06, 6.16475999999994E-06, 6.32859999999988E-06, 6.65627999999977E-06, 7E-06,
                    7.0001E-06, 7.0003E-06, 7.0007E-06, 7.001E-06, 7.00100000000008E-06, 7.00100000000024E-06,
                    7.00100000000056E-06, 7.0010000000012E-06, 7.00100000000248E-06, 7.00100000000504E-06,
                    7.00100000001016E-06, 7.0010000000204E-06, 7.00100000004088E-06, 7.00100000008184E-06,
                    7.00100000016376E-06, 7.0010000003276E-06, 7.00100000065528E-06, 7.00100000131064E-06,
                    7.00100000262136E-06, 7.0010000052428E-06, 7.00100001048568E-06, 7.00100002097144E-06,
                    7.00100004194296E-06, 7.001000083886E-06, 7.00100016777208E-06, 7.00100033554424E-06,
                    7.00100067108856E-06, 7.0010013421772E-06, 7.00100268435448E-06, 7.00100536870904E-06,
                    7.00101073741816E-06, 7.0010214748364E-06, 7.00104294967288E-06, 7.00108589934584E-06,
                    7.00117179869176E-06, 7.0013435973836E-06, 7.00168719476728E-06, 7.002E-06, 7.00206871947674E-06,
                    7.00220615843021E-06, 7.00248103633715E-06, 7.00303079215104E-06, 7.00413030377882E-06,
                    7.00632932703437E-06, 7.01072737354547E-06, 7.01952346656768E-06, 7.0371156526121E-06,
                    7.07230002470093E-06, 7.14266876887859E-06, 7.28340625723392E-06, 7.56488123394458E-06,
                    7.96488123394458E-06, 8E-06, 8.0001E-06, 8.0003E-06, 8.0007E-06, 8.001E-06, 8.00108E-06,
                    8.00124E-06, 8.00156E-06, 8.002E-06, 8.002064E-06, 8.002192E-06, 8.002448E-06, 8.00296E-06,
                    8.003984E-06, 8.006032E-06, 8.01012799999999E-06, 8.01831999999998E-06, 8.03470399999996E-06,
                    8.06747199999992E-06, 8.13300799999984E-06, 8.26407999999969E-06, 8.52622399999938E-06,
                    8.92622399999937E-06, 9E-06, 9.0001E-06, 9.0003E-06, 9.0007E-06, 9.001E-06, 9.00108E-06,
                    9.00124E-06, 9.00156E-06, 9.002E-06, 9.002064E-06, 9.002192E-06, 9.002448E-06, 9.00296E-06,
                    9.003984E-06, 9.006032E-06, 9.01012799999999E-06, 9.01831999999998E-06, 9.03470399999996E-06,
                    9.06747199999992E-06, 9.13300799999984E-06, 9.26407999999969E-06, 9.52622399999938E-06,
                    9.92622399999938E-06, 1E-05, 1.00001E-05, 1.00003E-05, 1.00007E-05, 1.0001E-05, 1.000108E-05,
                    1.000124E-05, 1.000156E-05, 1.0002E-05, 1.0002064E-05, 1.0002192E-05, 1.0002448E-05, 1.000296E-05,
                    1.0003984E-05, 1.0006032E-05, 1.0010128E-05, 1.001832E-05, 1.0034704E-05, 1.00674719999999E-05,
                    1.01330079999998E-05, 1.02640799999997E-05, 1.05262239999994E-05, 1.09262239999994E-05, 1.1E-05,
                    1.10001E-05, 1.10003E-05, 1.10007E-05, 1.1001E-05, 1.100108E-05, 1.100124E-05, 1.100156E-05,
                    1.1002E-05, 1.1002064E-05, 1.1002192E-05, 1.1002448E-05, 1.100296E-05, 1.1003984E-05, 1.1006032E-05,
                    1.1010128E-05, 1.101832E-05, 1.1034704E-05, 1.10674719999999E-05, 1.11330079999998E-05,
                    1.12640799999997E-05, 1.15262239999994E-05, 1.19262239999994E-05, 1.2E-05, 1.20000000000001E-05,
                    1.20000000000002E-05, 1.20000000000006E-05, 1.20000000000012E-05, 1.20000000000025E-05,
                    1.2000000000005E-05, 1.20000000000102E-05, 1.20000000000204E-05, 1.20000000000409E-05,
                    1.20000000000818E-05, 1.20000000001638E-05, 1.20000000003276E-05, 1.20000000006553E-05,
                    1.20000000013106E-05, 1.20000000026214E-05, 1.20000000052428E-05, 1.20000000104857E-05,
                    1.20000000209714E-05, 1.2000000041943E-05, 1.2000000083886E-05, 1.20000001677721E-05,
                    1.20000003355442E-05, 1.20000006710886E-05, 1.20000013421772E-05, 1.20000026843545E-05,
                    1.2000005368709E-05, 1.20000107374182E-05, 1.20000214748364E-05, 1.20000429496729E-05,
                    1.20000858993458E-05, 1.20001717986918E-05, 1.20003435973836E-05, 1.20006871947673E-05, 1.2001E-05,
                    1.20010687194767E-05, 1.20012061584302E-05, 1.20014810363372E-05, 1.2002E-05, 1.20020549755814E-05,
                    1.20021649267442E-05, 1.20023848290697E-05, 1.20028246337208E-05, 1.20037042430231E-05,
                    1.20054634616275E-05, 1.20089818988364E-05, 1.20160187732541E-05, 1.20300925220897E-05,
                    1.20582400197607E-05, 1.21145350151029E-05, 1.22271250057871E-05, 1.24523049871557E-05,
                    1.28523049871557E-05, 1.3E-05, 1.30001E-05, 1.30003E-05, 1.30007E-05, 1.3001E-05, 1.300108E-05,
                    1.300124E-05, 1.300156E-05, 1.3002E-05, 1.3002064E-05, 1.3002192E-05, 1.3002448E-05, 1.300296E-05,
                    1.3003984E-05, 1.3006032E-05, 1.3010128E-05, 1.301832E-05, 1.3034704E-05, 1.3067472E-05,
                    1.31330080000001E-05, 1.32640800000001E-05, 1.35262240000003E-05, 1.39262240000003E-05, 1.4E-05,
                    1.40001E-05, 1.40003E-05, 1.40007E-05, 1.4001E-05, 1.400108E-05, 1.400124E-05, 1.400156E-05,
                    1.4002E-05, 1.4002064E-05, 1.4002192E-05, 1.4002448E-05, 1.400296E-05, 1.4003984E-05, 1.4006032E-05,
                    1.4010128E-05, 1.401832E-05, 1.4034704E-05, 1.4067472E-05, 1.41330080000001E-05,
                    1.42640800000001E-05, 1.45262240000003E-05, 1.49262240000003E-05, 1.5E-05, 1.50001E-05, 1.50003E-05,
                    1.50007E-05, 1.5001E-05, 1.500108E-05, 1.500124E-05, 1.500156E-05, 1.5002E-05, 1.5002064E-05,
                    1.5002192E-05, 1.5002448E-05, 1.500296E-05, 1.5003984E-05, 1.5006032E-05, 1.5010128E-05,
                    1.501832E-05, 1.5034704E-05, 1.5067472E-05, 1.51330080000001E-05, 1.52640800000001E-05,
                    1.55262240000003E-05, 1.59262240000003E-05, 1.6E-05, 1.60001E-05, 1.60003E-05, 1.60007E-05,
                    1.6001E-05, 1.600108E-05, 1.600124E-05, 1.600156E-05, 1.6002E-05, 1.6002064E-05, 1.6002192E-05,
                    1.6002448E-05, 1.600296E-05, 1.6003984E-05, 1.6006032E-05, 1.6010128E-05, 1.601832E-05,
                    1.6034704E-05, 1.60674719999999E-05, 1.61330079999998E-05, 1.62640799999997E-05,
                    1.65262239999994E-05, 1.69262239999994E-05, 1.7E-05, 1.70001E-05, 1.70003E-05, 1.70007E-05,
                    1.7001E-05, 1.700108E-05, 1.700124E-05, 1.700156E-05, 1.7002E-05, 1.70020000000001E-05,
                    1.70020000000002E-05, 1.70020000000006E-05, 1.70020000000012E-05, 1.70020000000025E-05,
                    1.7002000000005E-05, 1.70020000000102E-05, 1.70020000000204E-05, 1.70020000000409E-05,
                    1.70020000000818E-05, 1.70020000001638E-05, 1.70020000003276E-05, 1.70020000006553E-05,
                    1.70020000013106E-05, 1.70020000026214E-05, 1.70020000052428E-05, 1.70020000104857E-05,
                    1.70020000209714E-05, 1.7002000041943E-05, 1.7002000083886E-05, 1.70020001677721E-05,
                    1.70020003355442E-05, 1.70020006710886E-05, 1.70020013421772E-05, 1.70020026843545E-05,
                    1.7002005368709E-05, 1.70020107374182E-05, 1.70020214748364E-05, 1.70020429496729E-05,
                    1.70020858993458E-05, 1.70021717986918E-05, 1.70023435973836E-05, 1.70026871947673E-05,
                    1.70033743895346E-05, 1.70047487790694E-05, 1.70074975581388E-05, 1.70129951162777E-05,
                    1.70239902325554E-05, 1.7045980465111E-05, 1.7089960930222E-05, 1.71779218604441E-05,
                    1.73538437208882E-05, 1.77056874417766E-05, 1.8E-05, 1.80001E-05, 1.80003E-05, 1.80007E-05,
                    1.8001E-05, 1.800108E-05, 1.800124E-05, 1.800156E-05, 1.8002E-05, 1.8002064E-05, 1.8002192E-05,
                    1.8002448E-05, 1.800296E-05, 1.8003984E-05, 1.8006032E-05, 1.8010128E-05, 1.801832E-05,
                    1.8034704E-05, 1.80674719999999E-05, 1.81330079999998E-05, 1.82640799999997E-05,
                    1.85262239999994E-05, 1.89262239999994E-05, 1.9E-05, 1.90001E-05, 1.90003E-05, 1.90007E-05,
                    1.9001E-05, 1.900108E-05, 1.900124E-05, 1.900156E-05, 1.9002E-05, 1.9002064E-05, 1.9002192E-05,
                    1.9002448E-05, 1.900296E-05, 1.9003984E-05, 1.9006032E-05, 1.9010128E-05, 1.901832E-05,
                    1.9034704E-05, 1.90674719999999E-05, 1.91330079999998E-05, 1.92640799999997E-05,
                    1.95262239999994E-05, 1.99262239999994E-05, 2E-05
                },
                new[]
                {
                    0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.333333333333061,
                    0.599999999999748, 1.13333333333312, 1.53333333333333, 1.53333333333333, 1.53333333333333,
                    1.53333333333333, 1.53333333333333, 1.53333333333333, 1.53333333333333, 1.53333333333333,
                    1.53333333333333, 1.53333333333333, 1.53333333333333, 1.53333333333333, 1.53333333333333,
                    1.53333333333333, 1.53333333333333, 1.53333333333333, 1.53333333333333, 1.53333333333333,
                    1.53333333333333, 1.53333333333333, 1.53333333333333, 1.53333333333333, 1.53333333333333,
                    1.53333333333333, 1.53333333333333, 1.53333333333333, 1.53333333333333, 1.53333333333333,
                    1.53333333333333, 1.53333333333333, 1.53333333333333, 1.53333333333333, 1.53333333333308,
                    1.47407407407394, 1.35555555555542, 1.11851851851836, 0.940740740740741, 0.940740740740741,
                    0.940740740740741, 0.940740740740741, 0.940740740740741, 0.940740740740741, 0.940740740740741,
                    0.940740740740741, 0.940740740740741, 0.940740740740741, 0.940740740740741, 0.940740740740741,
                    0.940740740740741, 0.940740740740741, 0.940740740740741, 0.940740740740741, 0.940740740740741,
                    0.940740740740741, 0.940740740740741, 0.940740740740741, 0.940740740740741, 0.940740740740741,
                    0.940740740740741, 0.940740740740741, 0.940740740740741, 0.940740740740741, 0.940740740740741,
                    0.940740740740741, 0.940740740740741, 0.940740740740741, 0.940740740740741, 0.940740740740741,
                    0.940740740740769, 0.947325102880673, 0.960493827160509, 0.986831275720182, 1.00658436213992,
                    1.00658436213992, 1.00658436213992, 1.00658436213992, 1.00658436213992, 1.00658436213992,
                    1.00658436213992, 1.00658436213992, 1.00658436213992, 1.00658436213992, 1.00658436213992,
                    1.00658436213992, 1.00658436213992, 1.00658436213992, 1.00658436213992, 1.00658436213992,
                    1.00658436213992, 1.00658436213992, 1.00658436213992, 1.00658436203601, 1.00658436182143,
                    1.00658436139453, 1.00658436054072, 1.00658435883536, 1.00658435542238, 1.00658434859417,
                    1.00658433494225, 1.00658430763391, 1.00658425302175, 1.00658414379515, 1.00658392534197,
                    1.0065834884356, 1.0065826146206, 1.00658086699512, 1.00657737174189, 1.00657038123545,
                    1.00655640022256, 1.00652843819452, 1.0064725141407, 1.00636066603533, 1.00613696982232,
                    1.00568957739404, 1.00479479254199, 1.00300522283565, 0.999426083420701, 0.992267804595323,
                    0.977951246942309, 0.949318131634021, 0.892051901021963, 0.777519439795588, 0.54845451734058,
                    0.09032467243508, -0.326748971193416, -0.326748971193416, -0.326748971193416, -0.326748971193415,
                    -0.326748971193416, -0.326748971193416, -0.326748971193416, -0.326748971193416, -0.326748971193416,
                    -0.326748971193416, -0.326748971193416, -0.326748971193416, -0.326748971193416, -0.326748971193416,
                    -0.326748971193418, -0.326748971193425, -0.327480566986747, -0.328943758573402, -0.331870141746693,
                    -0.334064929126658, -0.334064929126658, -0.334064929126658, -0.334064929126658, -0.334064929126658,
                    -0.334064929126658, -0.334064929126658, -0.334064929126657, -0.334064929126658, -0.334064929126658,
                    -0.334064929126657, -0.334064929126658, -0.334064929126658, -0.334064929126658, -0.334064929126658,
                    -0.334064929126658, -0.334064929126657, -0.334064929126658, -0.334064929126658, -0.334064929126658,
                    -0.334064929126658, -0.334064929126657, -0.334064929126658, -0.334064929126658, -0.28665752171988,
                    -0.19184270690541, -0.00221307727549839, 0.258527663465935, 0.258527663465935, 0.258527663465935,
                    0.258527663465935, 0.258527663465935, 0.258527663465935, 0.258527663465935, 0.258527663465935,
                    0.258527663465935, 0.258527663465935, 0.258527663465935, 0.258527663465935, 0.258527663465935,
                    0.258527663465935, 0.258527663465935, 0.258527663465936, 0.258608951887416, 0.258771528730378,
                    0.259096682416299, 0.25934054768074, 0.25934054768074, 0.25934054768074, 0.25934054768074,
                    0.25934054768074, 0.25934054768074, 0.25934054768074, 0.25934054768074, 0.25934054768074,
                    0.25934054768074, 0.25934054768074, 0.25934054768074, 0.25934054768074, 0.25934054768074,
                    0.25934054768074, 0.25934054768074, 0.25934054768074, 0.25934054768074, 0.25934054768074,
                    0.25934054768074, 0.25934054768074, 0.25934054768074, 0.25934054768074, 0.25934054768074,
                    0.254073057968876, 0.243538078545046, 0.222468119697278, 0.193496926281563, 0.193496926281563,
                    0.193496926281563, 0.193496926281563, 0.193496926281563, 0.193496926281563, 0.193496926281563,
                    0.193496926281563, 0.193496926281563, 0.193496926281563, 0.193496926281563, 0.193496926281563,
                    0.193496926281563, 0.193496926281563, 0.193496926281563, 0.193496926281563, 0.193496926387717,
                    0.193496926600026, 0.193496927026901, 0.193496927880653, 0.193496929588156, 0.193496933000903,
                    0.193496939826397, 0.193496953479645, 0.193496980783881, 0.193497035394612, 0.193497144613816,
                    0.193497363052223, 0.193497799929037, 0.193498673682666, 0.193500421192182, 0.193503916208955,
                    0.193510906242501, 0.193524886309594, 0.193552846443778, 0.193608766714402, 0.193720607255637,
                    0.193944288335796, 0.194391650495908, 0.195286374817564, 0.197075823455315, 0.200654720717602,
                    0.207812515191575, 0.222128103925831, 0.250759280548608, 0.308021628932632, 0.422546331689856,
                    0.651595737204304, 1.10969454823546, 1.52673993914658, 1.52673993914658, 1.52673993914658,
                    1.52673993914658, 1.52673993914658, 1.52673993914658, 1.52673993914658, 1.52673993914658,
                    1.52673993914658, 1.52673993914658, 1.52673993914658, 1.52673993914658, 1.52673993914658,
                    1.52673993914658, 1.52673993914658, 1.52673993914658, 1.52673993914658, 1.52673993914658,
                    1.52673993914658, 1.52673993914658, 1.52673993914658, 1.52673993914658, 1.52673993914658,
                    1.52673993914658, 1.52732521578124, 1.52849576905055, 1.53083687558919, 1.53405589707983,
                    1.53405589707983, 1.53405589707983, 1.53405589707983, 1.53405589707983, 1.53405589707983,
                    1.53405589707983, 1.53405589707983, 1.53405589707983, 1.53405589707983, 1.53405589707983,
                    1.53405589707983, 1.53405589707983, 1.53405589707983, 1.53405589707983, 1.53405589707983,
                    1.4747976413812, 1.35628112998394, 1.11924810718942, 0.941473340094824, 0.941473340094824,
                    0.941473340094824, 0.941473340094824, 0.941473340094824, 0.941473340094824, 0.941473340094824,
                    0.941473340094824, 0.941473340094824, 0.941473340094824, 0.941473340094824, 0.941473340094824,
                    0.941473340094824, 0.941473340094824, 0.941473340094824, 0.941473340094824, 0.941473340094824,
                    0.941473340094824, 0.941473340094824, 0.941473340094824, 0.941473340094824, 0.941473340094824,
                    0.941473340094824, 0.941473340094824, 0.94140830935764, 0.941278247883272, 0.941018124934534,
                    0.940660455880019, 0.940660455880019, 0.940660455880019, 0.940660455880019, 0.940660455880019,
                    0.940660455880019, 0.940660455880019, 0.940660455880019, 0.940660455880019, 0.940660455880019,
                    0.940660455880019, 0.940660455880019, 0.940660455880019, 0.940660455880019, 0.940660455880019,
                    0.940660455880019, 0.9472447065132, 0.960413207779563, 0.986750210312287, 1.00650296221157,
                    1.00650296221169, 1.00650296221169, 1.00650296221169, 1.00650296221169, 1.00650296221169,
                    1.00650296221169, 1.00650296221169, 1.00650296221169, 1.00650296221169, 1.00650296221169,
                    1.00650296221169, 1.00650296221169, 1.00650296221169, 1.00650296221169, 1.00650296221169,
                    1.00650296221169, 1.00650296221169, 1.00650296221169, 1.00650296221169, 1.00650296221169,
                    1.00650296221169, 1.00650296221169, 1.00650296220943, 0.899843521181798, 0.686524639122024,
                    0.259886875006992, -0.326740050653336, -0.326740050653336, -0.326740050653333, -0.326740050653329,
                    -0.326740050653322, -0.326740050653307, -0.326740050653278, -0.326740050653219, -0.326740050653102,
                    -0.326740050652867, -0.326740050652397, -0.326740050651458, -0.326740050649578, -0.32674005064582,
                    -0.326740050638304, -0.326740050623272, -0.32674005059321, -0.326740050533092, -0.326740050412887,
                    -0.326740050172596, -0.326740049692486, -0.326740048734157, -0.326740046825067, -0.32674004303715,
                    -0.32674003558238, -0.326740021157085, -0.326739994243483, -0.326739948164225, -0.326739886997498,
                    -0.326740050653336, -0.326740050653335, -0.326740050653335, -0.326740050653335, -0.326740050653335,
                    -0.326740050653335, -0.326740050653335, -0.326740050653335, -0.326740050653335, -0.326740050653335,
                    -0.326740050653335, -0.326740050653335, -0.326740050653335, -0.326740050653335, -0.326740050653335,
                    -0.326740050653335, -0.327471634057022, -0.328934800864396, -0.331861134479143, -0.334055884690175,
                    -0.334055884690188, -0.334055884690188, -0.334055884690188, -0.334055884690188, -0.334055884690188,
                    -0.334055884690188, -0.334055884690187, -0.334055884690187, -0.334055884690188, -0.334055884690188,
                    -0.334055884690187, -0.334055884690187, -0.334055884690187, -0.334055884690187, -0.334055884690188,
                    -0.334055884690187, -0.334055884690187, -0.334055884690187, -0.334055884690188, -0.334055884690188,
                    -0.334055884690188, -0.334055884690188, -0.334055884689184, -0.286649280131082, -0.191836071012872,
                    -0.0022096527784606, 0.258526672294815, 0.258526672294815, 0.258526672294815, 0.258526672294815,
                    0.258526672294815, 0.258526672294815, 0.258526672294815, 0.258526672294815, 0.258526672294815,
                    0.258526672294815, 0.258526672294815, 0.258526672294815, 0.258526672294815, 0.258526672294815,
                    0.258526672294815, 0.258526672294815
                },
                new[]
                {
                    0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2,
                    0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.200000000000377, 0.288888888888707,
                    0.466666666666875, 0.822222222222457, 1.08888888888889, 1.08888888888889, 1.08888888888889,
                    1.08888888888889, 1.08888888888889, 1.08888888888889, 1.08888888888889, 1.08888888888889,
                    1.08888888888889, 1.08888888888889, 1.08888888888889, 1.08888888888889, 1.08888888888889,
                    1.08888888888889, 1.08888888888889, 1.08888888888889, 1.08888888888889, 1.08888888888889,
                    1.08888888888889, 1.08888888888889, 1.08888888888889, 1.08888888888889, 1.08888888888889,
                    1.08888888888889, 1.08888888888889, 1.08888888888889, 1.08888888888889, 1.08888888888889,
                    1.08888888888889, 1.08888888888889, 1.08888888888889, 1.08888888888889, 1.08888888888885,
                    1.07901234567899, 1.05925925925924, 1.01975308641973, 0.990123456790124, 0.990123456790124,
                    0.990123456790124, 0.990123456790124, 0.990123456790124, 0.990123456790124, 0.990123456790124,
                    0.990123456790124, 0.990123456790124, 0.990123456790124, 0.990123456790124, 0.990123456790124,
                    0.990123456790124, 0.990123456790124, 0.990123456790124, 0.990123456790124, 0.990123456790124,
                    0.990123456790124, 0.990123456790124, 0.990123456790124, 0.990123456790124, 0.990123456790124,
                    0.990123456790124, 0.990123456790124, 0.990123456790124, 0.990123456790124, 0.990123456790124,
                    0.990123456790124, 0.990123456790124, 0.990123456790124, 0.990123456790124, 0.990123456790124,
                    0.990123456790128, 0.991220850480112, 0.993415637860085, 0.997805212620031, 1.00109739368999,
                    1.00109739368999, 1.00109739369054, 1.00109739369128, 1.00109739369276, 1.00109739369572,
                    1.00109739370163, 1.00109739371346, 1.00109739373712, 1.00109739378443, 1.00109739387906,
                    1.00109739406832, 1.00109739444684, 1.00109739520387, 1.00109739671791, 1.00109739974592,
                    1.00109740580165, 1.0010974179119, 1.00109744212766, 1.00109749054011, 1.00109758728881,
                    1.00109778048138, 1.00109816564722, 1.00109893110172, 1.00110044250195, 1.00110338726733,
                    1.00110896465776, 1.00111887087731, 1.00113368907122, 1.00109739368999, 1.00109739368999,
                    1.00109739368999, 1.00109739368999, 1.00109739368999, 1.00109739368999, 1.00109739368999,
                    1.00109739368999, 1.00109739368999, 1.00109739368999, 1.00109739368999, 1.00109739368999,
                    1.00109739368999, 1.00109739368999, 1.00109739368999, 1.00109739368999, 1.00109739368999,
                    1.00109739368999, 1.00109739368999, 1.00109739368999, 1.00109739368999, 1.00109739368999,
                    1.00109739368999, 1.00109739368999, 1.00109739368999, 0.929986282579821, 0.787764060358115,
                    0.503319615913248, 0.112208504801097, 0.112208504801097, 0.112208504801097, 0.112208504801097,
                    0.112208504801097, 0.112208504801097, 0.112208504801097, 0.112208504801097, 0.112208504801097,
                    0.112208504801097, 0.112208504801097, 0.112208504801097, 0.112208504801097, 0.112208504801097,
                    0.112208504801097, 0.112208504801096, 0.112086572168875, 0.111842706904433, 0.111354976375551,
                    0.11098917847889, 0.11098917847889, 0.11098917847889, 0.11098917847889, 0.11098917847889,
                    0.11098917847889, 0.11098917847889, 0.110989178478891, 0.11098917847889, 0.11098917847889,
                    0.11098917847889, 0.11098917847889, 0.11098917847889, 0.11098917847889, 0.11098917847889,
                    0.11098917847889, 0.110989178478891, 0.11098917847889, 0.11098917847889, 0.11098917847889,
                    0.11098917847889, 0.110989178478891, 0.11098917847889, 0.11098917847889, 0.118890413046687,
                    0.134692882182432, 0.166297820454084, 0.209754610577656, 0.209754610577656, 0.209754610577656,
                    0.209754610577656, 0.209754610577656, 0.209754610577656, 0.209754610577656, 0.209754610577656,
                    0.209754610577656, 0.209754610577656, 0.209754610577656, 0.209754610577656, 0.209754610577656,
                    0.209754610577656, 0.209754610577656, 0.209754610577656, 0.209768158647903, 0.209795254788396,
                    0.209849447069383, 0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123,
                    0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123,
                    0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123,
                    0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123,
                    0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123,
                    0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123,
                    0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123,
                    0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123,
                    0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123,
                    0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123,
                    0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123, 0.209890091280123,
                    0.209135968078651, 0.207627721675661, 0.204611228869694, 0.198916154380261, 0.198916154380261,
                    0.198916154380261, 0.198916154380261, 0.198916154380261, 0.198916154380261, 0.198916154380261,
                    0.198916154380261, 0.198916154380261, 0.198916154380261, 0.198916154380261, 0.198916154380261,
                    0.198916154380261, 0.198916154380261, 0.198916154380261, 0.198916154380261, 0.287803537928206,
                    0.465578305024096, 0.821127839215868, 1.08778998985776, 1.08778998985776, 1.08778998985776,
                    1.08778998985776, 1.08778998985776, 1.08778998985776, 1.08778998985776, 1.08778998985776,
                    1.08778998985776, 1.08778998985776, 1.08778998985776, 1.08778998985776, 1.08778998985776,
                    1.08778998985776, 1.08778998985776, 1.08778998985776, 1.08778998985776, 1.08778998985776,
                    1.08778998985776, 1.08778998985776, 1.08778998985776, 1.08778998985776, 1.08778998985776,
                    1.08778998985776, 1.08788753596354, 1.08808262817509, 1.0884728125982, 1.08900931617997,
                    1.08900931617997, 1.08900931617997, 1.08900931617997, 1.08900931617997, 1.08900931617997,
                    1.08900931617997, 1.08900931617997, 1.08900931617997, 1.08900931617997, 1.08900931617997,
                    1.08900931617997, 1.08900931617997, 1.08900931617997, 1.08900931617997, 1.08900931617997,
                    1.0791329402302, 1.05938018833066, 1.01987468453157, 0.990245556682471, 0.990245556682471,
                    0.990245556682471, 0.990245556682471, 0.990245556682471, 0.990245556682471, 0.990245556682471,
                    0.990245556682471, 0.990245556682471, 0.990245556682471, 0.990245556682471, 0.990245556682471,
                    0.990245556682471, 0.990245556682471, 0.990245556682471, 0.990245556682471, 0.990245556682471,
                    0.990245556682471, 0.990245556682471, 0.990245556682471, 0.990245556682471, 0.990245556682471,
                    0.990245556682471, 0.990245556682471, 0.990234718226274, 0.990213041313879, 0.99016968748909,
                    0.990110075980004, 0.990110075980003, 0.990110075980003, 0.990110075980003, 0.990110075980003,
                    0.990110075980003, 0.990110075980003, 0.990110075980003, 0.990110075980003, 0.990110075980003,
                    0.990110075980003, 0.990110075980003, 0.990110075980003, 0.990110075980003, 0.990110075980003,
                    0.990110075980003, 0.991207451085534, 0.993402201296594, 0.997791701718715, 1.00108382703526,
                    1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528,
                    1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528,
                    1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528,
                    1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528,
                    1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528,
                    1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528,
                    1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528,
                    1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528,
                    1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528,
                    1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528, 1.00108382703528,
                    1.00108382703528, 1.00108382703378, 0.929973920196624, 0.787754106519309, 0.503314479167691,
                    0.112209991557777, 0.112209991557777, 0.112209991557777, 0.112209991557778, 0.112209991557777,
                    0.112209991557777, 0.112209991557777, 0.112209991557778, 0.112209991557778, 0.112209991557778,
                    0.112209991557778, 0.112209991557778, 0.112209991557777, 0.112209991557777, 0.112209991557778,
                    0.112209991557778, 0.112088060990496, 0.111844199855934, 0.11135647758681, 0.110990685884971,
                    0.110990685884969, 0.110990685884969, 0.110990685884969, 0.110990685884969, 0.110990685884969,
                    0.110990685884969, 0.110990685884969, 0.110990685884969, 0.110990685884969, 0.110990685884969,
                    0.110990685884969, 0.110990685884969, 0.110990685884969, 0.110990685884969, 0.110990685884969,
                    0.110990685884969, 0.110990685884969, 0.110990685884969, 0.110990685884969
                }
            };

            // Analyze
            AnalyzeTransient(tran, ckt, exports, references);
        }

        [Test]
        public void When_LosslessTransmissionLineFrequency_Expect_Reference()
        {
            // Parameters
            double rsource = 100.0, rload = 25.0;
            double impedance = 50.0, delay = 1.0e-6;
            
            // Build the circuit
            var ckt = new Circuit(
                new VoltageSource("V1", "in", "0", 0.0),
                new Resistor("Rsource", "in", "a", rsource),
                new LosslessTransmissionLine("T1", "a", "0", "b", "0", impedance, delay),
                new Resistor("Rload", "b", "0", rload));
            ckt.Entities["V1"].SetParameter("acmag", 1.0);

            // Build the analysis
            var ac = new AC("ac", new DecadeSweep(0.1, 1e8, 5));
            var exports = new Export<Complex>[]
            {
                new ComplexVoltageExport(ac, "a"),
                new ComplexVoltageExport(ac, "b"), 
            };

            double rsnorm = rsource / impedance, rlnorm = rload / impedance;
            var references = new Func<double, Complex>[]
            {
                frequency =>
                {
                    var k = Complex.Exp(-ac.ComplexState.Laplace * delay);
                    k = (k * k - 1) / (k * k + 1);
                    return (k - rlnorm) / ((1 + rlnorm * rsnorm) * k - rsnorm - rlnorm);
                },
                frequency =>
                {
                    var k = Complex.Exp(-ac.ComplexState.Laplace * delay);
                    return -2 * rlnorm * k / (k * k + 1) /
                           ((1 + rlnorm * rsnorm) * (k * k - 1) / (k * k + 1) - rsnorm - rlnorm);
                }
            };

            // Analyze
            AnalyzeAC(ac, ckt, exports, references);
        }
    }
}
