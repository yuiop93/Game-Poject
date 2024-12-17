using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
    [TaskCategory("Unity/NavMeshAgent")]
    [TaskDescription("Checks if the distance between the agent's position and the destination exceeds a specified threshold. Returns Success if within range, Failure otherwise.")]
    public class CheckDistanceThreshold : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;

        [Tooltip("The distance threshold to compare against.")]
        public SharedFloat distanceThreshold;

        // Cache the NavMeshAgent component
        private NavMeshAgent navMeshAgent;
        private GameObject prevGameObject;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject)
            {
                navMeshAgent = currentGameObject.GetComponent<NavMeshAgent>();
                prevGameObject = currentGameObject;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (navMeshAgent == null)
            {
                Debug.LogWarning("NavMeshAgent is null");
                return TaskStatus.Failure;
            }

            // Check if the remaining distance exceeds the threshold
            if (navMeshAgent.remainingDistance > distanceThreshold.Value)
            {
                return TaskStatus.Failure;
            }
            if (navMeshAgent.remainingDistance <= distanceThreshold.Value)
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            distanceThreshold = 0;
        }
    }
}
