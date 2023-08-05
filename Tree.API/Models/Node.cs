namespace Tree.API.Models;

public class Node<T>
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
    public Node<T>? Parent { get; private set; }
    private bool IsLeaf => Children?.Count == 0;
    public ICollection<Node<T>>? Children { get; set; }

    public Task<Node<T>> AddChildAsync(string name, T? data)
    {
        return Task.Run(async () =>
        {
            var curNode = await FindChildAsync(name);
            if (curNode != null) throw new ArgumentException("Node name was duplicated");

            Node<T> node = new(name, data) { Parent = this };
            Children?.Add(node);

            return node;
        });
    }

    public Task<bool?> DeleteChildAsync(Node<T> node)
    {
        return Task.Run(() => Children?.Remove(node));
    }

    public Task<Node<T>?> FindChildAsync(string name)
    {
        return Task.Run(() =>
        {
            Node<T>? node = null;

            if (Name.Equals(name)) return this;

            var queue = new Queue<Node<T>>();

            if (!IsLeaf)
                foreach (var child in Children!)
                    queue.Enqueue(child);

            while (queue.Count > 0)
            {
                var nodeInQueue = queue.Dequeue();

                if (nodeInQueue.Name.Equals(name))
                {
                    node = nodeInQueue;
                    break;
                }

                if (!nodeInQueue.IsLeaf)
                    foreach (var child in nodeInQueue.Children!)
                        queue.Enqueue(child);
            }

            return node;
        });
    }
}