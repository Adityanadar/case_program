/* this is oops version of program.cs
this file is created on 22-03-2024
development time : ~1 hrs
improves the previous version of program.cs by implementing oop concepts and better code readability.
*/

using System; 
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.IO.Compression;
using System.Text.RegularExpressions;

class temperature_data {
    public void compute_temp_difference(string[] lines, List<int> differenceList, List<string> daynumList)   // method
    {
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
    }
}

class socker_data{
    
    // filtering the file lines to remove  ---------
    
    public void compute_forag_difference(string[] lines_socker, List<int> differenceList_socker, List<string> cityList){
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
class case_program { 

    // Main Method 
    public static void Main() 
    { 
        
        string filePath = "temperature.dat";
        string[] lines = File.ReadAllLines(filePath);
        List<int> differenceList = new List<int>(); // list for difference between max and min
        List<string> daynumList = new List<string>(); // list for storing day number

        string filePath_socker = "socker.dat";
        string[] lines_socker = File.ReadAllLines(filePath_socker);
        List<string> cityList = new List<string>(); // list for storing city/ team names
        List<int> differenceList_socker = new List<int>(); // list for difference between for and against

        temperature_data obj1 = new temperature_data();
        socker_data obj2 = new socker_data();

        obj1.compute_temp_difference(lines, differenceList, daynumList);
        obj2.compute_forag_difference(lines_socker, differenceList_socker, cityList);

    } 
} 
