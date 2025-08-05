using System;
using System.Collections.Generic;
using System.Drawing;

public class CodonPath
{
    public List<PointF> GeneratePath(List<string> codons, float stepLength = 10f)
    {
        var points = new List<PointF> { new PointF(0, 0) };
        float angle = 26f;
        float currentAngle = 0f;
        PointF currentPoint = new PointF(0, 0);

        foreach (var codon in codons)
        {
            currentAngle += angle;
            float radians = currentAngle * (float)Math.PI / 180f;
            PointF nextPoint = new PointF(
                currentPoint.X + stepLength * (float)Math.Cos(radians),
                currentPoint.Y + stepLength * (float)Math.Sin(radians)
            );
            points.Add(nextPoint);
            currentPoint = nextPoint;
            angle += 2f;
        }

        return points;
    }
}
