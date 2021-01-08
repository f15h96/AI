using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Node
{
    public List<Node> Children = new List<Node>();
    
    public List<string> Decisions = new List<string>();
    public List<Node> possibleChildren = new List<Node>();

    public Node parent;

    public string Decision;

    public bool explored;

    public int score;

    public bool isRoot;

    public int timesVisited;

    public double reward;

    public int currentCurrency;

    private Random random;
    // Start is called before the first frame update
    void Start()
    {
        random = new Random();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * adds a child to the children list
     */
    public void AddChild(Node parent, Node node)
    {
        parent.Children.Add(node);
        node.Decisions.Add(parent.Decision);
    }
    

    /*
     * reduces the currency to work out what possible next nodes
     */
    public void DecrementCurrentCurrency(string decision, int currency)
    {
        currentCurrency = currency;
        switch (decision)
        {
            case "EnemyL":
                currentCurrency -= 5;
                break;
            case "EnemyM":
                currentCurrency -= 3;
                break;
            case "EnemyS":
                currentCurrency -= 1;
                break;
        }
    }

    /*
     * selects the next random state from the possible nodes
     */
    public Node RandomState(List<Node> possNodes)
    {
        random = new Random();
        var n = random.Next(0, possNodes.Count);
        return possNodes[n];
    }
    
    /*
     * adds the possible states to the possible nodes list based on the currency
     */
    public void possibleStates(Node currNode, int currencyRemaining)
    {
        if (currencyRemaining > 0)
        {
            if (currencyRemaining > 5)
            {
                currNode.AddPossible(currNode, MCTS.Decision.EnemyL);
                currNode.AddPossible(currNode, MCTS.Decision.EnemyM);
                currNode.AddPossible(currNode, MCTS.Decision.EnemyS);
            }
            else if (currencyRemaining > 3)
            {
                currNode.AddPossible(currNode, MCTS.Decision.EnemyM);
                currNode.AddPossible(currNode, MCTS.Decision.EnemyS);
            }
            else
            {
                currNode.AddPossible(currNode, MCTS.Decision.EnemyS);
            }
        }else if (currNode.Decision.Equals("Spread")|| currNode.Decision.Equals("Group"))
        {
        }
        
        else
        {
            currNode.AddPossible(currNode, MCTS.Decision.Group);
            currNode.AddPossible(currNode, MCTS.Decision.Spread);
        }
    }
    

    /*
     * adds the possible states to the possible nodes list
     */
    public void AddPossible(Node parent, MCTS.Decision decision)
    {
        Node node = new Node();
        node.parent = parent;
        node.Decision = decision.ToString();
        parent.possibleChildren.Add(node);
    }

    /*
     * checks to see if the node has been explored
     */
    public bool ExploredPossibleCheck(Node node)
    {
        if (!(node.possibleChildren.Count > 0))
        {
            
        }
        for (int index = 0; index < node.possibleChildren.Count; index++)
        {
            if (!node.possibleChildren[index].explored)
            {
                return false;
            } 
        }

        return true;
    }
    
}
