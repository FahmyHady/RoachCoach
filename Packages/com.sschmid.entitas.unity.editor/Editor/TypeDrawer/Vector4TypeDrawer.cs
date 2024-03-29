using System;
using UnityEditor;
using UnityEngine;

namespace Entitas.Unity.Editor
{
    public class Vector4TypeDrawer : ITypeDrawer
    {
        public bool HandlesType(Type type) => type == typeof(Vector4);

        public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target) =>
            EditorGUILayout.Vector4Field(memberName, (Vector4)value);
    }
}
