using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;

namespace Paint
{
    public partial class MainForm : Form
    {       

        // TODO: Handle the borders to check that no shape is being drawn outside the borders
        // TODO2: Better comments!
        Pen pen;
        Point startPos;      // mouse-down position
        Point currentPos;    // current mouse position
        // Variables for program logic
        bool drawing = false;
        bool freeDraw = true;
        bool rectangleDraw = false;
        bool ellipseDraw = false;
        bool circleDraw = false;
        bool lineDraw = false;
        // Collection of strokes and shapes for drawing 
        List<List<Pixel>> strokes = new List<List<Pixel>>();
        List<Pixel> currStroke;
        List<Rectangle> rectangles = new List<Rectangle>();
        List<Line> lines = new List<Line>();
        List<Ellipse> ellipses = new List<Ellipse>();
        List<Circle> circles = new List<Circle>();
        public MainForm()
        {
            //Initialize paint components and event handlers, double buffer the panel to avoid flickering
            InitializeComponent();
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
            | BindingFlags.Instance | BindingFlags.NonPublic, null,
            panel, new object[] { true });
            DoubleBuffered = true;
            clearMenu.Click += new EventHandler(menuStrip_ItemClicked);
            sizeMenu.Click += new EventHandler(menuStrip_PenSizeclicked);
            colorMenu.Click += new EventHandler(menuStrip_ColorClicked);
            lineMenu.Click += new EventHandler(menuStrip_LineClicked);
            circleMenu.Click += new EventHandler(menuStrip_CircleClicked);
            ellipseMenu.Click += new EventHandler(menuStrip_EllipseClicked);
            rectangleMenu.Click += new EventHandler(menuStrip_RectangleClicked);
            freeMenu.Click += new EventHandler(menuStrip_FreeClicked);
            logMenu.Click += new EventHandler(menuStrip_LogClicked);
            pen = new Pen(Color.Black, 5);
            //Database handler
            using (var connection = new SqliteConnection("Data Source=log.db"))
            {
                connection.Open();
                string sql = "CREATE TABLE IF NOT EXISTS shapes (shape TEXT, timestamp TEXT)";
                SqliteCommand command = new SqliteCommand(sql, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }      

        private void panel_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
            /* A lot is going on here so let's break it down
               We have our own class named Rectangle, which has a property called rectangle (System.Drawing.Rectangle)
               which is used to construct a new rectangle object alonside its pen
            */
            if (rectangleDraw)
            {
                Rectangle rc = new Rectangle { rectangle = new System.Drawing.Rectangle(
                Math.Min(startPos.X, currentPos.X),
                Math.Min(startPos.Y, currentPos.Y),
                Math.Abs(startPos.X - currentPos.X),
                Math.Abs(startPos.Y - currentPos.Y)), pen = pen};
                if (rc.rectangle.Width > 0 && rc.rectangle.Height > 0)
                {
                    rectangles.Add(rc);
                }
                Refresh();
            }
            else if (ellipseDraw)
            {
                Ellipse ellipse = new Ellipse { start = startPos, end = currentPos, pen = pen };              
                ellipses.Add(ellipse);
            }
           
        }
           
        private void panel_MouseDown(object sender, MouseEventArgs e)
        {           
            drawing = true;
            if (freeDraw)
            {
                // mouse is down, starting new stroke
                currStroke = new List<Pixel>();
                Pixel newPixel = new Pixel(e.Location, pen);
                currStroke.Add(newPixel);
                // add the initial point to the new stroke
                strokes.Add(currStroke);
                InsertToDB("free");
            }
            else if (lineDraw)
            {
                //start a new line
                Line line = new Line { start = e.Location, pen = pen };
                lines.Add(line);
                InsertToDB("line");
            }
            //rectangle
            else if (rectangleDraw)
            {
                currentPos = startPos = e.Location;
                InsertToDB("rectangle");
            }
            //ellipse
            else if (ellipseDraw)
            {
                startPos = e.Location;
                InsertToDB("ellipse");
            }
            //Construct and draw a circle around the mouse
            else if (circleDraw)
            {
                try
                {                  
                    int radius = Int32.Parse(Interaction.InputBox("Enter desired radius", "Circle radius"));
                    Circle circle = new Circle { center = e.Location, pen = pen, radius = radius };
                    circles.Add(circle);
                    Refresh();
                    InsertToDB("circle");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Please enter a valid integer number!");
                }
            }

        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            // now handle and redraw our strokes on the paint event
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (List<Pixel> stroke in strokes.Where(x => x.Count > 1))
            {
                e.Graphics.DrawLines(stroke[0].pen, stroke.Select(c => c.point).ToArray());
            }
            // redraw the lines
            foreach (var line in lines)
            {
                e.Graphics.DrawLine(line.pen, line.start, line.end);
            }
            //redraw rectangles
            foreach (Rectangle rect in rectangles)
            {
               e.Graphics.DrawRectangle(rect.pen, rect.rectangle);
            }
            
            if (drawing)
            {
                if (rectangleDraw)
                {
                    Rectangle rc = new Rectangle
                    {
                        rectangle = new System.Drawing.Rectangle(
                        Math.Min(startPos.X, currentPos.X),
                        Math.Min(startPos.Y, currentPos.Y),
                        Math.Abs(startPos.X - currentPos.X),
                        Math.Abs(startPos.Y - currentPos.Y)),
                        pen = pen
                    };
                    //Draw rectangle while drawing
                    e.Graphics.DrawRectangle(rc.pen, rc.rectangle);
                }
                else if (ellipseDraw)
                {
                    //Draw ellipse while drawing
                    Ellipse ellipse = new Ellipse { start = startPos, end = currentPos, pen = pen };
                    e.Graphics.DrawEllipse(ellipse.pen, ellipse.start.X, ellipse.start.Y, ellipse.end.X - ellipse.start.X, ellipse.end.Y - ellipse.start.Y);
                }
            };
            //redraw ellipses
            foreach (Ellipse ellipse in ellipses)
            {
                e.Graphics.DrawEllipse(ellipse.pen, ellipse.start.X, ellipse.start.Y, ellipse.end.X - ellipse.start.X, ellipse.end.Y - ellipse.start.Y);
            }
            //draw circle
            foreach (Circle circle in circles)
            {
                e.Graphics.DrawEllipse(circle.pen, circle.center.X - circle.radius, circle.center.Y - circle.radius, 2 * circle.radius, 2 * circle.radius);
            }

        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                if (freeDraw)
                {
                    // Track the stroke
                    Pixel newPixel = new Pixel(e.Location, pen);
                    currStroke.Add(newPixel);
                    Refresh();
                }
                else if (lineDraw)
                {
                    // Move the straight alone along with the mouse
                    if (lines.Count > 0 && e.Button == MouseButtons.Left)
                    {
                        lines[lines.Count - 1].end = e.Location;
                        Refresh();
                    }
                }
                else if (rectangleDraw)
                {
                    // Track current rectangle location
                    currentPos = e.Location;
                    Refresh();
                }
                else if (ellipseDraw)
                {
                    currentPos = e.Location;
                    Refresh();
                }
            }
        }     
       
