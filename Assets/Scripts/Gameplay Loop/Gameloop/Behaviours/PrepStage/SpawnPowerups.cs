using UnityEngine;
using TrojanMouse.Gruttel;
using UnityEngine.UI;

namespace TrojanMouse.GameplayLoop
{
    public class SpawnPowerups : GLNode
    {
        GameObject powerupObj;
        Level.PowerupsToBeDispenced[] nanaBetsys;
        Transform storage;

        bool hasSpawned;
        public SpawnPowerups(GameObject powerupObj, Level.PowerupsToBeDispenced[] powerups, Transform powerupStorage)
        {
            GruttelData.staticsInitialized = false;
            this.powerupObj = powerupObj;
            this.nanaBetsys = powerups;
            this.storage = powerupStorage;
        }

        public override NodeState Evaluate()
        {
            if (hasSpawned)
            {
                return NodeState.SUCCESS;
            }

            for (int i = 0; i < nanaBetsys.Length; i++){
                GameObject powerup = GameLoopBT.instance.SpawnObj(powerupObj, Vector3.zero, Quaternion.identity, storage);
                powerup.GetComponent<DragUI>().powerupType = nanaBetsys[i].powerupType;
                powerup.GetComponent<Image>().sprite = nanaBetsys[i].image;
            }
            hasSpawned = true;
            return NodeState.SUCCESS;
        }
    }
}