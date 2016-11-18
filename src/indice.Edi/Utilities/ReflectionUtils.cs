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
#if !(PORTABLE || PORTABLE40 || NET35 || NET20)
using System.Numerics;
#endif
using System.Reflection;
using System.Collections;
using System.Globalization;
using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Linq;

namespace indice.Edi.Utilities
{
#if (DOTNET || PORTABLE || PORTABLE40)
  	/// <summary>
  	/// Member types.
  	/// </summary>
	internal enum MemberTypes
    {
		/// <summary>
		/// The property.
		/// </summary>
        Property = 0,
		/// <summary>
		/// The field.
		/// </summary>
        Field = 1,
		/// <summary>
		/// The event.
		/// </summary>
        Event = 2,
		/// <summary>
		/// The method.
		/// </summary>
        Method = 3,
		/// <summary>
		/// The other.
		/// </summary>
        Other = 4
    }
#endif

#if PORTABLE
	/// <summary>
	/// Binding flags.
	/// </summary>
    [Flags]
    internal enum BindingFlags
    {
		/// <summary>
		/// The default.
		/// </summary>
        Default = 0,
        /// <summary>
        /// The ignore case.
        /// </summary>
		IgnoreCase = 1,
        /// <summary>
        /// The declared only.
        /// </summary>
		DeclaredOnly = 2,
        /// <summary>
        /// The instance.
        /// </summary>
		Instance = 4,
        /// <summary>
        /// The static.
        /// </summary>
		Static = 8,
        /// <summary>
        /// The public.
        /// </summary>
		Public = 16,
        /// <summary>
        /// The non public.
        /// </summary>
		NonPublic = 32,
        /// <summary>
        /// The flatten hierarchy.
        /// </summary>
		FlattenHierarchy = 64,
        /// <summary>
        /// The invoke method.
        /// </summary>
		InvokeMethod = 256,
        /// <summary>
        /// The create instance.
        /// </summary>
		CreateInstance = 512,
        /// <summary>
        /// The get field.
        /// </summary>
		GetField = 1024,
        /// <summary>
        /// The set field.
        /// </summary>
		SetField = 2048,
        /// <summary>
        /// The get property.
        /// </summary>
		GetProperty = 4096,
        /// <summary>
        /// The set property.
        /// </summary>
		SetProperty = 8192,
        /// <summary>
        /// The put disp property.
        /// </summary>
		PutDispProperty = 16384,
        /// <summary>
        /// The exact binding.
        /// </summary>
		ExactBinding = 65536,
        /// <summary>
        /// The put reference disp property.
        /// </summary>
		PutRefDispProperty = 32768,
        /// <summary>
        /// The type of the suppress change.
        /// </summary>
		SuppressChangeType = 131072,
        /// <summary>
        /// The optional parameter binding.
        /// </summary>
		OptionalParamBinding = 262144,
        /// <summary>
        /// The ignore return.
        /// </summary>
		IgnoreReturn = 16777216
    }
#endif

	/// <summary>
	/// Reflection utils.
	/// </summary>
    internal static class ReflectionUtils
    {
		/// <summary>
		/// The empty types.
		/// </summary>
        public static readonly Type[] EmptyTypes;

		/// <summary>
		/// Initializes the <see cref="T:indice.Edi.Utilities.ReflectionUtils"/> class.
		/// </summary>
        static ReflectionUtils() {
#if !(PORTABLE40 || PORTABLE)
            EmptyTypes = Type.EmptyTypes;
#else
            EmptyTypes = new Type[0];
#endif
        }

		/// <summary>
		/// Ises the virtual.
		/// </summary>
		/// <returns><c>true</c>, if virtual was ised, <c>false</c> otherwise.</returns>
		/// <param name="propertyInfo">Property info.</param>
        public static bool IsVirtual(this PropertyInfo propertyInfo) {
            ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");

            MethodInfo m = propertyInfo.GetGetMethod();
            if (m != null && m.IsVirtual)
                return true;

            m = propertyInfo.GetSetMethod();
            if (m != null && m.IsVirtual)
                return true;

            return false;
        }

		/// <summary>
		/// Gets the base definition.
		/// </summary>
		/// <returns>The base definition.</returns>
		/// <param name="propertyInfo">Property info.</param>
        public static MethodInfo GetBaseDefinition(this PropertyInfo propertyInfo) {
            ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");

            MethodInfo m = propertyInfo.GetGetMethod();
            if (m != null)
                return m.GetBaseDefinition();

            m = propertyInfo.GetSetMethod();
            if (m != null)
                return m.GetBaseDefinition();

            return null;
        }

		/// <summary>
		/// Ises the public.
		/// </summary>
		/// <returns><c>true</c>, if public was ised, <c>false</c> otherwise.</returns>
		/// <param name="property">Property.</param>
        public static bool IsPublic(PropertyInfo property) {
            if (property.GetGetMethod() != null && property.GetGetMethod().IsPublic)
                return true;
            if (property.GetSetMethod() != null && property.GetSetMethod().IsPublic)
                return true;

            return false;
        }

