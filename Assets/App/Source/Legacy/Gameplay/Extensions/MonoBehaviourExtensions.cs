using System;
using UnityEngine;

namespace Systemagedon.App.Extensions
{

    public static class MonoBehaviourExtensions
    {
        public static void AssignInterfaceField<TInterface>(this MonoBehaviour _,
            ref GameObject objectField, ref TInterface componentField, string name)
        {
            if (!objectField) return;
            componentField = objectField.GetComponent<TInterface>();
            if (componentField == null)
            {
                Debug.LogError($"{name} must have component that implements " +
                    $"{typeof(TInterface).Name}");
            }
        }
    }

}
