/*
 * Course: CSCI-473   Assignment :4   Spring 2021
 * 
 * Erind Hysa   zid: z1879691
 * Simeon Lico  zid: z1885981
 * 
 * Due Date : 03/18/2021
 * 
 * Description:
 * This windows forms  is creating a drawing calculator which provides calculations
 * for drawing linear,quadratic,cubic and circle equations. It has an option to change 
 * the limits of xmax xmin ymax y min and xiterval,y interval. Also it lets you pick
 * a different color of your choice for every graph. It provided a clear graph option if
 * you decide to clear the graphs that are drawn.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace ErindSimeon_Assignment4
{
    public partial class Form1 : Form
    {
        private static Pen whitePen; //pen selected for axis
        private static Pen selectedPen; // pen selected for drawing equations
        public static float xScale; // xScale variable
        public static float yScale; //yScale variable
        public static float xAxis; //xAxis variable
        public static float yAxis; //yAxis variable 
        Font drawFont = new Font("Arial", 8); //drawFont set to Arial size 8
        SolidBrush drawBrush = new SolidBrush(Color.White); //brush color white
        public Form1()
        {
            InitializeComponent();
            whitePen = new Pen(Color.White); //leave whitePen default to white
            calc(); //calll calc functions
        }
        /*
        * calc() this method will calculate the x and y point of x and y axis 
        * params: none
        * return: none
         */
        private void calc()
        {
            
            if (xMaxNumeric.Value < 0) // if xMax is negative use the below formulat to calculate the xScale
            {
                float distance = (float)Math.Abs(xMinNumeric.Value) - (float)Math.Abs(xMaxNumeric.Value);
                xScale = 600 / distance;
            }
            else if(xMinNumeric.Value > 0) // xMin is postive
            {
                float distance = (float)Math.Abs(xMaxNumeric.Value) - (float)Math.Abs(xMinNumeric.Value);
                xScale = 600 / distance;
            }
            else // else xmax is positive and x min is negative
            {
                float distance = (float)Math.Abs(xMinNumeric.Value) + (float)Math.Abs(xMaxNumeric.Value);
                xScale = 600 / distance;
            }
            if (yMaxNumeric.Value < 0) // if yMax is negative we use the below formula to calcualte x sclae
            {
                float distance = (float)Math.Abs(yMinNumeric.Value) - (float)Math.Abs(yMaxNumeric.Value);
                yScale = 600 / distance;
            }
            else if(yMinNumeric.Value > 0) //Ymins is postive
            {
                float distance = (float)Math.Abs(yMaxNumeric.Value) - (float)Math.Abs(yMinNumeric.Value);
                yScale = 600 / distance;
            }
            else // yMax is postiive and we the abs value of yMin with yMax
            {
                float distance = (float)Math.Abs(yMinNumeric.Value) + (float)Math.Abs(yMaxNumeric.Value);
                yScale = 600 / distance;
            }
            xAxis = (float)Math.Abs(yMaxNumeric.Value) * yScale; // coordinates of xAxis
            yAxis = (float)Math.Abs(xMinNumeric.Value) * xScale; // coordinates of yAxis
        }
        /*
        * ConvertXPoint(float xPoint) this method converts the Xpoint in our pannel coordinates
        * params: xPoint
        * return: the representative point of XPoint on the pannel
         */
        private float ConvertXPoint(float xPoint)
        {
            if (xMaxNumeric.Value < 0) //if xMaxNumeric is negative than the distance will be absMin - abs ofr Max
            {
                float distance= (float)(Math.Abs(xMinNumeric.Value) - Math.Abs(xMaxNumeric.Value));
                return (600 * (xPoint + (float)Math.Abs(xMinNumeric.Value)) / distance);
            }
            else if (xMinNumeric.Value > 0) // if xMinNumeric is postive than the distance will be max - min
            {
                float distance = (float)(Math.Abs(xMaxNumeric.Value) - Math.Abs(xMinNumeric.Value));
                return 600 * (xPoint - (float)xMinNumeric.Value) / distance;
            }//else for cases where xMin is negative and xMax is postive the distance is the sum of their absolute values
            else
            {
                float distance= (float)(Math.Abs(xMinNumeric.Value) + Math.Abs(xMaxNumeric.Value));
                return (600 * (xPoint + (float)Math.Abs(xMinNumeric.Value)) /distance);
            }
        }
        /*
        * ConvertYPoint(float yPoint) this method converts the ypoint in our pannel coordinates
        * params: yPoint
        * return: the representative point of yPoint on the pannel
        */
        private float ConvertYPoint(float yPoint)
        {
            if (yMaxNumeric.Value < 0) // if yMax is negative than the distance will be Absoulte value of ymin - Absoulte value of yMax
            {
                float distance = (float)(Math.Abs(yMinNumeric.Value) - Math.Abs(yMaxNumeric.Value));
                float num = distance - ((float)Math.Abs(yMinNumeric.Value) + yPoint);
                return (600 * (num / distance));
            }
            else if (yMinNumeric.Value > 0) // if yMin is postive than the distaance will be Ymax - yMin
            {
                float distance = (float)(Math.Abs(yMaxNumeric.Value) - Math.Abs(yMinNumeric.Value));
                return 600 * (distance - (yPoint - (float)yMinNumeric.Value)) / distance;
            }
            else // else if xMax is positve and xMax is negative than the distance will be the sum of their absolute values
            {
                float distance = (float)(Math.Abs(yMinNumeric.Value) + Math.Abs(yMaxNumeric.Value));
                float num = distance - ((float)Math.Abs(yMinNumeric.Value) + yPoint);
                return (600 * (num / distance));
            }
        }
        /*
         *  graphLinear(object sender, EventArgs e) this method will graph the line represented by the linear equation
         * params: Sender: Reference to the object that called this function,
         *         EventArgs: The arguments passed from the calling object
         * return: none
         */
        private void graphLinear(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            Graphics g = canvas.CreateGraphics();
            colorDlg.AllowFullOpen = false;
            int number;
            if (int.TryParse(m.Text.ToString(), out number) && int.TryParse(b.Text.ToString(), out number)) // check to see if entered input is a number
            {
                if (colorDlg.ShowDialog() == DialogResult.OK) // check to see if colorDialog opened correctly
                {
                    selectedPen = new Pen(colorDlg.Color); // give the selectedPen the picked color
                }             
                float x1 = (float)xMinNumeric.Value; // give the x1 the xMin value
                float y1 = (x1 * (float.Parse(m.Text)) + float.Parse(b.Text)); //calculate teh value of y1 by using the value of x1
                x1 = ConvertXPoint(x1); //convert to x1 coordiante
                y1 = ConvertYPoint(y1); //convert to y1 cooordinate
                float x2 = (float)xMaxNumeric.Value; //x2 is eqaul to xmaxNumeric
                float y2 = (x2 * (float.Parse(m.Text)) + float.Parse(b.Text)); //find hte values of y2 for x2 = xMax
                x2 = ConvertXPoint(x2); //convert to x2 coordinate
                y2 = ConvertYPoint(y2); //convert to y1 coordinate
                g.DrawLine(selectedPen, x1, y1, x2, y2); //draw linear graph
            }
            else //if the input is not a number display the message below
            {
                MessageBox.Show("Linear Equation is on the format y=mx+b where m and b must be numbers");
            }
        }
        /*
         * graphQuadratic(object sender, EventArgs e) this method will draw the quadratic equation represented by teh entered quadratic equation
         * params: Sender: Reference to the object that called this function,
         *         EventArgs: The arguments passed from the calling object
         * return: none
         */
        private void graphQuadratic(object sender, EventArgs e)
        {
            Graphics g = canvas.CreateGraphics();
            ColorDialog colorDlg = new ColorDialog();
            Pen whitePen = new Pen(Color.White);
            colorDlg.AllowFullOpen = false;
            int number;
            if (int.TryParse(a.Text.ToString(), out number) && int.TryParse(bQuad.Text.ToString(), out number) && int.TryParse(bQuad.Text.ToString(), out number))// check to see if entered input is a number
            {
                if (colorDlg.ShowDialog() == DialogResult.OK)// check to see if colorDialog opened correctly
                {
                    selectedPen = new Pen(colorDlg.Color);// give the selectedPen the picked color
                }
                float aVal = Convert.ToSingle(a.Text); // get the value of a
                float bVal = Convert.ToSingle(bQuad.Text); //get the value of b
                float cVal = Convert.ToSingle(c.Text); // get the value of c                                                   
                List<PointF> myList = new List<PointF>(); //declare a lsit of Points
                //iterate from xmin to xmax and add the x1,y1 to the list
                for (float x = Convert.ToSingle(xMinNumeric.Value); x < Convert.ToSingle(xMaxNumeric.Value); x++)
                {
                    float y = (aVal * x * x) + (bVal * x) + cVal; //calcualte the value of y
                    float x1 = ConvertXPoint(x); //convert to x coordinate
                    float y1 = ConvertYPoint(y); //convert to y coordinate
                    myList.Add(new PointF(x1, y1)); //add the point to the list
                }
                PointF[] pointsArray = myList.ToArray(); //convert the list to an Array
                g.DrawCurve(selectedPen, pointsArray); //draw the quadratic equation
            }
            else //display this message if the input is not a number
            {
                MessageBox.Show("Quadratic Equation is on the format y=ax2+bx+c where a,b and c must be numbers");
            }
        }
        /*
        * graphCubic(object sender, EventArgs e) this method will draw the cubic equation represented the by the entered cubic equation
        * params: Sender: Reference to the object that called this function,
        *         EventArgs: The arguments passed from the calling object
        * return: none
        */
        private void graphCubic(object sender, EventArgs e)
        {
            Graphics g = canvas.CreateGraphics();
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = false;
            int number;
            // check to see if entered input is a number
            if (int.TryParse(aCub.Text.ToString(), out number) && int.TryParse(bCub.Text.ToString(), out number) && int.TryParse(cCub.Text.ToString(), out number) && int.TryParse(dCub.Text.ToString(), out number))
            {
                if (colorDlg.ShowDialog() == DialogResult.OK)// check to see if colorDialog opened correctly
                {
                    selectedPen = new Pen(colorDlg.Color);// give the selectedPen the picked color
                }
                float aVal = Convert.ToSingle(aCub.Text); // get the value of a coefficient
                float bVal = Convert.ToSingle(bCub.Text); //get the value of b coefficient
                float cVal = Convert.ToSingle(cCub.Text); //get the value of c coefficient
                float dVal = Convert.ToSingle(dCub.Text); //get the value of d coefficient
                List<PointF> myList = new List<PointF>(); //declare a list of points
                //iterate from xmin to xmax and add the points to the list
                for (float x = Convert.ToSingle(xMinNumeric.Value); x < Convert.ToSingle(xMaxNumeric.Value); x++)
                {
                    float y = (aVal * x * x * x) + (bVal * x * x) + (cVal * x) + dVal; //calculate the y value
                    float x1 = ConvertXPoint(x); //convert to x coord
                    float y1 = ConvertYPoint(y); //convert to y coord
                    myList.Add(new PointF(x1, y1)); //add the new point to hte list
                }
                PointF[] pointsArray = myList.ToArray(); //convert teh list to an array
                g.DrawCurve(selectedPen, pointsArray); //draw the cubic equation
            }
            else // if the input is not a number display the message below
            {
                MessageBox.Show("Put a number cofficient! Click the Hint button for mroe details!");
            }
        }
        /*
        * graphCircle(object sender, EventArgs e) this method will draw a circle with center h,k and radius r
        * params: Sender: Reference to the object that called this function,
        *         EventArgs: The arguments passed from the calling object
        * return: none
        */
        private void graphCircle(object sender, EventArgs e)
        {
            Graphics g = canvas.CreateGraphics();
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = false;
            int number;
            if (int.TryParse(h.Text.ToString(), out number) && int.TryParse(k.Text.ToString(), out number) && int.TryParse(r.Text.ToString(), out number))// check to see if entered input is a number
            {
                if (colorDlg.ShowDialog() == DialogResult.OK)// check to see if colorDialog opened correctly
                {
                    selectedPen = new Pen(colorDlg.Color);// give the selectedPen the picked color
                }
                 calc(); //call calc function
                float hVal = Convert.ToSingle(h.Text); //get the h value
                float kVal = Convert.ToSingle(k.Text); // get the k value
                float rVal = Convert.ToSingle(r.Text); //get the r value
                float a = ConvertXPoint(hVal) - rVal * xScale; //calculate the coord of x for the center
                float b = ConvertYPoint(kVal) - rVal *yScale; //calcualte the coord of y for the center
                float c = 2 * rVal*xScale; //calulate the radius for xScale
                float d = 2 * rVal*yScale; //calculate the radius for yScale
                //g.DrawEllipse(selectedPen, a, b, c, d); //draw the circle equation
				g.DrawEllipse(selectedPen, ConvertXPoint(45), ConvertYPoint(55), 10 *xScale, 10 *yScale);
                g.DrawEllipse(selectedPen, ConvertXPoint(40), ConvertYPoint(60), 20 * xScale, 20 * yScale);
                g.DrawEllipse(selectedPen, ConvertXPoint(35), ConvertYPoint(65), 30 * xScale, 30 * yScale);
              
            }
            else //if the input is not a number display the message below
            {
                MessageBox.Show("Put number coefficients! For mroe details click the Hint button!");
            }
        }
        /*
        * zeroToYmax( float YAxis) this function is responsible of drawing tickmarks from the center to Ymax value, also with tickmars draws the labels too
        * params:float YAxis
        * return: none
        */
        private void zeroToYmax( float YAxis)
        {
            Graphics g = canvas.CreateGraphics();
            //it iterates from 0 to yMax and draws ticks every yInterval value and also calls drawLabelsYPositive(i) to draw the labels
            for (int i = 0; i < yMaxNumeric.Value; i++)
            {
                if (i % yInterval.Value == 0) // draw the ticks every yIntervalValue
                {
                    g.DrawLine(whitePen, YAxis - 5, xAxis - i * yScale, YAxis + 5, xAxis - i * yScale); //draw the ticks
                }
                if(xMinNumeric.Value > 0) // draw labels for cases where xMin is positve
                {
                    drawLabels("", i, drawFont, drawBrush,  10, xAxis - (i * yScale) - 7, (float)yInterval.Value);
                }
                else if(xMaxNumeric.Value < 0) //draw labels for cases where xMax is negative
                {
                    drawLabels("", i, drawFont, drawBrush, 580, xAxis - (i * yScale) - 7, (float)yInterval.Value);
                }
                else //draw labeles for cases where xmin is negative and xmax is positve
                {
                    drawLabels("", i, drawFont, drawBrush, yAxis - 20, xAxis - (i * yScale) - 7, (float)yInterval.Value);
                }                                    
            }
        }
        /*
        * zeroToYmin( float YAxis) this function is responsible of drawing tickmarks from the center to Yin value, also with tickmars draws the labels too
        * params:float YAxis
        * return: none
        */
        private void zeroToYmin(float YAxis)
        {
            Graphics g = canvas.CreateGraphics();
            //it iterates from 0 to yMax and draws ticks every yInterval value and also calls drawLabelsYNegative(i) to draw the labels
            for (int i = 0; i < Math.Abs(yMinNumeric.Value); i++)
            {
                if (i % yInterval.Value == 0) // draw the ticks every yIntervalValue
                {
                    g.DrawLine(whitePen, YAxis - 5, xAxis + i * yScale, YAxis + 5, xAxis + i * yScale); // draw the ticks
                }
                if(xMaxNumeric.Value < 0) //draw labels for cases where xmax is negative
                {
                    drawLabels("-", i, drawFont, drawBrush, 570, xAxis + (i * yScale) - 7, (float)yInterval.Value);
                }
                else if(xMinNumeric.Value > 0) //darw labels for cases where xmin is positve
                {
                    drawLabels("-", i, drawFont, drawBrush, 10, xAxis + (i * yScale) - 7, (float)yInterval.Value);
                }
                else //draw labeles for cases where xmin is negative and xMax is positive
                {
                    drawLabels("-", i, drawFont, drawBrush, yAxis - 25, xAxis + (i * yScale) - 7, (float)yInterval.Value);
                }    
            }
        }
        /*
        * zeroToXmax( float XAxis) this function is responsible of drawing tickmarks from the center to xMax value, also with tickmars draws the labels too
        * params:float XAxis
        * return: none
        */
        private void zeroToXmax(float XAxis)
        {
            Graphics g = canvas.CreateGraphics();
            //it iterates from 0 to xMax and draws ticks every xInterval value and also calls drawLabelsYPositive(i) to draw the labels
            for (int i = 0; i < xMaxNumeric.Value; i++)
            {
                if (i % xInterval.Value == 0)// draw the ticks every xIntervalValue
                {
                    g.DrawLine(whitePen, yAxis + (i * xScale), XAxis - 5, yAxis + (i * xScale), XAxis + 5); //draw the ticks
                }
                if(yMinNumeric.Value > 0) //draw labeles for cases where ymin is positive
                {
                    drawLabels("", i, drawFont, drawBrush, yAxis + (i * xScale) - 7, 600 - 20, (float)xInterval.Value);

                }
                else if (yMaxNumeric.Value < 0) //draw labeles for cases where yMax is negative
                {
                    drawLabels("", i, drawFont, drawBrush, yAxis + (i * xScale) - 7, 10, (float)xInterval.Value);
                }
                else //draw labels for cases where ymax positive and ymin is negative
                {
                    drawLabels("", i, drawFont, drawBrush, yAxis + (i * xScale) - 7, xAxis + 5, (float)xInterval.Value);
                }
            }
        }
        /*
        * zeroToXmin( float XAxis) this function is responsible of drawing tickmarks from the center to xMin value, also with tickmars draws the labels too
        * params:float XAxis
        * return: none
        */
        private void zeroToXmin(float XAxis)
        {
            Graphics g = canvas.CreateGraphics();    
            //it iterates from 0 to xMin and draws ticks every xInterval value and also calls drawLabelsYPositive(i) to draw the labels
            for (int i = 0; i < Math.Abs(xMinNumeric.Value); i++)
            {
                if (i % xInterval.Value == 0)// draw the ticks every xIntervalValue
                {
                    g.DrawLine(whitePen, yAxis - (i * xScale), XAxis - 5, yAxis - (i * xScale), XAxis + 5); //draw the ticks
                }
                if (yMinNumeric.Value >0 ) // draw labels for cases where ymin is postive
                {
                    drawLabels("-", i, drawFont, drawBrush, yAxis - (i * xScale) - 7, 580, (float)xInterval.Value);
                } else if (yMaxNumeric.Value <0) //draw labels for cases where ymax is negative
                {
                    drawLabels("-", i, drawFont, drawBrush, yAxis - (i * xScale) - 7, 10, (float)xInterval.Value);
                }
                else //draw labeles for casses where ymax is postive and ymin negative
                {
                    drawLabels("-", i, drawFont, drawBrush, yAxis - (i * xScale) - 7, xAxis + 5, (float)xInterval.Value);
                }                
            }
        }
        /*
        * drawLabels(string s,int i, Font x, Brush y, float xPoint, float yPoint, float interval) this function draws labels
        * params:string s,int i, Font x, Brush y, float xPoint, float yPoint, float interval
        * return: none
        */
        private void drawLabels(string s,int i, Font x, Brush y, float xPoint, float yPoint, float interval )
        {
            Graphics g = canvas.CreateGraphics();
            if (interval <= 5)//draw labels every for intervals less than 5
            {
                if (i % 10 == 0 && i != 0) //draw labeles every 10 ticks
                {                 
                    g.DrawString(s + i.ToString(), x, y, xPoint, yPoint);
                }
            }
            else if (interval <= 10)//draw labels every for intervals less than 10
            {
                if (i % 20 == 0 && i != 0) //draw labeles every 20 ticks
                {
                    g.DrawString(s + i.ToString(), x, y, xPoint, yPoint);
                }
            }
            else//draw labels every for intervals bigger 10
            {
                if (i % 50 == 0 && i != 0) //draw labeles every 50 ticks
                {
                    g.DrawString(s + i.ToString(), x, y, xPoint, yPoint);
                }
            }
        }
        /*
        *drawLabelsCases(string s, int i, Font x, Brush y, float xPoint, float yPoint, float interval ,float minValue) this function draws labels
        * params:string s, int i, Font x, Brush y, float xPoint, float yPoint, float interval ,float minValue
        * return: none
        */
        private void drawLabelsCases(string s, int i, Font x, Brush y, float xPoint, float yPoint, float interval ,float minValue)
        {
            Graphics g = canvas.CreateGraphics();
            if (interval == 1) //draw labels for itnerval equal to 1
            {
                if (i % 10 == 0 && i != 0) //draw labels every 10 ticks
                {
                    g.DrawString("" + (i  + minValue).ToString(), x, y, xPoint, yPoint);
                }
            }
            else if(interval ==5) //draw labeles for intervals equal to 5
            {
                if (i % 2 == 0 && i != 0) //draw labels every 2 ticks
                {
                    g.DrawString("" + (i*5 + minValue).ToString(), x, y, xPoint, yPoint);

                }
            }
            else  //else for all other cases
            {
                if (i % interval == 0 && i != 0) //draw labels every 2 ticks
                {
                    g.DrawString("" + (i * interval + minValue).ToString(), x, y, xPoint, yPoint);

                }
            }
        }
        /*
        * canvas_Paint(object sender, PaintEventArgs e) this method will paint the canvas with the initiliazed values of xmin xmax, ymin, ymax
        * params: Sender: Reference to the object that called this function,
        *         EventArgs: The arguments passed from the calling object
        * return: none
        */
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = canvas.CreateGraphics();
            calc();
            Pen whitePen = new Pen(Color.White); // pick teh color of axis
            float dem = (float)yMaxNumeric.Value - (float)yMinNumeric.Value; //calculate the distance between ymax and ymin
            float dem1 = (float)xMaxNumeric.Value - (float)xMinNumeric.Value; //calculate the distance between xMax and xMin
            float x = 600 * ((float)xInterval.Value / dem1); 
            float y = 600 * ((float)yInterval.Value / dem);
            //Both axes are present: if both min values are <= 0, and the max values are > 0,
            //then the axis is drawn from top-to-bottom (y-axis) or from left-to-right (x-axis).
            //Tick marks should be very short, but visible above and below (or left and right) of each axis.
            if (xMinNumeric.Value <= 0 && yMinNumeric.Value <= 0 && xMaxNumeric.Value > 0 && yMaxNumeric.Value > 0)
            {
                g.DrawLine(whitePen, 0, xAxis, 600, xAxis); // draws x axis
                g.DrawLine(whitePen, yAxis, 0, yAxis, 600); //drays y axis
                zeroToYmax(yAxis); //draws ticks zero to ymax
                zeroToYmin(yAxis); //draw ticks zero ymin
                zeroToXmax(xAxis); //draw ticks zero to xmax
                zeroToXmin(xAxis); //draws ticks zero to xmin
            }
            //Only the Y-axis is present: if yMin > 0 or yMax < 0, then do not draw the X-axis itself but instead draw tick marks across the bottom (or top, respectively)
            //edge of the drawing field. The y-axis and associated tick marks will be drawn as normal.
            else if ((yMinNumeric.Value > 0 || yMaxNumeric.Value < 0) && (xMinNumeric.Value < 0 && xMaxNumeric.Value > 0))
            {
                if (yMinNumeric.Value > 0) //ymin is positive
                {
                    zeroToXmin(600); //draw ticks
                    zeroToXmax(600); //draw ticks
                    //iterate from 0 to the number of ticks
                    for (int i = 0; i < Convert.ToInt32(dem / (float)yInterval.Value); i++)
                    {
                        g.DrawLine(whitePen, yAxis - 5, 600 - (i * y), yAxis + 5, 600 - (i * y)); //draw ticks
                        drawLabelsCases("", i, drawFont, drawBrush, yAxis + 10, 600 - (i * y) - 7, (float)yInterval.Value, (float)yMinNumeric.Value); //draw labels
                    }
                }
                if (yMaxNumeric.Value < 0) //ymax is negatve
                {
                    zeroToXmin(0); //draw ticks
                    zeroToXmax(0); //draw ticks
                    // interate throug hthe for loop and draw ticks and labels on yAxis
                    for (int i = 0; i < Convert.ToInt32(dem / (float)yInterval.Value); i++)
                    {
                        g.DrawLine(whitePen, yAxis - 5, 0 + (i * y), yAxis + 5, 0 + (i * y)); //draw ticks
                        drawLabelsCases("", i, drawFont, drawBrush, yAxis + 10, (600 - (i * y) - 7), (float)yInterval.Value, (float)yMinNumeric.Value); //draw labels
                    }
                }
                g.DrawLine(whitePen, (float)Math.Abs(xMinNumeric.Value) * (600 / ((float)xMaxNumeric.Value - (float)xMinNumeric.Value)), 0, (float)Math.Abs(xMinNumeric.Value) * (600 / ((float)xMaxNumeric.Value - (float)xMinNumeric.Value)), canvas.Height);
            }
            //Only the X-axis is present: if xMin > 0 or xMax < 0, then do not draw the Y-axis itself but instead draw tick marks across the left (or right, respectively) edge
            //of the drawing field. The x-axis and associated tick marks will be drawn as normal.
            if ((xMinNumeric.Value > 0 || xMaxNumeric.Value < 0) && (yMinNumeric.Value < 0 && yMaxNumeric.Value > 0))
            {
                if (xMinNumeric.Value > 0) //xMin is positive
                {
                    zeroToYmax(0); //draw ticks
                    zeroToYmin(0); //draw ticks
                    //iterate through x axis and draws ticks and labels
                    for (int i = 0; i < Convert.ToInt32(dem1 / (float)xInterval.Value); i++)
                    {
                        g.DrawLine(whitePen, 0 + (i * x), xAxis - 5, 0 + (i * x), xAxis + 5); //draw ticks
                        drawLabelsCases("", i, drawFont, drawBrush, 0 + (i * x) - 7, xAxis + 10, (float)xInterval.Value, (float)xMinNumeric.Value); //draw labels
                    }
                }
                if (xMaxNumeric.Value < 0) //xMaxnumeric is negative
                {
                    zeroToYmax(600); //draw tick
                    zeroToYmin(600); //draw ticks
                    //iterate in the for loop and draw ticks and labels
                    for (int i = 0; i < Convert.ToInt32(dem1 / (float)xInterval.Value); i++)
                    {
                        g.DrawLine(whitePen, (float)(600 - (i * x)), xAxis - 5, (float)(600 - (i * x)), xAxis + 5);//draw ticks
                        drawLabelsCases("", i, drawFont, drawBrush, 0 + (i * x) - 7, xAxis + 10, (float)xInterval.Value, (float)xMinNumeric.Value); //draw labels
                    }
                }
                //Horizontal Axis
                g.DrawLine(whitePen, 0, (float)yMaxNumeric.Value * (600 / ((float)yMaxNumeric.Value - (float)yMinNumeric.Value)), canvas.Width, (float)yMaxNumeric.Value * (600 / ((float)yMaxNumeric.Value - (float)yMinNumeric.Value)));
            }
            //Neither axis is present: if (xMin > 0 && yMin > 0) || (xMax < 0 && yMin > 0) || (xMax < 0 && yMax < 0) || (xMin > 0 && yMax < 0),
            //then both axes are to be omitted and only their tick marks drawn across the (left/bottom), (right/bottom), (right/top), (left, top), respectively.
            if ((xMinNumeric.Value > 0 && yMinNumeric.Value > 0) || (xMaxNumeric.Value < 0 && yMinNumeric.Value > 0) || (xMaxNumeric.Value < 0 && yMaxNumeric.Value < 0) || (xMinNumeric.Value > 0 && yMaxNumeric.Value < 0))
            {
                if (xMinNumeric.Value > 0 && yMinNumeric.Value > 0) // postiive postive
                {
                    // iterate on the for loop and draw x ticks
                    for (int i = 0; i < Convert.ToInt32(dem1 / (float)xInterval.Value); i++)
                    {
                        g.DrawLine(whitePen, (float)(600 - (i * x)), 600 - 5, (float)(600 - (i * x)), 600 + 5); //draw ticks
                        drawLabelsCases("", i, drawFont, drawBrush, 0 + (i * x) - 7, 575, (float)xInterval.Value, (float)xMinNumeric.Value);//draw labels
                    }
                    //draw y ticks
                    for (int i = 0; i < Convert.ToInt32(dem / (float)yInterval.Value); i++)
                    {
                        g.DrawLine(whitePen, 0 - 5, (float)(0 + (i * x)), 5, (float)(0 + (i * x))); //darw ticks
                        drawLabelsCases("", i, drawFont, drawBrush, 10, 600 - (i * y) - 7, (float)yInterval.Value, (float)yMinNumeric.Value); //draw labels
                    }
                }
                else if (xMaxNumeric.Value < 0 && yMinNumeric.Value > 0) //second quadrat x negative y postive  
                {
                    //iterate and darw ticks and lables
                    for (int i = 0; i < Convert.ToInt32(dem1 / (float)xInterval.Value); i++)
                    {
                        g.DrawLine(whitePen, (float)(0 + (i * x)), 600 - 5, (float)(0 + (i * x)), 600 + 5); //draw ticks
                        drawLabelsCases("", i, drawFont, drawBrush, 0 + (i * x) - 7, 575, (float)xInterval.Value, (float)xMinNumeric.Value); //draw labels
                    }
                    //iterate through the loop and draw y ticks and labels
                    for (int i = 0; i < Convert.ToInt32(dem / (float)yInterval.Value); i++)
                    {
                        g.DrawLine(whitePen, 600 - 5, (float)(0 + (i * x)), 600 + 5, (float)(0 + (i * x))); //draw ticks
                        drawLabelsCases("", i, drawFont, drawBrush, 575, 600 - (i * y) - 7, (float)yInterval.Value, (float)yMinNumeric.Value); //draw labels
                    }
                }
                else if (xMaxNumeric.Value < 0 && yMaxNumeric.Value < 0) // negative negative 
                {
                    //iterate through x axis and draw ticks and labeles
                    for (int i = 0; i < Convert.ToInt32(dem1 / (float)xInterval.Value); i++)
                    {
                        g.DrawLine(whitePen, (float)(0 + (i * x)), 0 - 5, (float)(0 + (i * x)), 0 + 5); //draw ticks
                        drawLabelsCases("", i, drawFont, drawBrush, 0 + (i * x) - 7, 15, (float)xInterval.Value, (float)xMinNumeric.Value); //draw labels
                    }
                    //iterate through y axis and draw ticks and labeles
                    for (int i = 0; i < Convert.ToInt32(dem / (float)yInterval.Value); i++)
                    {
                        g.DrawLine(whitePen, 600 - 5, (float)(0 + (i * x)), (float)600 + 5, (float)(0 + (i * x))); //draw ticks
                        drawLabelsCases("", i, drawFont, drawBrush, 575, 600 - (i * y) - 7, (float)yInterval.Value, (float)yMinNumeric.Value); //draw labels
                    }
                }
                else if (xMinNumeric.Value > 0 && yMaxNumeric.Value < 0) //xmin is positive and ymax is negative
                {
                    //iterate through x axis and draw ticks and lables
                    for (int i = 0; i < Convert.ToInt32(dem1 / (float)xInterval.Value); i++)
                    {
                        g.DrawLine(whitePen, (float)(0 + (i * x)), 0 - 5, (float)(0 + (i * x)), (float)0 + 5); //draw ticks
                        drawLabelsCases("", i, drawFont, drawBrush, 0 + (i * x) - 7, 15, (float)xInterval.Value, (float)xMinNumeric.Value); //draw labeles
                    }
                    //iterate through y axis and draw ticks and labeles
                    for (int i = 0; i < Convert.ToInt32(dem / (float)yInterval.Value); i++)
                    {
                        g.DrawLine(whitePen, 0 - 5, (float)(0 + (i * x)), (float)0 + 5, (float)(0 + (i * x))); //draw ticks
                        drawLabelsCases("-", i, drawFont, drawBrush, 15, 600 - (i * y) - 7, (float)yInterval.Value, (float)yMinNumeric.Value); //draw lables
                    }
                }
                else if (xMinNumeric.Value > xMaxNumeric.Value) //print the message below if xmin is > than xMax
                {
                    MessageBox.Show("Equation x is out of scope! ( Xmin cannot be bigger than Xmax");
                }
                else if (yMinNumeric.Value > yMaxNumeric.Value) //print the message below if ymin > ymax
                {
                    MessageBox.Show("Equation y is out of scope! ( Ymin cannot be bigger than Ymax");
                }
            }
        }
        /*
        *  setLimits(object sender, EventArgs e) the purpose of this function is to adjust the graph based on the bounds changed 
        *  by the user. 
        * params: Sender: Reference to the object that called this function,
        *         EventArgs: The arguments passed from the calling object
        * return: none
        */
        private void setLimits(object sender, EventArgs e)
        {
            canvas.Refresh();
        }
        /*
        *  ClearGraph(object sender, EventArgs e) the purpose of this function is to clear the graphs which are drawns
        * params: Sender: Reference to the object that called this function,
        *         EventArgs: The arguments passed from the calling object
        * return: none
        */
        private void ClearGraph(object sender, EventArgs e)
        {
            Graphics g = canvas.CreateGraphics();
            canvas.Refresh();
        }
        /*
        *  LinearHint(object sender, EventArgs e) this method will provide an hint for the user on how to use draw linear equation
        * params: Sender: Reference to the object that called this function,
        *         EventArgs: The arguments passed from the calling object
        * return: none
        */
        private void LinearHint(object sender, EventArgs e)
        {
            MessageBox.Show("Linear Equations (Y = mx+b ) where 'm' is the slope and 'b' is the y-intercept");
        }
        /*
        * QuadraticHint(object sender, EventArgs e) this function will provide hints on how to draw quadratic functions
        * params: Sender: Reference to the object that called this function,
        *         EventArgs: The arguments passed from the calling object
        * return: none
        */
        private void QuadraticHint(object sender, EventArgs e)
        {
            MessageBox.Show("Quadratic Equations (y = ax^2 +bx +c ) where a,b,c and real numbers");
        }
        /*
        * CubicHint(object sender, EventArgs e) this function provides hints on how to draw cubic equations
        * params: Sender: Reference to the object that called this function,
        *         EventArgs: The arguments passed from the calling object
        * return: none
        */
        private void CubicHint(object sender, EventArgs e)
        {
            MessageBox.Show("Cubic Equations (y=ax^3 +bx^2 +cx + d ) where a,b,c,d are real numbers");
        }
        /*
        * CircleHint(object sender, EventArgs e) the purpose of this function is to give hints on what to put for the circle equation
        * params: Sender: Reference to the object that called this function,
        *         EventArgs: The arguments passed from the calling object
        * return: none
        */
        private void CircleHint(object sender, EventArgs e)
        {
            MessageBox.Show("Circle Equation ((x - h)^2 + (y - k)^2 = r^2 ) where (h,k) is the center of the circle and r is the radius ");
        }
    }
}