		/// <summary>
		/// Gets the type of the object.
		/// </summary>
		/// <returns>The object type.</returns>
		/// <param name="v">V.</param>
        public static Type GetObjectType(object v) {
            return (v != null) ? v.GetType() : null;
        }
        
		/// <summary>
		/// Removes the assembly details.
		/// </summary>
		/// <returns>The assembly details.</returns>
		/// <param name="fullyQualifiedTypeName">Fully qualified type name.</param>
        private static string RemoveAssemblyDetails(string fullyQualifiedTypeName) {
            StringBuilder builder = new StringBuilder();

            // loop through the type name and filter out qualified assembly details from nested type names
            bool writingAssemblyName = false;
            bool skippingAssemblyDetails = false;
            for (int i = 0; i < fullyQualifiedTypeName.Length; i++) {
                char current = fullyQualifiedTypeName[i];
                switch (current) {
                    case '[':
                        writingAssemblyName = false;
                        skippingAssemblyDetails = false;
                        builder.Append(current);
                        break;
                    case ']':
                        writingAssemblyName = false;
                        skippingAssemblyDetails = false;
                        builder.Append(current);
                        break;
                    case ',':
                        if (!writingAssemblyName) {
                            writingAssemblyName = true;
                            builder.Append(current);
                        } else {
                            skippingAssemblyDetails = true;
                        }
                        break;
                    default:
                        if (!skippingAssemblyDetails)
                            builder.Append(current);
                        break;
                }
            }

            return builder.ToString();
        }

		/// <summary>
		/// Hases the default constructor.
		/// </summary>
		/// <returns><c>true</c>, if default constructor was hased, <c>false</c> otherwise.</returns>
		/// <param name="t">T.</param>
		/// <param name="nonPublic">If set to <c>true</c> non public.</param>
        public static bool HasDefaultConstructor(Type t, bool nonPublic) {
            ValidationUtils.ArgumentNotNull(t, "t");

            if (t.IsValueType())
                return true;

            return (GetDefaultConstructor(t, nonPublic) != null);
        }

		/// <summary>
		/// Gets the default constructor.
		/// </summary>
		/// <returns>The default constructor.</returns>
		/// <param name="t">T.</param>
        public static ConstructorInfo GetDefaultConstructor(Type t) {
            return GetDefaultConstructor(t, false);
        }

		/// <summary>
		/// Gets the default constructor.
		/// </summary>
		/// <returns>The default constructor.</returns>
		/// <param name="t">T.</param>
		/// <param name="nonPublic">If set to <c>true</c> non public.</param>
        public static ConstructorInfo GetDefaultConstructor(Type t, bool nonPublic) {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
            if (nonPublic)
                bindingFlags = bindingFlags | BindingFlags.NonPublic;

            return t.GetConstructors(bindingFlags).SingleOrDefault(c => !c.GetParameters().Any());
        }

		/// <summary>
		/// Ises the nullable.
		/// </summary>
		/// <returns><c>true</c>, if nullable was ised, <c>false</c> otherwise.</returns>
		/// <param name="t">T.</param>
        public static bool IsNullable(Type t) {
            ValidationUtils.ArgumentNotNull(t, "t");

            if (t.IsValueType())
                return IsNullableType(t);

            return true;
        }

		/// <summary>
		/// Ises the type of the nullable.
		/// </summary>
		/// <returns><c>true</c>, if nullable type was ised, <c>false</c> otherwise.</returns>
		/// <param name="t">T.</param>
        public static bool IsNullableType(Type t) {
            ValidationUtils.ArgumentNotNull(t, "t");

            return (t.IsGenericType() && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

		/// <summary>
		/// Ensures the type of the not nullable.
		/// </summary>
		/// <returns>The not nullable type.</returns>
		/// <param name="t">T.</param>
        public static Type EnsureNotNullableType(Type t) {
            return (IsNullableType(t))
                ? Nullable.GetUnderlyingType(t)
                : t;
        }

		/// <summary>
		/// Ises the generic definition.
		/// </summary>
		/// <returns><c>true</c>, if generic definition was ised, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
		/// <param name="genericInterfaceDefinition">Generic interface definition.</param>
        public static bool IsGenericDefinition(Type type, Type genericInterfaceDefinition) {
            if (!type.IsGenericType())
                return false;

            Type t = type.GetGenericTypeDefinition();
            return (t == genericInterfaceDefinition);
        }

		/// <summary>
		/// Implementses the generic definition.
		/// </summary>
		/// <returns><c>true</c>, if generic definition was implementsed, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
		/// <param name="genericInterfaceDefinition">Generic interface definition.</param>
        public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition) {
            Type implementingType;
            return ImplementsGenericDefinition(type, genericInterfaceDefinition, out implementingType);
        }

		/// <summary>
		/// Implementses the generic definition.
		/// </summary>
		/// <returns><c>true</c>, if generic definition was implementsed, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
		/// <param name="genericInterfaceDefinition">Generic interface definition.</param>
		/// <param name="implementingType">Implementing type.</param>
        public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition, out Type implementingType) {
            ValidationUtils.ArgumentNotNull(type, "type");
            ValidationUtils.ArgumentNotNull(genericInterfaceDefinition, "genericInterfaceDefinition");

            if (!genericInterfaceDefinition.IsInterface() || !genericInterfaceDefinition.IsGenericTypeDefinition())
                throw new ArgumentNullException("'{0}' is not a generic interface definition.".FormatWith(CultureInfo.InvariantCulture, genericInterfaceDefinition));

            if (type.IsInterface()) {
                if (type.IsGenericType()) {
                    Type interfaceDefinition = type.GetGenericTypeDefinition();

                    if (genericInterfaceDefinition == interfaceDefinition) {
                        implementingType = type;
                        return true;
                    }
                }
            }

            foreach (Type i in type.GetInterfaces()) {
                if (i.IsGenericType()) {
                    Type interfaceDefinition = i.GetGenericTypeDefinition();

                    if (genericInterfaceDefinition == interfaceDefinition) {
                        implementingType = i;
                        return true;
                    }
                }
            }

            implementingType = null;
            return false;
        }

