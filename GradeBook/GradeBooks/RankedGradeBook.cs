using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GradeBook.GradeBooks
{
    class RankedGradeBook:BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) :base(name,isWeighted)
        {
            Type = GradeBookType.Ranked;
        }
        public  override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count > 5)
                throw new InvalidOperationException("Ranked - grading requires a minimum of 5 students to work");
            var twentyPercent = (int)Math.Ceiling((Students.Count * 0.2));

            var grades = Students.OrderByDescending(e => e.AverageGrade).Select(e => e.AverageGrade).ToList();

            int CurrentGrade = 'A';
            for (int i = 1; twentyPercent * i < Students.Count; i++)
            {
                if (twentyPercent * i > grades.IndexOf(averageGrade))

                    return (char)CurrentGrade;

                CurrentGrade += 1;
            }
            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count > 5)
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            else
                base.CalculateStatistics();
        }
    }
}
