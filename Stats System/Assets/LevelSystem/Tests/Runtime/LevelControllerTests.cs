using System.Collections;  
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace LevelSystem.Tests
{
    public class LevelControllerTests
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            EditorSceneManager.LoadSceneInPlayMode("Assets/LevelSystem/Tests/Scenes/Test.unity", new LoadSceneParameters(LoadSceneMode.Single));
        }

        [UnityTest]
        public IEnumerator LevelController_WhenLevelUp_UpdateRequiredFormulaExperience()
        {
            yield return null;
            LevelController levelController = GameObject.FindObjectOfType<LevelController>();
            Assert.AreEqual(1, levelController.Level);
            levelController.CurrentExperience += 83;
            Assert.AreEqual(2, levelController.Level);
            Assert.AreEqual(0, levelController.CurrentExperience);
            Assert.AreEqual(92, levelController.RequiredExperience);
        }
    }
}