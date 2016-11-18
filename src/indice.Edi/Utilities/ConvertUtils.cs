#region License
// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
#if !PORTABLE
using System.Numerics;
#endif
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
#if !(DOTNET || PORTABLE)
using System.Data.SqlTypes;

#endif

namespace indice.Edi.Utilities
{
	/// <summary>
	/// Primitive type code.
	/// </summary>
    internal enum PrimitiveTypeCode
    {
		/// <summary>
		/// The empty.
		/// </summary>
        Empty = 0,
        /// <summary>
        /// The object.
        /// </summary>
		Object = 1,
        /// <summary>
        /// The char.
        /// </summary>
		Char = 2,
        /// <summary>
        /// The char nullable.
        /// </summary>
		CharNullable = 3,
        /// <summary>
        /// The boolean.
        /// </summary>
		Boolean = 4,
        /// <summary>
        /// The boolean nullable.
        /// </summary>
		BooleanNullable = 5,
        /// <summary>
        /// The SB yte.
        /// </summary>
		SByte = 6,
        /// <summary>
        /// The SB yte nullable.
        /// </summary>
		SByteNullable = 7,
        /// <summary>
        /// The int16.
        /// </summary>
		Int16 = 8,
        /// <summary>
        /// The int16 nullable.
        /// </summary>
		Int16Nullable = 9,
        /// <summary>
        /// The user interface nt16.
        /// </summary>
		UInt16 = 10,
        /// <summary>
        /// The user interface nt16 nullable.
        /// </summary>
		UInt16Nullable = 11,
        /// <summary>
        /// The int32.
        /// </summary>
		Int32 = 12,
        /// <summary>
        /// The int32 nullable.
        /// </summary>
		Int32Nullable = 13,
        /// <summary>
        /// The byte.
        /// </summary>
		Byte = 14,
        /// <summary>
        /// The byte nullable.
        /// </summary>
		ByteNullable = 15,
        /// <summary>
        /// The user interface nt32.
        /// </summary>
		UInt32 = 16,
        /// <summary>
        /// The user interface nt32 nullable.
        /// </summary>
		UInt32Nullable = 17,
        /// <summary>
        /// The int64.
        /// </summary>
		Int64 = 18,
        /// <summary>
        /// The int64 nullable.
        /// </summary>
		Int64Nullable = 19,
        /// <summary>
        /// The user interface nt64.
        /// </summary>
		UInt64 = 20,
        /// <summary>
        /// The user interface nt64 nullable.
        /// </summary>
		UInt64Nullable = 21,
        /// <summary>
        /// The single.
        /// </summary>
		Single = 22,
        /// <summary>
        /// The single nullable.
        /// </summary>
		SingleNullable = 23,
        /// <summary>
        /// The double.
        /// </summary>
		Double = 24,
        /// <summary>
        /// The double nullable.
        /// </summary>
		DoubleNullable = 25,
        /// <summary>
        /// The date time.
        /// </summary>
		DateTime = 26,
        /// <summary>
        /// The date time nullable.
        /// </summary>
		DateTimeNullable = 27,
        /// <summary>
        /// The date time offset.
        /// </summary>
		DateTimeOffset = 28,
        /// <summary>
        /// The date time offset nullable.
        /// </summary>
		DateTimeOffsetNullable = 29,
        /// <summary>
        /// The decimal.
        /// </summary>
		Decimal = 30,
        /// <summary>
        /// The decimal nullable.
        /// </summary>
		DecimalNullable = 31,
        /// <summary>
        /// The GUID.
        /// </summary>
		Guid = 32,
        /// <summary>
        /// The GUID nullable.
        /// </summary>
		GuidNullable = 33,
        /// <summary>
        /// The time span.
        /// </summary>
		TimeSpan = 34,
        /// <summary>
        /// The time span nullable.
        /// </summary>
		TimeSpanNullable = 35,
        /// <summary>
        /// The big integer.
        /// </summary>
		BigInteger = 36,
        /// <summary>
        /// The big integer nullable.
        /// </summary>
		BigIntegerNullable = 37,
        /// <summary>
        /// The URI.
        /// </summary>
		Uri = 38,
        /// <summary>
        /// The string.
        /// </summary>
		String = 39,
        /// <summary>
        /// The bytes.
        /// </summary>
		Bytes = 40,
        /// <summary>
        /// The DBN ull.
        /// </summary>
		DBNull = 41
    }

	/// <summary>
	/// Type information.
	/// </summary>
    internal class TypeInformation
    {
		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>The type.</value>
        public Type Type { get; set; }
        /// <summary>
        /// Gets or sets the type code.
        /// </summary>
        /// <value>The type code.</value>
		public PrimitiveTypeCode TypeCode { get; set; }
    }

