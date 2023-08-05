using System.Collections;

namespace Tree.API.Models;

public class Node<T> : IEnumerable<Node<T>>
{
    public Node(string name)
    {
        Name = name;
        Children = new List<Node<T>>();
    }

    private Node(string name, T? data)
    {
        Name = name;
        Data = data;
        Children = new List<Node<T>>();
    }

    public string Name { get; set; }
    public T? Data { get; set; }
    public Node<T>? Parent { get; set; }
    public bool IsLeaf => Children?.Count == 0;
    public ICollection<Node<T>>? Children { get; set; }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<Node<T>> GetEnumerator()
    {
        yield return this;
        foreach (var directChild in Children!)
        foreach (var anyChild in directChild)
            yield return anyChild;
    }

    public Node<T> AddChild(string name, T? data)
    {
        var curNode = FindChild(name);
        if (curNode != null)
        {
            throw new ArgumentException("Node name was duplicated");
        }
        
        Node<T> node = new(name, data) { Parent = this };
        Children?.Add(node);

        return node;
    }

    public void DeleteChild(Node<T> node)
    {
        Children?.Remove(node);
    }

    public Node<T>? FindChild(string name)
    {
        Node<T>? node = null;
        
        if (Name.Equals(name))
        {
            return this;
        }

        Queue<Node<T>> queue = new Queue<Node<T>>();

        if (!IsLeaf)
        {
            foreach (var child in this.Children!)
            {
                queue.Enqueue(child);
            }
        }

        while (queue.Count > 0)
        {
            var nodeInQueue = queue.Dequeue();

            if (nodeInQueue.Name.Equals(name))
            {
                node = nodeInQueue;
                break;
            }

            if (!nodeInQueue.IsLeaf)
            {
                foreach (var child in nodeInQueue.Children!)
                {
                    queue.Enqueue(child);
                }
            }
        }

        return node;
    }
}