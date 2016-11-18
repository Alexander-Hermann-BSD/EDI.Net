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
using System.Reflection;
using System.Linq;

namespace indice.Edi.Utilities
{
	/// <summary>
	/// Type extensions.
	/// </summary>
    internal static class TypeExtensions
    {
#if DOTNET || PORTABLE
#if !DOTNET
		/// <summary>
		/// The default flags.
		/// </summary>
        private static BindingFlags DefaultFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;

		/// <summary>
		/// Gets the get method.
		/// </summary>
		/// <returns>The get method.</returns>
		/// <param name="propertyInfo">Property info.</param>
        public static MethodInfo GetGetMethod(this PropertyInfo propertyInfo)
        {
            return propertyInfo.GetGetMethod(false);
        }

		/// <summary>
		/// Gets the get method.
		/// </summary>
		/// <returns>The get method.</returns>
		/// <param name="propertyInfo">Property info.</param>
		/// <param name="nonPublic">If set to <c>true</c> non public.</param>
        public static MethodInfo GetGetMethod(this PropertyInfo propertyInfo, bool nonPublic)
        {
            MethodInfo getMethod = propertyInfo.GetMethod;
            if (getMethod != null && (getMethod.IsPublic || nonPublic))
                return getMethod;

            return null;
        }

		/// <summary>
		/// Gets the set method.
		/// </summary>
		/// <returns>The set method.</returns>
		/// <param name="propertyInfo">Property info.</param>
        public static MethodInfo GetSetMethod(this PropertyInfo propertyInfo)
        {
            return propertyInfo.GetSetMethod(false);
        }

		/// <summary>
		/// Gets the set method.
		/// </summary>
		/// <returns>The set method.</returns>
		/// <param name="propertyInfo">Property info.</param>
		/// <param name="nonPublic">If set to <c>true</c> non public.</param>
        public static MethodInfo GetSetMethod(this PropertyInfo propertyInfo, bool nonPublic)
        {
            MethodInfo setMethod = propertyInfo.SetMethod;
            if (setMethod != null && (setMethod.IsPublic || nonPublic))
                return setMethod;

            return null;
        }
#endif

		/// <summary>
		/// Ises the subclass of.
		/// </summary>
		/// <returns><c>true</c>, if subclass of was ised, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
		/// <param name="c">C.</param>
        public static bool IsSubclassOf(this Type type, Type c)
        {
            return type.GetTypeInfo().IsSubclassOf(c);
        }

#if !DOTNET
		/// <summary>
		/// Ises the assignable from.
		/// </summary>
		/// <returns><c>true</c>, if assignable from was ised, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
		/// <param name="c">C.</param>
        public static bool IsAssignableFrom(this Type type, Type c)
        {
            return type.GetTypeInfo().IsAssignableFrom(c.GetTypeInfo());
        }
#endif
		/// <summary>
		/// Ises the type of the instance of.
		/// </summary>
		/// <returns><c>true</c>, if instance of type was ised, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
		/// <param name="o">O.</param>
        public static bool IsInstanceOfType(this Type type, object o)
        {
            if (o == null)
                return false;

            return type.IsAssignableFrom(o.GetType());
        }
#endif
		/// <summary>
		/// Method the specified d.
		/// </summary>
		/// <param name="d">D.</param>
        public static MethodInfo Method(this Delegate d)
        {
#if !(DOTNET || PORTABLE)
            return d.Method;
#else
            return d.GetMethodInfo();
#endif
        }

		/// <summary>
		/// Members the type.
		/// </summary>
		/// <returns>The type.</returns>
		/// <param name="memberInfo">Member info.</param>
        public static MemberTypes MemberType(this MemberInfo memberInfo)
        {
#if !(DOTNET || PORTABLE || PORTABLE40)
            return memberInfo.MemberType;
#else
            if (memberInfo is PropertyInfo)
                return MemberTypes.Property;
            else if (memberInfo is FieldInfo)
                return MemberTypes.Field;
            else if (memberInfo is EventInfo)
                return MemberTypes.Event;
            else if (memberInfo is MethodInfo)
                return MemberTypes.Method;
            else
                return MemberTypes.Other;
#endif
        }