	/// <summary>
	/// Parse result.
	/// </summary>
    internal enum ParseResult
    {
		/// <summary>
		/// nothing
		/// </summary>
        None = 0,
		/// <summary>
		/// The success.
		/// </summary>
        Success = 1,
        /// <summary>
        /// The overflow.
        /// </summary>
		Overflow = 2,
        /// <summary>
        /// The invalid.
        /// </summary>
		Invalid = 3
    }

	/// <summary>
	/// Convert utils.
	/// </summary>
    internal static class ConvertUtils
    {
		/// <summary>
		/// The type code map.
		/// </summary>
        private static readonly Dictionary<Type, PrimitiveTypeCode> TypeCodeMap =
            new Dictionary<Type, PrimitiveTypeCode>
            {
                { typeof(char), PrimitiveTypeCode.Char },
                { typeof(char?), PrimitiveTypeCode.CharNullable },
                { typeof(bool), PrimitiveTypeCode.Boolean },
                { typeof(bool?), PrimitiveTypeCode.BooleanNullable },
                { typeof(sbyte), PrimitiveTypeCode.SByte },
                { typeof(sbyte?), PrimitiveTypeCode.SByteNullable },
                { typeof(short), PrimitiveTypeCode.Int16 },
                { typeof(short?), PrimitiveTypeCode.Int16Nullable },
                { typeof(ushort), PrimitiveTypeCode.UInt16 },
                { typeof(ushort?), PrimitiveTypeCode.UInt16Nullable },
                { typeof(int), PrimitiveTypeCode.Int32 },
                { typeof(int?), PrimitiveTypeCode.Int32Nullable },
                { typeof(byte), PrimitiveTypeCode.Byte },
                { typeof(byte?), PrimitiveTypeCode.ByteNullable },
                { typeof(uint), PrimitiveTypeCode.UInt32 },
                { typeof(uint?), PrimitiveTypeCode.UInt32Nullable },
                { typeof(long), PrimitiveTypeCode.Int64 },
                { typeof(long?), PrimitiveTypeCode.Int64Nullable },
                { typeof(ulong), PrimitiveTypeCode.UInt64 },
                { typeof(ulong?), PrimitiveTypeCode.UInt64Nullable },
                { typeof(float), PrimitiveTypeCode.Single },
                { typeof(float?), PrimitiveTypeCode.SingleNullable },
                { typeof(double), PrimitiveTypeCode.Double },
                { typeof(double?), PrimitiveTypeCode.DoubleNullable },
                { typeof(DateTime), PrimitiveTypeCode.DateTime },
                { typeof(DateTime?), PrimitiveTypeCode.DateTimeNullable },
                { typeof(DateTimeOffset), PrimitiveTypeCode.DateTimeOffset },
                { typeof(DateTimeOffset?), PrimitiveTypeCode.DateTimeOffsetNullable },
                { typeof(decimal), PrimitiveTypeCode.Decimal },
                { typeof(decimal?), PrimitiveTypeCode.DecimalNullable },
                { typeof(Guid), PrimitiveTypeCode.Guid },
                { typeof(Guid?), PrimitiveTypeCode.GuidNullable },
                { typeof(TimeSpan), PrimitiveTypeCode.TimeSpan },
                { typeof(TimeSpan?), PrimitiveTypeCode.TimeSpanNullable },
#if !PORTABLE
                { typeof(BigInteger), PrimitiveTypeCode.BigInteger },
                { typeof(BigInteger?), PrimitiveTypeCode.BigIntegerNullable },
#endif
                { typeof(Uri), PrimitiveTypeCode.Uri },
                { typeof(string), PrimitiveTypeCode.String },
                { typeof(byte[]), PrimitiveTypeCode.Bytes },
#if !(PORTABLE || DOTNET)
                { typeof(DBNull), PrimitiveTypeCode.DBNull }
#endif
            };

#if !PORTABLE
		/// <summary>
		/// The primitive type codes.
		/// </summary>
        private static readonly TypeInformation[] PrimitiveTypeCodes =
        {
            // need all of these. lookup against the index with TypeCode value
            new TypeInformation { Type = typeof(object), TypeCode = PrimitiveTypeCode.Empty },
            new TypeInformation { Type = typeof(object), TypeCode = PrimitiveTypeCode.Object },
            new TypeInformation { Type = typeof(object), TypeCode = PrimitiveTypeCode.DBNull },
            new TypeInformation { Type = typeof(bool), TypeCode = PrimitiveTypeCode.Boolean },
            new TypeInformation { Type = typeof(char), TypeCode = PrimitiveTypeCode.Char },
            new TypeInformation { Type = typeof(sbyte), TypeCode = PrimitiveTypeCode.SByte },
            new TypeInformation { Type = typeof(byte), TypeCode = PrimitiveTypeCode.Byte },
            new TypeInformation { Type = typeof(short), TypeCode = PrimitiveTypeCode.Int16 },
            new TypeInformation { Type = typeof(ushort), TypeCode = PrimitiveTypeCode.UInt16 },
            new TypeInformation { Type = typeof(int), TypeCode = PrimitiveTypeCode.Int32 },
            new TypeInformation { Type = typeof(uint), TypeCode = PrimitiveTypeCode.UInt32 },
            new TypeInformation { Type = typeof(long), TypeCode = PrimitiveTypeCode.Int64 },
            new TypeInformation { Type = typeof(ulong), TypeCode = PrimitiveTypeCode.UInt64 },
            new TypeInformation { Type = typeof(float), TypeCode = PrimitiveTypeCode.Single },
            new TypeInformation { Type = typeof(double), TypeCode = PrimitiveTypeCode.Double },
            new TypeInformation { Type = typeof(decimal), TypeCode = PrimitiveTypeCode.Decimal },
            new TypeInformation { Type = typeof(DateTime), TypeCode = PrimitiveTypeCode.DateTime },
            new TypeInformation { Type = typeof(object), TypeCode = PrimitiveTypeCode.Empty }, // no 17 in TypeCode for some reason
            new TypeInformation { Type = typeof(string), TypeCode = PrimitiveTypeCode.String }
        };
#endif

