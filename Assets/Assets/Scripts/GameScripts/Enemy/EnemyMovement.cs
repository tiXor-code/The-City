using System;
using System.Collections.Generic;
using TheCity.ChessMaze;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TheCity.EnemyAI
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyMovement : MonoBehaviour
    {
        private Transform target;
        private int wavepointIndex = 0;

        private int nextPathCellIndex;
        private MapData data;

        [HideInInspector]
        public List<Vector3> pathCells;

        private bool enemyRunCompleted = false;

        public Enemy enemy;
        private EnemyFactory enemyFactory;

        private Vector3 currentPosition;
        private Vector3 nextPosition;

        //private Direction startPositionEdge = Direction.None;
        private MapVisualizer mapVisualizer;

        public MapBrain mapBrain;

        private void Start()
        {
            enemy = GetComponent<Enemy>();
            enemyFactory = GetComponent<EnemyFactory>();
            //mapVisualizer = new MapVisualizer();//GetComponent<MapVisualizer>();

            //target = Waypoints.points[0];
            //nextPosition = new Vector3(pathCells[nextPathCellIndex].x + 0.5f, 1.5f, pathCells[nextPathCellIndex].z + 0.5f);
        }

        private void MoveOnTheGrid()
        {
            foreach (var enemy in WaveManager.enemyList)
            {
                //Enemy enemyComponent = enemy.GetComponent<Enemy>();
                var rotationSpeed = 135f * Time.deltaTime;

                if (enemy == null)
                {
                    //enemyComponent.speed = 2;
                    //enemyComponent.health = 100;
                    //ene
                }
                else
                {
                    //nextPathCellIndex = enemy.GetComponent<Enemy>();

                    //nextPathCellIndex = 0;
                    currentPosition = enemy.transform.position;   //enemy.transform.position;
                                                                  //nextPosition = new Vector3(pathCells[nextPathCellIndex].x + 0.5f, 1.5f, pathCells[nextPathCellIndex].z + 0.5f);
                    nextPosition = new Vector3(pathCells[enemy.nextPathCellIndex].x, 1f, pathCells[enemy.nextPathCellIndex].z);
                    //enemies.
                    //SetDirection(currentPosition, data);
                    //Debug.Log(enemy.transform.rotation);

                    if (enemy.nextPathCellIndex >= pathCells.Count - 1)
                    {
                        enemy.transform.position = Vector3.MoveTowards(currentPosition, nextPosition, Time.deltaTime * enemy.speed);
                    }
                    else enemy.transform.SetPositionAndRotation(
                        Vector3.MoveTowards(
                            currentPosition,
                            nextPosition,
                            Time.deltaTime * enemy.speed),
                        Quaternion.RotateTowards(
                            enemy.transform.rotation,
                            Quaternion.Euler(SetDirection(
                                enemy.transform.position,
                                enemy.nextPathCellIndex)),
                            rotationSpeed));

                    if (Vector3.Distance(currentPosition, nextPosition) < 0.05f)
                    {
                        if (enemy.nextPathCellIndex >= pathCells.Count - 1)
                        {
                            Debug.Log("Reached end!");

                            enemy.EndPath();

                            if (WaveManager.enemyList.Count < 1)
                            {
                                enemyRunCompleted = true;
                            }
                        }
                        else
                        {
                            enemy.nextPathCellIndex++;
                        }

                        //SetDirection(currentPosition, enemy.transform.rotation, rotationSpeed, data);
                    }
                }
            }
        }

        public void SetPathCells(MapData data)
        {
            this.data = data;
            this.pathCells = data.path;
        }

        private void Update()
        {
            //Vector3 dir = nextPosition - currentPosition;
            //transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

            //if (Vector3.Distance(currentPosition, nextPosition) <= 0.4f)
            //{
            if (!mapBrain.IsAlgorithmRunning || mapBrain.DidAlgorithmEverRan)
            {
                try
                {
                    if (pathCells.Count > 1 && !enemyRunCompleted && WaveManager.enemyList.Count > 0)
                    {
                        MoveOnTheGrid();
                    }
                }
                catch (NullReferenceException e)
                {
                    if (pathCells == null) throw new Exception("pathCells are null! Hmm, peculiar...");
                    if (WaveManager.enemyList == null) throw new Exception("WaveManager.enemyList is null! OH FUCK, HERE WE GO AGAIN...");
                }
            }

            //}

            //enemy.speed = enemy.startSpeed;

            //if (WaveManager.enemyList != null)
            //{
            //    var lastIndex = pathCells.Count - 1;
            //    foreach (var enemy in WaveManager.enemyList)
            //    {
            //        if (Vector3.Distance(pathCells[lastIndex], enemy.transform.position) < 0.51f)
            //        {
            //            Object.Destroy(enemy);
            //            WaveManager.enemyList.Remove(enemy);
            //        }
            //    }
            //}
        }

        //void GetNextWaypoint()
        //{
        //    if (wavepointIndex >= Waypoints.points.Length - 1)
        //    {
        //        EndPath();
        //        return;
        //    }

        //    wavepointIndex++;
        //    target = Waypoints.points[wavepointIndex];
        //}

        private void EndPath()
        {
            //PlayerStats.Lives--;
            WaveManager.EnemiesAlive--;
            //Destroy(gameObject);

            //for (var i = this.transform.childCount - 1; i >= 0; i--)
            //{
            Object.Destroy(WaveManager.enemyList[0]);//this.transform.GetChild(i).gameObject);
                                                     //WaveManager.enemyList[0].;
            WaveManager.enemyList.RemoveAll(item => item == null);
            for (int i = 0; i < WaveManager.enemyList.Count; i++)
            {
                Debug.Log(WaveManager.enemyList[i]);
            }
            //}
        }

        public Direction GetDirectionOfNextCell(int currentCellPathIndex)
        {
            //int index = pathCells.FindIndex(a => a == position);
            var nextCellPosition = new Vector3(0f, 0f, 0f);
            var currentCellPosition = new Vector3(0f, 0f, 0f);

            //if (pathCells.Count <= currentCellPathIndex)
            //{
            //    nextCellPosition = pathCells[currentCellPathIndex];
            //    currentCellPosition = pathCells[currentCellPathIndex];
            //}
            if (pathCells.Count > currentCellPathIndex + 1)
            {
                nextCellPosition = pathCells[currentCellPathIndex + 1];
                currentCellPosition = pathCells[currentCellPathIndex];
            }
            return GetDirectionFromVectors(nextCellPosition, currentCellPosition);
        }

        private Direction GetDirectionFromVectors(Vector3 positionToGoTo, Vector3 currentPosition)
        {
            if (positionToGoTo.x > currentPosition.x)
            {
                return Direction.Right;
            }
            else if (positionToGoTo.x < currentPosition.x)
            {
                return Direction.Left;
            }
            else if (positionToGoTo.z < currentPosition.z)
            {
                return Direction.Down;
            }
            return Direction.Up;
        }

        private Vector3 SetDirection(Vector3 currentPosition, int cellPathIndex)
        {
            for (int i = 0; i < data.path.Count; i++)
            {
                //var position = pathCells[i];
                //if (position != data.exitPosition)
                //{
                //    grid.SetCell(position.x, position.z, CellObjectType.Road);
                //}
            }

            var enemy = this.enemy.GetEnemy();

            //var index = grid.CalculateIndexFromCoordinates(position.x, position.z);
            //if (data.obstacleArray[index] && cell.IsTaken == false)
            //{
            //    cell.ObjectType = CellObjectType.Obstacle;
            //}
            //Direction previousDirection = Direction.None;
            Direction nextDirection = Direction.None;

            var newRotation = new Vector3(0, 0, 0);

            if (pathCells.Count > 0)// && enemy.nextPathCellIndex <= pathCells.Count - 2)
            {
                //previousDirection = mapVisualizer.GetDirectionOfPreviousCell(currentPosition, data);
                nextDirection = GetDirectionOfNextCell(cellPathIndex);
            }

            if (Vector3.Distance(currentPosition, data.exitPosition) < 0.5f)
            {
                // Don't rotate
            }
            else
            {
                if (nextDirection == Direction.Up)
                    newRotation = new(0, 0, 0);
                else if (nextDirection == Direction.Down)
                    newRotation = new(0, 180, 0);
                else if (nextDirection == Direction.Left)
                    newRotation = new(0, 270, 0);
                else if (nextDirection == Direction.Right)
                    newRotation = new(0, 90, 0);
            }
            //Debug.Log(nextDirection);

            //if (previousDirection == Direction.Left)
            //{
            //    if (nextDirection == Direction.Up)
            //        newRotation = new(0, 0, 0);
            //    else if (nextDirection == Direction.Down)
            //        newRotation = new(0, 180, 0);
            //}
            //else if (previousDirection == Direction.Right)
            //{
            //    if(nextDirection == Direction.Up)
            //        newRotation = new()
            //}
            return newRotation;    // = Quaternion.RotateTowards(rotation, Quaternion.Euler(newRotation), rotationSpeed);
            //return rotation;
        }

        //switch (enemy.Direction)
        //{
        //case CellObjectType.Empty:
        //    break;
        //case CellObjectType.Road:
        //    if (data.path.Count > 0)
        //    {
        //        previousDirection = GetDirectionOfPreviousCell(position, data);
        //        nextDirection = GetDicrectionOfNextCell(position, data);
        //    }
        //    if (previousDirection == Direction.Up && nextDirection == Direction.Right || previousDirection == Direction.Right && nextDirection == Direction.Up)
        //    {
        //        CreateIndicator(position, roadTileCorner, Quaternion.Euler(0, 90, 0));
        //    }
        //    else if (previousDirection == Direction.Right && nextDirection == Direction.Down || previousDirection == Direction.Down && nextDirection == Direction.Right)
        //    {
        //        CreateIndicator(position, roadTileCorner, Quaternion.Euler(0, 180, 0));
        //    }
        //    else if (previousDirection == Direction.Down && nextDirection == Direction.Left || previousDirection == Direction.Left && nextDirection == Direction.Down)
        //    {
        //        CreateIndicator(position, roadTileCorner, Quaternion.Euler(0, -90, 0));
        //    }
        //    else if (previousDirection == Direction.Left && nextDirection == Direction.Up || previousDirection == Direction.Up && nextDirection == Direction.Left)
        //    {
        //        CreateIndicator(position, roadTileCorner);
        //    }
        //    else if (previousDirection == Direction.Right && nextDirection == Direction.Left || previousDirection == Direction.Left && nextDirection == Direction.Right)
        //    {
        //        CreateIndicator(position, roadStraight, Quaternion.Euler(0, 90, 0));
        //    }
        //    else
        //    {
        //        CreateIndicator(position, roadStraight);
        //    }

        //    break;
        //case CellObjectType.Start:
        //    if (data.path.Count > 0)
        //    {
        //        nextDirection = GetDirectionFromVectors(data.path[0], position);
        //    }
        //    switch (nextDirection)
        //    {
        //        case Direction.Right:
        //            CreateIndicator(position, startTile, Quaternion.Euler(0, -90, 0));
        //            break;
        //        case Direction.Left:
        //            CreateIndicator(position, startTile, Quaternion.Euler(0, 90, 0));
        //            break;
        //        case Direction.Up:
        //            CreateIndicator(position, startTile, Quaternion.Euler(0, 180, 0));
        //            break;
        //        default:
        //            CreateIndicator(position, startTile);
        //            break;
        //    }

        //    break;
        //case CellObjectType.Exit:
        //    if (data.path.Count > 0)
        //    {
        //        previousDirection = GetDirectionOfPreviousCell(position, data);

        // } switch (previousDirection) { case Direction.Right: CreateIndicator(position, exitTile,
        // Quaternion.Euler(0, -90, 0)); break; case Direction.Left: CreateIndicator(position,
        // exitTile, Quaternion.Euler(0, 90, 0)); break; case Direction.Up:
        // CreateIndicator(position, exitTile, Quaternion.Euler(0, 180, 0)); break; default:
        // CreateIndicator(position, exitTile); break; }

        //    break;
        //default:
        //    break;
    }
}