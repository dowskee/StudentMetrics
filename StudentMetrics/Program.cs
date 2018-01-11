using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMetrics
{
    class Program
      static List<StudentData> data = new List<StudentData>();
    static void Main(string[] args)
    {
        //Create an program to record and track students in a class. Be able to run analysis and reports 
        //on at least 5 metrics

        //GPA, Attendance(days absent out of 180), Test Scores(average), On pace for Graduation(bool), Grade Level

        //txt file should contain Grade, DaysPresent, TestScores, Yes/No for Graduation, Grade Level 
        bool AnotherSearch = true;
        while (AnotherSearch == true)
        {

            string studentList = @"Students.txt";
            List<string> linesofStudents = File.ReadAllLines(studentList).ToList();

            bool isFirstLine = true;

            foreach (var line in linesofStudents)
            {
                var strArray = line.Split(',');

                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }

                StudentData datastream = new StudentData();

                datastream.Name = strArray[0];
                datastream.Grade = int.Parse(strArray[1]);
                datastream.DaysHere = int.Parse(strArray[2]);
                datastream.Scores = strArray[3];
                datastream.OnTrack = strArray[4];
                datastream.GradeLevel = int.Parse(strArray[5]);

                data.Add(datastream);
            }
            StudentData newStudent = new StudentData();

            Console.WriteLine("Student List:\nAnto\nJim\nGregg\nAlexandra\nBob\nSammy\nJohn\nAlice\nRick");
            Console.WriteLine("Please enter a student's name to search for their metrics:");
            string nameentry = Console.ReadLine();

            if (nameentry == "Anto" || nameentry == "Jim" || nameentry == "Gregg" || nameentry == "Alexandra" || nameentry == "Bob" || nameentry == "Sammy" || nameentry == "John" || nameentry == "Alice" || nameentry == "Rick")
            {
                newStudent.Name = nameentry;
            }
            else
            {
                Console.WriteLine("That is not a valid student. Please select a student from the list. This is a school, capitalization is important");
                AnotherSearch = true;
            }
            //add validation for students

            foreach (var item in data.Where(x => x.Name == newStudent.Name))
            {
                double gpa = GPA(item.Grade);
                double avgAbsent = DaysAbsent(item.DaysHere);
                double testAVG = TestScoresAvg(item.Scores);
                bool trackgrad = OnTrackGrad(item.OnTrack);
                int levelGrade = GradeLevel(item.GradeLevel);
                //need to access the items where the name matches the searched name (item.GPA, etc)
                Console.WriteLine($"Student Name: {newStudent.Name}\nGPA: {gpa}\nDays Absent: {avgAbsent}\nTest Scores Average: {testAVG}%\nOn Track to Graduate: {trackgrad}\nGrade Level: {levelGrade}");
                Console.WriteLine("Would you like to serach another student's metrics? Y/N");
                string response = Console.ReadLine().ToLower();

                if (response == "y")
                {
                    AnotherSearch = true;
                }
                else
                {
                    AnotherSearch = false;
                    Console.WriteLine("Goodbye!");
                    Console.ReadLine();
                }
            }

        }

    }
    public static double GPA(int grade)
    {

        foreach (var item in data.Where(x => x.Grade == grade))
        {
            item.Grade = grade;
        }
        if (grade >= 100)
        {
            return 4.00;
        }
        else if (grade >= 95 && grade < 100)
        {
            return 3.90;
        }

        else if (grade >= 90 && grade < 95)
        {
            return 3.70;
        }
        else if (grade >= 85 && grade < 90)
        {
            return 3.33;
        }
        else if (grade >= 80 && grade < 85)
        {
            return 3.00;
        }
        else if (grade >= 75 && grade < 80)
        {
            return 2.30;
        }
        else if (grade >= 70 && grade < 75)
        {
            return 1.70;
        }
        else if (grade >= 65 && grade < 70)
        {
            return 1.30;
        }
        else if (grade >= 60 && grade < 65)
        {
            return 1.00;
        }
        else if (grade >= 55 && grade < 60)
        {
            return 0.70;
        }
        else
        {
            return 0.00;
        }
        //make a switch statement for GPA


    }

    public static double DaysAbsent(int daysPresent)
    {
        int absentDays = 0;

        foreach (var item in data.Where(x => x.DaysHere == daysPresent))
        {
            item.DaysHere = daysPresent;
        }

        absentDays = 180 - daysPresent;

        return absentDays;
    }

    public static double TestScoresAvg(string scores)
    {
        double average = 0;
        double sum = 0;
        double total = 0;

        string[] scoresSplit = scores.Split(' ');
        int firstScore = Convert.ToInt32(scoresSplit[0]);
        int secondScore = Convert.ToInt32(scoresSplit[1]);
        int thirdScore = Convert.ToInt32(scoresSplit[2]);

        total = scoresSplit.Length;

        foreach (var item in data.Where(x => x.Scores == scores))
        {
            sum = firstScore + secondScore + thirdScore;
        }

        average = sum / total;
        return Math.Round(average, 2);
    }

    public static bool OnTrackGrad(string Track)
    {
        foreach (var item in data.Where(x => x.OnTrack == Track))
        {
            if (item.OnTrack.Contains("Yes") || item.OnTrack.Contains("yes"))
            {
                return true;
            }

            else
            {
                return false;
            }
        }
        return true; //defaults to true if it is empty
    }

    public static int GradeLevel(int level)
    {
        int grdLVL = 0;
        foreach (var item in data.Where(x => x.GradeLevel == level))
        {
            grdLVL = item.GradeLevel;
        }
        return grdLVL;
    }
}
