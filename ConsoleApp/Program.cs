using System;

class Node
{
    public int Key;
    public Node Left;
    public Node Right;
    public int Height;

    public Node(int key)
    {
        Key = key;
        Left = null;
        Right = null;
        Height = 1; // Altura inicial del nodo
    }
}

class AVLTree
{
    // Método para obtener la altura de un nodo
    private int GetHeight(Node node)
    {
        return node == null ? 0 : node.Height;
    }

    // Método para obtener el balance de un nodo
    private int GetBalance(Node node)
    {
        return node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
    }

    // Rotación a la izquierda
    private Node LeftRotate(Node z)
    {
        Node y = z.Right;
        Node T2 = y.Left;

        // Realizar rotación
        y.Left = z;
        z.Right = T2;

        // Actualizar alturas
        z.Height = 1 + Math.Max(GetHeight(z.Left), GetHeight(z.Right));
        y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));

        return y;
    }

    // Rotación a la derecha
    private Node RightRotate(Node z)
    {
        Node y = z.Left;
        Node T3 = y.Right;

        // Realizar rotación
        y.Right = z;
        z.Left = T3;

        // Actualizar alturas
        z.Height = 1 + Math.Max(GetHeight(z.Left), GetHeight(z.Right));
        y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));

        return y;
    }

    // Método para insertar un nodo en el árbol
    public Node Insert(Node node, int key)
    {
        // 1. Realizar la inserción normal
        if (node == null) return new Node(key);
        if (key < node.Key)
            node.Left = Insert(node.Left, key);
        else
            node.Right = Insert(node.Right, key);

        // 2. Actualizar la altura del nodo antecesor
        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

        // 3. Obtener el balance
        int balance = GetBalance(node);

        // Si el nodo se vuelve desbalanceado, hay 4 casos

        // Caso 1 - Rotación derecha
        if (balance > 1 && key < node.Left.Key)
            return RightRotate(node);

        // Caso 2 - Rotación izquierda
        if (balance < -1 && key > node.Right.Key)
            return LeftRotate(node);

        // Caso 3 - Rotación izquierda-derecha
        if (balance > 1 && key > node.Left.Key)
        {
            node.Left = LeftRotate(node.Left);
            return RightRotate(node);
        }

        // Caso 4 - Rotación derecha-izquierda
        if (balance < -1 && key < node.Right.Key)
        {
            node.Right = RightRotate(node.Right);
            return LeftRotate(node);
        }

        return node; // Retornar el nodo sin cambios
    }

    // Método para hacer un recorrido en preorden del árbol
    public void PreOrder(Node node)
    {
        if (node != null)
        {
            Console.Write(node.Key + " ");
            PreOrder(node.Left);
            PreOrder(node.Right);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        AVLTree tree = new AVLTree();
        Node root = null;

        // Inserción de nodos
        int[] keys = { 10, 20, 30, 40, 50, 25 };
        foreach (int key in keys)
        {
            root = tree.Insert(root, key);
        }

        // Recorrido en preorden
        Console.WriteLine("Recorrido en preorden del árbol AVL: ");
        tree.PreOrder(root);
        Console.WriteLine();
    }
}
