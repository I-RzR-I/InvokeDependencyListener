#region U S A G E S

using System.Reflection;

#if NETSTANDARD2_0_OR_GREATER
using System.Resources;
#endif

#endregion

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyCompany("RzR SOFT & TECH ®")]
[assembly: AssemblyProduct("RzR.Infrastructure.DependencyResolver")]
[assembly: AssemblyCopyright("Copyright © 2022-2026 RzR All rights reserved.")]
[assembly: AssemblyTrademark("RzR SOFT Solution™")]
[assembly: AssemblyDescription("A straightforward, lightweight, minimalistic implementation of Dependency Injection (DI) that avoids redundant class instance initialization and promotes a cleaner, more maintainable code.")]

#if NET45_OR_GREATER || NET || NETSTANDARD1_0_OR_GREATER
[assembly: AssemblyMetadata("TermsOfService", "")]
[assembly: AssemblyMetadata("ContactUrl", "")]
[assembly: AssemblyMetadata("ContactName", "RzR")]
[assembly: AssemblyMetadata("ContactEmail", "ddpRzR@hotmail.com")]
#endif

#if NETSTANDARD2_0_OR_GREATER
[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.MainAssembly)]
#endif

[assembly: AssemblyVersion("2.0.0.0")]
[assembly: AssemblyFileVersion("2.0.0.0")]
[assembly: AssemblyInformationalVersion("2.0.0.0")]