		/// <summary>
		/// Gets the type code.
		/// </summary>
		/// <returns>The type code.</returns>
		/// <param name="t">T.</param>
        public static PrimitiveTypeCode GetTypeCode(Type t) {
            bool isEnum;
            return GetTypeCode(t, out isEnum);
        }

		/// <summary>
		/// Gets the type code.
		/// </summary>
		/// <returns>The type code.</returns>
		/// <param name="t">T.</param>
		/// <param name="isEnum">If set to <c>true</c> is enum.</param>
        public static PrimitiveTypeCode GetTypeCode(Type t, out bool isEnum) {
            PrimitiveTypeCode typeCode;
            if (TypeCodeMap.TryGetValue(t, out typeCode)) {
                isEnum = false;
                return typeCode;
            }

            if (t.IsEnum()) {
                isEnum = true;
                return GetTypeCode(Enum.GetUnderlyingType(t));
            }

            // performance?
            if (ReflectionUtils.IsNullableType(t)) {
                Type nonNullable = Nullable.GetUnderlyingType(t);
                if (nonNullable.IsEnum()) {
                    Type nullableUnderlyingType = typeof(Nullable<>).MakeGenericType(Enum.GetUnderlyingType(nonNullable));
                    isEnum = true;
                    return GetTypeCode(nullableUnderlyingType);
                }
            }

            isEnum = false;
            return PrimitiveTypeCode.Object;
        }

#if !PORTABLE
		/// <summary>
		/// Gets the type information.
		/// </summary>
		/// <returns>The type information.</returns>
		/// <param name="convertable">Convertable.</param>
        public static TypeInformation GetTypeInformation(IConvertible convertable)
        {
            TypeInformation typeInformation = PrimitiveTypeCodes[(int)convertable.GetTypeCode()];
            return typeInformation;
        }
#endif

		/// <summary>
		/// Ises the convertible.
		/// </summary>
		/// <returns><c>true</c>, if convertible was ised, <c>false</c> otherwise.</returns>
		/// <param name="t">T.</param>
        public static bool IsConvertible(Type t) {
#if !PORTABLE
            return typeof(IConvertible).IsAssignableFrom(t);
#else
            return (
                t == typeof(bool) || t == typeof(byte) || t == typeof(char) || t == typeof(DateTime) || t == typeof(decimal) || t == typeof(double) || t == typeof(short) || t == typeof(int) ||
                t == typeof(long) || t == typeof(sbyte) || t == typeof(float) || t == typeof(string) || t == typeof(ushort) || t == typeof(uint) || t == typeof(ulong) || t.IsEnum());
#endif
        }

		/// <summary>
		/// Parses the time span.
		/// </summary>
		/// <returns>The time span.</returns>
		/// <param name="input">Input.</param>
        public static TimeSpan ParseTimeSpan(string input) {
            return TimeSpan.Parse(input, CultureInfo.InvariantCulture);
        }

