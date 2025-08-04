
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DnaCodonPathDrawer
{
    public class MainForm : Form
    {
        private List<List<Point>> paths = new List<List<Point>>();
        private int squareSize = 10;
        private int codonsPerRow = 20;

        public MainForm()
        {
            this.Text = "DNA Codon Path Drawer";
            this.Size = new Size(1200, 800);
            GeneratePaths();
        }

        private void GeneratePaths()
        {
            string dna = "ATGGAGGAGCCGCAGTCAGATCCTAGCGTCGAGCCCCCTCTGAGTCAGGAAACATTTTCAGACCTATGGAAACTACTTCCTGAAAACAACGTTCTGCTTTGAGGTGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCG";
            List<string> codons = new List<string>();
            for (int i = 0; i < dna.Length - 2; i += 3)
            {
                codons.Add(dna.Substring(i, 3));
            }

            foreach (string codon in codons)
            {
                paths.Add(GeneratePath(codon));
            }
        }

        private List<Point> GeneratePath(string codon)
        {
            List<Point> path = new List<Point>();
            int x = 0, y = 0;
            int direction = 0; // 0 = right, 90 = up, 180 = left, 270 = down
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
            int margin = 20;
            int panelSize = 50;
            for (int i = 0; i < paths.Count; i++)
            {
                int row = i / codonsPerRow;
                int col = i % codonsPerRow;
                int offsetX = margin + col * panelSize;
                int offsetY = margin + row * panelSize;

                List<Point> path = paths[i];
                Point[] translated = new Point[path.Count];
                for (int j = 0; j < path.Count; j++)
                {
                    translated[j] = new Point(path[j].X + offsetX, path[j].Y + offsetY);
                }
                e.Graphics.DrawLines(Pens.Black, translated);
            }
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}