		/// <summary>
		/// Inheritses the generic definition.
		/// </summary>
		/// <returns><c>true</c>, if generic definition was inheritsed, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
		/// <param name="genericClassDefinition">Generic class definition.</param>
        public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition) {
            Type implementingType;
            return InheritsGenericDefinition(type, genericClassDefinition, out implementingType);
        }

		/// <summary>
		/// Inheritses the generic definition.
		/// </summary>
		/// <returns><c>true</c>, if generic definition was inheritsed, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
		/// <param name="genericClassDefinition">Generic class definition.</param>
		/// <param name="implementingType">Implementing type.</param>
        public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition, out Type implementingType) {
            ValidationUtils.ArgumentNotNull(type, "type");
            ValidationUtils.ArgumentNotNull(genericClassDefinition, "genericClassDefinition");

            if (!genericClassDefinition.IsClass() || !genericClassDefinition.IsGenericTypeDefinition())
                throw new ArgumentNullException("'{0}' is not a generic class definition.".FormatWith(CultureInfo.InvariantCulture, genericClassDefinition));

            return InheritsGenericDefinitionInternal(type, genericClassDefinition, out implementingType);
        }

		/// <summary>
		/// Inheritses the generic definition internal.
		/// </summary>
		/// <returns><c>true</c>, if generic definition internal was inheritsed, <c>false</c> otherwise.</returns>
		/// <param name="currentType">Current type.</param>
		/// <param name="genericClassDefinition">Generic class definition.</param>
		/// <param name="implementingType">Implementing type.</param>
        private static bool InheritsGenericDefinitionInternal(Type currentType, Type genericClassDefinition, out Type implementingType) {
            if (currentType.IsGenericType()) {
                Type currentGenericClassDefinition = currentType.GetGenericTypeDefinition();

                if (genericClassDefinition == currentGenericClassDefinition) {
                    implementingType = currentType;
                    return true;
                }
            }

            if (currentType.BaseType() == null) {
                implementingType = null;
                return false;
            }

            return InheritsGenericDefinitionInternal(currentType.BaseType(), genericClassDefinition, out implementingType);
        }

        /// <summary>
        /// Gets the type of the typed collection's items.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The type of the typed collection's items.</returns>
        public static Type GetCollectionItemType(Type type) {
            ValidationUtils.ArgumentNotNull(type, "type");
            Type genericListType;

            if (type.IsArray) {
                return type.GetElementType();
            }
            if (ImplementsGenericDefinition(type, typeof(IEnumerable<>), out genericListType)) {
                if (genericListType.IsGenericTypeDefinition())
                    throw new Exception("Type {0} is not a collection.".FormatWith(CultureInfo.InvariantCulture, type));

                return genericListType.GetGenericArguments()[0];
            }
            if (typeof(IEnumerable).IsAssignableFrom(type)) {
                return null;
            }

            throw new Exception("Type {0} is not a collection.".FormatWith(CultureInfo.InvariantCulture, type));
        }

		/// <summary>
		/// Gets the dictionary key value types.
		/// </summary>
		/// <param name="dictionaryType">Dictionary type.</param>
		/// <param name="keyType">Key type.</param>
		/// <param name="valueType">Value type.</param>
        public static void GetDictionaryKeyValueTypes(Type dictionaryType, out Type keyType, out Type valueType) {
            ValidationUtils.ArgumentNotNull(dictionaryType, "type");

            Type genericDictionaryType;
            if (ImplementsGenericDefinition(dictionaryType, typeof(IDictionary<,>), out genericDictionaryType)) {
                if (genericDictionaryType.IsGenericTypeDefinition())
                    throw new Exception("Type {0} is not a dictionary.".FormatWith(CultureInfo.InvariantCulture, dictionaryType));

                Type[] dictionaryGenericArguments = genericDictionaryType.GetGenericArguments();

                keyType = dictionaryGenericArguments[0];
                valueType = dictionaryGenericArguments[1];
                return;
            }
            if (typeof(IDictionary).IsAssignableFrom(dictionaryType)) {
                keyType = null;
                valueType = null;
                return;
            }

            throw new Exception("Type {0} is not a dictionary.".FormatWith(CultureInfo.InvariantCulture, dictionaryType));
        }

        /// <summary>
        /// Gets the member's underlying type.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <returns>The underlying type of the member.</returns>
        public static Type GetMemberUnderlyingType(MemberInfo member) {
            ValidationUtils.ArgumentNotNull(member, "member");

            switch (member.MemberType()) {
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                case MemberTypes.Event:
                    return ((EventInfo)member).EventHandlerType;
                case MemberTypes.Method:
                    return ((MethodInfo)member).ReturnType;
                default:
                    throw new ArgumentException("MemberInfo must be of type FieldInfo, PropertyInfo, EventInfo or MethodInfo", nameof(member));
            }
        }

        /// <summary>
        /// Determines whether the member is an indexed property.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <returns>
        /// 	<c>true</c> if the member is an indexed property; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsIndexedProperty(MemberInfo member) {
            ValidationUtils.ArgumentNotNull(member, "member");

            PropertyInfo propertyInfo = member as PropertyInfo;

            if (propertyInfo != null)
                return IsIndexedProperty(propertyInfo);
            else
                return false;
        }

        /// <summary>
        /// Determines whether the property is an indexed property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>
        /// 	<c>true</c> if the property is an indexed property; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsIndexedProperty(PropertyInfo property) {
            ValidationUtils.ArgumentNotNull(property, "property");

            return (property.GetIndexParameters().Length > 0);
        }

        /// <summary>
        /// Gets the member's value on the object.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="target">The target object.</param>
        /// <returns>The member's value on the object.</returns>
        public static object GetMemberValue(MemberInfo member, object target) {
            ValidationUtils.ArgumentNotNull(member, "member");
            ValidationUtils.ArgumentNotNull(target, "target");

            switch (member.MemberType()) {
                case MemberTypes.Field:
                    return ((FieldInfo)member).GetValue(target);
                case MemberTypes.Property:
                    try {
                        return ((PropertyInfo)member).GetValue(target, null);
                    } catch (TargetParameterCountException e) {
                        throw new ArgumentException("MemberInfo '{0}' has index parameters".FormatWith(CultureInfo.InvariantCulture, member.Name), e);
                    }
                default:
                    throw new ArgumentException("MemberInfo '{0}' is not of type FieldInfo or PropertyInfo".FormatWith(CultureInfo.InvariantCulture, CultureInfo.InvariantCulture, member.Name), nameof(member));
            }
        }

        /// <summary>
        /// Sets the member's value on the target object.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        public static void SetMemberValue(MemberInfo member, object target, object value) {
            ValidationUtils.ArgumentNotNull(member, "member");
            ValidationUtils.ArgumentNotNull(target, "target");

            switch (member.MemberType()) {
                case MemberTypes.Field:
                    ((FieldInfo)member).SetValue(target, value);
                    break;
                case MemberTypes.Property:
                    ((PropertyInfo)member).SetValue(target, value, null);
                    break;
                default:
                    throw new ArgumentException("MemberInfo '{0}' must be of type FieldInfo or PropertyInfo".FormatWith(CultureInfo.InvariantCulture, member.Name), nameof(member));
            }
        }

        /// <summary>
        /// Determines whether the specified MemberInfo can be read.
        /// </summary>
        /// <param name="member">The MemberInfo to determine whether can be read.</param>
        /// /// <param name="nonPublic">if set to <c>true</c> then allow the member to be gotten non-publicly.</param>
        /// <returns>
        /// 	<c>true</c> if the specified MemberInfo can be read; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanReadMemberValue(MemberInfo member, bool nonPublic) {
            switch (member.MemberType()) {
                case MemberTypes.Field:
                    FieldInfo fieldInfo = (FieldInfo)member;

                    if (nonPublic)
                        return true;
                    else if (fieldInfo.IsPublic)
                        return true;
                    return false;
                case MemberTypes.Property:
                    PropertyInfo propertyInfo = (PropertyInfo)member;

                    if (!propertyInfo.CanRead)
                        return false;
                    if (nonPublic)
                        return true;
                    return (propertyInfo.GetGetMethod(nonPublic) != null);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Determines whether the specified MemberInfo can be set.
        /// </summary>
        /// <param name="member">The MemberInfo to determine whether can be set.</param>
        /// <param name="nonPublic">if set to <c>true</c> then allow the member to be set non-publicly.</param>
        /// <param name="canSetReadOnly">if set to <c>true</c> then allow the member to be set if read-only.</param>
        /// <returns>
        /// 	<c>true</c> if the specified MemberInfo can be set; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanSetMemberValue(MemberInfo member, bool nonPublic, bool canSetReadOnly) {
            switch (member.MemberType()) {
                case MemberTypes.Field:
                    FieldInfo fieldInfo = (FieldInfo)member;

                    if (fieldInfo.IsLiteral)
                        return false;
                    if (fieldInfo.IsInitOnly && !canSetReadOnly)
                        return false;
                    if (nonPublic)
                        return true;
                    if (fieldInfo.IsPublic)
                        return true;
                    return false;
                case MemberTypes.Property:
                    PropertyInfo propertyInfo = (PropertyInfo)member;

                    if (!propertyInfo.CanWrite)
                        return false;
                    if (nonPublic)
                        return true;
                    return (propertyInfo.GetSetMethod(nonPublic) != null);
                default:
                    return false;
            }
        }

		/// <summary>
		/// Gets the fields and properties.
		/// </summary>
		/// <returns>The fields and properties.</returns>
		/// <param name="type">Type.</param>
		/// <param name="bindingAttr">Binding attr.</param>
        public static List<MemberInfo> GetFieldsAndProperties(Type type, BindingFlags bindingAttr) {
            List<MemberInfo> targetMembers = new List<MemberInfo>();

            targetMembers.AddRange(GetFields(type, bindingAttr));
            targetMembers.AddRange(GetProperties(type, bindingAttr));

            // for some reason .NET returns multiple members when overriding a generic member on a base class
            // http://social.msdn.microsoft.com/Forums/en-US/b5abbfee-e292-4a64-8907-4e3f0fb90cd9/reflection-overriden-abstract-generic-properties?forum=netfxbcl
            // filter members to only return the override on the topmost class
            // update: I think this is fixed in .NET 3.5 SP1 - leave this in for now...
            List<MemberInfo> distinctMembers = new List<MemberInfo>(targetMembers.Count);

            foreach (var groupedMember in targetMembers.GroupBy(m => m.Name)) {
                int count = groupedMember.Count();
                IList<MemberInfo> members = groupedMember.ToList();

                if (count == 1) {
                    distinctMembers.Add(members.First());
                } else {
                    IList<MemberInfo> resolvedMembers = new List<MemberInfo>();
                    foreach (MemberInfo memberInfo in members) {
                        // this is a bit hacky
                        // if the hiding property is hiding a base property and it is virtual
                        // then this ensures the derived property gets used
                        if (resolvedMembers.Count == 0)
                            resolvedMembers.Add(memberInfo);
                        else if (!IsOverridenGenericMember(memberInfo, bindingAttr) || memberInfo.Name == "Item")
                            resolvedMembers.Add(memberInfo);
                    }

                    distinctMembers.AddRange(resolvedMembers);
                }
            }

            return distinctMembers;
        }

		/// <summary>
		/// Ises the overriden generic member.
		/// </summary>
		/// <returns><c>true</c>, if overriden generic member was ised, <c>false</c> otherwise.</returns>
		/// <param name="memberInfo">Member info.</param>
		/// <param name="bindingAttr">Binding attr.</param>
        private static bool IsOverridenGenericMember(MemberInfo memberInfo, BindingFlags bindingAttr) {
            if (memberInfo.MemberType() != MemberTypes.Property)
                return false;

            PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
            if (!IsVirtual(propertyInfo))
                return false;

            Type declaringType = propertyInfo.DeclaringType;
            if (!declaringType.IsGenericType())
                return false;
            Type genericTypeDefinition = declaringType.GetGenericTypeDefinition();
            if (genericTypeDefinition == null)
                return false;
            MemberInfo[] members = genericTypeDefinition.GetMember(propertyInfo.Name, bindingAttr);
            if (members.Length == 0)
                return false;
            Type memberUnderlyingType = GetMemberUnderlyingType(members[0]);
            if (!memberUnderlyingType.IsGenericParameter)
                return false;

            return true;
        }

		/// <summary>
		/// Gets the attribute.
		/// </summary>
		/// <returns>The attribute.</returns>
		/// <param name="attributeProvider">Attribute provider.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T GetAttribute<T>(object attributeProvider) where T : Attribute {
            return GetAttribute<T>(attributeProvider, true);
        }

		/// <summary>
		/// Gets the attribute.
		/// </summary>
		/// <returns>The attribute.</returns>
		/// <param name="attributeProvider">Attribute provider.</param>
		/// <param name="inherit">If set to <c>true</c> inherit.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T GetAttribute<T>(object attributeProvider, bool inherit) where T : Attribute {
            T[] attributes = GetAttributes<T>(attributeProvider, inherit);

            return (attributes != null) ? attributes.FirstOrDefault() : null;
        }

