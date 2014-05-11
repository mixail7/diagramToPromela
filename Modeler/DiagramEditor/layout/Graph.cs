using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testGraphLayout
{
    class Graph
    {
        public Dictionary<string, Node> _nodes = new Dictionary<string, Node>();
        public List<Edge> _edges = new List<Edge>();
        private const int def_weight = 100;
        private const int def_height = 50;

        public class Node
        {
            public Node(int widht, object tag)
            {
                this.widht = widht;
                this.tag = tag;
                id = (new Guid()).ToString();
            }

            public Node(int widht, object tag, string id)
            {
                this.widht = widht;
                this.tag = tag;
                this.id = id;
            }

            public string id;
            public object tag;
            public int x = 0;
            public int y = 0;
            public int widht = 0;
            public List<Edge> edges_in = new List<Edge>();
            public List<Edge> edges_out = new List<Edge>();
            public int x_cluster = -1, y_cluster = -1, is_cluster = 0;
            public override string ToString()
            {
                return String.Format("{0}, [{1}, {2}] x_cl: {3}, y_cl:{4}", id, x, y, x_cluster, y_cluster);
            }
        }

        public class Edge
        {
            public Edge(Node sr, Node dst)
            {
                sourse = sr;
                dest = dst;
            }
            public Node sourse;
            public Node dest;
        }

        public void printMatrix(object[,] arr, int w, int h)
        {
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    var g = arr[j,i] as List<string>;
                    if (g == null)
                    {
                        Console.Write("\t[]");
                    }
                    else
                    {
                        Console.Write("[");
                        foreach (var str in g)
                        {
                            Console.Write(" " + str);
                        }
                        Console.Write("]");
                    }

                }
                Console.WriteLine("");
            }
        }
        public void addNode(string id, int widht, object tag)
        {
            var nd = new Node(widht, tag, id);
            _nodes.Add(id, nd);
        }

        public void addNode(string id, int widht)
        {
            addNode(id, widht, null);
        }

        public void AddEdge(string id_src, string id_dst)
        {
            var edg = new Edge(_nodes[id_src], _nodes[id_dst]);
            _nodes[id_src].edges_out.Add(edg);
            _nodes[id_dst].edges_in.Add(edg);
            _edges.Add(edg);
        }

        //private void setClustersX
        public void Arrange(string start_id)
        {
            var nd = findById(start_id);
            nd.x_cluster = _nodes.Count/2;
            nd.is_cluster = 1;
            nd.y_cluster = 0;
            int max_index = -1;
            /*выстраиваем по y*/
            while (true)
            {
                bool is_process = false;
                foreach (var node in _nodes)
                {
                    if (node.Value.is_cluster == 0)
                    {
                        int max = -1;
                        bool is_set = true;
                        foreach (var edg in node.Value.edges_in)
                        {
                            if (edg.sourse.y_cluster == -1)
                            {
                                is_set = false;
                                break;
                            }
                            if (max < edg.sourse.y_cluster)
                                max = edg.sourse.y_cluster;
                        }
                        if (is_set)
                        {
                            is_process = true;
                            node.Value.is_cluster = 1;
                            node.Value.y_cluster = max+1;
                            if (max_index < node.Value.y_cluster)
                                max_index = node.Value.y_cluster;
                        }
                    }
                }
                if (!is_process)
                    break;
            }
            int arr_x_max = _nodes.Count*2, arr_y_max = max_index + 1;
            var arr = new object[arr_x_max, arr_y_max];
            foreach (var node in _nodes)
            {
                var d = arr[_nodes.Count, node.Value.y_cluster] as List<string>;
                if (d == null)
                    d = new List<string>();
                d.Add(node.Value.id);
                arr[_nodes.Count, node.Value.y_cluster] = d;
            }
            while (true)
            {
                bool is_process = false;
                for (int i = 0; i < arr_x_max; i++)
                {
                    for (int j = 0; j < arr_y_max; j++)
                    {
                        var ls = arr[i, j] as List<string>;
                        if (ls != null && ls.Count > 1)
                        {
                            is_process = true;
                            if ((arr[i + 1, j] != null) && (i - 1) > 0 && (arr[i - 1, j] == null))
                            {
                                var nl = new List<string>();
                                nl.Add(ls.ElementAt(ls.Count - 1));
                                ls.RemoveAt(ls.Count - 1);
                                arr[i - 1, j] = nl;
                            }
                            else if ((arr[i - 1, j] != null) && (i + 1) < arr_x_max && (arr[i + 1, j] == null))
                            {
                                var nl = new List<string>();
                                nl.Add(ls.ElementAt(ls.Count - 1));
                                ls.RemoveAt(ls.Count - 1);
                                arr[i + 1, j] = nl;
                            }
                            else if (arr[i - 1, j] == null || arr[i + 1, j] == null)
                            {
                                var nl = new List<string>();
                                nl.Add(ls.ElementAt(ls.Count - 1));
                                ls.RemoveAt(ls.Count - 1);
                                arr[i + 1, j] = nl;
                            }
                            else
                            {
                                var nl = arr[i + 1, j] as List<string>;
                                nl.Add(ls.ElementAt(ls.Count - 1));
                                ls.RemoveAt(ls.Count - 1);
                            }
                        }
                    }
                }
                if(!is_process)
                    break;
            }
            //printMatrix(arr, arr_x_max, arr_y_max);
            /*считаем x,y*/
            int start_with = -1;
            for (int i = 0; i < arr_x_max; i++)
            {
                for (int j = 0; j < arr_y_max; j++)
                {
                    var g = arr[i, j] as List<string>;
                    if (g != null)
                    {
                        if (start_with == -1)
                            start_with = i;
                        foreach (var str in g)
                        {
                            var nd1 = findById(str);
                            nd1.x = (i - start_with)*def_weight;
                            nd1.y = j*def_height;
                        }
                    }
                }
            }
        }

        private Node findById(string id)
        {
            foreach (var node in _nodes)
            {
                if (node.Value.id == id)
                    return node.Value;
            }
            return null;
        }
    }
}