		/// <summary>
		/// Type convert key.
		/// </summary>
        internal struct TypeConvertKey : IEquatable<TypeConvertKey>
        {
			/// <summary>
			/// The initial type.
			/// </summary>
            private readonly Type _initialType;
            /// <summary>
            /// The type of the target.
            /// </summary>
			private readonly Type _targetType;

			/// <summary>
			/// Gets the initial type.
			/// </summary>
			/// <value>The initial type.</value>
            public Type InitialType {
                get { return _initialType; }
            }

			/// <summary>
			/// Gets the type of the target.
			/// </summary>
			/// <value>The type of the target.</value>
            public Type TargetType {
                get { return _targetType; }
            }

			/// <summary>
			/// Initializes a new instance of the <see cref="T:indice.Edi.Utilities.ConvertUtils.TypeConvertKey"/> struct.
			/// </summary>
			/// <param name="initialType">Initial type.</param>
			/// <param name="targetType">Target type.</param>
            public TypeConvertKey(Type initialType, Type targetType) {
                _initialType = initialType;
                _targetType = targetType;
            }

			/// <summary>
			/// Serves as a hash function for a <see cref="T:indice.Edi.Utilities.ConvertUtils.TypeConvertKey"/> object.
			/// </summary>
			/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
            public override int GetHashCode() {
                return _initialType.GetHashCode() ^ _targetType.GetHashCode();
            }

			/// <summary>
			/// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:indice.Edi.Utilities.ConvertUtils.TypeConvertKey"/>.
			/// </summary>
			/// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:indice.Edi.Utilities.ConvertUtils.TypeConvertKey"/>.</param>
			/// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
			/// <see cref="T:indice.Edi.Utilities.ConvertUtils.TypeConvertKey"/>; otherwise, <c>false</c>.</returns>
            public override bool Equals(object obj) {
                if (!(obj is TypeConvertKey)) {
                    return false;
                }

                return Equals((TypeConvertKey)obj);
            }

			/// <summary>
			/// Determines whether the specified <see cref="indice.Edi.Utilities.ConvertUtils.TypeConvertKey"/> is equal to the
			/// current <see cref="T:indice.Edi.Utilities.ConvertUtils.TypeConvertKey"/>.
			/// </summary>
			/// <param name="other">The <see cref="indice.Edi.Utilities.ConvertUtils.TypeConvertKey"/> to compare with the current <see cref="T:indice.Edi.Utilities.ConvertUtils.TypeConvertKey"/>.</param>
			/// <returns><c>true</c> if the specified <see cref="indice.Edi.Utilities.ConvertUtils.TypeConvertKey"/> is equal to the
			/// current <see cref="T:indice.Edi.Utilities.ConvertUtils.TypeConvertKey"/>; otherwise, <c>false</c>.</returns>
            public bool Equals(TypeConvertKey other) {
                return (_initialType == other._initialType && _targetType == other._targetType);
            }
        }

		/// <summary>
		/// The cast converters.
		/// </summary>
        private static readonly ThreadSafeStore<TypeConvertKey, Func<object, object>> CastConverters =
            new ThreadSafeStore<TypeConvertKey, Func<object, object>>(CreateCastConverter);

		/// <summary>
		/// Creates the cast converter.
		/// </summary>
		/// <returns>The cast converter.</returns>
		/// <param name="t">T.</param>
        private static Func<object, object> CreateCastConverter(TypeConvertKey t) {
            MethodInfo castMethodInfo = t.TargetType.GetMethod("op_Implicit", new[] { t.InitialType });
            if (castMethodInfo == null) {
                castMethodInfo = t.TargetType.GetMethod("op_Explicit", new[] { t.InitialType });
            }

            if (castMethodInfo == null) {
                return null;
            }


            MethodCall<object, object> call = (o, a) => castMethodInfo.Invoke(o, a);

            return o => call(null, o);
        }

#if !PORTABLE
		/// <summary>
		/// Tos the big integer.
		/// </summary>
		/// <returns>The big integer.</returns>
		/// <param name="value">Value.</param>
        internal static BigInteger ToBigInteger(object value)
        {
            if (value is BigInteger)
            {
                return (BigInteger)value;
            }
            if (value is string)
            {
                return BigInteger.Parse((string)value, CultureInfo.InvariantCulture);
            }
            if (value is float)
            {
                return new BigInteger((float)value);
            }
            if (value is double)
            {
                return new BigInteger((double)value);
            }
            if (value is decimal)
            {
                return new BigInteger((decimal)value);
            }
            if (value is int)
            {
                return new BigInteger((int)value);
            }
            if (value is long)
            {
                return new BigInteger((long)value);
            }
            if (value is uint)
            {
                return new BigInteger((uint)value);
            }
            if (value is ulong)
            {
                return new BigInteger((ulong)value);
            }
            if (value is byte[])
            {
                return new BigInteger((byte[])value);
            }

            throw new InvalidCastException("Cannot convert {0} to BigInteger.".FormatWith(CultureInfo.InvariantCulture, value.GetType()));
        }