		/// <summary>
		/// Containses the generic parameters.
		/// </summary>
		/// <returns><c>true</c>, if generic parameters was containsed, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
        public static bool ContainsGenericParameters(this Type type)
        {
#if !(DOTNET || PORTABLE)
            return type.ContainsGenericParameters;
#else
            return type.GetTypeInfo().ContainsGenericParameters;
#endif
        }

		/// <summary>
		/// Ises the interface.
		/// </summary>
		/// <returns><c>true</c>, if interface was ised, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
        public static bool IsInterface(this Type type)
        {
#if !(DOTNET || PORTABLE)
            return type.IsInterface;
#else
            return type.GetTypeInfo().IsInterface;
#endif
        }

		/// <summary>
		/// Ises the type of the generic.
		/// </summary>
		/// <returns><c>true</c>, if generic type was ised, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
        public static bool IsGenericType(this Type type)
        {
#if !(DOTNET || PORTABLE)
            return type.IsGenericType;
#else
            return type.GetTypeInfo().IsGenericType;
#endif
        }

		/// <summary>
		/// Ises the generic type definition.
		/// </summary>
		/// <returns><c>true</c>, if generic type definition was ised, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
        public static bool IsGenericTypeDefinition(this Type type)
        {
#if !(DOTNET || PORTABLE)
            return type.IsGenericTypeDefinition;
#else
            return type.GetTypeInfo().IsGenericTypeDefinition;
#endif
        }

		/// <summary>
		/// Bases the type.
		/// </summary>
		/// <returns>The type.</returns>
		/// <param name="type">Type.</param>
        public static Type BaseType(this Type type)
        {
#if !(DOTNET || PORTABLE)
            return type.BaseType;
#else
            return type.GetTypeInfo().BaseType;
#endif
        }

		/// <summary>
		/// Assembly the specified type.
		/// </summary>
		/// <param name="type">Type.</param>
        public static Assembly Assembly(this Type type)
        {
#if !(DOTNET || PORTABLE)
            return type.Assembly;
#else
            return type.GetTypeInfo().Assembly;
#endif
        }

		/// <summary>
		/// Ises the enum.
		/// </summary>
		/// <returns><c>true</c>, if enum was ised, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
        public static bool IsEnum(this Type type)
        {
#if !(DOTNET || PORTABLE)
            return type.IsEnum;
#else
            return type.GetTypeInfo().IsEnum;
#endif
        }

		/// <summary>
		/// Ises the class.
		/// </summary>
		/// <returns><c>true</c>, if class was ised, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
        public static bool IsClass(this Type type)
        {
#if !(DOTNET || PORTABLE)
            return type.IsClass;
#else
            return type.GetTypeInfo().IsClass;
#endif
        }

		/// <summary>
		/// Ises the sealed.
		/// </summary>
		/// <returns><c>true</c>, if sealed was ised, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
        public static bool IsSealed(this Type type)
        {
#if !(DOTNET || PORTABLE)
            return type.IsSealed;
#else
            return type.GetTypeInfo().IsSealed;
#endif
        }

#if (PORTABLE40 || DOTNET || PORTABLE)
        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <returns>The property.</returns>
        /// <param name="type">Type.</param>
        /// <param name="name">Name.</param>
        /// <param name="bindingFlags">Binding flags.</param>
        /// <param name="placeholder1">Placeholder1.</param>
        /// <param name="propertyType">Property type.</param>
        /// <param name="indexParameters">Index parameters.</param>
        /// <param name="placeholder2">Placeholder2.</param>
		public static PropertyInfo GetProperty(this Type type, string name, BindingFlags bindingFlags, object placeholder1, Type propertyType, IList<Type> indexParameters, object placeholder2)
        {
            IEnumerable<PropertyInfo> propertyInfos = type.GetProperties(bindingFlags);

            return propertyInfos.Where(p =>
            {
                if (name != null && name != p.Name)
                    return false;
                if (propertyType != null && propertyType != p.PropertyType)
                    return false;
                if (indexParameters != null)
                {
                    if (!p.GetIndexParameters().Select(ip => ip.ParameterType).SequenceEqual(indexParameters))
                        return false;
                }

                return true;
            }).SingleOrDefault();
        }

