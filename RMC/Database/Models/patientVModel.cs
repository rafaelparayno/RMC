using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class patientVModel
    {
        public int id { get; set; }

        public int patient_id { get; set; }

        public string date_vital { get; set; }
        public string bp { get; set; }
        public string temp { get; set; }
        public float wt { get; set; }
        public float height { get; set; }
        public string lmp { get; set; }
        public string heartrate { get; set; }

        public string allergies { get; set; }

        private float meter
        {
            get { return height / 100; }
        }

        public double bmi
        {
            get { return Math.Round(wt / (meter * meter),2); }
        }


        public string bmiLabel
        {
            get
            {
                if(bmi< 18.5)
                {
                    return "Normal";
                }
                else if(bmi > 18.5 && bmi <24.9)
                {
                    return "Normal";
                }
                else if (bmi > 25.0 && bmi < 29.9)
                {
                    return "Overweight";
                }
                else
                {
                    return "Obese";
                }
            }
        }
    }
}
