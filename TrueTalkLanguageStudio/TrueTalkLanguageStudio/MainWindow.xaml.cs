
namespace TrueTalkLanguageStudio
{
    using edu.stanford.nlp.ling;
    using edu.stanford.nlp.parser.lexparser;
    using edu.stanford.nlp.process;
    using edu.stanford.nlp.trees;
    using java.io;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Console = System.Console;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Prevent the TreeView from responding to the keyboard.
            // Since there's no sane way to set a TreeView's SelectedItem,
            // we can't customize the keyboard navigation logic. :(
            this.tree.PreviewKeyDown += delegate (object obj, KeyEventArgs args) { args.Handled = true; };

            // Put some data into the TreeView.
            this.PopulateTreeView();
        }

        void PopulateTreeView()
        {
            Node root = new Node("Big Daddy Root");

            int branches = 0;
            int subBranches = 0;

            for (int i = 0; i < 2; ++i)
            {
                Node child = new Node("Branch " + ++branches);
                root.ChildNodes.Add(child);

                for (int j = 0; j < 3; ++j)
                {
                    Node gchild = new Node("Sub-Branch " + ++subBranches);
                    child.ChildNodes.Add(gchild);

                    for( int k = 0; k < 2; ++k )
                    {
                        gchild.ChildNodes.Add(new Node("Leaf"));
                    }
                }
            }

            // Create a dummy node so that we can bind the TreeView
            // it's ChildNodes collection.
            Node dummy = new Node();
            dummy.ChildNodes.Add(root);

            this.tree.ItemsSource = dummy.ChildNodes;
        }

        private void ParseButton_Click(object sender, RoutedEventArgs e)
        {
            // Loading english PCFG parser from file
            var lp = LexicalizedParser.loadModel(@"C:\src\TrueTalk\TrueTalkLanguageStudio\Resources\Models\englishPCFG.ser.gz");

            // This sample shows parsing a list of correctly tokenized words
            var sent = new[] { "This", "is", "an", "easy", "sentence", "." };
            var rawWords = SentenceUtils.toCoreLabelList(sent);
            var tree = lp.apply(rawWords);
            tree.pennPrint();

            // This option shows loading and using an explicit tokenizer
            var sent2 = "When temperature is higher than 70 degrees start air conditioning.";
            var tokenizerFactory = PTBTokenizer.factory(new CoreLabelTokenFactory(), "");
            var sent2Reader = new StringReader(sent2);
            var rawWords2 = tokenizerFactory.getTokenizer(sent2Reader).tokenize();
            sent2Reader.close();
            var tree2 = lp.apply(rawWords2);
            tree2.pennPrint();

            var children = tree2.children( );
            var children2 = children[0].children();
            var children3 = children2[0].children();
            var children4 = children3[0].children();
            var children5 = children4[0].children();
            var children6 = children5[0].children();

            var constituents0 = children4[0].isPreTerminal();
            var constituents00 = children4[0].isLeaf();
            var constituents000 = children4[0].label();
            var constituents1 = children4[0].children();
            var constituents11 = constituents1[0].isLeaf();
            var constituents2 = children2[2].isPreTerminal();


            // Extract dependencies from lexical tree
            var tlp = new PennTreebankLanguagePack();
            var gsf = tlp.grammaticalStructureFactory();
            var gs = gsf.newGrammaticalStructure(tree2);
            var tdl = gs.typedDependenciesCCprocessed();
            Console.WriteLine("\n{0}\n", tdl);

            // Extract collapsed dependencies from parsed tree
            var tp = new TreePrint("penn,typedDependenciesCollapsed");
            tp.printTree(tree2);

        }

        private void ProgramTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ParseProgramTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
