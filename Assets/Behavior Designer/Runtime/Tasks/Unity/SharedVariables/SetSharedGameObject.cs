namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
    [TaskCategory("Unity/SharedVariable")]
    [TaskDescription("Sets the SharedGameObject variable to the specified object. Returns Success.")]
    public class SetSharedGameObject : Action
    {
        [Tooltip("The value to set the SharedGameObject to. If null the variable will be set to the current GameObject")]
        public SharedGameObject targetValue;
        [RequiredField]
        [Tooltip("The SharedGameObject to set")]
        public SharedGameObject targetVariable;
        [Tooltip("Can the target value be null?")]
        public SharedBool valueCanBeNull;

        public override TaskStatus OnUpdate()
        {
            // 如果 targetValue.Value 為 null 且不允許空值，返回失敗
            if (targetValue.Value == null && !valueCanBeNull.Value)
            {
                return TaskStatus.Failure;
            }

            // 設置 targetVariable 的值
            targetVariable.Value = targetValue.Value != null ? targetValue.Value : gameObject;

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            valueCanBeNull = false;
            targetValue = null;
            targetVariable = null;
        }
    }
}