		/// <summary>
		/// Froms the big integer.
		/// </summary>
		/// <returns>The big integer.</returns>
		/// <param name="i">The index.</param>
		/// <param name="targetType">Target type.</param>
        public static object FromBigInteger(BigInteger i, Type targetType)
        {
            if (targetType == typeof(decimal))
            {
                return (decimal)i;
            }
            if (targetType == typeof(double))
            {
                return (double)i;
            }
            if (targetType == typeof(float))
            {
                return (float)i;
            }
            if (targetType == typeof(ulong))
            {
                return (ulong)i;
            }
            if (targetType == typeof(bool))
            {
                return i != 0;
            }

            try
            {
                return System.Convert.ChangeType((long)i, targetType, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Can not convert from BigInteger to {0}.".FormatWith(CultureInfo.InvariantCulture, targetType), ex);
            }
        }
#endif

        #region TryConvert
        /// <summary>
        /// Convert result.
        /// </summary>
		internal enum ConvertResult
        {
            Success = 0,
            CannotConvertNull = 1,
            NotInstantiableType = 2,
            NoValidConversion = 3
        }

		/// <summary>
		/// Convert the specified initialValue, culture and targetType.
		/// </summary>
		/// <param name="initialValue">Initial value.</param>
		/// <param name="culture">Culture.</param>
		/// <param name="targetType">Target type.</param>
        public static object Convert(object initialValue, CultureInfo culture, Type targetType) {
            object value;
            switch (TryConvertInternal(initialValue, culture, targetType, out value)) {
                case ConvertResult.Success:
                    return value;
                case ConvertResult.CannotConvertNull:
                    throw new Exception("Can not convert null {0} into non-nullable {1}.".FormatWith(CultureInfo.InvariantCulture, initialValue.GetType(), targetType));
                case ConvertResult.NotInstantiableType:
                    throw new ArgumentException("Target type {0} is not a value type or a non-abstract class.".FormatWith(CultureInfo.InvariantCulture, targetType), nameof(targetType));
                case ConvertResult.NoValidConversion:
                    throw new InvalidOperationException("Can not convert from {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, initialValue.GetType(), targetType));
                default:
                    throw new InvalidOperationException("Unexpected conversion result.");
            }
        }

		/// <summary>
		/// Tries the convert.
		/// </summary>
		/// <returns><c>true</c>, if convert was tryed, <c>false</c> otherwise.</returns>
		/// <param name="initialValue">Initial value.</param>
		/// <param name="culture">Culture.</param>
		/// <param name="targetType">Target type.</param>
		/// <param name="value">Value.</param>
        private static bool TryConvert(object initialValue, CultureInfo culture, Type targetType, out object value) {
            try {
                if (TryConvertInternal(initialValue, culture, targetType, out value) == ConvertResult.Success) {
                    return true;
                }

                value = null;
                return false;
            } catch {
                value = null;
                return false;
            }
        }