		/// <summary>
		/// Gets the member.
		/// </summary>
		/// <returns>The member.</returns>
		/// <param name="type">Type.</param>
		/// <param name="name">Name.</param>
		/// <param name="memberType">Member type.</param>
		/// <param name="bindingFlags">Binding flags.</param>
        public static IEnumerable<MemberInfo> GetMember(this Type type, string name, MemberTypes memberType, BindingFlags bindingFlags)
        {
            return type.GetMember(name, bindingFlags).Where(m =>
            {
                if (m.MemberType() != memberType)
                    return false;

                return true;
            });
        }
#endif

#if (DOTNET || PORTABLE)
        /// <summary>
        /// Gets the base definition.
        /// </summary>
        /// <returns>The base definition.</returns>
        /// <param name="method">Method.</param>
		public static MethodInfo GetBaseDefinition(this MethodInfo method)
        {
            return method.GetRuntimeBaseDefinition();
        }
#endif

#if (DOTNET || PORTABLE)
        /// <summary>
        /// Ises the defined.
        /// </summary>
        /// <returns><c>true</c>, if defined was ised, <c>false</c> otherwise.</returns>
        /// <param name="type">Type.</param>
        /// <param name="attributeType">Attribute type.</param>
        /// <param name="inherit">If set to <c>true</c> inherit.</param>
		public static bool IsDefined(this Type type, Type attributeType, bool inherit)
        {
            return type.GetTypeInfo().CustomAttributes.Any(a => a.AttributeType == attributeType);
        }

#if !DOTNET
		/// <summary>
		/// Gets the method.
		/// </summary>
		/// <returns>The method.</returns>
		/// <param name="type">Type.</param>
		/// <param name="name">Name.</param>
        public static MethodInfo GetMethod(this Type type, string name)
        {
            return type.GetMethod(name, DefaultFlags);
        }

		/// <summary>
		/// Gets the method.
		/// </summary>
		/// <returns>The method.</returns>
		/// <param name="type">Type.</param>
		/// <param name="name">Name.</param>
		/// <param name="bindingFlags">Binding flags.</param>
        public static MethodInfo GetMethod(this Type type, string name, BindingFlags bindingFlags)
        {
            return type.GetTypeInfo().GetDeclaredMethod(name);
        }

		/// <summary>
		/// Gets the method.
		/// </summary>
		/// <returns>The method.</returns>
		/// <param name="type">Type.</param>
		/// <param name="parameterTypes">Parameter types.</param>
        public static MethodInfo GetMethod(this Type type, IList<Type> parameterTypes)
        {
            return type.GetMethod(null, parameterTypes);
        }

		/// <summary>
		/// Gets the method.
		/// </summary>
		/// <returns>The method.</returns>
		/// <param name="type">Type.</param>
		/// <param name="name">Name.</param>
		/// <param name="parameterTypes">Parameter types.</param>
        public static MethodInfo GetMethod(this Type type, string name, IList<Type> parameterTypes)
        {
            return type.GetMethod(name, DefaultFlags, null, parameterTypes, null);
        }

