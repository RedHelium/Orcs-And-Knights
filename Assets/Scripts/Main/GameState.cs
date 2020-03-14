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

        public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        private void CheckWinState()
        {
            if (!CalculateWin()) return;

            playersSwitcher.enabled = false;
            winAnimatorGroup.SetTrigger("Win");
            outputWinner.text = playersSwitcher.FormatPlayerName() + " " + "WON!";
        }

        private bool CalculateWin()
        {
            idCells = cellsStorage.GetCellsID();

            for (byte index = 0; index < idCells.Length; index++)
            {
    
                if (isWinning) return isWinning;

                switch (index)
                {
                    case 0:
                        {
                            if (CheckWinningIteration(0, 1) || CheckWinningIteration(0, 3) || CheckWinningIteration(0, 4))
                                isWinning = true;
                            
                            break;
                        }
                    case 1:
                        {
                            if(CheckWinningIteration(1, 3))
                            isWinning = true;

                            break;
                        }
                    case 3: break;
                    case 6: break;
                    case 2:
                        {
                            if (CheckWinningIteration(2, 2) || CheckWinningIteration(2, 3))
                                isWinning = true;

                            break;
                        }
                    default: continue;
                        
                }

                if(index % 3 == 0 && CheckWinningIteration(index, 1))
                    isWinning = true;
                
            }

            return isWinning;
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

        public override OnClickedCell GetClickedCell() => OnClickedCell;
    }
}