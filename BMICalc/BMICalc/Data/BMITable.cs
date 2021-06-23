using System;
using System.Collections.Generic;
using System.Text;

namespace BMICalc
{
    //This is the lookup-table for the BMI-Calculator page
    public class BMITable
    {
        public string GetBMIInfo(double BMI)
        {

            if (BMI <= 18.5) { return "Underweight"; }
            if (BMI <= 24.9) { return "Normal weight"; }
            if (BMI <= 29.9) { return "Overweight"; }
            if (BMI <= 34.9) { return "Obese"; }
            if (BMI >= 35) { return "Extremely Obese"; }

            return null;
        }
    }
}
