using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class MCTS : MonoBehaviour
{
    public enum Decision
    {
        EnemyL,
        EnemyM,
        EnemyS,
        Spread,
        Group,
    };

    public EnemySpawn Spawn;

    public Tree tree;
    
    private Node CurrNode = new Node();

    private Node bestScore;
    
    public int Currency;

    public GameCopy gameCopy;

    private int iterations;
    public int maxIterations;

    public bool simulationDone = true;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    /*
     * setting up and starting the MCTS algorithm
     */
    public void startMCTS()
    {
                
        CurrNode.isRoot = true;
        Currency = Spawn.Currency;
        CurrNode.possibleStates(CurrNode, Currency);
        MonteCarlowTreeSearch(CurrNode);
        
    }


    /*
     * selects the best node to go down if the node has been explored
     */
    public Node SelectNode(Node node)
    {
        while (node.explored)
        {
            node = FindNodeWithHighestUCB(node);
        }

        return node;
    }
    
    
    /*
     * the main MCTS algorithm
     */
    private void MonteCarlowTreeSearch(Node node)
    {
        while (iterations != maxIterations)
        {
            List<GameObject> enemies = new List<GameObject>();
            if (simulationDone)
            {
                SelectNode(node);
                if (node.possibleChildren.Count > 0)
                {
                    PickNode(node);
                    if (CurrNode == node)
                    {
                        MonteCarlowTreeSearch(node);
                    }
                }
                enemies = CreateEnemyList(CurrNode, true);
                gameCopy.Reset();
            

            gameCopy.startWave(enemies);
            }
            /*
            while (GameObject.FindGameObjectsWithTag("Enemy") != null)
            {
                simulationDone = false;
            }
            */

            if (simulationDone)
            {
                iterations++;
                CurrNode.score = gameCopy.furthest;
                CurrNode.reward = UCB(CurrNode);
                CurrNode.possibleStates(CurrNode, CurrNode.currentCurrency);
                BackPropagate(CurrNode);
            }
        }
        var resultNode = FindNodeWithHighestUCB(CurrNode);
        resultNode.Decisions.Add(resultNode.Decision);
        for (int i = 0; i < resultNode.Decisions.Count; i++)
        {
            switch (resultNode.Decision)
            {
                case "EnemyL":
                    Spawn.SpawnL();
                    break;
                case "EnemyM":
                    Spawn.SpawnM();
                   break;
                case "EnemyS":
                   Spawn.SpawnS();
                   break;
            }
        }
    }

    /*
     * Created the list of enemies that are parsed to the game copy to simulate in the MCTS
     */
    private List<GameObject> CreateEnemyList(Node node, bool fake)
    {
        List<GameObject> returnList = new List<GameObject>();
        var enemyL = Spawn.enemyL;
        var enemyM = Spawn.enemyM;
        var enemyS = Spawn.enemyS;
        for (int i = 0; i < node.Decisions.Count; i++)
        {
            if (fake)
            {
               enemyL = Spawn.FakeEnemyL;
               enemyM = Spawn.FakeEnemyM;
               enemyS = Spawn.FakeEnemyS; 
            }
            switch (node.Decisions[i])
            {
                case "EnemyL":
                    Instantiate(enemyL);
                    returnList.Add(enemyL);
                    break;
                case "EnemyM":
                    Instantiate(enemyM);
                    returnList.Add(enemyM);
                    break;
                case "EnemyS":
                    Instantiate(enemyS);
                    returnList.Add(enemyS);
                    break;
                default: break;
            }
            
                
        }

        switch (node.Decision)
        {
            case "EnemyL":
                Instantiate(enemyL);
                returnList.Add(enemyL);
                break;
            case "EnemyM":
                Instantiate(enemyM);
                returnList.Add(enemyM);
                break;
            case "EnemyS":
                Instantiate(enemyS);
                returnList.Add(enemyS);
                break;
            default: break;
        }
        return returnList;

    }

    
    /*
     * finds the highest UCB node and returns it
     */
    public Node FindNodeWithHighestUCB(Node node)
    {
        if (node.Children.Count > 0)
        {
            Node returnNode = new Node();
            double maxUCB = 0;
            for (int i = 0; i < node.Children.Count; i++)
            {
                if (maxUCB > node.Children[i].reward)
                {
                    maxUCB = node.Children[i].reward;
                    returnNode = node.Children[i];
                }
            }

            return returnNode;
        }

        return node;
    }

    /*
     * works out the UCB
     */
    public double UCB(Node node)
    {
        if (node.timesVisited == 0)
        {
            return double.MaxValue;
        }

        return (double) node.score / (double) node.timesVisited +
               1.41 * Math.Sqrt(Math.Log(iterations) / (double) node.timesVisited);
    }


    /*
     * picks the next node for the MCTS used for expantion
     */
    private void PickNode(Node node)
    {
        var newNode = node.RandomState(node.possibleChildren);
        if (newNode.explored)
        {
            if (node.ExploredPossibleCheck(node))
            {
                node.explored = true;
            }
            else
            {
                PickNode(node);
            }
        }
        else
        {
            CurrNode = newNode;
            node.AddChild(node, newNode);
            newNode.DecrementCurrentCurrency(newNode.Decision, Currency);
            CurrNode.timesVisited++;
        }
    }

    /*
     * backpropigates through the nodes to assign the best score to the nodes
     */
    private void BackPropagate(Node child)
    {
        if (!child.parent.isRoot)
        {
            child.parent.score += child.score;
            child.parent.timesVisited++;
            BackPropagate(child.parent);
        }
    }
    
}
