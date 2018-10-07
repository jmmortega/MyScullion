﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace MyScullion
{
    public static class CustomDependencyService
    {
        private static Dictionary<Type, object> instances = new Dictionary<Type, object>();

        public static void Register(Type type)
        {
            var instance = Activator.CreateInstance(type);
            var implementationType = type.GetInterfaces().FirstOrDefault();
            
            if(instances.ContainsKey(implementationType))
            {
                instances[implementationType] = instance;
            }
            else
            {
                instances.Add(implementationType, instance);
            }
        }

        public static void Register<T>()
        {
            Register(typeof(T));
        }

        public static T Get<T>()
        {
            var implementationType = typeof(T).GetInterfaces().FirstOrDefault();
            if (instances.ContainsKey(implementationType))
            {
                return (T)instances[typeof(T)];
            }
            else
            {
                return default(T);
            }
        }
    }
}