		/// <summary>
		/// Gets the method.
		/// </summary>
		/// <returns>The method.</returns>
		/// <param name="type">Type.</param>
		/// <param name="name">Name.</param>
		/// <param name="bindingFlags">Binding flags.</param>
		/// <param name="placeHolder1">Place holder1.</param>
		/// <param name="parameterTypes">Parameter types.</param>
		/// <param name="placeHolder2">Place holder2.</param>
        public static MethodInfo GetMethod(this Type type, string name, BindingFlags bindingFlags, object placeHolder1, IList<Type> parameterTypes, object placeHolder2)
        {
            return type.GetTypeInfo().DeclaredMethods.Where(m =>
                                                                {
                                                                    if (name != null && m.Name != name)
                                                                        return false;

                                                                    if (!TestAccessibility(m, bindingFlags))
                                                                        return false;

                                                                    return m.GetParameters().Select(p => p.ParameterType).SequenceEqual(parameterTypes);
                                                                }).SingleOrDefault();
        }

		/// <summary>
		/// Gets the constructors.
		/// </summary>
		/// <returns>The constructors.</returns>
		/// <param name="type">Type.</param>
        public static IEnumerable<ConstructorInfo> GetConstructors(this Type type)
        {
            return type.GetConstructors(DefaultFlags);
        }

		/// <summary>
		/// Gets the constructors.
		/// </summary>
		/// <returns>The constructors.</returns>
		/// <param name="type">Type.</param>
		/// <param name="bindingFlags">Binding flags.</param>
        public static IEnumerable<ConstructorInfo> GetConstructors(this Type type, BindingFlags bindingFlags)
        {
            return type.GetConstructors(bindingFlags, null);
        }

		/// <summary>
		/// Gets the constructors.
		/// </summary>
		/// <returns>The constructors.</returns>
		/// <param name="type">Type.</param>
		/// <param name="bindingFlags">Binding flags.</param>
		/// <param name="parameterTypes">Parameter types.</param>
        private static IEnumerable<ConstructorInfo> GetConstructors(this Type type, BindingFlags bindingFlags, IList<Type> parameterTypes)
        {
            return type.GetTypeInfo().DeclaredConstructors.Where(c =>
                                                                     {
                                                                         if (!TestAccessibility(c, bindingFlags))
                                                                             return false;

                                                                         if (parameterTypes != null && !c.GetParameters().Select(p => p.ParameterType).SequenceEqual(parameterTypes))
                                                                             return false;

                                                                         return true;
                                                                     });
        }

		/// <summary>
		/// Gets the constructor.
		/// </summary>
		/// <returns>The constructor.</returns>
		/// <param name="type">Type.</param>
		/// <param name="parameterTypes">Parameter types.</param>
        public static ConstructorInfo GetConstructor(this Type type, IList<Type> parameterTypes)
        {
            return type.GetConstructor(DefaultFlags, null, parameterTypes, null);
        }

		/// <summary>
		/// Gets the constructor.
		/// </summary>
		/// <returns>The constructor.</returns>
		/// <param name="type">Type.</param>
		/// <param name="bindingFlags">Binding flags.</param>
		/// <param name="placeholder1">Placeholder1.</param>
		/// <param name="parameterTypes">Parameter types.</param>
		/// <param name="placeholder2">Placeholder2.</param>
        public static ConstructorInfo GetConstructor(this Type type, BindingFlags bindingFlags, object placeholder1, IList<Type> parameterTypes, object placeholder2)
        {
            return type.GetConstructors(bindingFlags, parameterTypes).SingleOrDefault();
        }

		/// <summary>
		/// Gets the member.
		/// </summary>
		/// <returns>The member.</returns>
		/// <param name="type">Type.</param>
		/// <param name="member">Member.</param>
        public static MemberInfo[] GetMember(this Type type, string member)
        {
            return type.GetMember(member, DefaultFlags);
        }

		/// <summary>
		/// Gets the member.
		/// </summary>
		/// <returns>The member.</returns>
		/// <param name="type">Type.</param>
		/// <param name="member">Member.</param>
		/// <param name="bindingFlags">Binding flags.</param>
        public static MemberInfo[] GetMember(this Type type, string member, BindingFlags bindingFlags)
        {
            return type.GetTypeInfo().GetMembersRecursive().Where(m => m.Name == member && TestAccessibility(m, bindingFlags)).ToArray();
        }

