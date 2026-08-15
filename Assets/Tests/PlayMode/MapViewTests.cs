using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Pantheon.Unity;

namespace Pantheon.PlayTests
{
    public class MapViewTests
    {
        [UnityTest]
        public IEnumerator Update_FewerButtonsThanStageNodes_LogsWarning()
        {
            var mapGo = new GameObject();
            var mapView = mapGo.AddComponent<MapView>();
            var controllerGo = new GameObject();
            var combatRunner = controllerGo.AddComponent<CombatEncounterRunner>();
            var controller = controllerGo.AddComponent<RunController>();
            controller.CombatRunner = combatRunner;
            mapView.Controller = controller;

            // GreekStages.SampleStage() has 4 nodes; leave the pool empty so it's
            // always insufficient regardless of future content changes.
            mapView.NodeButtons = new List<Button>();

            LogAssert.Expect(LogType.Warning, new Regex("NodeButtons"));
            yield return null;

            Object.Destroy(mapGo);
            Object.Destroy(controllerGo);
        }
    }
}