#if !(DOTNET || PORTABLE)
		/// <summary>
		/// Gets the attributes.
		/// </summary>
		/// <returns>The attributes.</returns>
		/// <param name="attributeProvider">Attribute provider.</param>
		/// <param name="inherit">If set to <c>true</c> inherit.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T[] GetAttributes<T>(object attributeProvider, bool inherit) where T : Attribute
        {
            Attribute[] a = GetAttributes(attributeProvider, typeof(T), inherit);

            T[] attributes = a as T[];
            if (attributes != null)
                return attributes;

            return a.Cast<T>().ToArray();
        }

		/// <summary>
		/// Gets the attributes.
		/// </summary>
		/// <returns>The attributes.</returns>
		/// <param name="attributeProvider">Attribute provider.</param>
		/// <param name="attributeType">Attribute type.</param>
		/// <param name="inherit">If set to <c>true</c> inherit.</param>
        public static Attribute[] GetAttributes(object attributeProvider, Type attributeType, bool inherit)
        {
            ValidationUtils.ArgumentNotNull(attributeProvider, "attributeProvider");

            object provider = attributeProvider;

            // http://hyperthink.net/blog/getcustomattributes-gotcha/
            // ICustomAttributeProvider doesn't do inheritance

            if (provider is Type)
            {
                Type t = (Type)provider;
                object[] a = (attributeType != null) ? t.GetCustomAttributes(attributeType, inherit) : t.GetCustomAttributes(inherit);
                Attribute[] attributes = a.Cast<Attribute>().ToArray();

#if (NET20 || NET35)
                // ye olde .NET GetCustomAttributes doesn't respect the inherit argument
                if (inherit && t.BaseType != null)
                    attributes = attributes.Union(GetAttributes(t.BaseType, attributeType, inherit)).ToArray();
#endif

                return attributes;
            }

            if (provider is Assembly)
            {
                Assembly a = (Assembly)provider;
                return (attributeType != null) ? Attribute.GetCustomAttributes(a, attributeType) : Attribute.GetCustomAttributes(a);
            }

            if (provider is MemberInfo)
            {
                MemberInfo m = (MemberInfo)provider;
                return (attributeType != null) ? Attribute.GetCustomAttributes(m, attributeType, inherit) : Attribute.GetCustomAttributes(m, inherit);
            }

#if !PORTABLE40
            if (provider is Module)
            {
                Module m = (Module)provider;
                return (attributeType != null) ? Attribute.GetCustomAttributes(m, attributeType, inherit) : Attribute.GetCustomAttributes(m, inherit);
            }
#endif

            if (provider is ParameterInfo)
            {
                ParameterInfo p = (ParameterInfo)provider;
                return (attributeType != null) ? Attribute.GetCustomAttributes(p, attributeType, inherit) : Attribute.GetCustomAttributes(p, inherit);
            }

#if !PORTABLE40
            ICustomAttributeProvider customAttributeProvider = (ICustomAttributeProvider)attributeProvider;
            object[] result = (attributeType != null) ? customAttributeProvider.GetCustomAttributes(attributeType, inherit) : customAttributeProvider.GetCustomAttributes(inherit);

            return (Attribute[])result;
#else
            throw new Exception("Cannot get attributes from '{0}'.".FormatWith(CultureInfo.InvariantCulture, provider));
#endif
        }