		/// <summary>
		/// Gets the field.
		/// </summary>
		/// <returns>The field.</returns>
		/// <param name="type">Type.</param>
		/// <param name="member">Member.</param>
        public static MemberInfo GetField(this Type type, string member)
        {
            return type.GetField(member, DefaultFlags);
        }

		/// <summary>
		/// Gets the field.
		/// </summary>
		/// <returns>The field.</returns>
		/// <param name="type">Type.</param>
		/// <param name="member">Member.</param>
		/// <param name="bindingFlags">Binding flags.</param>
        public static MemberInfo GetField(this Type type, string member, BindingFlags bindingFlags)
        {
            return type.GetTypeInfo().GetDeclaredField(member);
        }

		/// <summary>
		/// Gets the properties.
		/// </summary>
		/// <returns>The properties.</returns>
		/// <param name="type">Type.</param>
		/// <param name="bindingFlags">Binding flags.</param>
        public static IEnumerable<PropertyInfo> GetProperties(this Type type, BindingFlags bindingFlags)
        {
            IList<PropertyInfo> properties = (bindingFlags.HasFlag(BindingFlags.DeclaredOnly))
                                                 ? type.GetTypeInfo().DeclaredProperties.ToList()
                                                 : type.GetTypeInfo().GetPropertiesRecursive();

            return properties.Where(p => TestAccessibility(p, bindingFlags));
        }

		/// <summary>
		/// Gets the members recursive.
		/// </summary>
		/// <returns>The members recursive.</returns>
		/// <param name="type">Type.</param>
        private static IList<MemberInfo> GetMembersRecursive(this TypeInfo type)
        {
            TypeInfo t = type;
            IList<MemberInfo> members = new List<MemberInfo>();
            while (t != null)
            {
                foreach (var member in t.DeclaredMembers)
                {
                    if (!members.Any(p => p.Name == member.Name))
                        members.Add(member);
                }
                t = (t.BaseType != null) ? t.BaseType.GetTypeInfo() : null;
            }

            return members;
        }

		/// <summary>
		/// Gets the properties recursive.
		/// </summary>
		/// <returns>The properties recursive.</returns>
		/// <param name="type">Type.</param>
        private static IList<PropertyInfo> GetPropertiesRecursive(this TypeInfo type)
        {
            TypeInfo t = type;
            IList<PropertyInfo> properties = new List<PropertyInfo>();
            while (t != null)
            {
                foreach (var member in t.DeclaredProperties)
                {
                    if (!properties.Any(p => p.Name == member.Name))
                        properties.Add(member);
                }
                t = (t.BaseType != null) ? t.BaseType.GetTypeInfo() : null;
            }

            return properties;
        }

		/// <summary>
		/// Gets the fields recursive.
		/// </summary>
		/// <returns>The fields recursive.</returns>
		/// <param name="type">Type.</param>
        private static IList<FieldInfo> GetFieldsRecursive(this TypeInfo type)
        {
            TypeInfo t = type;
            IList<FieldInfo> fields = new List<FieldInfo>();
            while (t != null)
            {
                foreach (var member in t.DeclaredFields)
                {
                    if (!fields.Any(p => p.Name == member.Name))
                        fields.Add(member);
                }
                t = (t.BaseType != null) ? t.BaseType.GetTypeInfo() : null;
            }

            return fields;
        }

		/// <summary>
		/// Gets the methods.
		/// </summary>
		/// <returns>The methods.</returns>
		/// <param name="type">Type.</param>
		/// <param name="bindingFlags">Binding flags.</param>
        public static IEnumerable<MethodInfo> GetMethods(this Type type, BindingFlags bindingFlags)
        {
            return type.GetTypeInfo().DeclaredMethods;
        }

