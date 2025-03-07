// Copyright (c) Ben A Adams. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Ben.Demystifier;

namespace System.Diagnostics

{
#if NET6_0_OR_GREATER
    [RequiresUnreferencedCode(Constants.TrimWarning)]
#endif
    public class ResolvedParameter
    {
        public string? Name { get; set; }

        public Type ResolvedType { get; set; }

        public string? Prefix { get; set; }
        public bool IsDynamicType { get; set; }

        public ResolvedParameter(Type resolvedType) => ResolvedType = resolvedType;

        public override string ToString() => Append(new StringBuilder()).ToString();

#if NET6_0_OR_GREATER
        [UnconditionalSuppressMessage("SingleFile", "IL3002: calling members marked with 'RequiresAssemblyFilesAttribute'", Justification = Constants.SingleFileFallback)]
#endif
        public StringBuilder Append(StringBuilder sb)
        {
            if (ResolvedType.Assembly.ManifestModule.Name == "FSharp.Core.dll" && ResolvedType.Name == "Unit")
                return sb;

            if (!string.IsNullOrEmpty(Prefix))
            {
                sb.Append(Prefix)
                  .Append(" ");
            }

            if (IsDynamicType)
            {
                sb.Append("dynamic");
            }
            else if (ResolvedType != null)
            {
                AppendTypeName(sb);
            }
            else
            {
                sb.Append("?");
            }

            if (!string.IsNullOrEmpty(Name))
            {
                sb.Append(" ")
                  .Append(Name);
            }

            return sb;
        }

        protected virtual void AppendTypeName(StringBuilder sb)
        {
            sb.AppendTypeDisplayName(ResolvedType, fullName: false, includeGenericParameterNames: true);
        }
    }
}