#else

		/// <summary>
		/// Gets the attributes.
		/// </summary>
		/// <returns>The attributes.</returns>
		/// <param name="attributeProvider">Attribute provider.</param>
		/// <param name="inherit">If set to <c>true</c> inherit.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T[] GetAttributes<T>(object attributeProvider, bool inherit) where T : Attribute {
            return GetAttributes(attributeProvider, typeof(T), inherit).Cast<T>().ToArray();
        }

		/// <summary>
		/// Gets the attributes.
		/// </summary>
		/// <returns>The attributes.</returns>
		/// <param name="provider">Provider.</param>
		/// <param name="attributeType">Attribute type.</param>
		/// <param name="inherit">If set to <c>true</c> inherit.</param>
        public static Attribute[] GetAttributes(object provider, Type attributeType, bool inherit) {
            if (provider is Type) {
                Type t = (Type)provider;
                return (attributeType != null)
                    ? t.GetTypeInfo().GetCustomAttributes(attributeType, inherit).ToArray()
                    : t.GetTypeInfo().GetCustomAttributes(inherit).ToArray();
            }

            if (provider is Assembly) {
                Assembly a = (Assembly)provider;
                return (attributeType != null) ? a.GetCustomAttributes(attributeType).ToArray() : a.GetCustomAttributes().ToArray();
            }

            if (provider is MemberInfo) {
                MemberInfo m = (MemberInfo)provider;
                return (attributeType != null) ? m.GetCustomAttributes(attributeType, inherit).ToArray() : m.GetCustomAttributes(inherit).ToArray();
            }

            if (provider is Module) {
                Module m = (Module)provider;
                return (attributeType != null) ? m.GetCustomAttributes(attributeType).ToArray() : m.GetCustomAttributes().ToArray();
            }

            if (provider is ParameterInfo) {
                ParameterInfo p = (ParameterInfo)provider;
                return (attributeType != null) ? p.GetCustomAttributes(attributeType, inherit).ToArray() : p.GetCustomAttributes(inherit).ToArray();
            }

            throw new Exception("Cannot get attributes from '{0}'.".FormatWith(CultureInfo.InvariantCulture, provider));
        }
