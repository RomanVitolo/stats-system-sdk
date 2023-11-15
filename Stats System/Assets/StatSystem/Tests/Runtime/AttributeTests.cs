using System.Collections;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace StatSystem.Tests
{
    public class AttributeTests
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            EditorSceneManager.LoadSceneInPlayMode("Assets/StatSystem/Tests/Scenes/Test.unity", 
                new LoadSceneParameters(LoadSceneMode.Single));
        }

        [UnityTest]
        public IEnumerator Attribute_WhenModifierApplied_DoesNotExceedMaxValue()
        {
            yield return null;
            StatController statController = GameObject.FindObjectOfType<StatController>();
            Attribute health = statController.Stats["Health"] as Attribute;
            Assert.AreEqual(100, health.currentValue);
            Assert.AreEqual(100, health.Value);
            health.ApplyModifier(new StatModifier
            {
                Magnitude = 20,
                Type = ModifierOperationType.Additive
            });
            Assert.AreEqual(100, health.currentValue);
        }
        
        [UnityTest]
        [CanBeNull]
        public IEnumerator Attribute_WhenModifierApplied_DoesNotGoBelowZero()
        {
            yield return null;
            StatController statController = GameObject.FindObjectOfType<StatController>();
            Attribute health = statController.Stats["Health"] as Attribute;
            Assert.AreEqual(100, health.currentValue);
            health.ApplyModifier(new StatModifier
            {
                Magnitude = -150,
                Type = ModifierOperationType.Additive
            });
            Assert.AreEqual(0, health.currentValue);
        }    
    }
}