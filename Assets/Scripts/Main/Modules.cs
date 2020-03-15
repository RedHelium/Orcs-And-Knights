using UnityEngine;

namespace Main
{
    /// <summary>
    /// Base class for all classes that subscribe to a cell click event
    /// </summary>
    public abstract class ClickedOnCellModule : MonoBehaviour
    {
        public abstract OnClickedCell GetClickedCell();
    }

    public sealed class Modules : MonoBehaviour
    {
        [SerializeField] private ClickedOnCellModule[] ClickedOnCellModules;
        [SerializeField] private CellsStorage cellsStorage = null;

        private void Start()
        {
            for (byte index = 0; index < ClickedOnCellModules.Length; index++)
                cellsStorage.AddOnCellClickListeners(ClickedOnCellModules[index].GetClickedCell());          
        }

        private void OnDestroy()
        {
            for (byte index = 0; index < ClickedOnCellModules.Length; index++)
                cellsStorage.RemoveOnCellClickListeners(
                ClickedOnCellModules[index].GetClickedCell());       
        }
    }
}