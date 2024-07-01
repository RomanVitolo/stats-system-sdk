using System.Collections;
using LevelSystem;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace StatSystem.Tests
{
    public class PlayerStatControllerTests
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            EditorSceneManager.LoadSceneInPlayMode("Assets/StatSystem/Tests/Scenes/Test.unity", new LoadSceneParameters(LoadSceneMode.Single));
        }
        
        [UnityTest]
        public IEnumerator PlayerStatController_WhenLevelUp_GainStatPoints()
        {
            yield return null;
            PlayerStatController playerStatController = GameObject.FindObjectOfType<PlayerStatController>();
            ILevelable levelable = playerStatController.GetComponent<ILevelable>();
            Assert.AreEqual(5, playerStatController.StatPoints);
            Assert.AreEqual(1, levelable.Level);
            levelable.CurrentExperience += 100;
            Assert.AreEqual(2, levelable.Level);
            Assert.AreEqual(10, playerStatController.StatPoints);
        }
    }
}