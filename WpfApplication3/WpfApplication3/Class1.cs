using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OxyPlot.Annotations;
using System.Collections.Generic;

namespace WpfApplication3
{

    

    

    public class MainViewModel
    {




        public MainViewModel()
        {
            this.Title = "Solution Plot for Poisson 1D";
            this.Points = new List<DataPoint> { };
 }
        
        /*
        public void UpdateModel()
        {
            List<Measurement> measurements = Data.GetUpdateData(lastUpdate);
            var dataPerDetector = measurements.GroupBy(m => m.DetectorId).OrderBy(m => m.Key).ToList();

            foreach (var data in dataPerDetector)
            {
                var lineSerie = PlotModel.Series[data.Key] as LineSeries;
                if (lineSerie != null)
                {
                    data.ToList()
                        .ForEach(d => lineSerie.Points.Add(new DataPoint(DateTimeAxis.ToDouble(d.DateTime), d.Value)));
                }
            }
            lastUpdate = DateTime.Now;
        }
*/

        
        public void Set(double x, double y) {


            this.Points.Add(new DataPoint(x, y));
                        
                            

            

        }

        public void Clean() {
                this.Points = new List<DataPoint> { };
    
            }



  public string Title { get; private set; }
  public IList<DataPoint> Points { get; private set; }
    }
}