﻿namespace Merchello.Core.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// A case-insensitive configuration converter for enumerations.
    /// </summary>
    /// <typeparam name="T">The type of the enumeration.</typeparam>
    /// UMBRACO_SRC
    internal class CaseInsensitiveEnumConfigConverter<T> : ConfigurationConverterBase
        where T : struct
    {
        /// <summary>
        /// The convert from.
        /// </summary>
        /// <param name="ctx">
        /// The <see cref="ITypeDescriptorContext"/>.
        /// </param>
        /// <param name="ci">
        /// The <see cref="CultureInfo"/>.
        /// </param>
        /// <param name="data">
        /// The data to convert to an enum value.
        /// </param>
        /// <returns>
        /// The converted enum value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Throws an <see cref="ArgumentNullException"/> if the data was null.
        /// </exception>
        /// <exception cref="Exception">
        /// Throws and <see cref="Exception"/> if the value fails to convert to the enum type.
        /// </exception>
        public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            //// return Enum.Parse(typeof(T), (string)data, true);

            T value;
            if (Enum.TryParse((string)data, true, out value))
                return value;

            throw new Exception(
                string.Format("\"{0}\" is not valid {1} value. Valid values are: {2}.", data, typeof(T).Name, string.Join(", ", Enum.GetValues(typeof(T)).Cast<T>())));
        }
    }
}
