using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        MainViewModel p = new MainViewModel();
        MainViewModel p1 = new MainViewModel();
        PoissonSolvers1D p1D;
        public int ndim;
        public double x0, x1;
        public double[] x;
        public double dh;
        public double f0, f1;

        public MainWindow()
        {
            InitializeComponent();
            
            
            comboBox1.Items.Insert(0,"30*sin(sqrt(30)*x)");
            comboBox1.Items.Insert(1, "1+2*x^2-12*x^4");
            comboBox1.Items.Insert(2, "x");
            comboBox1.Items.Insert(3, "e^(-x^2)*Cos(10*x)");
            comboBox1.Items.Insert(4, "1.0 - 2.0*x^2");
            comboBox1.Items.Insert(5, "2*Sech(x)*Tanh(x)");
            comboBox1.SelectedIndex = 0;
            
            /*
            
//            DataContext = p;

            ndim = 1212;


            x0 = 0.0;
            x1 = 1.0;
            double[] x = new double[ndim];
            double dh = (x1 - x0) / (ndim - 1);

            f0 = 0.0;
            f1 = 1.0;
            
            
            x = new double [ndim];
            dh = (x1-x0)/(ndim-1);

            double tempVal;
            double[] rhs = new double[ndim];



            p1D = new PoissonSolvers1D(ndim);
            


            p1D.initPoint = f0;
            p1D.endPoint = f1;
            p1D.dh = dh;

         
            for (int i = 0; i < ndim; i++)
            {
                x[i] = i*dh;
//                tempVal = 1.0 + 2.0 * x[i] * x[i] - 12 * x[i] * x[i] * x[i] * x[i];
                tempVal  =  30.0 * Math.Sin(Math.Sqrt(30.0) * x[i]);
                
                p1D.setRHS(i, tempVal);
//                    Console.WriteLine("  {0:0.##} ", p1D.getB(i));
             
            }

            p1D.augB();







      

         double [] xx = new double[ndim];

         xx = p1D.Solve();
        

            for(int i = 1; i <ndim; i++ )
            {
                p1.Set(x[i], xx[i]);

            }
        */
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
//            p.Set(-2.0, 4.0);
            //p.Set(-4.0, 14.0);
//            p.Set(13.0, -14.0);
            
            
            //p.Set(1.0, 2.0);

            string txt1 = nPnt.Text;
            string txt2 = point0.Text;
            string txt3 = point1.Text;
            string txt4 = func0.Text;
            string txt5 = func1.Text;


            if ((String.IsNullOrEmpty(txt1.Trim())) || (String.IsNullOrEmpty(txt2.Trim())) || (String.IsNullOrEmpty(txt3.Trim())) || (String.IsNullOrEmpty(txt4.Trim())) || (String.IsNullOrEmpty(txt5.Trim())))

            {
                MessageBox.Show("Error. Please input all required parameters");
                return;
            
            }

            ndim = int.Parse(nPnt.Text);


            x0 = double.Parse(point0.Text);
            x1 = double.Parse(point1.Text);

            x = new double[ndim];
            dh = (x1 - x0) / (ndim - 1);

            f0 = double.Parse(func0.Text);
            f1 = double.Parse(func1.Text);



            double tempVal;
            double[] rhs = new double[ndim];



            p1D = new PoissonSolvers1D(ndim);



            


            p1D.initPoint = f0;
            p1D.endPoint = f1;
            p1D.dh = dh;

            tempVal = 0;

            if (comboBox1.SelectedIndex == -1)
            {

                MessageBox.Show("No Item is Selected, Choosing Default Value");
                comboBox1.SelectedIndex = 0;
            }

            for (int i = 0; i < ndim; i++)
            {
                x[i] = x0 + i * dh;
                
                if (comboBox1.SelectedIndex == 0)
                {
                    tempVal = 30.0 * Math.Sin(Math.Sqrt(30.0) * x[i]);
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                               tempVal = 1.0 + 2.0 * x[i] * x[i] - 12 * x[i] * x[i] * x[i] * x[i];
                }

                else if (comboBox1.SelectedIndex == 2)
                {
                    tempVal = x[i];
                }

                else if (comboBox1.SelectedIndex == 3)
                {
                    tempVal = Math.Exp(-x[i]*x[i])*Math.Cos(10.0*x[i]);
                }
                else if (comboBox1.SelectedIndex == 4)
                {
                    tempVal = 1.0 - 2.0*x[i]*x[i];
                }
                else if (comboBox1.SelectedIndex == 5)
                {
                    tempVal = 2.0 * (1.0 / (Math.Exp(x[i]) + Math.Exp(-x[i]))) * Math.Tanh(x[i]);
                }


                p1D.setRHS(i, tempVal);
                

            }

            p1D.augB();









            double[] xx = new double[ndim];

            xx = p1D.Solve();


            
            for (int i = 0; i < ndim; i++)
            {
                p1.Set(x[i], -xx[i]);

            }
            

            DataContext = p1;

            
            x = null;
            p1D = null;
            rhs = null;
            xx = null;
            
        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9-+]*(?:\.[0-9]*)?$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void NumberValidationTextBox1(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cln_Click(object sender, RoutedEventArgs e)
        {
            p1.Clean();
            DataContext = p;


        }

    
    }
}
