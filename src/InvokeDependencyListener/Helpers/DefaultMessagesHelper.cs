// ***********************************************************************
//  Assembly         : RzR.Infrastructure.DependencyResolver
//  Author           : RzR
//  Created On       : 2025-08-07 14:52
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-09-01 23:08
// ***********************************************************************
//  <copyright file="DefaultMessagesHelper.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using RzR.Extensions.Domain.Text;

#endregion

namespace RzR.Infrastructure.DependencyResolver.Helpers
{
    /// <summary>
    ///     A default messages helper.
    /// </summary>
    internal static class DefaultMessagesHelper
    {
        /// <summary>
        ///     (Immutable) the format event log.
        /// </summary>
        private const string FormatEventLog = "['{0}'] - {1}";

        /// <summary>
        ///     (Immutable) the circular dependency.
        /// </summary>
        internal const string CircularDependency = "Circular dependency detected: {0}.";

        /// <summary>
        ///     (Immutable) the no public constructor found.
        /// </summary>
        internal const string NoPublicCtorFound = "No public constructors found for {0}.";

        /// <summary>
        ///     (Immutable) the unable service resolve.
        /// </summary>
        internal const string UnableServiceResolve = "Unable to resolve service for type {0} when attempting to activate {1}.";

        /// <summary>
        ///     (Immutable) the required service not registered.
        /// </summary>
        internal const string RequiredServiceNotRegistered = "Service of type {0} is not registered.";

        /// <summary>
        ///     (Immutable) the not accepted service life time.
        /// </summary>
        internal const string NotAcceptedServiceLifeTime = "Service of type {0} is not registered.";

        /// <summary>
        ///     Format to default.
        /// </summary>
        /// <param name="source">Source for the.</param>
        /// <param name="message">The message.</param>
        /// <returns>
        ///     The formatted to default.
        /// </returns>
        internal static string FormatToDefault(string source, string message)
            => FormatEventLog.FormatWith(source, message);

        /// <summary>
        ///     Format to default.
        /// </summary>
        /// <param name="source">Source for the.</param>
        /// <param name="message">The message.</param>
        /// <param name="paramList">A variable-length parameters list containing parameter list.</param>
        /// <returns>
        ///     The formatted to default.
        /// </returns>
        internal static string FormatToDefault(string source, string message, params object[] paramList)
            => FormatEventLog.FormatWith(source, message.FormatWith(paramList));
    }
}