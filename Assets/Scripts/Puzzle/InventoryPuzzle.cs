using Items;
using Player;

namespace Puzzle
{
    public abstract class InventoryPuzzle : Puzzle
    {
        public InventoryItemData requiredItem;
        public override bool CanSolve()
        {
            var playerInventory = GameManager.Instance.Player.GetComponent<PlayerInventory>();
            if (!playerInventory.HasItem(requiredItem)) return false;
            playerInventory.RemoveInventoryItem(requiredItem);
            return true;
        }
    }
}