		/// <summary>
		/// Gets the property.
		/// </summary>
		/// <returns>The property.</returns>
		/// <param name="type">Type.</param>
		/// <param name="name">Name.</param>
        public static PropertyInfo GetProperty(this Type type, string name)
        {
            return type.GetProperty(name, DefaultFlags);
        }

		/// <summary>
		/// Gets the property.
		/// </summary>
		/// <returns>The property.</returns>
		/// <param name="type">Type.</param>
		/// <param name="name">Name.</param>
		/// <param name="bindingFlags">Binding flags.</param>
        public static PropertyInfo GetProperty(this Type type, string name, BindingFlags bindingFlags)
        {
            return type.GetTypeInfo().GetDeclaredProperty(name);
        }

		/// <summary>
		/// Gets the fields.
		/// </summary>
		/// <returns>The fields.</returns>
		/// <param name="type">Type.</param>
        public static IEnumerable<FieldInfo> GetFields(this Type type)
        {
            return type.GetFields(DefaultFlags);
        }

		/// <summary>
		/// Gets the fields.
		/// </summary>
		/// <returns>The fields.</returns>
		/// <param name="type">Type.</param>
		/// <param name="bindingFlags">Binding flags.</param>
        public static IEnumerable<FieldInfo> GetFields(this Type type, BindingFlags bindingFlags)
        {
            IList<FieldInfo> fields = (bindingFlags.HasFlag(BindingFlags.DeclaredOnly))
                                          ? type.GetTypeInfo().DeclaredFields.ToList()
                                          : type.GetTypeInfo().GetFieldsRecursive();

            return fields.Where(f => TestAccessibility(f, bindingFlags)).ToList();
        }

		/// <summary>
		/// Tests the accessibility.
		/// </summary>
		/// <returns><c>true</c>, if accessibility was tested, <c>false</c> otherwise.</returns>
		/// <param name="member">Member.</param>
		/// <param name="bindingFlags">Binding flags.</param>
        private static bool TestAccessibility(PropertyInfo member, BindingFlags bindingFlags)
        {
            if (member.GetMethod != null && TestAccessibility(member.GetMethod, bindingFlags))
                return true;

            if (member.SetMethod != null && TestAccessibility(member.SetMethod, bindingFlags))
                return true;

            return false;
        }

		/// <summary>
		/// Tests the accessibility.
		/// </summary>
		/// <returns><c>true</c>, if accessibility was tested, <c>false</c> otherwise.</returns>
		/// <param name="member">Member.</param>
		/// <param name="bindingFlags">Binding flags.</param>
        private static bool TestAccessibility(MemberInfo member, BindingFlags bindingFlags)
        {
            if (member is FieldInfo)
            {
                return TestAccessibility((FieldInfo) member, bindingFlags);
            }
            else if (member is MethodBase)
            {
                return TestAccessibility((MethodBase) member, bindingFlags);
            }
            else if (member is PropertyInfo)
            {
                return TestAccessibility((PropertyInfo) member, bindingFlags);
            }

            throw new Exception("Unexpected member type.");
        }

		/// <summary>
		/// Tests the accessibility.
		/// </summary>
		/// <returns><c>true</c>, if accessibility was tested, <c>false</c> otherwise.</returns>
		/// <param name="member">Member.</param>
		/// <param name="bindingFlags">Binding flags.</param>
        private static bool TestAccessibility(FieldInfo member, BindingFlags bindingFlags)
        {
            bool visibility = (member.IsPublic && bindingFlags.HasFlag(BindingFlags.Public)) ||
                              (!member.IsPublic && bindingFlags.HasFlag(BindingFlags.NonPublic));

            bool instance = (member.IsStatic && bindingFlags.HasFlag(BindingFlags.Static)) ||
                            (!member.IsStatic && bindingFlags.HasFlag(BindingFlags.Instance));

            return visibility && instance;
        }

