using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Main
{

    public sealed class GameState : ClickedOnCellModule
    {
        private readonly KeyCode RESTART_KEY = KeyCode.R;

        [SerializeField] private CellsStorage cellsStorage = null;
        [SerializeField] private PlayersSwitcher playersSwitcher = null;
        [SerializeField] private Text outputWinner = null;
        [SerializeField] private Animator winAnimatorGroup = null;

        private byte[] idCells;
        private bool isWinning;
        private OnClickedCell OnClickedCell;

        public override OnClickedCell GetClickedCell() => OnClickedCell;


        public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        private void CheckWinState()
        {
            if (!CalculateWin()) return;

            winAnimatorGroup.SetTrigger("Win");
            outputWinner.text = playersSwitcher.FormatPlayerName() + " " + "WON!";
        }

        private bool CalculateWin()
        {
            idCells = cellsStorage.GetCellsID();

            for (byte index = 0; index < idCells.Length; index++)
            {
                if (isWinning) return isWinning;

                isWinning = CheckWinningIteration(index);
            }

            return isWinning;
        }

        private bool CheckWinningIteration(byte n)
        {
            // CheckWinningIteration(n, 3)/(n,1) called in other lines... Hmm...
            switch (n)
            {
                case 0:
                    return CheckWinningIteration(n, 1) || CheckWinningIteration(n, 3) || CheckWinningIteration(n, 4);
                case 1:
                    return CheckWinningIteration(n, 3);
                case 2:
                    return CheckWinningIteration(n, 2) || CheckWinningIteration(n, 3);
                case byte index when index % 3 == 0:
                    return CheckWinningIteration(index, 1);
                default: return false;
            }
        }

        private bool CheckWinningIteration(byte n, byte x)
        => idCells[n] == idCells[n + x] && idCells[n] == idCells[n + 2 * x];

        private void Awake()
        {
            OnClickedCell = CheckWinState;
        }

        private void Update()
        {
            if (Input.GetKeyDown(RESTART_KEY)) RestartLevel();
        }
    }
}