		/// <summary>
		/// Tries the convert internal.
		/// </summary>
		/// <returns>The convert internal.</returns>
		/// <param name="initialValue">Initial value.</param>
		/// <param name="culture">Culture.</param>
		/// <param name="targetType">Target type.</param>
		/// <param name="value">Value.</param>
        private static ConvertResult TryConvertInternal(object initialValue, CultureInfo culture, Type targetType, out object value) {
            if (initialValue == null) {
                throw new ArgumentNullException(nameof(initialValue));
            }

            if (ReflectionUtils.IsNullableType(targetType)) {
                targetType = Nullable.GetUnderlyingType(targetType);
            }

            Type initialType = initialValue.GetType();

            if (targetType == initialType) {
                value = initialValue;
                return ConvertResult.Success;
            }

            // use Convert.ChangeType if both types are IConvertible
            if (ConvertUtils.IsConvertible(initialValue.GetType()) && ConvertUtils.IsConvertible(targetType)) {
                if (targetType.IsEnum()) {
                    if (initialValue is string) {
                        value = Enum.Parse(targetType, initialValue.ToString(), true);
                        return ConvertResult.Success;
                    } else if (IsInteger(initialValue)) {
                        value = Enum.ToObject(targetType, initialValue);
                        return ConvertResult.Success;
                    }
                }

                value = System.Convert.ChangeType(initialValue, targetType, culture);
                return ConvertResult.Success;
            }

            if (initialValue is DateTime && targetType == typeof(DateTimeOffset)) {
                value = new DateTimeOffset((DateTime)initialValue);
                return ConvertResult.Success;
            }

            if (initialValue is byte[] && targetType == typeof(Guid)) {
                value = new Guid((byte[])initialValue);
                return ConvertResult.Success;
            }

            if (initialValue is Guid && targetType == typeof(byte[])) {
                value = ((Guid)initialValue).ToByteArray();
                return ConvertResult.Success;
            }

            string s = initialValue as string;
            if (s != null) {
                if (targetType == typeof(Guid)) {
                    value = new Guid(s);
                    return ConvertResult.Success;
                }
                if (targetType == typeof(Uri)) {
                    value = new Uri(s, UriKind.RelativeOrAbsolute);
                    return ConvertResult.Success;
                }
                if (targetType == typeof(TimeSpan)) {
                    value = ParseTimeSpan(s);
                    return ConvertResult.Success;
                }
                if (targetType == typeof(byte[])) {
                    value = System.Convert.FromBase64String(s);
                    return ConvertResult.Success;
                }
                if (targetType == typeof(Version)) {
                    Version result;
                    if (VersionTryParse(s, out result)) {
                        value = result;
                        return ConvertResult.Success;
                    }
                    value = null;
                    return ConvertResult.NoValidConversion;
                }
                if (typeof(Type).IsAssignableFrom(targetType)) {
                    value = Type.GetType(s, true);
                    return ConvertResult.Success;
                }
            }

#if !PORTABLE
            if (targetType == typeof(BigInteger))
            {
                value = ToBigInteger(initialValue);
                return ConvertResult.Success;
            }
            if (initialValue is BigInteger)
            {
                value = FromBigInteger((BigInteger)initialValue, targetType);
                return ConvertResult.Success;
            }
#endif

#if !PORTABLE
            // see if source or target types have a TypeConverter that converts between the two
            TypeConverter toConverter = GetConverter(initialType);

            if (toConverter != null && toConverter.CanConvertTo(targetType))
            {
                value = toConverter.ConvertTo(null, culture, initialValue, targetType);
                return ConvertResult.Success;
            }

            TypeConverter fromConverter = GetConverter(targetType);

            if (fromConverter != null && fromConverter.CanConvertFrom(initialType))
            {
                value = fromConverter.ConvertFrom(null, culture, initialValue);
                return ConvertResult.Success;
            }
#endif
#if !(DOTNET || PORTABLE)
            // handle DBNull and INullable
            if (initialValue == DBNull.Value)
            {
                if (ReflectionUtils.IsNullable(targetType))
                {
                    value = EnsureTypeAssignable(null, initialType, targetType);
                    return ConvertResult.Success;
                }

                // cannot convert null to non-nullable
                value = null;
                return ConvertResult.CannotConvertNull;
            }
#endif
#if !(DOTNET || PORTABLE)
            if (initialValue is INullable)
            {
                value = EnsureTypeAssignable(ToValue((INullable)initialValue), initialType, targetType);
                return ConvertResult.Success;
            }
#endif

            if (targetType.IsInterface() || targetType.IsGenericTypeDefinition() || targetType.IsAbstract()) {
                value = null;
                return ConvertResult.NotInstantiableType;
            }

            value = null;
            return ConvertResult.NoValidConversion;
        }
        #endregion

        #region ConvertOrCast
        /// <summary>
        /// Converts the value to the specified type. If the value is unable to be converted, the
        /// value is checked whether it assignable to the specified type.
        /// </summary>
        /// <param name="initialValue">The value to convert.</param>
        /// <param name="culture">The culture to use when converting.</param>
        /// <param name="targetType">The type to convert or cast the value to.</param>
        /// <returns>
        /// The converted type. If conversion was unsuccessful, the initial value
        /// is returned if assignable to the target type.
        /// </returns>
        public static object ConvertOrCast(object initialValue, CultureInfo culture, Type targetType) {
            object convertedValue;

            if (targetType == typeof(object)) {
                return initialValue;
            }

            if (initialValue == null && ReflectionUtils.IsNullable(targetType)) {
                return null;
            }

            if (TryConvert(initialValue, culture, targetType, out convertedValue)) {
                return convertedValue;
            }

