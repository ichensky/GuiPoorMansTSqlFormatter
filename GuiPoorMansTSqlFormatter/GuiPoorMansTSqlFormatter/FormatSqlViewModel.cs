using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using PoorMansTSqlFormatterLib.Formatters;
using PoorMansTSqlFormatterLib.Interfaces;
using PoorMansTSqlFormatterLib.Parsers;
using PoorMansTSqlFormatterLib.Tokenizers;
using System.Threading;
using System.Windows;

namespace GuiPoorMansTSqlFormatter
{
    public class FormatSqlViewModel : BaseViewModel<FormatSqlViewModel>
    {
        static readonly ISqlTokenizer tokenizer = new TSqlStandardTokenizer();
        static readonly ISqlTokenParser parser = new TSqlStandardParser();
        static readonly TSqlStandardFormatter treeFormatter = new TSqlStandardFormatter();

        private Thread t;
        public FormatSqlViewModel(string rootFolderPath, Delegate dDelegate)
        {
            t = new Thread(x =>
                {
                    var files = new DirectoryInfo(rootFolderPath).GetFiles("*.sql", SearchOption.AllDirectories);
                    var i = 0.0;
                    try
                    {
                        foreach (var item in files)
                        {
                            item.FullName.SetRWAtt();
                            GetFormatedString(item.FullName.ReadFile()).WriteToFile(item.FullName);
                            Application.Current.Dispatcher.Invoke(dDelegate, DispatcherPriority.Background,
                                                                  new object[]
                                                                      {
                                                                          RangeBase.ValueProperty,
                                                                          ((++i*100)/files.Length)
                                                                      });

                        }

                    }
                    catch (Exception)
                    {
                        throw new Exception("Epic Feil.");
                    }

                }) {IsBackground = true};
            t.Start();
            
        }

        static string GetFormatedString(string inputSql)
        {
            var tokenized = tokenizer.TokenizeSQL(inputSql);
            var parsed = parser.ParseSQL(tokenized);
            return treeFormatter.FormatSQLTree(parsed);
        }
    }
}
