using System.Collections;

namespace Tree.API.Models;

public class Node<T> : IEnumerable<Node<T>>
{
    public Node(string name)
    {
        Name = name;
        Children = new List<Node<T>>();

        ElementsIndex = new List<Node<T>>();
    }

    public Node(string name, T? data)
    {
        Name = name;
        Data = data;
        Children = new List<Node<T>>();

        ElementsIndex = new List<Node<T>>();
    }

    public string Name { get; set; }
    public T? Data { get; set; }
    public Node<T>? Parent { get; set; }
    public ICollection<Node<T>>? Children { get; set; }
    private ICollection<Node<T>> ElementsIndex { get; }

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
        var curNode = FindChild(n => n.Name.Equals(name));
        if (curNode != null)
        {
            throw new ArgumentException("Node name was duplicated");
        }
        Node<T> node = new(name, data) { Parent = this };
        Children?.Add(node);
        AddChildForSearch(node);

        return node;
    }

    private void AddChildForSearch(Node<T> node)
    {
        ElementsIndex.Add(node);
        if (Parent != null)
            Parent.AddChildForSearch(node);
    }

    public void DeleteChild(Node<T> node)
    {
        Children?.Remove(node);
        DeleteChildForSearch(node);
    }

    private void DeleteChildForSearch(Node<T> node)
    {
        ElementsIndex.Remove(node);
        foreach (var child in node.Children)
        {
            ElementsIndex.Remove(child);
        }
        if (Parent != null)
            Parent.DeleteChildForSearch(node);
    }

    public Node<T>? FindChild(Func<Node<T>, bool> predicate)
    {
        return ElementsIndex.FirstOrDefault(predicate);
    }

    public override string ToString()
    {
        return Name;
    }
}