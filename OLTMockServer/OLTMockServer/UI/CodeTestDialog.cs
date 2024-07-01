using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using static OLTMockServer.UI.CodeTestDialog;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OLTMockServer.UI
{
    public partial class CodeTestDialog : RadForm
    {
        public CodeTestDialog()
        {
            InitializeComponent();

            var dogs = new List<Dog>();
            dogs.Add(new Dog() { Name = "dany", Age = 3, Wight = 8, EatBone = true });
            dogs.Add(new Dog() { Name = "rex", Age = 7, Wight = 14, EatBone = true });
            radGridView.DataSource = dogs;
        }
    }

    public class animal
    {
        public int Age { get; set; }
        public int Wight { get; set; }
    }

    public class Dog : animal
    {
        public bool EatBone { get; set; }
        public string Name { get; set; }
    }
}
