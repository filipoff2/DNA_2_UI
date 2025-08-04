
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CodonPathDrawer
{
    public class CodonForm : Form
    {
        private Dictionary<string, List<Point>> codonPaths = new Dictionary<string, List<Point>>();
        private int squareSize = 20;

        public CodonForm()
        {
            this.Text = "Codon Path Drawer";
            this.Size = new Size(1300, 900);
            GenerateAllPaths();
        }

        private void GenerateAllPaths()
        {
            string[] bases = new string[] { "A", "T", "C", "G" };
            foreach (var b1 in bases)
            foreach (var b2 in bases)
            foreach (var b3 in bases)
            {
                string codon = b1 + b2 + b3;
                codonPaths[codon] = GeneratePath(codon);
            }
        }

        private List<Point> GeneratePath(string codon)
        {
            int x = 50, y = 50;
            int direction = 0;
            List<Point> path = new List<Point>();
            path.Add(new Point(x, y));

            foreach (char basePair in codon)
            {
                switch (basePair)
                {
                    case 'A':
                        x += (int)(Math.Cos(direction * Math.PI / 180) * squareSize);
                        y -= (int)(Math.Sin(direction * Math.PI / 180) * squareSize);
                        break;
                    case 'T':
                        direction = (direction + 90) % 360;
                        x += (int)(Math.Cos(direction * Math.PI / 180) * squareSize);
                        y -= (int)(Math.Sin(direction * Math.PI / 180) * squareSize);
                        break;
                    case 'C':
                        direction = (direction + 270) % 360;
                        x += (int)(Math.Cos(direction * Math.PI / 180) * squareSize);
                        y -= (int)(Math.Sin(direction * Math.PI / 180) * squareSize);
                        break;
                    case 'G':
                        x -= (int)(Math.Cos(direction * Math.PI / 180) * squareSize);
                        y += (int)(Math.Sin(direction * Math.PI / 180) * squareSize);
                        break;
                }
                path.Add(new Point(x, y));
            }
            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int cols = 8;
            int spacingX = 150;
            int spacingY = 120;
            int index = 0;

            foreach (var kvp in codonPaths)
            {
                int col = index % cols;
                int row = index / cols;
                int offsetX = col * spacingX + 50;
                int offsetY = row * spacingY + 50;

                List<Point> path = kvp.Value;
                Point[] translated = new Point[path.Count];
                for (int i = 0; i < path.Count; i++)
                {
                    translated[i] = new Point(path[i].X + offsetX, path[i].Y + offsetY);
                }

                e.Graphics.DrawLines(Pens.Black, translated);
                e.Graphics.DrawString(kvp.Key, this.Font, Brushes.Blue, translated[0].X, translated[0].Y - 15);
                index++;
            }
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CodonForm());
        }
    }
}
