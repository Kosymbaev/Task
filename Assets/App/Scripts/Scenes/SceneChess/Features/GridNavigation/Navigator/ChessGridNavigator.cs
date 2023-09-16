using System.Collections.Generic;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Types;
using Unity.VisualScripting;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator
{
    public class ChessGridNavigator : IChessGridNavigator
    {
        private Vector2Int GetDR(Vector2Int currentCell, ChessGrid grid)
        {
            Vector2Int dr = currentCell;
            for (int i = 1; i < 8; i++)
            {
                if (currentCell.y - i >= 0 && currentCell.x + i <= 7 && dr == currentCell)
                {
                    if (grid.Get(currentCell.y - i, currentCell.x + i) != null || currentCell.y - i == 0 || currentCell.x + i == 7)
                    {
                        dr = new Vector2Int(currentCell.x + i, currentCell.y - i);
                    }
                }
                else break;
            }

            return dr;
        }

        private Vector2Int GetUL(Vector2Int currentCell, ChessGrid grid)
        {
            Vector2Int ul = currentCell;
            for (int i = 1; i < 8; i++)
            {
                if (currentCell.y + i <= 7 && currentCell.x - i >= 0)
                {
                    if (grid.Get(currentCell.y + i, currentCell.x - i) != null || currentCell.y + i == 7 || currentCell.x - i == 0)
                    {
                        ul = new Vector2Int(currentCell.x - i, currentCell.y + i);
                    }
                    if (ul != currentCell) break;
                }
                else break;
            }
            return ul;
        }
        private Vector2Int GetDL(Vector2Int currentCell, ChessGrid grid)
        {
            Vector2Int dl = currentCell;
            for (int i = 1; i < 8; i++)
            {
                if (currentCell.y - i >= 0 && currentCell.x - i >= 0)
                {
                    if (grid.Get(currentCell.y - i, currentCell.x - i) != null || currentCell.y - i == 0 || currentCell.x - i == 0)
                    {
                        dl = new Vector2Int(currentCell.x - i, currentCell.y - i);
                    }
                    if (dl != currentCell) break;
                }
                else break;
            }
            return dl;
        }
        private Vector2Int GetUR(Vector2Int currentCell, ChessGrid grid)
        {
            Vector2Int ur = currentCell;
            for (int i = 1; i < 8; i++)
            {
                if (currentCell.y + i <= 7 && currentCell.x + i <= 7)
                {
                    if (grid.Get(currentCell.y + i, currentCell.x + i) != null || currentCell.y + i == 7 || currentCell.x + i == 7)
                    {
                        ur = new Vector2Int(currentCell.x + i, currentCell.y + i);
                    }
                    if (ur != currentCell) break;
                }
                else break;
            }
            return ur;
        }
        private int GetDown(Vector2Int currentCell, ChessGrid grid)
        {
            int down = currentCell.y;
            for (int i = 1; i < 8; i++)
            {
                if (currentCell.y - i >= 0)
                {
                    if (grid.Get(currentCell.y - i, currentCell.x) != null || currentCell.y - i == 0)
                    {
                        down = currentCell.y - i;
                    }
                    if (down != currentCell.y) break;
                }
                else break;
            }
            return down;
        }
        private int GetUp(Vector2Int currentCell, ChessGrid grid)
        {
            int up = currentCell.y;
            for (int i = 1; i < 8; i++)
            {
                if (currentCell.y + i <= 7)
                {
                    if (grid.Get(currentCell.y + i, currentCell.x) != null || currentCell.y + i == 7)
                    {
                        up = currentCell.y + i;
                    }
                    if (up != currentCell.y) break;
                }
                else break;
            }
            return up;
        }
        private int GetRight(Vector2Int currentCell, ChessGrid grid)
        {
            int right = currentCell.x;
            for (int i = 1; i < 8; i++)
            {
                if (currentCell.x + i <= 7)
                {
                    if (grid.Get(currentCell.y, currentCell.x + i) != null || currentCell.x + i == 7)
                    {
                        right = currentCell.x + i;
                    }
                    if (right != currentCell.x) break;
                }
                else break;
            }
            return right;
        }
        private int GetLeft(Vector2Int currentCell, ChessGrid grid)
        {
            int left = currentCell.x;
            for (int i = 1; i < 8; i++)
            {
                if (currentCell.x - i >= 0)
                {
                    if (grid.Get(currentCell.y, currentCell.x - i) != null || currentCell.x - i == 0)
                    {
                        left = currentCell.x - i;
                    }
                    if (left != currentCell.x) break;
                }
                else break;
            }
            return left;
        }
        private List<Vector2Int> RecoveryPath(Stack<Vector2Int> recovery, Dictionary<Vector2Int, Vector2Int> history, Vector2Int from)
        {
            List<Vector2Int> result = new();
            if (!recovery.IsUnityNull())
            {
                Vector2Int currentCell = recovery.Peek();
                while (true)
                {
                    currentCell = history[currentCell];
                    recovery.Push(currentCell);
                    if (currentCell == from) break;
                }
                foreach (var cell in recovery) result.Add(cell);
            }
            return result;
        }
        public List<Vector2Int> GetPathForKnight(Vector2Int from, Vector2Int to, ChessGrid grid)
        {
            int[] horizontal = { 2, 2, 1, 1, -2, -2, -1, -1 };
            int[] vertical = { 1, -1, 2, -2, 1, -1, 2, -2 };

            Dictionary<Vector2Int, Vector2Int> history = new Dictionary<Vector2Int, Vector2Int>();
            Dictionary<Vector2Int, bool> visitedCell = new Dictionary<Vector2Int, bool>();
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            Stack<Vector2Int> recovery = new Stack<Vector2Int>();

            queue.Enqueue(from);
            visitedCell.Add(from, true);
            while (!queue.IsUnityNull())
            {
                var currentCell = queue.Dequeue();
                if (currentCell == to)
                {
                    recovery.Push(currentCell);
                    break;
                }
                for (int i = 0; i < 8; i++)
                {
                    var buffer = new Vector2Int(currentCell.x + horizontal[i], currentCell.y + vertical[i]);
                    if (buffer.x < 8 && buffer.x > -1 && buffer.y > -1 && buffer.y < 8)
                    {
                        var reserved = grid.Get(buffer);
                        bool check = visitedCell.TryGetValue(buffer, out _);
                        if (!check && reserved == null)
                        {
                            queue.Enqueue(buffer);
                            history.Add(buffer, currentCell);
                            visitedCell.Add(buffer, true);
                        }
                    }
                }
            }

            return RecoveryPath(recovery, history, from);
        }

        public List<Vector2Int> GetPathForRook(Vector2Int from, Vector2Int to, ChessGrid grid)
        {
            Dictionary<Vector2Int, Vector2Int> history = new Dictionary<Vector2Int, Vector2Int>();
            Dictionary<Vector2Int, bool> visitedCell = new Dictionary<Vector2Int, bool>();
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            Stack<Vector2Int> recovery = new Stack<Vector2Int>();

            queue.Enqueue(from);
            visitedCell.Add(from, true);
            while (!queue.IsUnityNull())
            {
                var currentCell = queue.Dequeue();
                if (currentCell == to)
                {
                    recovery.Push(currentCell);
                    break;
                }

                int left = GetLeft(currentCell, grid);
                int right = GetRight(currentCell, grid);
                int up = GetUp(currentCell, grid);
                int down = GetDown(currentCell, grid);

                for (int i = left; i <= right; i++)
                {
                    var buffer = new Vector2Int(i, currentCell.y);
                    var reserved = grid.Get(buffer);
                    bool check = visitedCell.TryGetValue(buffer, out _);

                    if (!check && reserved == null)
                    {
                        queue.Enqueue(buffer);
                        history.Add(buffer, currentCell);
                        visitedCell.Add(buffer, true);
                    }
                }
                for (int i = down; i <= up; i++)
                {
                    var buffer = new Vector2Int(currentCell.x, i);
                    var reserved = grid.Get(buffer);
                    bool check = visitedCell.TryGetValue(buffer, out _);

                    if (!check && reserved == null)
                    {
                        queue.Enqueue(buffer);
                        history.Add(buffer, currentCell);
                        visitedCell.Add(buffer, true);
                    }
                }
            }

            return RecoveryPath(recovery, history, from);
        }

        public List<Vector2Int> GetPathForPon(Vector2Int from, Vector2Int to, ChessGrid grid, ChessUnitColor color)
        {

            Dictionary<Vector2Int, Vector2Int> history = new Dictionary<Vector2Int, Vector2Int>();
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            Stack<Vector2Int> recovery = new Stack<Vector2Int>();

            queue.Enqueue(from);
            if (color.Equals(ChessUnitColor.White))
            {
                for (; !queue.IsUnityNull();)
                {
                    var currentCell = queue.Dequeue();
                    if (currentCell == to)
                    {
                        recovery.Push(currentCell);
                        break;
                    }
                    if (currentCell.y + 1 < 8)
                    {
                        var buffer = new Vector2Int(currentCell.x, currentCell.y + 1);
                        var reserved = grid.Get(buffer);
                        if (reserved == null)
                        {
                            queue.Enqueue(buffer);
                            history.Add(buffer, currentCell);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                for (; !queue.IsUnityNull();)
                {
                    var currentCell = queue.Dequeue();
                    if (currentCell == to)
                    {
                        recovery.Push(currentCell);
                        break;
                    }
                    if (currentCell.y - 1 > 0)
                    {
                        var buffer = new Vector2Int(currentCell.x, currentCell.y - 1);
                        var reserved = grid.Get(buffer);
                        if (reserved == null)
                        {
                            queue.Enqueue(buffer);
                            history.Add(currentCell, buffer);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return RecoveryPath(recovery, history, from);

        }

        public List<Vector2Int> GetPathForKing(Vector2Int from, Vector2Int to, ChessGrid grid)
        {
            int[] horizontal = { 1, 1, 1, 0, 0, -1, -1, -1 };
            int[] vertical = { 1, 0, -1, 1, -1, 1, 0, -1 };

            Dictionary<Vector2Int, Vector2Int> history = new Dictionary<Vector2Int, Vector2Int>();
            Dictionary<Vector2Int, bool> visitedCell = new Dictionary<Vector2Int, bool>();
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            Stack<Vector2Int> recovery = new Stack<Vector2Int>();

            queue.Enqueue(from);
            visitedCell.Add(from, true);
            while (!queue.IsUnityNull())
            {
                var currentCell = queue.Dequeue();
                if (currentCell == to)
                {
                    recovery.Push(currentCell);
                    break;
                }
                for (int i = 0; i < 8; i++)
                {
                    var buffer = new Vector2Int(currentCell.x + horizontal[i], currentCell.y + vertical[i]);
                    if (buffer.x < 8 && buffer.x > -1 && buffer.y > -1 && buffer.y < 8)
                    {
                        var reserved = grid.Get(buffer);
                        bool check = visitedCell.TryGetValue(buffer, out _);
                        if (!check && reserved == null)
                        {
                            queue.Enqueue(buffer);
                            history.Add(buffer, currentCell);
                            visitedCell.Add(buffer, true);
                        }
                    }
                }
            }

            return RecoveryPath(recovery, history, from);
        }

        public List<Vector2Int> GetPathForQueen(Vector2Int from, Vector2Int to, ChessGrid grid)
        {
            Dictionary<Vector2Int, Vector2Int> history = new Dictionary<Vector2Int, Vector2Int>();
            Dictionary<Vector2Int, bool> visitedCell = new Dictionary<Vector2Int, bool>();
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            Stack<Vector2Int> recovery = new Stack<Vector2Int>();

            queue.Enqueue(from);
            visitedCell.Add(from, true);
            while (!queue.IsUnityNull())
            {
                var currentCell = queue.Dequeue();

                if (currentCell == to)
                {
                    recovery.Push(currentCell);
                    break;
                }
                int left = GetLeft(currentCell, grid);
                int right = GetRight(currentCell, grid);
                int down = GetDown(currentCell, grid);
                int up = GetUp(currentCell, grid);
                Vector2Int dl = GetDL(currentCell, grid);
                Vector2Int ur = GetUR(currentCell, grid);
                Vector2Int dr = GetDR(currentCell, grid);
                Vector2Int ul = GetUL(currentCell, grid);

                for (int i = left; i <= right; i++)
                {
                    var buffer = new Vector2Int(i, currentCell.y);
                    var reserved = grid.Get(buffer);
                    bool check = visitedCell.TryGetValue(buffer, out _);
                    if (!check && reserved == null)
                    {
                        queue.Enqueue(buffer);
                        history.Add(buffer, currentCell);
                        visitedCell.Add(buffer, true);
                    }
                }

                for (int i = down; i <= up; i++)
                {
                    var buffer = new Vector2Int(currentCell.x, i);
                    var reserved = grid.Get(buffer);
                    bool check = visitedCell.TryGetValue(buffer, out _);
                    if (!check && reserved == null)
                    {
                        queue.Enqueue(buffer);
                        history.Add(buffer, currentCell);
                        visitedCell.Add(buffer, true);
                    }
                }

                for (Vector2Int i = dl; i != ur; i.x++, i.y++)
                {
                    var reserved = grid.Get(i);
                    bool check = visitedCell.TryGetValue(i, out _);
                    if (!check && reserved == null)
                    {
                        queue.Enqueue(i);
                        history.Add(i, currentCell);
                        visitedCell.Add(i, true);
                    }
                }

                for (Vector2Int i = ul; i != dr; i.x++, i.y--)
                {
                    var reserved = grid.Get(i);
                    bool check = visitedCell.TryGetValue(i, out _);
                    if (!check && reserved == null)
                    {
                        queue.Enqueue(i);
                        history.Add(i, currentCell);
                        visitedCell.Add(i, true);
                    }
                }

            }

            return RecoveryPath(recovery, history, from);
        }

        public List<Vector2Int> GetPathForBishop(Vector2Int from, Vector2Int to, ChessGrid grid)
        {
            return null;
        }

        public List<Vector2Int> FindPath(ChessUnitType unit, Vector2Int from, Vector2Int to, ChessGrid grid)
        {
            switch (unit)
            {
                case ChessUnitType.Bishop:
                    {
                        return GetPathForBishop(from, to, grid);
                    }
                case ChessUnitType.Rook:
                    {
                        return GetPathForRook(from, to, grid);
                    }
                case ChessUnitType.Pon:
                    {
                        var color = grid.Get(from).PieceModel.Color;
                        return GetPathForPon(from, to, grid, color);
                    }
                case ChessUnitType.Queen:
                    {
                        return GetPathForQueen(from, to, grid);
                    }
                case ChessUnitType.King:
                    {
                        return GetPathForKing(from, to, grid);
                    }
                case ChessUnitType.Knight:
                    {
                        return GetPathForKnight(from, to, grid);
                    }
            }
            return null;
        }
    }
}