#endif

		/// <summary>
		/// Splits the name of the fully qualified type.
		/// </summary>
		/// <param name="fullyQualifiedTypeName">Fully qualified type name.</param>
		/// <param name="typeName">Type name.</param>
		/// <param name="assemblyName">Assembly name.</param>
        public static void SplitFullyQualifiedTypeName(string fullyQualifiedTypeName, out string typeName, out string assemblyName) {
            int? assemblyDelimiterIndex = GetAssemblyDelimiterIndex(fullyQualifiedTypeName);

            if (assemblyDelimiterIndex != null) {
                typeName = fullyQualifiedTypeName.Substring(0, assemblyDelimiterIndex.Value).Trim();
                assemblyName = fullyQualifiedTypeName.Substring(assemblyDelimiterIndex.Value + 1, fullyQualifiedTypeName.Length - assemblyDelimiterIndex.Value - 1).Trim();
            } else {
                typeName = fullyQualifiedTypeName;
                assemblyName = null;
            }
        }

		/// <summary>
		/// Gets the index of the assembly delimiter.
		/// </summary>
		/// <returns>The assembly delimiter index.</returns>
		/// <param name="fullyQualifiedTypeName">Fully qualified type name.</param>
        private static int? GetAssemblyDelimiterIndex(string fullyQualifiedTypeName) {
            // we need to get the first comma following all surrounded in brackets because of generic types
            // e.g. System.Collections.Generic.Dictionary`2[[System.String, mscorlib,Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
            int scope = 0;
            for (int i = 0; i < fullyQualifiedTypeName.Length; i++) {
                char current = fullyQualifiedTypeName[i];
                switch (current) {
                    case '[':
                        scope++;
                        break;
                    case ']':
                        scope--;
                        break;
                    case ',':
                        if (scope == 0)
                            return i;
                        break;
                }
            }

            return null;
        }

		/// <summary>
		/// Gets the type of the member info from.
		/// </summary>
		/// <returns>The member info from type.</returns>
		/// <param name="targetType">Target type.</param>
		/// <param name="memberInfo">Member info.</param>
        public static MemberInfo GetMemberInfoFromType(Type targetType, MemberInfo memberInfo) {
            const BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            switch (memberInfo.MemberType()) {
                case MemberTypes.Property:
                    PropertyInfo propertyInfo = (PropertyInfo)memberInfo;

                    Type[] types = propertyInfo.GetIndexParameters().Select(p => p.ParameterType).ToArray();

                    return targetType.GetProperty(propertyInfo.Name, bindingAttr, null, propertyInfo.PropertyType, types, null);
                default:
                    return targetType.GetMember(memberInfo.Name, memberInfo.MemberType(), bindingAttr).SingleOrDefault();
            }
        }

        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <returns>The fields.</returns>
        /// <param name="targetType">Target type.</param>
        /// <param name="bindingAttr">Binding attr.</param>
		public static IEnumerable<FieldInfo> GetFields(Type targetType, BindingFlags bindingAttr) {
            ValidationUtils.ArgumentNotNull(targetType, "targetType");

            List<MemberInfo> fieldInfos = new List<MemberInfo>(targetType.GetFields(bindingAttr));
#if !PORTABLE
            // Type.GetFields doesn't return inherited private fields
            // manually find private fields from base class
            GetChildPrivateFields(fieldInfos, targetType, bindingAttr);
#endif

            return fieldInfos.Cast<FieldInfo>();
        }

