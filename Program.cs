/* This file was developed on 20-03-2024 , duration of development was ~ 3 hours

During the sitting hours of 10:00 to 14:32 , which included breakfast and lunch breaks and occasional use of phone to publish important/urgent messages.

key highlights: 
for temperature.dat file, day with minimum difference of Max and min temperature value is computed.
for socker.dat file, city with minimum absolute difference between for and against is computed.

Drawbacks :
Structure could be improved but the time duration of 3 hours posed an issue.
Object oriented code structure could have been implemented if more time was available.

*/



using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace HelloWorld;

class Program
{
    static void Main()
    {
        string filePath = "temperature.dat";
        string filePath_socker = "socker.dat"; 
        string[] lines = File.ReadAllLines(filePath);
        string[] lines_socker = File.ReadAllLines(filePath_socker);
        //Console.WriteLine(lines.Length);
        List<int> differenceList = new List<int>(); // list for difference between max and min
        List<string> daynumList = new List<string>(); // list for storing day number

        List<string> cityList = new List<string>(); // list for storing day number
        List<int> differenceList_socker = new List<int>(); // list for difference between max and min

        // ------------ loop for temperature.dat ------------------
        for (int i = 0; i < lines.Length; i++)
            {
                // Split the line by spaces
                string[] parts = lines[i].Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                //Console.WriteLine(parts.Length);
                // convert parts[1] , parts[2] to int --> string to int
                //int.TryParse(parts[1], out int Max);

                // Ensure the line has enough parts
                if (parts.Length >= 4)
                {
                    // Parse Max and Min values
                    if (int.TryParse(parts[1], out int max) && int.TryParse(parts[2], out int min))
                    {
                        // Calculate the difference between Max and Min
                        int difference = max - min;
                        
                        // Output the difference
                        Console.WriteLine($"Difference for line {i}: {difference}");
                        differenceList.Add(difference);
                        daynumList.Add(parts[0]);
                        
                        
                    }
                    else
                    {
                        Console.WriteLine($"Invalid format in line {i}");
                    }
                }
               
            }
            // 18 has minimum difference
            //Console.WriteLine(lines[7][3]);

            // getting smallest element
            long min_num = differenceList.AsQueryable().Min();
            
            Console.WriteLine(min_num);
            foreach (var(day,diff_value) in daynumList.Zip(differenceList)){
                if (diff_value == min_num){
                    Console.WriteLine($"Day with minimum max-min temperature difference is {day}: {diff_value}"); 
                }
            }
        //---------------loop for socker.dat -----------------------------------------
        
        // filtering the file lines to remove  ---------
        var filteredLines = lines_socker.Where(line => !line.Contains("----")).ToList();
        //Console.WriteLine(filteredLines);
        /*
        foreach (string var in filteredLines){
                Console.WriteLine(var);
                }
            */
        
        foreach (var line in filteredLines){
            string[] parts_socker = line.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            /*
            foreach (string var in parts_socker){
                Console.WriteLine(var);
                }
                */
            if (int.TryParse(parts_socker[6], out int for_column) && int.TryParse(parts_socker[8], out int against_column))
                    {
                        // Calculate the difference between for an against
                        int difference_socker = Math.Abs(for_column - against_column);
                        
                        // Output the difference
                        Console.WriteLine($"Difference for line {line}: {difference_socker}");
                        differenceList_socker.Add(difference_socker);
                        cityList.Add(parts_socker[1]);
                        
                    }
            else
                    {
                        Console.WriteLine($"Invalid format in line {line}");
                    }
            
            }
            long min_for_against = differenceList_socker.AsQueryable().Min();  
            Console.WriteLine(min_for_against);

            Console.WriteLine(min_for_against);
            foreach (var(city,diff_for_against) in cityList.Zip(differenceList_socker)){
                if (diff_for_against == min_for_against){
                    Console.WriteLine($"city with minimum for-against difference is {city}: {diff_for_against}"); 
                }
            }

        }   

    }
        