        private void menuStrip_ItemClicked(object sender, EventArgs e)
        {
            // Clears the panel
            strokes.Clear();
            lines.Clear();
            rectangles.Clear();
            circles.Clear();
            ellipses.Clear();
            panel.Invalidate();           
        }
        private void menuStrip_PenSizeclicked(object sender, EventArgs e)
        {
            //Ask for pen size using the visual basic input prompt
            try
            {
                int newSize = Int32.Parse(Interaction.InputBox("Enter desired pen size", "Pen size"));
                pen = new Pen(pen.Color, newSize);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid integer number!");
            }
        }
        private void menuStrip_ColorClicked(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            colorDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            colorDialog.ShowHelp = true;
            colorDialog.Color = pen.Color;
            //Change pen's color
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                pen = new Pen(colorDialog.Color, pen.Width);
            }
        }
        //Shapes draw event handlers
        private void menuStrip_LineClicked(object sender, EventArgs e)
        {
            drawing = false;
            freeDraw = false;
            circleDraw = false;
            ellipseDraw = false;
            rectangleDraw = false;
            lineDraw = true;
        }
        private void menuStrip_CircleClicked(object sender, EventArgs e)
        {
            drawing = false;
            freeDraw = false;
            lineDraw = false;
            ellipseDraw = false;
            rectangleDraw = false;
            circleDraw = true;
        }

        private void menuStrip_EllipseClicked(object sender, EventArgs e)
        {
            drawing = false;
            freeDraw = false;
            lineDraw = false;
            circleDraw = false;
            rectangleDraw = false;
            ellipseDraw = true;
        }
        private void menuStrip_RectangleClicked(object sender, EventArgs e)
        {
            drawing = false;
            freeDraw = false;
            lineDraw = false;
            circleDraw = false;
            ellipseDraw = false;
            rectangleDraw = true;
        }
        private void menuStrip_FreeClicked(object sender, EventArgs e)
        {
            drawing = false;
            lineDraw = false;
            circleDraw = false;
            ellipseDraw = false;
            rectangleDraw = false;
            freeDraw = true;
        }
        //Method for writing to database. Avoid sql injections by using parameters!
        public void InsertToDB(string shape)
        {
            using (var connection = new SqliteConnection("Data Source=log.db"))
            {
                connection.Open();
                string timestamp = DateTime.Now.ToString();
                SqliteCommand insertSQL = new SqliteCommand("INSERT INTO shapes(shape, timestamp) VALUES (@shape,@timestamp)", connection);
                insertSQL.Parameters.AddWithValue("@shape",shape);
                insertSQL.Parameters.AddWithValue("@timestamp",timestamp);
                try
                {
                    insertSQL.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }               
            }          
        }
        //Show log using sqlite reader

        private void menuStrip_LogClicked(object sender, EventArgs e)
        {
            using (var connection = new SqliteConnection("Data Source=log.db"))
            {
                connection.Open();
                SqliteCommand selectSQL = new SqliteCommand("SELECT shape, timestamp FROM shapes", connection);
                SqliteDataReader sqlReader = selectSQL.ExecuteReader();
                string results = "";
                try
                {
                    while (sqlReader.Read())
                    {
                        results += sqlReader.GetString(0) + " " + sqlReader.GetString(1) + '\n';
                    }
                }
                finally
                {
                    sqlReader.Close();
                    connection.Close();
                    MessageBox.Show(results, "Shape log");
                }
            }
        }
    }
}