#if !PORTABLE
		/// <summary>
		/// Gets the child private fields.
		/// </summary>
		/// <param name="initialFields">Initial fields.</param>
		/// <param name="targetType">Target type.</param>
		/// <param name="bindingAttr">Binding attr.</param>
        private static void GetChildPrivateFields(IList<MemberInfo> initialFields, Type targetType, BindingFlags bindingAttr) {
            // fix weirdness with private FieldInfos only being returned for the current Type
            // find base type fields and add them to result
            if ((bindingAttr & BindingFlags.NonPublic) != 0) {
                // modify flags to not search for public fields
                BindingFlags nonPublicBindingAttr = bindingAttr.RemoveFlag(BindingFlags.Public);

                while ((targetType = targetType.BaseType()) != null) {
                    // filter out protected fields
                    IEnumerable<MemberInfo> childPrivateFields =
                        targetType.GetFields(nonPublicBindingAttr).Where(f => f.IsPrivate).Cast<MemberInfo>();

                    initialFields.AddRange(childPrivateFields);
                }
            }
        }
#endif

		/// <summary>
		/// Gets the properties.
		/// </summary>
		/// <returns>The properties.</returns>
		/// <param name="targetType">Target type.</param>
		/// <param name="bindingAttr">Binding attr.</param>
        public static IEnumerable<PropertyInfo> GetProperties(Type targetType, BindingFlags bindingAttr) {
            ValidationUtils.ArgumentNotNull(targetType, "targetType");

            List<PropertyInfo> propertyInfos = new List<PropertyInfo>(targetType.GetProperties(bindingAttr));

            // GetProperties on an interface doesn't return properties from its interfaces
            if (targetType.IsInterface()) {
                foreach (Type i in targetType.GetInterfaces()) {
                    propertyInfos.AddRange(i.GetProperties(bindingAttr));
                }
            }

            GetChildPrivateProperties(propertyInfos, targetType, bindingAttr);

            // a base class private getter/setter will be inaccessable unless the property was gotten from the base class
            for (int i = 0; i < propertyInfos.Count; i++) {
                PropertyInfo member = propertyInfos[i];
                if (member.DeclaringType != targetType) {
                    PropertyInfo declaredMember = (PropertyInfo)GetMemberInfoFromType(member.DeclaringType, member);
                    propertyInfos[i] = declaredMember;
                }
            }

            return propertyInfos;
        }

		/// <summary>
		/// Removes the flag.
		/// </summary>
		/// <returns>The flag.</returns>
		/// <param name="bindingAttr">Binding attr.</param>
		/// <param name="flag">Flag.</param>
        public static BindingFlags RemoveFlag(this BindingFlags bindingAttr, BindingFlags flag) {
            return ((bindingAttr & flag) == flag)
                ? bindingAttr ^ flag
                : bindingAttr;
        }

		/// <summary>
		/// Gets the child private properties.
		/// </summary>
		/// <param name="initialProperties">Initial properties.</param>
		/// <param name="targetType">Target type.</param>
		/// <param name="bindingAttr">Binding attr.</param>
        private static void GetChildPrivateProperties(IList<PropertyInfo> initialProperties, Type targetType, BindingFlags bindingAttr) {
            // fix weirdness with private PropertyInfos only being returned for the current Type
            // find base type properties and add them to result

            // also find base properties that have been hidden by subtype properties with the same name

            while ((targetType = targetType.BaseType()) != null) {
                foreach (PropertyInfo propertyInfo in targetType.GetProperties(bindingAttr)) {
                    PropertyInfo subTypeProperty = propertyInfo;

                    if (!IsPublic(subTypeProperty)) {
                        // have to test on name rather than reference because instances are different
                        // depending on the type that GetProperties was called on
                        int index = initialProperties.IndexOf(p => p.Name == subTypeProperty.Name);
                        if (index == -1) {
                            initialProperties.Add(subTypeProperty);
                        } else {
                            PropertyInfo childProperty = initialProperties[index];
                            // don't replace public child with private base
                            if (!IsPublic(childProperty)) {
                                // replace nonpublic properties for a child, but gotten from
                                // the parent with the one from the child
                                // the property gotten from the child will have access to private getter/setter
                                initialProperties[index] = subTypeProperty;
                            }
                        }
                    } else {
                        if (!subTypeProperty.IsVirtual()) {
                            int index = initialProperties.IndexOf(p => p.Name == subTypeProperty.Name
                                                                       && p.DeclaringType == subTypeProperty.DeclaringType);

                            if (index == -1)
                                initialProperties.Add(subTypeProperty);
                        } else {
                            int index = initialProperties.IndexOf(p => p.Name == subTypeProperty.Name
                                                                       && p.IsVirtual()
                                                                       && p.GetBaseDefinition() != null
                                                                       && p.GetBaseDefinition().DeclaringType.IsAssignableFrom(subTypeProperty.DeclaringType));

                            if (index == -1)
                                initialProperties.Add(subTypeProperty);
                        }
                    }
                }
            }
        }

		/// <summary>
		/// Ises the method overridden.
		/// </summary>
		/// <returns><c>true</c>, if method overridden was ised, <c>false</c> otherwise.</returns>
		/// <param name="currentType">Current type.</param>
		/// <param name="methodDeclaringType">Method declaring type.</param>
		/// <param name="method">Method.</param>
        public static bool IsMethodOverridden(Type currentType, Type methodDeclaringType, string method) {
            bool isMethodOverriden = currentType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Any(info =>
                    info.Name == method &&
                    // check that the method overrides the original on DynamicObjectProxy
                    info.DeclaringType != methodDeclaringType
                    && info.GetBaseDefinition().DeclaringType == methodDeclaringType
                );

            return isMethodOverriden;
        }

		/// <summary>
		/// Gets the default value.
		/// </summary>
		/// <returns>The default value.</returns>
		/// <param name="type">Type.</param>
        public static object GetDefaultValue(Type type) {
            if (!type.IsValueType())
                return null;

            switch (ConvertUtils.GetTypeCode(type)) {
                case PrimitiveTypeCode.Boolean:
                    return false;
                case PrimitiveTypeCode.Char:
                case PrimitiveTypeCode.SByte:
                case PrimitiveTypeCode.Byte:
                case PrimitiveTypeCode.Int16:
                case PrimitiveTypeCode.UInt16:
                case PrimitiveTypeCode.Int32:
                case PrimitiveTypeCode.UInt32:
                    return 0;
                case PrimitiveTypeCode.Int64:
                case PrimitiveTypeCode.UInt64:
                    return 0L;
                case PrimitiveTypeCode.Single:
                    return 0f;
                case PrimitiveTypeCode.Double:
                    return 0.0;
                case PrimitiveTypeCode.Decimal:
                    return 0m;
                case PrimitiveTypeCode.DateTime:
                    return new DateTime();
#if !(PORTABLE || PORTABLE40 || NET35 || NET20)
                case PrimitiveTypeCode.BigInteger:
                    return new BigInteger();
#endif
                case PrimitiveTypeCode.Guid:
                    return new Guid();
#if !NET20
                case PrimitiveTypeCode.DateTimeOffset:
                    return new DateTimeOffset();
#endif
            }

            if (IsNullable(type))
                return null;

            // possibly use IL initobj for perf here?
            return Activator.CreateInstance(type);
        }
    }
}