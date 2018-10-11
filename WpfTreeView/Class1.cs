using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTreeView
{
    public class Class1 : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged =(sender,e) => { };

        public string Test { get; set; } = "My Property";
        public Class1()
        {
            Task.Run(async () =>
            {
                int i = 0;
                while(true)
                {
                    await Task.Delay(200);
                    Test = (i++).ToString();
                }
            });
        }

        public override string ToString()
        {
            return "Hello World";
        }
    }
}
