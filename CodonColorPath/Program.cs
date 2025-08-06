using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CodonColorPath
{
    public class Program : Form
    {
        private List<(PointF, PointF, Color)> segments;

        public Program()
        {
            string dna = @"ATGGAGGAGCCGCAGTCAGATCCTAGCGTCGAGCCCCCTCTGAGTCAGGAAACATTTTCAGACCTATGGAAACTACTTCCTGAAAACAACGTTCTGCTTTGAGGTGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCGCAAGAAAGGGGAGCGTGTTTGTGCCTGTCCTGGGAGAGACCGGCGCACAGAGGAAGAGAATCTCCG";
            var codons = ParseCodons(dna);
            segments = GenerateSegments(codons);
            this.DoubleBuffered = true;
            this.WindowState = FormWindowState.Maximized;
            this.Paint += DrawSegments;
        }

        private List<string> ParseCodons(string dna)
        {
            var codons = new List<string>();
            for (int i = 0; i < dna.Length - 2; i += 3)
            {
                codons.Add(dna.Substring(i, 3));
            }
            return codons;
        }

        private List<(PointF, PointF, Color)> GenerateSegments(List<string> codons)
        {
            var segments = new List<(PointF, PointF, Color)>();
            PointF current = new PointF(0, 0);
            float angle = 26f;
            float currentAngle = 0f;

            foreach (var codon in codons)
            {
                currentAngle += angle;
                float radians = currentAngle * (float)Math.PI / 180f;
                PointF next = new PointF(
                    current.X + 10 * (float)Math.Cos(radians),
                    current.Y + 10 * (float)Math.Sin(radians)
                );

                Color color = Color.Black;
                if (CodonColors.TryGetValue(codon, out var c))
                    color = c;

                segments.Add((current, next, color));
                current = next;
                angle += 2f;
            }

            return segments;
        }

        private void DrawSegments(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.TranslateTransform(this.ClientSize.Width / 2, this.ClientSize.Height / 2);
            foreach (var (start, end, color) in segments)
            {
                using var pen = new Pen(color, 2);
                g.DrawLine(pen, start, end);
            }
        }

        [STAThread]
        public static void Main()
        {
            Application.Run(new Program());
        }

        private static readonly Dictionary<string, Color> CodonColors = new Dictionary<string, Color>
        {
            ["ATG"] = Color.Red,
            ["GAG"] = Color.Blue,
            ["GGA"] = Color.Green,
            ["GCC"] = Color.Yellow,
            ["CAG"] = Color.Orange,
            ["TCA"] = Color.Purple,
            ["GAT"] = Color.Brown,
            ["CCT"] = Color.Cyan,
            ["AGC"] = Color.Magenta,
            ["GTC"] = Color.Lime,
            ["CCC"] = Color.Gray,
            ["CTC"] = Color.Pink,
            ["TGA"] = Color.Teal,
            ["GAA"] = Color.Navy,
            ["ACA"] = Color.Maroon,
            ["TTT"] = Color.Olive,
            ["ACC"] = Color.Silver,
            ["TAT"] = Color.Gold,
            ["ACT"] = Color.Aqua,
            ["TTC"] = Color.Beige,
            ["AAA"] = Color.Khaki,
            ["CAA"] = Color.Coral,
            ["CGT"] = Color.Salmon,
            ["TCT"] = Color.Tomato,
            ["TTG"] = Color.Plum,
            ["AGG"] = Color.Sienna,
            ["TGC"] = Color.Peru,
            ["GTT"] = Color.Tan,
            ["TGT"] = Color.Azure,
            ["CTG"] = Color.Indigo,
            ["GGC"] = Color.Chocolate,
            ["GCA"] = Color.Crimson,
            ["AAG"] = Color.Violet,
            ["AGA"] = Color.PeachPuff,
            ["CCG"] = Color.DarkGreen,
            ["GGG"] = Color.DarkBlue,
            ["GTG"] = Color.DarkRed
        };
    }
}
