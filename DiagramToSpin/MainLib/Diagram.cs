using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace MainLib
{
    public class MainHelper
    {
        public static void InitCounters()
        {
            Diagram.counter_diagrams = 1;
            Node.NodeCounter = 1;
        }

        private static readonly Dictionary<string, string> Gost = new Dictionary<string, string>();

        public static void Init()
        {
            Gost.Clear();
            Gost.Add("Є", "EH");
            Gost.Add("І", "I");
            Gost.Add("і", "i");
            Gost.Add("№", "#");
            Gost.Add("є", "eh");
            Gost.Add("А", "A");
            Gost.Add("Б", "B");
            Gost.Add("В", "V");
            Gost.Add("Г", "G");
            Gost.Add("Д", "D");
            Gost.Add("Е", "E");
            Gost.Add("Ё", "JO");
            Gost.Add("Ж", "ZH");
            Gost.Add("З", "Z");
            Gost.Add("И", "I");
            Gost.Add("Й", "JJ");
            Gost.Add("К", "K");
            Gost.Add("Л", "L");
            Gost.Add("М", "M");
            Gost.Add("Н", "N");
            Gost.Add("О", "O");
            Gost.Add("П", "P");
            Gost.Add("Р", "R");
            Gost.Add("С", "S");
            Gost.Add("Т", "T");
            Gost.Add("У", "U");
            Gost.Add("Ф", "F");
            Gost.Add("Х", "KH");
            Gost.Add("Ц", "C");
            Gost.Add("Ч", "CH");
            Gost.Add("Ш", "SH");
            Gost.Add("Щ", "SHH");
            Gost.Add("Ъ", "'");
            Gost.Add("Ы", "Y");
            Gost.Add("Ь", "");
            Gost.Add("Э", "EH");
            Gost.Add("Ю", "YU");
            Gost.Add("Я", "YA");
            Gost.Add("а", "a");
            Gost.Add("б", "b");
            Gost.Add("в", "v");
            Gost.Add("г", "g");
            Gost.Add("д", "d");
            Gost.Add("е", "e");
            Gost.Add("ё", "jo");
            Gost.Add("ж", "zh");
            Gost.Add("з", "z");
            Gost.Add("и", "i");
            Gost.Add("й", "jj");
            Gost.Add("к", "k");
            Gost.Add("л", "l");
            Gost.Add("м", "m");
            Gost.Add("н", "n");
            Gost.Add("о", "o");
            Gost.Add("п", "p");
            Gost.Add("р", "r");
            Gost.Add("с", "s");
            Gost.Add("т", "t");
            Gost.Add("у", "u");

            Gost.Add("ф", "f");
            Gost.Add("х", "kh");
            Gost.Add("ц", "c");
            Gost.Add("ч", "ch");
            Gost.Add("ш", "sh");
            Gost.Add("щ", "shh");
            Gost.Add("ъ", "");
            Gost.Add("ы", "y");
            Gost.Add("ь", "");
            Gost.Add("э", "eh");
            Gost.Add("ю", "yu");
            Gost.Add("я", "ya");
            Gost.Add("«", "");
            Gost.Add("»", "");
            Gost.Add("—", "-");
            Gost.Add(" ", "");
        }

        public static string Translit(string rus)
        {
            string s = "";
            foreach (var letter in rus)
            {
                if (Gost.ContainsKey(letter.ToString()))
                {
                    s += Gost[letter.ToString()];
                }
                else
                {
                    s += letter.ToString();
                }
            }
            return s;
        }

        public static void MergeListAndDic(List<Node> nodes, ref Dictionary<Node, int> dic)
        {
            foreach (var node in nodes)
            {
                if (!dic.ContainsKey(node))
                {
                    dic.Add(node, 1);
                }
            }
        }
    }

    public class XmlHelper
    {
        static public string GetValChildTag(string xml, string tag)
        {
            var xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(xml);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return GetValChildTag(xmlDoc.DocumentElement, tag);
        }

        static public string GetValChildTag(XmlNode elem, string tag)
        {
            foreach (XmlNode xmlNode in elem)
            {
                if (xmlNode.Name == tag)
                    return xmlNode.InnerText;
            }
            return "";
        }

        static public string GetValChildChildTag(XmlNode elem, string tag, string tagChild)
        {
            foreach (XmlNode xmlNode in elem)
            {
                if (xmlNode.Name == tag)
                    return GetValChildTag(xmlNode, tagChild);
            }
            return "";
        }

        static public string GetValAttrChildTag(XmlNode elem, string tag, string attrName)
        {
            foreach (XmlNode xmlNode in elem)
            {
                if (xmlNode.Name == tag)
                {
                    if (xmlNode.Attributes != null) return xmlNode.Attributes[attrName].InnerText;
                }
            }
            return "";
        }
    }

    public class NodeType
    {
        public enum Types
        {
            Activity,
            If,
            EndIf,
            Split,
            Merge,
            Begin,
            End,
            Loop,
            EndLoop,
            Undef
        }

        public Types Type;
        public string TextType;
        public NodeType(string name)
        {
            TextType = name;

            if (name == "ActyvityDiagram_Pal.AD_Action_Element" || name == "BPELDiagram_Pal.AD_Action_Element")
            {
                Type = Types.Activity;
                return;
            }
            if (name == "ActyvityDiagram_Pal.AD_Begin_Element" || name == "BPELDiagram_Pal.AD_Begin_Element")
            {
                Type = Types.Begin;
                return;
            }

            if (name == "ActyvityDiagram_Pal.AD_Condition_Element" || name == "BPELDiagram_Pal.AD_Condition_Element")
            {
                Type = Types.If;
                return;
            }

            if (name == "ActyvityDiagram_Pal.AD_Division_Element" ||
                name == "ActyvityDiagram_Pal.AD_Division2_Element" ||
                name == "ActyvityDiagram_Pal.AD_Division3_Element" || name == "BPELDiagram_Pal.AD_Thread_Element")
            {
                Type = Types.Split;
                return;
            }
            if (name == "ActyvityDiagram_Pal.AD_End_Element" || name == "BPELDiagram_Pal.AD_End_Element")
            {
                Type = Types.End;
                return;
            }
            if (name == "ActyvityDiagram_Pal.AD_EndCondition_Element" || name == "BPELDiagram_Pal.AD_End_Condition_Element")
            {
                Type = Types.EndIf;
                return;
            }
            if (name == "ActyvityDiagram_Pal.AD_Merge_Element" ||
                name == "ActyvityDiagram_Pal.AD_Merge2_Element" ||
                name == "ActyvityDiagram_Pal.AD_Merge3_Element" || name == "BPELDiagram_Pal.AD_EndThread_Element")
            {
                Type = Types.Merge;
                return;
            }
            if (name == "BPELDiagram_Pal.AD_While_Element")
            {
                Type = Types.Loop;
                return;
            }
            if (name == "BPELDiagram_Pal.AD_End_While_Element")
            {
                Type = Types.EndLoop;
                return;
            }
            Type = Types.Undef;
        }
    }

    public class Node
    {
        public Guid Id;
        public NodeType Type;
        public object Tag;
        public string Name;
        public string Text;

        public string UnName;
        public static int NodeCounter = 0;
        public XmlNode xml;
        public Node(Guid gid, string tp)
        {
            Id = gid;
            Type = new NodeType(tp);
        }
        public Node(XmlNode elem)
        {
            xml = elem;
            Tag = "";
            Id = new Guid(XmlHelper.GetValChildTag(elem, "ID"));
            Type = new NodeType(XmlHelper.GetValAttrChildTag(elem, "Content_UserXML", "ObjectType"));
            Name = XmlHelper.GetValChildChildTag(elem, "Content_UserXML", "Name");
            Text = XmlHelper.GetValChildChildTag(elem, "Content_UserXML", "Text");
            GenUnName();
        }

        public void GenUnName()
        {
            string t = Text;
            if (t == "")
                t = Name;
            if (t == "")
                t = Type.ToString();
            UnName = MainHelper.Translit(String.Format("{0}_{1}", t, NodeCounter++));
        }
    }

    public class Edge
    {
        public Guid Id;
        public Guid From;
        public Guid To;
        public string Text;

        public Edge(Guid f, Guid t)
        {
            From = f;
            To = t;
        }

        public Edge(XmlNode elem)
        {
            From = new Guid(XmlHelper.GetValChildTag(elem, "SourceID"));
            To = new Guid(XmlHelper.GetValChildTag(elem, "SinkID"));
            Id = new Guid(XmlHelper.GetValChildTag(elem, "ID"));
            Text = XmlHelper.GetValChildTag(elem, "Text");
        }
    }

    public class Diagram
    {
        private const string BeginTag = "<Root>";
        private const string EndTag = "</Root>";

        public List<Node> Nodes = new List<Node>();
        public List<Edge> Edges = new List<Edge>();
        public string DiagramUnName;
        public static uint counter_diagrams = 1;
        public uint DiagramId;

        public Diagram(string filename)
        {
            DiagramId = counter_diagrams++;
            DiagramUnName = MainHelper.Translit(String.Format("{0}_{1}", Path.GetFileNameWithoutExtension(filename), DiagramId));
            var xml = ConvertToXml(filename);
            var xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(xml);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            if (xmlDoc.DocumentElement == null) return;
            /*designer items*/
            var items = xmlDoc.DocumentElement.ChildNodes[0].ChildNodes;
            foreach (XmlNode item in items)
            {
                Nodes.Add(new Node(item));
            }
            /*items*/
            items = xmlDoc.DocumentElement.ChildNodes[1].ChildNodes;
            foreach (XmlNode item in items)
            {
                Edges.Add(new Edge(item));
            }
        }

        public Node GetStartNode()
        {
            foreach (var node in Nodes)
            {
                if (node.Type.Type == NodeType.Types.Begin)
                    return node;
            }
            return null;
        }

        public Node FindNodeById(Guid id)
        {
            foreach (var nd in Nodes)
            {
                if (nd.Id == id)
                {
                    return nd;
                }
            }
            return null;
        }

        public List<Node> GetNextNodes(Node nd)
        {
            List<Node> list = new List<Node>();
            foreach (var edge in Edges)
            {
                if (edge.From == nd.Id)
                {
                    list.Add(FindNodeById(edge.To));
                }
            }
            return list;
        }

        private string ConvertToXml(string path)
        {
            string xml;
            try
            {
                var data = File.ReadAllText(path);
                var startPoz = data.IndexOf(BeginTag, StringComparison.Ordinal);
                var endPoz = data.IndexOf(EndTag, StringComparison.Ordinal);
                if ((startPoz < 0) || (endPoz < 0))
                    throw new Exception("Неверный формат файла диаграммы");
                xml = data.Substring(startPoz, endPoz - startPoz + EndTag.Length);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return xml;
        }

        public List<Node> GetPredNode(Node nd)
        {
            var dc = new Dictionary<Node, int>();
            foreach (var edge in Edges)
            {
                if (edge.To == nd.Id)
                {
                    Node tmpNode = FindNodeById(edge.From);
                    if (!dc.ContainsKey(tmpNode))
                    {
                        dc.Add(tmpNode, 1);
                    }
                }
            }
            return dc.Keys.ToList();
        }

        public List<Node> GetPredNodes(Node nd)
        {
            var dc = new Dictionary<Node, int>();
            var predNodes = new List<Node>();
            predNodes.AddRange(GetPredNode(nd));
            MainHelper.MergeListAndDic(predNodes, ref dc);
            foreach (var tmpNode in predNodes)
            {
                var tmpPred = GetPredNode(tmpNode);
                MainHelper.MergeListAndDic(tmpPred, ref dc);
                foreach (var tmp in tmpPred)
                {
                    MainHelper.MergeListAndDic(GetPredNode(tmp), ref dc);
                }
            }
            return dc.Keys.ToList();
        }

        /**
         * true если nd1 является предком nd2
         * */
        public bool IsPred(Node nd1, Node nd2)
        {
            if (nd1 == null)
                return true;
            if (nd2 == null)
                return false;
            List<Node> arr = GetPredNodes(nd2);
            foreach (var node in arr)
            {
                if (nd1.Id == node.Id)
                    return true;
            }
            return false;
        }
    }

    public class PromelaCodes
    {
        List<PromelaCode> array = new List<PromelaCode>();

        public List<string>  GlobalVars = new List<string>();
        public List<string>  Assets = new List<string>();
        
        public void addCode(PromelaCode cd)
        {
            array.Add(cd);
        }

        public void initRes(ResourcesDiagram rdg)
        {
            foreach (var resource in rdg.resources)
            {
                GlobalVars.Add(string.Format("short active_res_{0}", MainHelper.Translit(resource.Key)));
                GlobalVars.Add(string.Format("short err_res_{0}", MainHelper.Translit(resource.Key)));
                Assets.Add(string.Format("assert(err_res_{0} < 2)", MainHelper.Translit(resource.Key)));
            }
        }

        public string toString()
        {
            string code = "", init = "\r\ninit\r\n{";
            foreach (var globalVar in GlobalVars)
            {
                code += globalVar + ";\r\n";
            }
            foreach (var cd in array)
            {
                foreach (var method in cd.methods)
                {
                    code += method.body;
                }
                foreach (var funct in cd.initfunctions)
                {
                    init += "\t" + funct + ";\r\n";
                }
            }
            foreach (var asset in Assets)
            {
                init += "\t" + asset + ";\r\n";
            }
            return code + init + "}";
        }
    }

    public class PromelaCode
    {
        public class Method
        {
            public Node node;
            public string body;
            public string toString()
            {
                return "";
            }
        }
        public List<string> initfunctions = new List<string>();
        public List<Method> methods = new List<Method>();
        public string dgName;

        public void addInitRun(Diagram dg)
        {
            dgName = dg.DiagramUnName;
            initfunctions.Add(String.Format("run {0}()", dg.DiagramUnName));
        }

        public void addMethod(Node nd, string body)
        {
            var mt = new Method();
            mt.node = nd;
            mt.body = String.Format("\r\ninline {0}()\r\n{{\r\n\tprintf(\"{2}\\n\");\r\n\t{1}\r\n}}", nd.UnName, body,
                                    dgName + "->" + nd.UnName);
            methods.Add(mt);
        }

        public void AddMethodRaw(Node nd, string body)
        {
            var mt = new Method();
            mt.node = nd;
            mt.body = body;
            methods.Add(mt);
        }

        public void Fin(Diagram dg)
        {
            methods.Sort(
                delegate(Method m1, Method m2)
                {
                    return dg.IsPred(m1.node, m2.node) ? 1 : -1;
                });
        }

        public string toString(Diagram dg)
        {

            string code = "";
            foreach (var method in methods)
            {
                code += method.body;
            }
            code += "\r\ninit\r\n{";
            foreach (var funct in initfunctions)
            {
                code += "\t" + funct + ";\r\n";
            }
            return code + "}";
        }

    }
    public class ResourcesDiagram
    {
        public Dictionary<string, Resource> resources = new Dictionary<string, Resource>();
        public Dictionary<Guid, List<Resource>> dicRes = new Dictionary<Guid, List<Resource>>();

        public void addRes(Guid gid, string res)
        {
            if (!resources.ContainsKey(res))
            {
                resources[res] = new Resource(res);
            }
            List<Resource> lr = new List<Resource>();
            if (dicRes.ContainsKey(gid))
            {
                lr = dicRes[gid];
            }
            lr.Add(resources[res]);
            dicRes[gid] = lr;
        }

        public class Resource
        {
            public string Name;
            public string UnName;
            public Resource(string name)
            {
                Name = name;
                UnName = MainHelper.Translit(name);
            }
        }
    }

    public class DiagramsToPromela
    {
        private ResourcesDiagram resDg = new ResourcesDiagram();
        public void initResDg(string path)
        {
            var entries = Directory.GetFileSystemEntries(path);

            foreach (var entrie in entries)
            {
                if (Directory.Exists(entrie))
                {
                    continue;
                }

                if (Path.GetExtension(entrie) == ".modelr")
                {
                    var dg = new Diagram(entrie);
                    foreach (var node in dg.Nodes)
                    {
                        if (node.Type.TextType == "dataLibrary_Pal.ActionElement")
                        {
                            List<Node> res = dg.GetNextNodes(node);
                            Guid gd = Guid.Parse(XmlHelper.GetValChildTag(node.Text, "Id"));
                            foreach (var re in res)
                            {
                                string nm = XmlHelper.GetValChildTag(re.Text, "Name");
                                resDg.addRes(gd, nm);
                            }
                        }
                    }
                    int f = 0;
                }
            }
        }

        public DiagramsToPromela(string path, out string code)
        {
            PromelaCodes prcds = new PromelaCodes();
            MainHelper.Init();
            initResDg(path);
            prcds.initRes(resDg);
            var entries = Directory.GetFileSystemEntries(path);

            foreach (var entrie in entries)
            {
                if (Directory.Exists(entrie))
                {
                    continue;
                }

                if (Path.GetExtension(entrie) == ".modelcf")
                {
                    PromelaCode cd = new PromelaCode();
                    /*try
                    {*/
                    var dg = new Diagram(entrie);
                    cd.addInitRun(dg);
                    //initFunctions.Add(String.Format("run {0}()", dg.DiagramUnName));
                    foreach (var node in dg.Nodes)
                    {
                        var nds = dg.GetNextNodes(node);
                        string tmp = "";
                        switch (node.Type.Type)
                        {
                            case NodeType.Types.Activity:
                                if (resDg.dicRes.ContainsKey(node.Id))
                                {
                                    tmp =
                                        string.Format(
                                            "active_res_{0} = {1};if\r\n\t:: (active_res_{0} == 0 || active_res_{0} == {1}) -> err_res_{0} = 0;active_res_{0} = {1};\r\n\t:: else -> err_res_{0} = 2\r\n\tfi;\r\n\tactive_res_{0} = 0;",
                                            resDg.dicRes[node.Id][0].UnName, dg.DiagramId);
                                }
                                cd.addMethod(node, String.Format("{1}\r\n\t{0}();", nds[0].UnName, tmp));
                                break;
                            case NodeType.Types.Begin:
                                cd.addMethod(node, String.Format("{0}();", nds[0].UnName));
                                break;
                            case NodeType.Types.End:
                                cd.addMethod(node, "");
                                break;
                            case NodeType.Types.If:
                                cd.addMethod(node,
                                             String.Format("if\r\n\t:: (1==1) -> {0}()\r\n\t:: (1!=1) -> {1}()\r\n\tfi;",
                                                           nds[0].UnName, nds[1].UnName));
                                break;
                            case NodeType.Types.EndIf:
                                cd.addMethod(node, "");
                                break;
                        }
                    }
                    var nd = dg.GetStartNode();
                    cd.AddMethodRaw(null,
                                    String.Format("\r\nproctype {0}()\r\n{{\r\n\t{1}();\r\n}}", dg.DiagramUnName,
                                                  nd.UnName));
                    cd.Fin(dg);
                    prcds.addCode(cd);
                    /*}
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        buf = "";
                    }*/
                }

            }
            code = prcds.toString();
        }
    }

}