            return EnsureTypeAssignable(initialValue, ReflectionUtils.GetObjectType(initialValue), targetType);
        }
        #endregion

		/// <summary>
		/// Ensures the type assignable.
		/// </summary>
		/// <returns>The type assignable.</returns>
		/// <param name="value">Value.</param>
		/// <param name="initialType">Initial type.</param>
		/// <param name="targetType">Target type.</param>
        private static object EnsureTypeAssignable(object value, Type initialType, Type targetType) {
            Type valueType = (value != null) ? value.GetType() : null;

            if (value != null) {
                if (targetType.IsAssignableFrom(valueType)) {
                    return value;
                }

                Func<object, object> castConverter = CastConverters.Get(new TypeConvertKey(valueType, targetType));
                if (castConverter != null) {
                    return castConverter(value);
                }
            } else {
                if (ReflectionUtils.IsNullable(targetType)) {
                    return null;
                }
            }

            throw new ArgumentException("Could not cast or convert from {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, (initialType != null) ? initialType.ToString() : "{null}", targetType));
        }

#if !(DOTNET || PORTABLE)
		/// <summary>
		/// Tos the value.
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="nullableValue">Nullable value.</param>
        public static object ToValue(INullable nullableValue)
        {
            if (nullableValue == null)
            {
                return null;
            }
            else if (nullableValue is SqlInt32)
            {
                return ToValue((SqlInt32)nullableValue);
            }
            else if (nullableValue is SqlInt64)
            {
                return ToValue((SqlInt64)nullableValue);
            }
            else if (nullableValue is SqlBoolean)
            {
                return ToValue((SqlBoolean)nullableValue);
            }
            else if (nullableValue is SqlString)
            {
                return ToValue((SqlString)nullableValue);
            }
            else if (nullableValue is SqlDateTime)
            {
                return ToValue((SqlDateTime)nullableValue);
            }

            throw new ArgumentException("Unsupported INullable type: {0}".FormatWith(CultureInfo.InvariantCulture, nullableValue.GetType()));
        }
#endif

#if !PORTABLE
		/// <summary>
		/// Gets the converter.
		/// </summary>
		/// <returns>The converter.</returns>
		/// <param name="t">T.</param>
        internal static TypeConverter GetConverter(Type t)
        {
            return TypeDescriptor.GetConverter(t);
        }
#endif

		/// <summary>
		/// Versions the try parse.
		/// </summary>
		/// <returns><c>true</c>, if try parse was versioned, <c>false</c> otherwise.</returns>
		/// <param name="input">Input.</param>
		/// <param name="result">Result.</param>
        public static bool VersionTryParse(string input, out Version result) {
            return Version.TryParse(input, out result);
        }

		/// <summary>
		/// Ises the integer.
		/// </summary>
		/// <returns><c>true</c>, if integer was ised, <c>false</c> otherwise.</returns>
		/// <param name="value">Value.</param>
        public static bool IsInteger(object value) {
            switch (GetTypeCode(value.GetType())) {
                case PrimitiveTypeCode.SByte:
                case PrimitiveTypeCode.Byte:
                case PrimitiveTypeCode.Int16:
                case PrimitiveTypeCode.UInt16:
                case PrimitiveTypeCode.Int32:
                case PrimitiveTypeCode.UInt32:
                case PrimitiveTypeCode.Int64:
                case PrimitiveTypeCode.UInt64:
                    return true;
                default:
                    return false;
            }
        }

		/// <summary>
		/// Int32s the try parse.
		/// </summary>
		/// <returns>The try parse.</returns>
		/// <param name="chars">Chars.</param>
		/// <param name="start">Start.</param>
		/// <param name="length">Length.</param>
		/// <param name="value">Value.</param>
        public static ParseResult Int32TryParse(char[] chars, int start, int length, out int value) {
            value = 0;

            if (length == 0) {
                return ParseResult.Invalid;
            }

            bool isNegative = (chars[start] == '-');

            if (isNegative) {
                // text just a negative sign
                if (length == 1) {
                    return ParseResult.Invalid;
                }

                start++;
                length--;
            }

            int end = start + length;

            // Int32.MaxValue and MinValue are 10 chars
            // Or is 10 chars and start is greater than two
            // Need to improve this!
            if (length > 10 || (length == 10 && chars[start] - '0' > 2)) {
                // invalid result takes precedence over overflow
                for (int i = start; i < end; i++) {
                    int c = chars[i] - '0';

                    if (c < 0 || c > 9) {
                        return ParseResult.Invalid;
                    }
                }

                return ParseResult.Overflow;
            }

            for (int i = start; i < end; i++) {
                int c = chars[i] - '0';

                if (c < 0 || c > 9) {
                    return ParseResult.Invalid;
                }

                int newValue = (10 * value) - c;

                // overflow has caused the number to loop around
                if (newValue > value) {
                    i++;

                    // double check the rest of the string that there wasn't anything invalid
                    // invalid result takes precedence over overflow result
                    for (; i < end; i++) {
                        c = chars[i] - '0';

                        if (c < 0 || c > 9) {
                            return ParseResult.Invalid;
                        }
                    }

                    return ParseResult.Overflow;
                }

                value = newValue;
            }

            // go from negative to positive to avoids overflow
            // negative can be slightly bigger than positive
            if (!isNegative) {
                // negative integer can be one bigger than positive
                if (value == int.MinValue) {
                    return ParseResult.Overflow;
                }

                value = -value;
            }

            return ParseResult.Success;
        }