		/// <summary>
		/// Tests the accessibility.
		/// </summary>
		/// <returns><c>true</c>, if accessibility was tested, <c>false</c> otherwise.</returns>
		/// <param name="member">Member.</param>
		/// <param name="bindingFlags">Binding flags.</param>
        private static bool TestAccessibility(MethodBase member, BindingFlags bindingFlags)
        {
            bool visibility = (member.IsPublic && bindingFlags.HasFlag(BindingFlags.Public)) ||
                              (!member.IsPublic && bindingFlags.HasFlag(BindingFlags.NonPublic));

            bool instance = (member.IsStatic && bindingFlags.HasFlag(BindingFlags.Static)) ||
                            (!member.IsStatic && bindingFlags.HasFlag(BindingFlags.Instance));

            return visibility && instance;
        }

		/// <summary>
		/// Gets the generic arguments.
		/// </summary>
		/// <returns>The generic arguments.</returns>
		/// <param name="type">Type.</param>
        public static Type[] GetGenericArguments(this Type type)
        {
            return type.GetTypeInfo().GenericTypeArguments;
        }

		/// <summary>
		/// Gets the interfaces.
		/// </summary>
		/// <returns>The interfaces.</returns>
		/// <param name="type">Type.</param>
        public static IEnumerable<Type> GetInterfaces(this Type type)
        {
            return type.GetTypeInfo().ImplementedInterfaces;
        }

		/// <summary>
		/// Gets the methods.
		/// </summary>
		/// <returns>The methods.</returns>
		/// <param name="type">Type.</param>
        public static IEnumerable<MethodInfo> GetMethods(this Type type)
        {
            return type.GetTypeInfo().DeclaredMethods;
        }
#endif
#endif

		/// <summary>
		/// Ises the abstract.
		/// </summary>
		/// <returns><c>true</c>, if abstract was ised, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
        public static bool IsAbstract(this Type type)
        {
#if !(DOTNET || PORTABLE)
            return type.IsAbstract;
#else
            return type.GetTypeInfo().IsAbstract;
#endif
        }

		/// <summary>
		/// Ises the visible.
		/// </summary>
		/// <returns><c>true</c>, if visible was ised, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
        public static bool IsVisible(this Type type)
        {
#if !(DOTNET || PORTABLE)
            return type.IsVisible;
#else
            return type.GetTypeInfo().IsVisible;
#endif
        }

		/// <summary>
		/// Ises the type of the value.
		/// </summary>
		/// <returns><c>true</c>, if value type was ised, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
        public static bool IsValueType(this Type type)
        {
#if !(DOTNET || PORTABLE)
            return type.IsValueType;
#else
            return type.GetTypeInfo().IsValueType;
#endif
        }

		/// <summary>
		/// Assignables the name of the to type.
		/// </summary>
		/// <returns><c>true</c>, if to type name was assignabled, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
		/// <param name="fullTypeName">Full type name.</param>
		/// <param name="match">Match.</param>
        public static bool AssignableToTypeName(this Type type, string fullTypeName, out Type match)
        {
            Type current = type;

            while (current != null)
            {
                if (string.Equals(current.FullName, fullTypeName, StringComparison.Ordinal))
                {
                    match = current;
                    return true;
                }

                current = current.BaseType();
            }

            foreach (Type i in type.GetInterfaces())
            {
                if (string.Equals(i.Name, fullTypeName, StringComparison.Ordinal))
                {
                    match = type;
                    return true;
                }
            }

            match = null;
            return false;
        }

		/// <summary>
		/// Assignables the name of the to type.
		/// </summary>
		/// <returns><c>true</c>, if to type name was assignabled, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
		/// <param name="fullTypeName">Full type name.</param>
        public static bool AssignableToTypeName(this Type type, string fullTypeName)
        {
            Type match;
            return type.AssignableToTypeName(fullTypeName, out match);
        }
    }
}