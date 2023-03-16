using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Jobs;
using Game.Models;
using Game.Models.GameLoop;
using Game.Models.GameLoop.Jobs;
using Gaze.Utilities;
using Unity.Jobs;

namespace Game.Services.GameBoard
{
    public class BoardBuildingJobService : BaseService
    {
        public BoardBuildingJobService(
            IDestroyable destroyable,
            ModelsRepositoryWrapper models
        ) : base(
            destroyable,
            models)
        {

        }

        public void BuildBoard()
        {
            UniTask.Void(
                RunBoardBuildingJobsAsync,
                ServiceCancellationToken);
        }

        async UniTaskVoid RunBoardBuildingJobsAsync(
            CancellationToken cancellationToken
        )
        {
            BoardJobData jobData = PrepareBuildJobData();

            var combos = 0;

            while (combos < 1)
            {
                var buildJobHandle = new BoardBuildingJob
                {
                    Seed = jobData.Seed,
                    Rows = jobData.Rows,
                    Columns = jobData.Columns,
                    MinimumRequiredCombos = jobData.MinimumRequiredCombos,
                    AvailablePieces = jobData.AvailablePieces,
                    PiecePool = jobData.PiecePool,
                    Board = jobData.Board
                };

                await buildJobHandle.Schedule();
                
                combos = await ComboFindJobAsync(
                    cancellationToken,
                    jobData);
            }
        }

        
        
        async UniTask<int> ComboFindJobAsync(
            CancellationToken cancellationToken,
            BoardJobData jobData
        )
        {
            var comboFindJobHandle = new ComboFindParallelJob
            {
                Rows = jobData.Rows,
                Columns = jobData.Columns,
                Board = jobData.Board,
                Combos = jobData.Combos
            };

            await comboFindJobHandle.Schedule(
                jobData.Board.Length,
                6);
            
            return jobData.Combos.Count(combo => combo.SwapOrientation != PieceSwapOrientation.Undefined);
        }
    }
}