		/// <summary>
		/// Int64s the try parse.
		/// </summary>
		/// <returns>The try parse.</returns>
		/// <param name="chars">Chars.</param>
		/// <param name="start">Start.</param>
		/// <param name="length">Length.</param>
		/// <param name="value">Value.</param>
        public static ParseResult Int64TryParse(char[] chars, int start, int length, out long value) {
            value = 0;

            if (length == 0) {
                return ParseResult.Invalid;
            }

            bool isNegative = (chars[start] == '-');

            if (isNegative) {
                // text just a negative sign
                if (length == 1) {
                    return ParseResult.Invalid;
                }

                start++;
                length--;
            }

            int end = start + length;

            // Int64.MaxValue and MinValue are 19 chars
            if (length > 19) {
                // invalid result takes precedence over overflow
                for (int i = start; i < end; i++) {
                    int c = chars[i] - '0';

                    if (c < 0 || c > 9) {
                        return ParseResult.Invalid;
                    }
                }

                return ParseResult.Overflow;
            }

            for (int i = start; i < end; i++) {
                int c = chars[i] - '0';

                if (c < 0 || c > 9) {
                    return ParseResult.Invalid;
                }

                long newValue = (10 * value) - c;

                // overflow has caused the number to loop around
                if (newValue > value) {
                    i++;

                    // double check the rest of the string that there wasn't anything invalid
                    // invalid result takes precedence over overflow result
                    for (; i < end; i++) {
                        c = chars[i] - '0';

                        if (c < 0 || c > 9) {
                            return ParseResult.Invalid;
                        }
                    }

                    return ParseResult.Overflow;
                }

                value = newValue;
            }

            // go from negative to positive to avoids overflow
            // negative can be slightly bigger than positive
            if (!isNegative) {
                // negative integer can be one bigger than positive
                if (value == long.MinValue) {
                    return ParseResult.Overflow;
                }

                value = -value;
            }

            return ParseResult.Success;
        }

		/// <summary>
		/// Tries the convert GUID.
		/// </summary>
		/// <returns><c>true</c>, if convert GUID was tryed, <c>false</c> otherwise.</returns>
		/// <param name="s">S.</param>
		/// <param name="g">The green component.</param>
        public static bool TryConvertGuid(string s, out Guid g) {
            // GUID has to have format 00000000-0000-0000-0000-000000000000
#if NET20 || NET35
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            Regex format = new Regex("^[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}$");
            Match match = format.Match(s);
            if (match.Success)
            {
                g = new Guid(s);
                return true;
            }

            g = Guid.Empty;
            return false;
#else
            return Guid.TryParseExact(s, "D", out g);
#endif
        }

		/// <summary>
		/// Hexs the text to int.
		/// </summary>
		/// <returns>The text to int.</returns>
		/// <param name="text">Text.</param>
		/// <param name="start">Start.</param>
		/// <param name="end">End.</param>
        public static int HexTextToInt(char[] text, int start, int end) {
            int value = 0;
            for (int i = start; i < end; i++) {
                value += HexCharToInt(text[i]) << ((end - 1 - i) * 4);
            }
            return value;
        }

		/// <summary>
		/// Hexs the char to int.
		/// </summary>
		/// <returns>The char to int.</returns>
		/// <param name="ch">Ch.</param>
        private static int HexCharToInt(char ch) {
            if (ch <= 57 && ch >= 48) {
                return ch - 48;
            }

            if (ch <= 70 && ch >= 65) {
                return ch - 55;
            }

            if (ch <= 102 && ch >= 97) {
                return ch - 87;
            }

            throw new FormatException("Invalid hex character: " + ch);
        }
    }
}