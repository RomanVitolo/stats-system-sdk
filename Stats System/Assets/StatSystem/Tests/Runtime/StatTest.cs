using System.Collections;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace StatSystem.Tests
{
    public class StatTest
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            EditorSceneManager.LoadSceneInPlayMode("Assets/StatSystem/Tests/Scenes/Test.unity", 
                new LoadSceneParameters(LoadSceneMode.Single));
        }
        
        [UnityTest]
        public IEnumerator Stat_WhenModifierApplied_ChangesValue()
        {
            yield return null;
            StatController statController = GameObject.FindObjectOfType<StatController>();
            Stat physicalAttack = statController.Stats["PhysicalAttack"];
            Assert.AreEqual(0, physicalAttack.BaseValue);
            physicalAttack.AddModifier(new StatModifier
            {
                Magnitude = 5,
                Type = ModifierOperationType.Additive
            });
            Assert.AreEqual(5, physicalAttack.Value);
        }

        [UnityTest]
        public IEnumerator Stat_WhenModifierApplied_DoesNotExceedCap()
        {
            yield return null;
            StatController statController = GameObject.FindObjectOfType<StatController>();
            Stat attackSpeed = statController.Stats["AttackSpeed"];
            Assert.AreEqual(1, attackSpeed.Value);
            attackSpeed.AddModifier(new StatModifier
            {
                Magnitude = 5,
                Type = ModifierOperationType.Additive      
            });
            Assert.AreEqual(3, attackSpeed.Value);
        }

        [UnityTest]
        public IEnumerator Stat_WhenStrengthIncreased_UpdatePhysicalAttack()
        {
            yield return null;
            StatController statController = GameObject.FindObjectOfType<StatController>();
            PrimaryStat strength = statController.Stats["Strength"] as PrimaryStat;
            Stat physicalAttack = statController.Stats["PhysicalAttack"];
            Assert.AreEqual(1, strength.Value);
            Assert.AreEqual(3, physicalAttack.Value);
            strength.Add(3);
            Assert.AreEqual(12 , physicalAttack.Value);
        }               
    } 
}          
